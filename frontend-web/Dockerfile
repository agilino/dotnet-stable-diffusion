#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["frontend-web/frontend-web.csproj", "frontend-web/"]
RUN dotnet restore "frontend-web/frontend-web.csproj"
COPY . .
WORKDIR "/src/frontend-web"
RUN dotnet build "frontend-web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "frontend-web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "frontend-web.dll"]
