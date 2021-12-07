FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["AutoFunnerWorker/AutoFunnerWorker.csproj", "AutoFunnerWorker/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "AutoFunnerWorker/AutoFunnerWorker.csproj"
COPY . .
WORKDIR "/src/AutoFunnerWorker"
RUN dotnet build "AutoFunnerWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AutoFunnerWorker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutoFunnerWorker.dll"]