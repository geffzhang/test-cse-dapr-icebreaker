FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["csharp/Vigilantes.DaprWorkshop.VirtualBarista.csproj", "csharp/"]
RUN dotnet restore "csharp/Vigilantes.DaprWorkshop.VirtualBarista.csproj"
COPY . .
WORKDIR /src/csharp
RUN dotnet build "Vigilantes.DaprWorkshop.VirtualBarista.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Vigilantes.DaprWorkshop.VirtualBarista.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Vigilantes.DaprWorkshop.VirtualBarista.dll"]