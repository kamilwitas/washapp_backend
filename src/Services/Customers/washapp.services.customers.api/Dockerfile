#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Customers/washapp.services.customers.api/washapp.services.customers.api.csproj", "src/Services/Customers/washapp.services.customers.api/"]
COPY ["src/Services/Customers/washapp.services.customers.domain/washapp.services.customers.domain.csproj", "src/Services/Customers/washapp.services.customers.domain/"]
COPY ["src/Services/Customers/washapp.services.customers.infrastructure/washapp.services.customers.infrastructure.csproj", "src/Services/Customers/washapp.services.customers.infrastructure/"]
COPY ["src/Services/Customers/washapp.services.customers.application/washapp.services.customers.application.csproj", "src/Services/Customers/washapp.services.customers.application/"]
COPY ["src/Shared/EventBus.Messages/EventBus.Messages.csproj", "src/Shared/EventBus.Messages/"]
RUN dotnet restore "src/Services/Customers/washapp.services.customers.api/washapp.services.customers.api.csproj"
COPY . .
WORKDIR "/src/src/Services/Customers/washapp.services.customers.api"
RUN dotnet build "washapp.services.customers.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "washapp.services.customers.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "washapp.services.customers.api.dll"]