# Simbir_Weather

## Description

### Simbirsoft_Weather
The main application of the project that processes weather forecasts, clothes and recommends them to users.

### Simbirsoft_Weather.Tests
Project with tests for Simbirsoft_Weather.

### TimerWorker
A helper application that checks the database for events ready to be dispatched. Also sends a POST request to the server to send a notification.

## Start

### 1. Cloning
Cloning this repository:
```
git clone https://github.com/Ars-Nikon/Simbir_Weather.git
```

### 2. Configuring
Configuring files:
  * Simbirsoft_Weather:
      * [appsettings.json](/Simbirsoft_Weather/appsettings.json)
  * TimerWorker:
      * [appsettings.json](/TimerWorker/appsettings.json)

### 3. Publication
Publication of projects:
  * Simbirsoft_Weather
  * TimerWorker

### 4. Deploy
Deploying projects:
  * Simbirsoft_Weather
  * TimerWorker

## ERD
[![ERD.png](https://i.postimg.cc/fLGNMmNh/ERD.png)](https://postimg.cc/qhxYLtqm)

## Used libraries
### Simbirsoft_Weather
* [Dadata](https://github.com/hflabs/dadata-csharp) v21.3.0
* [EntityFramework](https://github.com/dotnet/ef6/wiki) v6.4.4
* [MailKit](http://www.mimekit.net/) v2.13.0
* [Microsoft.AspNetCore.HttpOverrides](https://asp.net/) v2.2.0
* [Microsoft.AspNetCore.Identity.EntityFrameworkCore](https://asp.net/) v3.1.16
* [Microsoft.EntityFrameworkCore](https://docs.microsoft.com/ru-ru/ef/core/) v3.1.16
* [Microsoft.EntityFrameworkCore.SqlServer](https://docs.microsoft.com/ru-ru/ef/core/) v3.1.16
* [Microsoft.jQuery.Unobtrusive.Validation](https://asp.net/) v3.2.12
* [Npgsql.EntityFrameworkCore.PostgreSQL](https://github.com/npgsql/efcore.pg) v3.1.11

### Simbirsoft_Weather.Tests
* [coverlet.collector](https://github.com/coverlet-coverage/coverlet) v1.3.0
* [Microsoft.NET.Test.Sdk](https://github.com/microsoft/vstest/) v16.7.1
* [Moq](https://github.com/moq/moq4) v4.16.1
* [xunit](https://github.com/xunit/xunit) v2.4.1
* [xunit.runner.visualstudio](https://github.com/xunit/visualstudio.xunit) v2.4.3

### TimerWorker
* [Microsoft.EntityFrameworkCore](https://docs.microsoft.com/ru-ru/ef/core/) v3.1.16
* [Microsoft.EntityFrameworkCore.SqlServer](https://docs.microsoft.com/ru-ru/ef/core/) v3.1.16
* [Newtonsoft.Json](https://www.newtonsoft.com/json) v13.0.1
