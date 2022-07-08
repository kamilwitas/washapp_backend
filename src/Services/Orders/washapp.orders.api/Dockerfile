#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Orders/washapp.orders.api/washapp.orders.api.csproj", "src/Services/Orders/washapp.orders.api/"]
COPY ["src/Services/Orders/washapp.orders.core/washapp.orders.core.csproj", "src/Services/Orders/washapp.orders.core/"]
COPY ["src/Services/Orders/washapp.orders.application/washapp.orders.application.csproj", "src/Services/Orders/washapp.orders.application/"]
COPY ["src/Shared/EventBus.Messages/EventBus.Messages.csproj", "src/Shared/EventBus.Messages/"]
COPY ["src/Services/Orders/washapp.orders.infrastructure/washapp.orders.infrastructure.csproj", "src/Services/Orders/washapp.orders.infrastructure/"]
RUN dotnet restore "src/Services/Orders/washapp.orders.api/washapp.orders.api.csproj"
COPY . .
WORKDIR "/src/src/Services/Orders/washapp.orders.api"
RUN dotnet build "washapp.orders.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "washapp.orders.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "washapp.orders.api.dll"]