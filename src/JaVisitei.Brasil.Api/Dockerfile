FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
MAINTAINER Wellington dos Santos Castor
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
ARG AWS_PACKAGE
ARG AWS_ID
ARG AWS_KEY
ENV PATH $PATH:/root/.dotnet/tools

RUN apt-get update && apt install unzip && apt-get install -y curl
RUN curl "https://awscli.amazonaws.com/awscli-exe-linux-x86_64.zip" -o "awscliv2.zip"
RUN unzip awscliv2.zip && ./aws/install
RUN dotnet tool install -g AWS.CodeArtifact.NuGet.CredentialProvider
RUN dotnet codeartifact-creds install
RUN dotnet nuget locals all --clear
RUN dotnet codeartifact-creds configure set profile user1
RUN aws configure set region "us-east-1" --profile user1
RUN aws configure set aws_access_key_id $AWS_ID --profile user1
RUN aws configure set aws_secret_access_key $AWS_KEY --profile user1

COPY JaVisitei.Brasil.Api.csproj JaVisitei.Brasil.Api/
COPY ../JaVisitei.Brasil.Test/JaVisitei.Brasil.Test.csproj JaVisitei.Brasil.Test/

RUN dotnet restore JaVisitei.Brasil.Api/*.csproj -s $AWS_PACKAGE -s "https://api.nuget.org/v3/index.json"

COPY . .
RUN dotnet build "JaVisitei.Brasil.Api.csproj" -c Release -o /app/build
 

FROM build AS publish
RUN dotnet publish "JaVisitei.Brasil.Api.csproj" -c Release -o /app/publish --no-restore


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JaVisitei.Brasil.Api.dll"]