# 🚀 NetCoreMicroService - Identity Provider (IDP) Service

This is a modern, high-performance **Identity Provider (IDP)** microservice built with **.NET 10**, leveraging **Clean Architecture**, **CQRS (MediatR)**, and event-driven microservices patterns. It handles secure user authentication, dynamic OTP lifetime management via Docker Redis, and transactional operations using SQL Server.



---

## 🛠️ Tech Stack & Architecture 

* **Framework:** .NET 10 (ASP.NET Core Web API)
* **Architecture:** Clean Architecture (Domain, Application, Infrastructure, API, IoC)
* **Pattern:** CQRS via **MediatR** (Separation of Commands and Queries)
* **Distributed Cache:** **Redis** Hash Storage (Hosted inside Docker Container)
* **Database:** **SQL Server** (Database-per-Service Pattern)
* **Message Broker:** **RabbitMQ** via MassTransit (Async Event-Driven Communication)
* **Mapping Tool:** AutoMapper

---

## Solution Structure

```text
NetCoreMicroService/
├── Eshop.slnx
├── .gitignore
├── EventBus/
│   ├── BaseEvent/
│   ├── Events/
│   └── EventBus.csproj
├── Src/
│   ├── Common/
│   │   └── Auth/
│   └── Services/
│       ├── IDPService/
│       │   ├── IDP.Api/
│       │   ├── IDP.Application/
│       │   ├── IDP.Domain/
│       │   ├── IDP.Infra/
│       │   ├── IDP.Ioc/
│       │   └── IDP.UnitTest/
│       └── NotificationService/
```

---

## 🔍 Key Features Implemented 

- **Dynamic OTP Lifecycle:** Generates dynamic 4-digit verification codes, mapping payload metadata into a secure **Redis Hash** structure with sliding/absolute expirations linked to the user's mobile number.
- **CQRS Isolation:** 
  - **Commands:** Handles dynamic registration and state write mutations.
  - **Queries:** Highly optimized read operations with Entity Framework Core (`AsNoTracking`).
- **Transactional Outbox Readiness:** Designed around the Outbox pattern to prevent dual-write anomalies and guarantee message delivery to message channels.
- **Idempotent Consumer:** Prepared framework handles within the notification layer to suppress duplicate event handling.
- **Token Security:** Secure, fast JWT generation and bearer authentication.

---

## 🚀 How to Run Locally 

### 1. Run Infrastructures via Docker
Ensure your local Docker Desktop is active and start your Redis and RabbitMQ container instances:
```bash
docker run -d --name redis-server -p 6379:6379 redis
docker run -d --name rabbitmq-server -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

### 2. Configure Connections (`appsettings.json`)
Verify target port configurations inside `Src/Services/IDPService/IDP.Api/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "CommandDBConnection": "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=AuthDB;Integrated Security=True;TrustServerCertificate=True;",
    "QueryDBConnection": "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=AuthDB;Integrated Security=True;TrustServerCertificate=True;"
  },
  "CacheSetting": {
    "RedisUrl": "localhost:6379"
  },
  "Rabbit": {
    "Host": "rabbitmq://localhost",
    "UserName": "guest",
    "Password": "guest"
  }
}
```

### 3. Build & Run
Execute the following commands in your PowerShell console to purge previous artifacts and boot the hosting container:
```powershell
git add .
dotnet build
dotnet run --project Src/Services/IDPService/IDP.Api/IDP.Api.csproj
```
Open Swagger in your browser to start debugging endpoints:
```text
http://localhost:5131/swagger
```

---

## 📝 API Endpoints Logged

- `POST /api/v1/Auth/RegisterAndSendOtp` - Triggers registration, dynamic encryption, and pushes keys to Redis.
- `POST /api/v1/Auth/Login` - Queries identity state, validates OTP signatures, and signs JWT blocks.
