# About -  Work in Progress
This project is a Todo List made in .NET MAUI with an ASP.NET backend which uses a PostgreSQL database or an in-memory database.

# Setup
You can run the application by setting up the **Client** and the **WebApi** project as the startup projects.
This will start the application using an **in-memroy database**.

Seeded accounts(username, password):
- administrator@localhost, Passw0rd!
- managerone@localhost, Passw0rd!
- managertwo@localhost, Passw0rd!

## PostgreSQL
You can run PostgreSQL from a docker container, which is the easiest way in my opinion. You can find alternative images on [PostgreSQL Docker Hub](https://hub.docker.com/_/postgres).
```
docker pull postgres:16.1-alpine3.19
```
To run the container replace PWD with your password and the docker image name if you want to use a different one.
```
docker run -d -it --name postgres_todolist --restart=unless-stopped -p 5432:5432 -e POSTGRES_PASSWORD=postgres -d postgres:16.1-alpine3.19 -c shared_buffers=256MB -c max_connections=200 -c listen_addresses='*'
```
After the database is set up create an environment variable on your system with the name of **POSTGRE_TODOLIST** and a value of **Host=host.docker.internal; Port=5432; Database=todolist; Username=postgres; Password=postgres**, which is **your connection string**.
This way the project will connect to the database instead of using an in-memory database.

## Docker container - not yet tested
You can build a Docker container **from the directory of ToDoList.WebApi** using:
```
docker build .. -t todolist -f Dockerfile
```
To run the container replace PWD with your password for the PostgreSQL database:
```
docker run -d -it --name todolist --restart=unless-stopped -p 8080:8080 -e POSTGRE_TODOLIST='Host=host.docker.internal; Port=5432; Database=todolist; Username=postgres; Password=postgres' -d todolist
```

## Migrations
The application automatically applies the latest migration on startup.
To create migrations use the Package Manager Console of VS22:
```
Add-Migration InitialCreate -p ToDoList.Infrastructure -s ToDoList.WebApi -Context ApplicationDbContext
```
```
Update-Database -p ToDoList.Infrastructure -s ToDoList.WebApi -Context ApplicationDbContext
Remove-Migration -p ToDoList.Infrastructure -s ToDoList.WebApi -Context ApplicationDbContext
```

### Notes:
Building for android only works with an emulator, since the project is not yet set up for https connections.
Exec into the postgresql container: psql -U postgres
TODO place the connection string in a configuration file and use the in-memory database as a fallback option
dotnet build -f net8.0-android -c Debug
dotnet build -f net8.0-windows10.0.19041.0 -c Debug