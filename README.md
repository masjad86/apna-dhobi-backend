## Apna Dhobi (Backend)
Apna dhobi backend designed with microservices architecture to break down the large application into small services. Each microservice following the clean architecture so that it would easy maintain and deploy in different server or cloud app.

### Services

- Core
- Administration
- Gateway
- Order
- Delivery
- Payment
- Pricing
- Notification

##### Core

This application will be the core library which will contains the domain entity, object, value-type and infrastructure library and helper classes which will be referenced into multiple microservices using the nuget package.

**Core Projects**
- Core.Domain
- Core.Infrastructure

##### Gateway

A Gateway API is a single entry point that sits in front of multiple backend services (microservices) and handles cross‑cutting concerns before requests reach those services.

- Gateway.Api
- Gateway.Application
- Gateway.Domain
- Gateway.Infrastructure


### AspNetCore CLI Command
**Run tests**: dotnet test
**Run build**: dotnet build
**Run build-no-restore: dotnet build --no-restore
**Run app: dotnet run <project>
**Run add-package: dotnet add package <package_name>
**Run remove-package: dotnet remove package <package_name>

