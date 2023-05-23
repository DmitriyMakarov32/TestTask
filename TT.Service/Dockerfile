FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TT.Service/TT.Service.csproj", "TT.Service/"]
RUN dotnet restore "TT.Service/TT.Service.csproj"
COPY . .
WORKDIR "/src/TT.Service"
RUN dotnet build "TT.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TT.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TT.Service.dll"]
