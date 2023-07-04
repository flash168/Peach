#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#添加一个基于asp.net6.0的镜像，命名为base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#我们将会拉取.net镜像的.net core6.0的sdk 來构建我们这个项目,命名为build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Peach.Host/Peach.Host.csproj", "Peach.Host/"]
COPY ["Peach.Application/Peach.Application.csproj", "Peach.Application/"]
COPY ["Peach.Domain/Peach.Domain.csproj", "Peach.Domain/"]
COPY ["Peach.Drpy/Peach.Drpy.csproj", "Peach.Drpy/"]
COPY ["Peach.Infrastructure/Peach.Infrastructure.csproj", "Peach.Infrastructure/"]
RUN dotnet restore "Peach.Host/Peach.Host.csproj"
COPY . .
WORKDIR "/src/Peach.Host"
RUN dotnet build "Peach.Host.csproj" -c Release -o /app/build

#项目构建完成后我们需要打包发布，所以我们继续使用.net core6.0的sdk來发布,也就是上面命名的build，生成可执行的dll文件
FROM build AS publish
RUN dotnet publish "Peach.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

#我们将会切换会第一阶段的base镜像，然后我们将会使用COPY命令把构建发布完成的所有文件都复制到工作目录，最后给Docker设置一个启动点，通过我们的dotnet命令启动我们的项目
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Peach.Host.dll"]