# MyCare

<p align='left'><b>Your Medical Reminder</b></p>

<p align='left'>
	<a href='https://opensource.org/license/mit'><img alt="License" src="https://img.shields.io/static/v1?label=license&message=MIT&color=8257E5&labelColor=000000"></a>
	
</p>

## <a name="documentation">Documentation</a>

For more help and document, see our documentation:

- [Introduction](#apresentation)
- [Basic Usage](#basicUsage)
  - [Prerequisites](#prerequisites)
  - [Cloning the Repository](#cloning)
  - [Installation](#installation)
  - [Connect to the database](#connectdb)
  - [Running the Project](#running)
- [](#)

## <a name="apresentation">Introduction</a>

MyCare was created to facilitate the control and history of prescribed medication, also providing simple descriptions about the medicine, its components and what type of prescription is necessary, for example.

See for yourself by running it on your machine by following the steps below.

## <a name="basicUsage">Basic Usage</a>

Follow these steps to set up the project locally on your machine.

**Prerequisites**
<a name="prerequisites"></a>

Make sure you have the following installed on your machine:

- [Git](https://git-scm.com/)
- [.NET 8.0](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
- [SQL Server Express](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)

**Cloning the Repository**
<a name="cloning"></a>

```bash
git clone https://github.com/gcmorais/mycare-api.git 
cd mycare-api
```

**Installation**
<a name="installation"></a>

Open the MyCare solution in Visual Studio to automatically install the project dependencies.

```bash
MyCare.sln
```

> [!note]
>
>  Run the `dotnet restore` command, the .NET CLI uses NuGet to look for these dependencies and download them if necessary. 


**Connect to the database**
<a name="connectdb"></a>

We first need to connect to the database, so in the appsettings.json file (which is located in MyCare.API) in `DefaultConnection` in the server tag you will put the name of your server that was created in SQL Server Express;

```env
"DefaultConnection": "server= localhost\\Example; database= ExampleDB; trusted_connection=true; trustservercertificate=true"
```

I used the connection to the bank via Windows authentication, so use the tag `trusted_connection = true;` 

if you need it, use the tags `user id=login;` `password=password;` see if it makes sense in your use case.

**Running the Project**
<a name="running"></a>

First, let's run the migrations to create the tables for our database.
To do this, we'll open the Package Manager Console and issue the command:

```bash
add-migration CreateDatabase
```

After the Build successful message is returned from the console, use the following command to actually create the modifications within the database:

```bash
update-database
```


Run your project (CRTL + F5 in visual studio) to open [Swagger](https://swagger.io/) in your browser to view the project.

