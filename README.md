# Simple Cow API + Req'n'Roll
## Description
A small API and test project built for the specific purpose of demoing how Req 'n' Roll
can be implemented to test DotNet based API's
### Requirements
- .NET 8.0
- VS Code Extensions
    - C# Dev Kit
    - .NET Core Test Explorer

### VS Code Extension Settings
To get the Reqnroll tests showing up in the test explorer you need to do the following

- Open your settings in VS Code
    - File -> Preferences -> Settings
- Select 'Workspace'
- Expand 'Extensions' and select '.NET Core Test Explorer
- Scroll down until you get to 'Test Project Path'
- Paste **/*Tests.csproj into the field
- Rebuild the whole project
    - Run `dotnet build` in a terminal window in the root of the project

***
## Simple Cow API
To start the API, use the below command 
`dotnet run --project SimpleCowApi`

Once running you can view the endpoints at:

- http://localhost:5070/swagger/index.html
***

## Req 'n' Roll

[Req 'n' Roll documentation](https://docs.reqnroll.net/latest/)
***

## Running The Tests

if you have the test runner set up and working you can run them from the UI

If you don't have the test runner set up run `dotnet test` in a terminal window in the root of the project