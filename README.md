# 🚀 E-Shop Microservices - Identity Provider (IDP) Service

This is the core **Identity Provider (IDP)** microservice built with **.NET 10**, leveraging **Clean Architecture**, **CQRS (MediatR)**, and modern distributed systems tools. It handles secure user authentication, dynamic OTP lifetime management via Docker Redis, and persistent relational data storage using SQL Server.

---

## 🛠️ Tech Stack & Architecture 

* **Framework:** .NET 10
* **Architecture:** Clean Architecture (Domain, Application, Infrastructure, API)
* **Pattern:** CQRS via **MediatR** (Separation of Commands and Queries)
* **Distributed Cache:** **Redis** (Hosted inside Docker Container)
* **Database:** **SQL Server** (Database-per-Service Pattern)
* **Message Broker:** **RabbitMQ** via MassTransit (Async Event-Driven Communication)
* **Mapping Tool:** AutoMapper

---

## 🔍 Key Features Implemented 

1. **Dynamic OTP Lifecycle:** Generates dynamic 4-digit verification codes, maps payload metadata, and secures data into a **Redis Hash** structure with sliding/absolute expirations.
2. **CQRS Isolation:** 
   * **Commands:** Handles dynamic registration and token generation loops.
   * **Queries:** Highly optimized read operations with Entity Framework Core (`AsNoTracking`).
3. **Robust Fault Tolerance:** Fully equipped with `Try-Catch` safe execution blocks to prevent connection drops from hiding internal database failures.
4. **Clean Infrastructure:** Integrated with `.gitignore` to prevent localized IDE temporary cache artifacts (`.vs/`, `bin/`, `obj/`) from polluting version control history.

---

## 🚀 How to Run Locally 

### 1. Run Infrastructures via Docker
Ensure your local Docker Desktop is active and start your Redis and RabbitMQ instances:
```bash
docker run -d --name redis-server -p 6379:6379 redis
docker run -d --name rabbitmq-server -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

### 2. Configure Database Connections
Update the connection targets inside `Src/Services/IDPService/IDP.Api/appsettings.json`:
```json
"ConnectionStrings": {
  "CommandDBConnection": "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=AuthDB;Integrated Security=True;TrustServerCertificate=True;"
},
"CacheSetting": {
  "RedisUrl": "localhost:6379"
},
"Rabbit": {
  "Host": "rabbitmq://localhost",
  "UserName": "guest",
  "Password": "guest"
}
```

### 3. Compile and Boot via CLI
```powershell
git restore .
dotnet build
dotnet run --project Src/Services/IDPService/IDP.Api/IDP.Api.csproj
```
Open your browser and navigate to the local hosting port to view and test endpoints inside the **Swagger UI**.
