#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Src/Services/Identity/Identity.Api/Identity.Api.csproj", "Src/Services/Identity/Identity.Api/"]
COPY ["Src/Services/Identity/Identity.Application/Identity.Application.csproj", "Src/Services/Identity/Identity.Application/"]
COPY ["Src/Services/Identity/Identity.Domain/Identity.Domain.csproj", "Src/Services/Identity/Identity.Domain/"]
COPY ["Src/Services/Core/DomainCore/DomainBase.csproj", "Src/Services/Core/DomainCore/"]
COPY ["Src/Services/Identity/Identity.Infrastructure/Identity.Infrastructure.csproj", "Src/Services/Identity/Identity.Infrastructure/"]
COPY ["Src/Services/Core/InfrastructureBase/InfrastructureBase.csproj", "Src/Services/Core/InfrastructureBase/"]
RUN dotnet restore "Src/Services/Identity/Identity.Api/Identity.Api.csproj"
COPY . .
WORKDIR "/src/Src/Services/Identity/Identity.Api"
RUN dotnet build "Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Api.dll"]