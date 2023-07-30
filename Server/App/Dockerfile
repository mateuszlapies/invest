FROM ubuntu:22.04 as builder
RUN apt-get update
RUN apt-get install -y dotnet-sdk-6.0
COPY ./ /app
WORKDIR /app
RUN dotnet build -c Release

FROM ubuntu:22.04 as base
RUN apt-get update
RUN apt-get dist-upgrade
RUN apt-get install -y aspnetcore-runtime-6.0
COPY --from=builder /app/App/bin/Release/net6.0 /app
WORKDIR /app
CMD dotnet run --urls http://localhost:5000

FROM ubuntu:22.04 as migrations
RUN apt-get update
RUN apt-get dist-upgrade
RUN apt-get install -y dotnet-runtime-7.0
COPY --from=builder /app/App.Database/bin/Release/net6.0 /app
WORKDIR /app
RUN dotnet tool install --global dotnet-ef
CMD /home/ubuntu/.dotnet/tools/dotnet-ef database update