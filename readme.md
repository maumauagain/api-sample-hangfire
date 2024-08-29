## 📝 About
Just a repo to test the Hangfire lib for background jobs

## 💻 Download and Run the Project

In order to Hangfire to work, we need to already have the database created, so if you're running the application locally, please execute it on SQL Server instance:
```SQL
    CREATE DATABASE [HangfireTest]
    GO
```

```bash
    # Clone GIT repo
    $ git clone https://github.com/maumauagain/api-sample-hangfire

    # Access the directory containing the project files
    $ cd api-sample-hangfire/src/Hangfire.API

    # With the database created locally pointing to the same connection on appsettings.json, run:
    $ dotnet restore

    # Run the command below and the hangfire dashboard should be available on localhost:5029/hangfire
    $ dotnet run
```

If you want to run through Docker, then there's no need to create the database since the container will create it once started:


```bash
    # With Docker installed, just follow:

    # Clone GIT repo
    $ git clone https://github.com/maumauagain/api-sample-hangfire

    # Access the directory containing the project files
    $ cd api-sample-hangfire/src/conf

    # Run the command below and the dashboard should be available on localhost:8080/hangfire
    $ docker-compose up --build -d
```
---

<h4 align="center">
    Developed by <a href="https://www.linkedin.com/in/amauri-martins-junior/" target="_blank">Amauri Martins </a> ⚓
</h4>