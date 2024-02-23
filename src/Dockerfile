FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /
COPY ["Crud.Api/Crud.Api.csproj", "Crud.Api/"]
COPY ["Crud.Infrastructure/Crud.Infrastructure.csproj", "Crud.Infrastructure/"]
COPY ["Crud.Application/Crud.Application.csproj", "Crud.Application/"]
COPY ["Crud.Domain/Crud.Domain.csproj", "Crud.Domain/"]
RUN dotnet restore "Crud.Api/Crud.Api.csproj"
COPY . .
WORKDIR "/Crud.Api"
RUN dotnet build "Crud.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Crud.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Crud.Api.dll"]
