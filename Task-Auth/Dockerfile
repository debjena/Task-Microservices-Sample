FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Task-Auth/Task-Auth.csproj", "Task-Auth/"] 
RUN dotnet restore "Task-Auth/Task-Auth.csproj"
COPY . .
WORKDIR "/src/Task-Auth"
RUN dotnet build "Task-Auth.csproj" -c Release -o /app/build
 
FROM build AS publish
RUN dotnet publish "Task-Auth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Task-Auth.dll"]