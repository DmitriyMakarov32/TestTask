FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TT.ProwiderTwo/TT.ProwiderTwo.csproj", "TT.ProwiderTwo/"]
RUN dotnet restore "TT.ProwiderTwo/TT.ProwiderTwo.csproj"
COPY . .
WORKDIR "/src/TT.ProwiderTwo"
RUN dotnet build "TT.ProwiderTwo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TT.ProwiderTwo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TT.ProwiderTwo.dll"]
