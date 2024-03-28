FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MessageBus/MessageBus.csproj", "MessageBus/"]
RUN dotnet restore "MessageBus/MessageBus.csproj"
COPY . .
WORKDIR "/src/MessageBus"
RUN dotnet build "MessageBus.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MessageBus.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageBus.dll"]
