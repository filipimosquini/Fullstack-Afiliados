#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Backend.Api/Backend.Api.csproj", "Backend.Api/"]
COPY ["src/Backend.Core/Backend.Core.csproj", "Backend.Core/"]
COPY ["src/Backend.Infra/Backend.Infra.csproj", "Backend.Infra/"]
COPY ["src/Backend.Infra.CrossCutting/Backend.Infra.CrossCutting.csproj", "Backend.Infra.CrossCutting/"]
COPY ["src/Backend.Infra.Ioc/Backend.Infra.Ioc.csproj", "Backend.Infra.Ioc/"]

RUN dotnet restore "Backend.Api/Backend.Api.csproj"
COPY . .
WORKDIR "src/Backend.Api"
RUN dotnet build "Backend.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Backend.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backend.Api.dll"]