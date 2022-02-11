#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["JaVisitei.Brasil.Api/JaVisitei.Brasil.Api.csproj", "JaVisitei.Brasil.Api/"]
RUN dotnet restore "JaVisitei.Brasil.Api/JaVisitei.Brasil.Api.csproj"
COPY . .
WORKDIR "/src/JaVisitei.Brasil.Api"
RUN dotnet build "JaVisitei.Brasil.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JaVisitei.Brasil.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JaVisitei.Brasil.Api.dll"]