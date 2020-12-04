#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/ETax/MISA.Mintax.ETax.Api/MISA.Mintax.ETax.Api.csproj", "Services/ETax/MISA.Mintax.ETax.Api/"]
COPY ["Services/ETax/MISA.Mintax.ETax.Infrastructure/MISA.Mintax.ETax.Infrastructure.csproj", "Services/ETax/MISA.Mintax.ETax.Infrastructure/"]
COPY ["Services/ETax/MISA.Mintax.ETax.ApplicationCore/MISA.Mintax.ETax.ApplicationCore.csproj", "Services/ETax/MISA.Mintax.ETax.ApplicationCore/"]
RUN dotnet restore "Services/ETax/MISA.Mintax.ETax.Api/MISA.Mintax.ETax.Api.csproj"
COPY . .
WORKDIR "/src/Services/ETax/MISA.Mintax.ETax.Api"
RUN dotnet build "MISA.Mintax.ETax.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MISA.Mintax.ETax.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD cp /app/configs/appsettings.json /app && dotnet MISA.Mintax.ETax.Api.dll
