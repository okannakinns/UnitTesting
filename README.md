# UnitTesting

This repository is a .NET project that explores unit testing step by step, from fundamentals to real-world scenarios. The project is organized into four separate sections, starting with simple calculator tests and expanding into assertion techniques, dependency isolation, mocking, validation, logging, and ASP.NET Core Web API service testing.

The goal is not only to write tests, but also to demonstrate testable code design, dependency abstraction, service-layer behavior verification, and reliable success/error flows in a real API scenario.

## Project Structure

```text
UnitTesting/
├── Fundamentals/
│   ├── src/CalculatorLibrary
│   └── test/CalculatorLibraryTests
├── Concepts/
│   ├── src/UnderstandingDependencies.Api
│   └── test/UnderstandingDependencies.Api.Tests.Unit
├── Techniques/
│   ├── src/TestingTechniques
│   └── test/TestingTechniques.Test.Unit
├── RealWorld/
│   ├── src/Users.Api
│   └── test/Users.Api.Tests.Unit
├── UnitTesting.sln
└── UnitTesting.slnx
```

## Technologies Used

| Technology / Library | Purpose |
| --- | --- |
| .NET 10 | Target framework for all class library, Web API, and test projects |
| C# | Application, API, and test implementation |
| ASP.NET Core Web API | HTTP API projects in the `Concepts` and `RealWorld` sections |
| Entity Framework Core | SQL Server data access and migration management in the repository layer |
| SQL Server LocalDB | Local development database scenario |
| xUnit | Unit testing framework |
| FluentAssertions | Readable and expressive assertion syntax |
| NSubstitute | Mocking, substitution, and behavior verification |
| Moq | Alternative mocking approach demonstrated at the concept level |
| FluentValidation | DTO validation rule modeling |
| Coverlet | Test coverage collection infrastructure |
| Microsoft.NET.Test.Sdk | .NET test runner integration |

## Sections

### Fundamentals

The `CalculatorLibrary` project demonstrates the building blocks of unit testing. It uses four basic arithmetic operations to show `Theory`, `InlineData`, expected/actual comparisons, test fixture lifecycle, and xUnit output usage.

Covered topics:

- `Fact` and `Theory` usage
- Parameterized tests
- Readable assertions with FluentAssertions
- Test setup/teardown flow with `IAsyncLifetime`
- Skipped test scenario

### Techniques

The `TestingTechniques` section is designed to practice assertions across different data types and behaviors. It includes tests for strings, numbers, dates, objects, collections, exceptions, events, and internal members.

Covered topics:

- String assertions
- Numeric assertions
- `DateOnly` validations
- Object comparisons and `BeEquivalentTo`
- Collection assertions
- Exception tests
- Event raise verification
- Testing internal members with `InternalsVisibleTo`

### Concepts

`UnderstandingDependencies.Api` is an ASP.NET Core Web API example focused on understanding dependencies. It includes a data retrieval scenario through repository and service layers. In the test project, the repository dependency is mocked so that service behavior can be verified in isolation.

Covered topics:

- Dependency abstraction
- Repository interface usage
- Service unit tests
- Isolating dependencies with NSubstitute
- Alternative examples with Moq
- EF Core and SQL Server connection concept

### RealWorld

`Users.Api` is the most comprehensive real-world scenario in this repository. It includes service, repository, controller, validation, and logging layers for user management. The purpose of this section is to test an API's business rules without depending on database or framework implementation details.

API behaviors:

- Listing users
- Retrieving a user by id
- Creating a new user
- Deleting a user
- Preventing duplicate user names
- Rejecting invalid DTO input
- Logging operations
- Logging and rethrowing exceptions

Tested scenarios:

- Empty and populated list results
- User found / not found cases
- Validation errors
- Duplicate name checks
- Successful create/delete flows
- Repository exception cases
- Logger call verification
- Controller actions returning HTTP 200 responses

## Architectural Approach

The project makes testability principles explicit:

- The controller layer is responsible for HTTP request/response handling.
- The service layer manages business rules, validation, logging, and error flows.
- The repository layer abstracts data access.
- Interfaces allow services to be tested without requiring a real database.
- Logging is abstracted through `ILoggerAdapter`, making it verifiable in tests.
- Validation rules are defined centrally and readably with `FluentValidation`.

## Test Coverage

This repository contains 47 test scenarios:

- 46 passing tests
- 1 intentionally skipped test

Latest verification command:

```bash
dotnet test UnitTesting.sln --no-restore
```

Test result:

```text
Failed: 0, Passed: 46, Skipped: 1, Total: 47
```

## Key Takeaways

- Unit testing is approached not only at the assertion level, but also at the code design level.
- Dependencies are abstracted through interfaces.
- Fast and isolated tests are written with mocks/substitutes.
- Service and controller behaviors are verified separately.
- Real-world needs such as validation, exception handling, and logging are included in the test scope.
- Test names follow the `Method_ShouldExpectedBehavior_WhenCondition` format.
