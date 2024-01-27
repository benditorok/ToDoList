# ToDoList
## Work in Progress

adb root
adb reverse tcp:8080 tcp:8080
dotnet build -f net8.0-android -c Release

### PostgreSQL
You can run PostgreSQL from a docker container, which is the easiest way in my opinion. You can find alternative images on [PostgreSQL Docker Hub](https://hub.docker.com/_/postgres).
```
docker pull postgres:16.1-alpine3.19
```
To run the container replace PWD with your password and the docker image name if you want to use a different one.
```
docker run -d -it --name postgres_todolist --restart=unless-stopped -p 5432:5432 -e POSTGRES_PASSWORD=postgres -d postgres:16.1-alpine3.19 -c shared_buffers=256MB -c max_connections=200 -c listen_addresses='*'
```
psql -U postgres

### Migrations
Using the Package Manager Console:
```
Add-Migration InitialCreate -p ToDoList.Infrastructure -s ToDoList.WebApi -Context ApplicationDbContext
```
Update the database:
```
Update-Database -p ToDoList.Infrastructure -s ToDoList.WebApi -Context ApplicationDbContext
```
```
Remove-Migration -p ToDoList.Infrastructure -s ToDoList.WebApi -Context ApplicationDbContext
```
POSTGRE_TODOLIST = Host=host.docker.internal; Port=5432; Database=todolist; Username=postgres; Password=postgres