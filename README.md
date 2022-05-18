# Another URL Shortener
.NET 5 Web API, architecture and patterns
* Repository pattern
* Request handling with (self registerd) service

ToDo: updating...

### Commands [Entitiy Framework, Code First]
* First create the migration: ```dotnet ef migrations add InitialCreate```
* Then apply the database: ```dotnet ef database update```
* To update the schema: ```dotnet ef database update 0```
* To drop the migration: ```dotnet ef migrations remove```

### Command to build the solution
* ```dotnet build```
* ```dotnet restore```
* ```dotnet run```


add ```--project "Another URL Shortener"``` suffix.

### Docker steps
* Create Dockerfile
* Create .dockerignore
* Update Postgres configuration `pg_hbf.conf` to accept all requests:
`host    all             all             0.0.0.0/0               md5`
* Then restart Postgres service

#### Commands
* ```docker build -t another-url-shortener-image -f Dockerfile .```
* THIS ```docker create -p 44326:80 --name another-url-shortener-container another-url-shortener-image```
* ```docker start another-url-shortener-container```
* OR THIS ```docker run -rm -p 44326:80 another-url-shortener-image```

Request to: http://localhost:44326/api/ShortUrls/