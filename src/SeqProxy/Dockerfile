FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /repo
COPY . .
RUN dotnet publish -c Release -o /build

FROM base
WORKDIR /app
COPY --from=build /build .
ENTRYPOINT ["dotnet", "SeqProxy.dll"]
