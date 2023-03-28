#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["organizer-backend-NET/organizer-backend-NET.csproj", "organizer-backend-NET/"]
COPY ["organizer-backend-NET.DAL/organizer-backend-NET.DAL.csproj", "organizer-backend-NET.DAL/"]
COPY ["organizer-backend-NET.Domain/organizer-backend-NET.Domain.csproj", "organizer-backend-NET.Domain/"]
COPY ["organizer-backend-NET.Service/organizer-backend-NET.Service.csproj", "organizer-backend-NET.Service/"]
RUN dotnet restore "organizer-backend-NET/organizer-backend-NET.csproj"
COPY . .
WORKDIR "/src/organizer-backend-NET"
RUN dotnet build "organizer-backend-NET.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "organizer-backend-NET.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "organizer-backend-NET.dll"]