FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PurchaseManagementApi/PurchaseManagementApi.csproj", "PurchaseManagementApi/"]
RUN dotnet restore "PurchaseManagementApi/PurchaseManagementApi.csproj"
COPY . .
WORKDIR "/src/PurchaseManagementApi"
RUN dotnet build "PurchaseManagementApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PurchaseManagementApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PurchaseManagementApi.dll"]
