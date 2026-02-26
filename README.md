## Apna Dhobi (Backend)
Apna dhobi backend designed with microservices architecture to break down the large application into small services. Each microservice following the clean architecture so that it would easy maintain and deploy in different server or cloud app.

### Services

- Administration 
- Core
- Infrastructure
- Gateway
- Order
- Delivery
- Payment
- Pricing
- Notification


##### Administration

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

##### Insfrastructure

This is will be act as comman or shared functionality which will be accessible to other microservices via nuget package, Infrastructure solution will include the functionalities like:

- **Email**: This service will help to send the email and work as abstract layer IEmailSender contract and the base implementation for all the microservices apps and this will provide the configuration to use different email provider so that in future if needs to changed the email provider like STMP to SendGrid or others so that it will give you the abstract layer to provider the EmailSettings class to configure with ease.
- 

### AspNetCore CLI Command
**Run tests**: dotnet test
**Run build**: dotnet build
**Run build-no-restore**: dotnet build --no-restore
**Run app**: dotnet run <project>
**Run add-package**: dotnet add package <package_name>
**Run remove-package**: dotnet remove package <package_name>