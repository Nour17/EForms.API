#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["EForms.API/EForms.API.csproj", "EForms.API/"]
COPY ["EForms.API.Core/EForms.API.Core.csproj", "EForms.API.Core/"]
COPY ["EForms.API.Infrastructure/EForms.API.Infrastructure.csproj", "EForms.API.Infrastructure/"]
COPY ["EForms.API.Repository/EForms.API.Repository.csproj", "EForms.API.Repository/"]
RUN dotnet restore "EForms.API/EForms.API.csproj"
COPY . .
WORKDIR "/src/EForms.API"
RUN dotnet build "EForms.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EForms.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EForms.API.dll"]