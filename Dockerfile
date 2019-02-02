FROM microsoft/dotnet:2.2-sdk AS builder

WORKDIR /app
COPY ./IDResolver .

RUN dotnet restore
RUN dotnet publish -f netcoreapp2.2 -c Release -o /app-bin

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
WORKDIR /app-bin

COPY --from=builder /app-bin .
EXPOSE 5000
EXPOSE 5001

CMD [ "dotnet", "IDResolver.dll" ]