
# Tracking System Project

## Introduction

a Vehicle Tracking system has been designed to records all the vehicle's tracking point every 30 seconds. All the functionalities are ready to go and the non-functional requirement has been archived (Security, Scalability, Extensibility, Maintainability and Performance).

I will explain deeper about design decisions such as db, chosen archiectures and how the project measure the test's requirements below.

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
  6. Now, we are ready to go test following APIs below. The default username and password are "admin@gmail.com / 123456789".
## What is concurrency problem ?

The concurrency problem happend when a data are modified from 2 differences source and it cause the missing and update wrong data information (Last Win).

### Database Structure & Concurrency Solution

The Database contains three main tables (Account, AccountStatement, Transaction). The Account table have a relationship (one to many) with Statment & Transaction.

The designed idea is we will calculate the current balance base on the accoount's transactions because there is not "Current State" concept in Banking, Finance, Insurrance or Trading, etc... field, if we calculate the current balance eveytime the user do a deposit or withdrawal and store it back to current balance column, that'd be totally wrong because users may have some arguements with our system in the future relate to their balance, we can't just reply on the "CurrentBalance" column.

So if you understand the idea above, you'd think we should calculate current balance from transaction?. That's correct but what's gonna happen if we have too much transactions? surely our system will be slowing down because of the summation, that why we need the AccountStatement. 

The AccountStatement is acted as a Snapshot for the Transaction table, every month (or every week, depend on the volumn of transacions) we will take a snapshot for every account and store it into a new Statement and calculate the ClosingBalance with the fomula below:

```
 (ClosingBalance from last Statement + Deposited Transactions since the last Statement) - Withdrawn Transactions since the last Statement
```
The fomula above also apply for current balance calculation and everytime the user make a deposit or withdrawal, we will update the LastActivity column in the Account table to prevent the concurrency user problem.

### Testing

After you finish the Build step above, you can run BankingSystem.Test project to see the test results and make sure you install all the tools for Nunit Runner 3.

The purpose of all my Unit test are to prove my logic is corrected and this is also a way to document my code & logic, etc...

### Biggest Challenges

- Handling concurrency with stateless bank account.
- Applying CQRS.

### Improvements
There are many improvement for this project following below:

+ Creating unit test for the project.

+ Do APIs document with Swagger or OpenAPI. 

+ Adding more document for all the layers.

+ Improving the role base authorization more flexible.

+ Creating appsetting.json file for some environments (STAG, PROD, etc...). 

### References
* [CQRS](https://martinfowler.com/bliki/CQRS.html)
* [MediatR](https://github.com/jbogard/MediatR)
* [FluentValidation](https://github.com/JeremySkinner/FluentValidation)
 
Thanks for reading this.;)



