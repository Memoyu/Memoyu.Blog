#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Memoyu.Blog.HttpApi.Hosting/Memoyu.Blog.HttpApi.Hosting.csproj", "Memoyu.Blog.HttpApi.Hosting/"]
COPY ["Memoyu.Blog.BackgroundJobs/Memoyu.Blog.BackgroundJobs.csproj", "Memoyu.Blog.BackgroundJobs/"]
COPY ["Memoyu.Blog.Domain/Memoyu.Blog.Domain.csproj", "Memoyu.Blog.Domain/"]
COPY ["Memoyu.Blog.Domain.Shared/Memoyu.Blog.Domain.Shared.csproj", "Memoyu.Blog.Domain.Shared/"]
COPY ["Memoyu.Blog.EntityFrameworkCore/Memoyu.Blog.EntityFrameworkCore.csproj", "Memoyu.Blog.EntityFrameworkCore/"]
COPY ["Memoyu.Blog.Swagger/Memoyu.Blog.Swagger.csproj", "Memoyu.Blog.Swagger/"]
COPY ["Memoyu.Blog.HttpApi/Memoyu.Blog.HttpApi.csproj", "Memoyu.Blog.HttpApi/"]
COPY ["Memoyu.Blog.Application/Memoyu.Blog.Application.csproj", "Memoyu.Blog.Application/"]
COPY ["Memoyu.Blog.Application.Caching/Memoyu.Blog.Application.Caching.csproj", "Memoyu.Blog.Application.Caching/"]
COPY ["Memoyu.Blog.Application.Contracts/Memoyu.Blog.Application.Contracts.csproj", "Memoyu.Blog.Application.Contracts/"]
COPY ["Memoyu.Blog.ToolKits/Memoyu.Blog.ToolKits.csproj", "Memoyu.Blog.ToolKits/"]
RUN dotnet restore "Memoyu.Blog.HttpApi.Hosting/Memoyu.Blog.HttpApi.Hosting.csproj"
COPY . .
WORKDIR "/src/Memoyu.Blog.HttpApi.Hosting"
RUN dotnet build "Memoyu.Blog.HttpApi.Hosting.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Memoyu.Blog.HttpApi.Hosting.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Memoyu.Blog.HttpApi.Hosting.dll"]