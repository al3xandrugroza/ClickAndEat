# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
	
WORKDIR /source

RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs

# copy csproj and restore as distinct layers
COPY IdentityServer.sln .
COPY IdentityServer/IdentityServer.csproj ./IdentityServer/
RUN dotnet restore

# copy everything else and build app
COPY IdentityServer/. ./IdentityServer/
WORKDIR /source/IdentityServer
RUN dotnet publish -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "IdentityServer.dll"]