DIGITAL EDGE
===
A suite of mircorservice application components built with ASP.NET Core and C# https://docs.microsoft.com/en-us/dotnet/architecture/microservices/multi-container-microservice-net-applications/microservice-application-design. 

Overview
-----------
Digital Edge provides REST API and database services that support CTS (https://github.com/Digital-Prophets/cts-frontend), CTS Reports (https://github.com/Digital-Prophets/cts_reports_client), and SMS (https://github.com/Digital-Prophets/sms_platform) applications.

Installation
-------------
For local dev set up:
* Download .net core sdk(3.1) https://dotnet.microsoft.com/en-us/download/dotnet/3.1
* Download visual studio https://visualstudio.microsoft.com/ and set up necesarry workloads(IDE will suggest which workloads to include when you interact with codebase)
* Alternatively you can use visual studio code https://code.visualstudio.com/docs/languages/dotnet
* Fork https://github.com/Digital-Prophets/cts-backend-digitaledge and clone it onto local machine.

Database set up
---------------
### Using Visual Studio
* Navigate to "Tools" tab, click on "NugetPacket Manager" option and then open "Package Manager console".
*	Run **Update-Database** command in the PM console to create database with all tables.
*	Open MS SQL Managment Studio to view and interact with database.
* If you can see database with tables then migrations were successful

Launch Application
------------------
### Using Visual Studio
*	Navigate to "Debug" tab click on "Start Debugging" option or simply press F5
* When application is running, browser will be automatically launched on url http://localhost:57637/ with a black screen and wording of "This localhost page cannot be found"(Landing Page will be added soon), then application is successfully launched.
*	Navigate to http://localhost:57637/swagger/index.html to interact with APIs and test using PostMan (https://www.postman.com/) or api platform of choice.
	


For production set up:
* In production Digital Edge is running on Docker containers https://www.docker.com/ and detailed instructions will be attached soon to configure Digital Edge with Docker
