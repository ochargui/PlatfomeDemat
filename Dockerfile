FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build-env
WORKDIR /app

# Copy everything else and build
COPY . ./

RUN cd src/AmenProject && dotnet publish -c Release -o out DEMAT.sln

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim


RUN apt-get update
RUN apt-get install -y libgdiplus libc6-dev
RUN apt-get install -y libicu-dev libharfbuzz0b libfontconfig1 libfreetype6
RUN apt-get install -y wget
RUN apt install -y default-jre
RUN wget https://github.com/liquibase/liquibase/releases/download/v4.5.0/liquibase-4.5.0.tar.gz
RUN wget https://jdbc.postgresql.org/download/postgresql-42.3.0.jar
RUN mkdir liquibase
RUN tar -xvf liquibase-4.5.0.tar.gz -C /liquibase
RUN mv postgresql-42.3.0.jar /liquibase/lib/
ENV PATH="/liquibase:${PATH}"

WORKDIR /app
COPY --from=build-env /app/src/AmenProject/out ./

COPY migrations /app

RUN chmod 755 /app/docker-entrypoint.sh
ENTRYPOINT ["/app/docker-entrypoint.sh"]

CMD ["dotnet", "Api.dll"]

