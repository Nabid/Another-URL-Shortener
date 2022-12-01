# Another URL Shortener
.NET 5 Web API, architecture and patterns
* Repository pattern
* Dependency injection
* Self registered services with scoped lifetime
* Request handling by services

[![.NET](https://github.com/Nabid/Another-URL-Shortener/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/Nabid/Another-URL-Shortener/actions/workflows/dotnet.yml) [![CodeQL](https://github.com/Nabid/Another-URL-Shortener/actions/workflows/codeql-analysis.yml/badge.svg?branch=master)](https://github.com/Nabid/Another-URL-Shortener/actions/workflows/codeql-analysis.yml)

ToDo: updating...

## Commands [Entitiy Framework, Code First]
* First create the migration: ```dotnet ef migrations add InitialCreate```
* Then apply the database (double check connection string): ```dotnet ef database update```
* Update the schema (if changed after creation): ```dotnet ef database update 0```
* Drop the migration: ```dotnet ef migrations remove```

## Command to build the solution
* ```dotnet build```
* ```dotnet restore```
* ```dotnet run```

add ```--project "Another URL Shortener"``` suffix.

Request to: https://localhost:5001/api/ShortUrls/

## Docker steps
* Create Dockerfile
* Create .dockerignore
* Update Postgres configuration `pg_hbf.conf` to accept all requests:
`host    all             all             0.0.0.0/0               md5`
\***
* Then restart Postgres service
* Correct `ConnectionStrings`: update host address in `DockerAppLocalSql`
* Add `DockerAppLocalSql` in `Startup.cs`

*** Reference: [Stackoverflow](https://stackoverflow.com/a/31249288/3731282)
### Commands
* ```docker build -t another-url-shortener-image -f Dockerfile ../Another\ URL\ Shortener```
* THIS ```docker create -p 44326:80 --name another-url-shortener-container another-url-shortener-image```
* ```docker start another-url-shortener-container```
* OR THIS ```docker run -rm -p 44326:80 another-url-shortener-image```

Request to: http://localhost:44326/api/ShortUrls/

## Troubleshoot
### Unable to start Kestrel
Environment: MacOS, VS Code
`crit: Microsoft.AspNetCore.Server.Kestrel[0]`
System.InvalidOperationException: Unable to configure HTTPS endpoint. No server certificate was specified, and the default developer certificate could not be found or is out of date.
To generate a developer certificate run 'dotnet dev-certs https'. To trust the certificate (Windows and macOS only) run 'dotnet dev-certs https --trust'.
For more information on configuring HTTPS see https://go.microsoft.com/fwlink/?linkid=848054.

### error from sender: invalid excludepatterns: [bin\ obj\]: syntax error in pattern
Change "\" to "/" in .dockerignore file.