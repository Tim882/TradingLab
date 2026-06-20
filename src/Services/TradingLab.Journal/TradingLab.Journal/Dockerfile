# ===== STAGE 1: Build =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/Services/TradingLab.Journal/TradingLab.Journal/TradingLab.Journal.csproj", "src/TradingLab.Journal/"]
COPY ["src/Services/TradingLab.Journal/TradingLab.Journal.Application/TradingLab.Journal.Application.csproj", "src/TradingLab.Journal.Application/"]
COPY ["src/Services/TradingLab.Journal/TradingLab.Journal.Domain/TradingLab.Journal.Domain.csproj", "src/TradingLab.Journal.Domain/"]
COPY ["src/Services/TradingLab.Journal/TradingLab.Journal.Infrastructure/TradingLab.Journal.Infrastructure.csproj", "src/TradingLab.Journal.Infrastructure/"]

RUN dotnet restore "src/TradingLab.Journal/TradingLab.Journal.csproj"

COPY . .
WORKDIR "/src/src/Services/TradingLab.Journal/TradingLab.Journal"
RUN dotnet build "TradingLab.Journal.csproj" -c Release -o /app/build

# ===== STAGE 2: Publish =====
FROM build AS publish
RUN dotnet publish "TradingLab.Journal.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ===== STAGE 3: Runtime =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

RUN adduser --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

ENTRYPOINT ["dotnet", "TradingLab.Journal.dll"]