FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["IdentityServiceApi/IdentityServiceApi.csproj", "IdentityServiceApi/"]
RUN dotnet restore "IdentityServiceApi/IdentityServiceApi.csproj"
COPY . .
WORKDIR "/src/IdentityServiceApi"
RUN dotnet build "IdentityServiceApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityServiceApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityServiceApi.dll"]
