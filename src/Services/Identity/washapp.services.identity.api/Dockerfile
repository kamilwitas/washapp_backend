#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Identity/washapp.services.identity.api/washapp.services.identity.api.csproj", "src/Services/Identity/washapp.services.identity.api/"]
COPY ["src/Services/Identity/washapp.services.identity.application/washapp.services.identity.application.csproj", "src/Services/Identity/washapp.services.identity.application/"]
COPY ["src/Services/Identity/washapp.services.identity.domain/washapp.services.identity.domain.csproj", "src/Services/Identity/washapp.services.identity.domain/"]
COPY ["src/Services/Identity/washapp.services.identity.infrastructure/washapp.services.identity.infrastructure.csproj", "src/Services/Identity/washapp.services.identity.infrastructure/"]
RUN dotnet restore "src/Services/Identity/washapp.services.identity.api/washapp.services.identity.api.csproj"
COPY . .
WORKDIR "/src/src/Services/Identity/washapp.services.identity.api"
RUN dotnet build "washapp.services.identity.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "washapp.services.identity.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "washapp.services.identity.api.dll"]