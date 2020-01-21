FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TaskApiGateway/TaskApiGateway.csproj", "TaskApiGateway/"]
RUN dotnet restore "TaskApiGateway/TaskApiGateway.csproj"
COPY . .
WORKDIR "/src/TaskApiGateway"
RUN dotnet build "TaskApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskApiGateway.dll"]