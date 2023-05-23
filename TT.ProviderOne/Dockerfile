FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TT.ProviderOne/TT.ProviderOne.csproj", "TT.ProviderOne/"]
RUN dotnet restore "TT.ProviderOne/TT.ProviderOne.csproj"
COPY . .
WORKDIR "/src/TT.ProviderOne"
RUN dotnet build "TT.ProviderOne.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TT.ProviderOne.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TT.ProviderOne.dll"]
