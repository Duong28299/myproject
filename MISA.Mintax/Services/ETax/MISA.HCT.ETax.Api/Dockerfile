#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/ETax/MISA.HCT.ETax.Api/MISA.HCT.ETax.Api.csproj", "Services/ETax/MISA.HCT.ETax.Api/"]
COPY ["Services/ETax/MISA.HCT.ETax.Infrastructure/MISA.HCT.ETax.Infrastructure.csproj", "Services/ETax/MISA.HCT.ETax.Infrastructure/"]
COPY ["Services/ETax/MISA.HCT.ETax.ApplicationCore/MISA.HCT.ETax.ApplicationCore.csproj", "Services/ETax/MISA.HCT.ETax.ApplicationCore/"]
RUN dotnet restore "Services/ETax/MISA.HCT.ETax.Api/MISA.HCT.ETax.Api.csproj"
COPY . .
WORKDIR "/src/Services/ETax/MISA.HCT.ETax.Api"
RUN dotnet build "MISA.HCT.ETax.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MISA.HCT.ETax.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD cp /app/configs/appsettings.json /app && dotnet MISA.HCT.ETax.Api.dll
