FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
MAINTAINER Wellington dos Santos Castor
WORKDIR /app
EXPOSE 80
EXPOSE 443
#
##RUN --mount=type=secret,id=AWS_PACKAGE 
##  export AWS_PACKAGE=$(cat /run/secrets/AWS_PACKAGE)
#
#FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
#ENV PATH $PATH:/root/.dotnet/tools
#RUN --mount=type=secret,id=AWS_PACKAGE export AWS_PACKAGE=$(cat /run/secrets/AWS_PACKAGE)
#RUN --mount=type=secret,id=AWS_ID export AWS_ID=$(cat /run/secrets/AWS_ID)
#RUN --mount=type=secret,id=AWS_KEY export AWS_KEY=$(cat /run/secrets/AWS_KEY)

#RUN apt-get update && apt install unzip && apt-get install -y curl
#RUN curl "https://awscli.amazonaws.com/awscli-exe-linux-x86_64.zip" -o "awscliv2.zip"
#RUN unzip awscliv2.zip && ./aws/install
#RUN dotnet tool install -g AWS.CodeArtifact.NuGet.CredentialProvider
#RUN dotnet codeartifact-creds install
#RUN dotnet codeartifact-creds configure set profile user1
#RUN aws configure set region "us-east-1" --profile user1
#RUN aws configure set aws_access_key_id "${AWS_ID}" --profile user1
#RUN aws configure set aws_secret_access_key "${AWS_KEY}" --profile user1
#
#RUN ls -l

##COPY JaVisitei.Brasil.Api/JaVisitei.Brasil.Api.csproj JaVisitei.Brasil.Api/
##RUN dotnet restore JaVisitei.Brasil.Api/*.csproj -s "${AWS_PACKAGE}"
#COPY JaVisitei.Brasil.Api/JaVisitei.Brasil.Api.csproj JaVisitei.Brasil.Api/
#COPY JaVisitei.Brasil.Test/JaVisitei.Brasil.Test.csproj JaVisitei.Brasil.Test/
##
#RUN dotnet restore JaVisitei.Brasil.Api/*.csproj -s "${AWS_PACKAGE}"
##RUN dotnet restore JaVisitei.Brasil.Api/*.csproj -s ${{ secrets.AWS_PACKAGE }} -s "https://api.nuget.org/v3/index.json"
#COPY . .

#WORKDIR "/JaVisitei.Brasil.Api"
#RUN dotnet build "JaVisitei.Brasil.Api.csproj" -c Release -o /app/build
##RUN dotnet add "JaVisitei.Brasil.Api.csproj" package JaVisitei.Brasil.Nuget --version 1.0.2 --source "${AWS_PACKAGE}"
#
#FROM build AS publish
#RUN dotnet publish "JaVisitei.Brasil.Api.csproj" -c Release -o /app/publish

#COPY JaVisitei.Brasil.Api/JaVisitei.Brasil.Api.csproj JaVisitei.Brasil.Api/

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JaVisitei.Brasil.Api.dll"]