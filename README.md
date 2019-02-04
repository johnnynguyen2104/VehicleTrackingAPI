
# Vehicle Tracking System Project

## Introduction

a Vehicle Tracking system has been designed to records all the vehicle's tracking point every 30 seconds. All the functionalities are ready to go and the non-functional requirement has been archived (Security, Scalability, Extensibility, Maintainability and Performance).

I will explain deeper about design decisions such as db, chosen archiectures and how the project measure the test's requirements in below sections.

## Objectives

I designed the architecture for this project base on CQRS(Command Query Responsibility Segregation), Repository pattern and few software principals such as SOLID, Seperate of concern. Moreover, fulfilling all the functionalities and measure the non-functional requirements is the most important.

1. Security.
2. Readability.
3. Testability.
4. Performance.
5. Scalability.
6. Extensibility
7. Maintainability

## How Far Did I Get?

I completed the test's requirements as well as the bonus feature.

## Technologies & Libs

ASP.Net Core, Entity Framework Core, FluentValidations, MediatR, Sql Server. 

## How to build code

  1. Clone the repository (skip this step if you have the project on your machine)
  
  2. Running EF Migration to create Tracking & Vehicle database. Open a command prompt at root directory that contains project's solution and run these commands below to create database
     ```
     dotnet ef database update -c VehicleTracking.Persistence.VehicleTrackingDbContext -p VehicleTracking.WebApi
     
     dotnet ef database update -c VehicleTracking.Persistence.TrackingDbContext -p VehicleTracking.WebApi
     ```
  3. After that, on the current prompt, we will restore required packages by running:
     ```
     dotnet restore
     ```
  4. Next, build the solution by running:
     ```
     dotnet build
     ```
  5. Next, open a command prompt at `VehicleTrackingAPI\VehicleTracking.WebApi` directory (or use "cd" command), launch the Web API by running:
     ```
     dotnet run
     ```
  6. Once the api is running, it's ready to go with following APIs below. The default username/password and default port are "admin@gmail.com / 123456789" and localhost:5555.
      ```
     api/vehicle => register vehicle { "deviceCode": "12345", "activatedCode": "1234", "deviceModel" : "abcd", "RegisteredName": "xxx", "RegisteredPhone": "xxx"}
     
     api/tracking => record tracking point { "deviceCode": "12345", "activatedCode": "1234", "vehicleId" : "abcd", "latitude": "xxx", "longitude": "xxx"}
     
     api/tracking/{vehicleId}/{activatedCode}/{deviceCode} => Return the latest tracking point.
     
     api/tracking/{vehicleId}/{activatedCode}/{deviceCode}/journey/{fromDateTime}/{toDateTime} => Get a journey in a certain of time
     ```
## Dive Deeper To Architecture & Non-Functional Requirements ?

CQRS is stand for Command Query Responsibility Segregation. For large systems, this typically allows you to separate the querying of data from the updating of data (splitting the “read side” from the “write side”) into "Command" and "Query" so the benefits that we get are :

  - Scalability (read exceeds the write, so does the scaling requirements for each differs and can be addressed better)
  - Flexibility (separate read / write models)
  - Reduced Complexity (shifting complexity into separate concerns)
  - Concentrate on Domain / Business
  - Facilitates designing intuitive task-based UIs
  - Large team - You can split development tasks between people easily if you have chosen CQRS architecture. Your top people can work on domain logic leaving usual stuff to less skilled developers.
  - Difficult business logic - CQRS forces you to avoid mixing domain logic and infrastructural operations.
  - Scalability matters - With CQRS you can achieve great read and write performance, command handling can be scaled out on multiple nodes and as queries are read-only operations they can be optimized to do fast read operations.

For more information about CQRS, please read the link in the referenced section.
    
## Database Structure & Concurrency Solution

For the Vehicle Database, it mainly contains basic information of Vehicle + some other tables for Authentication & Authorization.

The second database is Tracking Database, the whole idea about scaling is on this database. First of all, I will explain the way to design and store tracking information so we can boost up our query. The idea is I created a tracking-snapshot table to snapshot our tracking information everday for each vehicle (the snapshot will be automatically created by the first tracking information of the day). Then when a tracking device send a tracking information to our API, we will record it base on some information as I listed above and map the tracking information with the current day's snapshot. You can imagine the way that people manage a Dictionary, it have header (similar to snapshot) with dictionary's words (tracking information). 

Eventually, when we do a query to get a journey or the current position of a vehicle, we can put a period of time into the query  and query Tracking Snapshot table then inner join with the Tracking point to reduce I/O reading and boost up our query instead of doing query directly from the Tracking Point table. In addition, splitting this system into two different databases, we will receive few benefits such as: 

  - Easily to scale and apply the database replication for the Tracking Database.
  - Using full benefits of CQRS and easily to apply Event-Sourcing to pub/sub our message for some future functions relate to "real-time" or communication between models. 

## Improvements
There are many improvement for this project following below:

+ Creating unit test for the project.

+ Do APIs document with Swagger or OpenAPI. 

+ Adding more document for all the layers.

+ Improving the role base authorization more flexible.

+ Creating appsetting.json file for some environments (STAG, PROD, etc...). 

+ Using Docker to dock this project for further deployment and easier to deliver.

+ Something that I haven't figured it out :D!

## References
* [CQRS](https://martinfowler.com/bliki/CQRS.html)
* [More CQRS](https://sookocheff.com/post/architecture/what-is-cqrs/)
* [MediatR](https://github.com/jbogard/MediatR)
* [FluentValidation](https://github.com/JeremySkinner/FluentValidation)
 
Thanks for reading this.;)



