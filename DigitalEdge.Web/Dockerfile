#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["DigitalEdge.Web/DigitalEdge.Web.csproj", "DigitalEdge.Web/"]
COPY ["DigitalEdge.Services/DigitalEdge.Services.csproj", "DigitalEdge.Services/"]
COPY ["DigitalEdge.Domain/DigitalEdge.Domain.csproj", "DigitalEdge.Domain/"]
COPY ["DigitalEdge.Repository/DigitalEdge.Repository.csproj", "DigitalEdge.Repository/"]
COPY ["DigitalEdge.Utility/DigitalEdge.Utility.csproj", "DigitalEdge.Utility/"]
RUN dotnet restore "DigitalEdge.Web/DigitalEdge.Web.csproj"
COPY . .
WORKDIR "/src/DigitalEdge.Web"
RUN dotnet build "DigitalEdge.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DigitalEdge.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DigitalEdge.Web.dll"]