FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Task-Api/Task-Api.csproj", "Task-Api/"]
COPY ["EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
RUN dotnet restore "Task-Api/Task-Api.csproj"
COPY . .
WORKDIR "/src/Task-Api"
RUN dotnet build "Task-Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Task-Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Task-Api.dll"]