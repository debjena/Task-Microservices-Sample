FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SubTask-Api/SubTask-Api.csproj", "SubTask-Api/"]
COPY ["EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
RUN dotnet restore "SubTask-Api/SubTask-Api.csproj"
COPY . .
WORKDIR "/src/SubTask-Api"
RUN dotnet build "SubTask-Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SubTask-Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SubTask-Api.dll"]