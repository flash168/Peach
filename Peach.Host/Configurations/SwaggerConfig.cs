using Microsoft.OpenApi.Models;

namespace Peach.Host.Configurations
{
    public static class SwaggerConfig
    {
        /// <summary>
        /// 添加Swagger配置
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Video Api 文档",
                    Description = "Video Api 文档 Swagger",
                    Contact = new OpenApiContact { Name = "flash", Email = "flash@126.com" }
                });

                // 加载xml文档 显示Swagger的注释
                string[] files = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");//获取api文档 
                string[] array = files;
                foreach (string filePath in array)
                {
                    s.IncludeXmlComments(filePath, includeControllerXmlComments: true);
                }

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                                              {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                                }
                        },
                        Array.Empty<string>()
                        }
                });

                // 添加Authorization的输入框
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Input the JWT like: Bearer {your token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
