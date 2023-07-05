using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Peach.Application.Configuration;
using Peach.Host.Configurations;
using Peach.Host.Filters;
using Serilog;
using Serilog.Events;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .ReadFrom.Configuration(new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",// 根据环境变量加载指定配置 
                     optional: true).Build())
    .Enrich.FromLogContext()
    .WriteTo.Async(c => c.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "/log/", "log"),
                               rollingInterval: RollingInterval.Day)) // 写入日志到文件 
    .WriteTo.Async(c => c.Console())
    .CreateLogger();

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    Args = args,
    ContentRootPath = AppContext.BaseDirectory
});

builder.Services.AddSingleton<AppSettingsHelper>(new AppSettingsHelper(builder.Configuration));

// 使用Serilog
builder.Host.UseSerilog();

//Cors Conig
builder.Services.AddCors(c =>
{
    //允许任意跨域请求
    c.AddPolicy("cors", policy =>
    {
        policy.SetIsOriginAllowed((host) => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Swagger Config
builder.Services.AddSwaggerConfiguration();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddApplication();

// 添加过滤器
builder.Services.AddMvcCore(options =>
{
    //options.Filters.Add<ResponseFilter>();
    options.Filters.Add<ExceptionFilter>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerSetup();
}

var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".exe"] = "application/octet-stream";


app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.GetTypedHeaders().ContentType = new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("text/html")
        {
            Encoding = Encoding.UTF8,
        };
    },
    ServeUnknownFileTypes = true,
   // DefaultContentType = "text/plain; charset=utf-8",
    ContentTypeProvider = provider,
    FileProvider = new PhysicalFileProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory))
});

app.UseAuthentication(); // 认证 
app.UseAuthorization(); // 授权


app.MapControllers();

app.Run();
