FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["csharp/Vigilantes.DaprWorkshop.ReceiptGenerationService.csproj", "csharp/"]
RUN dotnet restore "csharp/Vigilantes.DaprWorkshop.ReceiptGenerationService.csproj"
COPY . .
WORKDIR /src/csharp
RUN dotnet build "Vigilantes.DaprWorkshop.ReceiptGenerationService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Vigilantes.DaprWorkshop.ReceiptGenerationService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Vigilantes.DaprWorkshop.ReceiptGenerationService.dll"]