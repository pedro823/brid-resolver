FROM microsoft/dotnet:2.1-aspnetcore-runtime

WORKDIR /app
ADD ./IDResolver .
EXPOSE 5000
EXPOSE 5001

ENTRYPOINT [ "dotnet", "run", "IDResolver/IDResolver.csproj" ]
# ENTRYPOINT [ "/bin/bash" ]