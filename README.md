# Timelogger with ReactJs and .Core

## Scenario

### Overview
The task is to implement a simple time logger web application that solves the following three user stories:

1. As a freelancer I want to be able to register how I spend time on my _projects_, so that I can provide my _customers_ with an overview of my work.
2. As a freelancer I want to be able to get an _overview of my time registrations per project_, so that I can create correct invoices for my customers.
2. As a freelancer I want to be able to _sort my projects by their deadline_, so that I can prioritise my work.

### Run Steps

To run this project you will need both .NET Core v3.1 and Node installed on your environment.

Server - `dotnet restore` - to restore nuget packages, `dotnet build` - to build the solution, `cd Timelogger.Api && dotnet run` - starts a server on http://localhost:3001. 

Client - `npm install` to install dependencies, `npm start` runs the create-react-app development server