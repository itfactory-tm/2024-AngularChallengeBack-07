# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the official ASP.NET Core build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FritFest.API/FritFest.API.csproj", "FritFest.API/"]
RUN dotnet restore "FritFest.API/FritFest.API.csproj"
COPY . .
WORKDIR "/src/FritFest.API"
RUN dotnet build "FritFest.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FritFest.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FritFest.API.dll"]
