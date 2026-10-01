# IDP Service (Simple)

A minimal Identity/OTP API using .NET and Redis (work in progress).

Prerequisites
- .NET SDK
- Redis reachable by the app

Quick start
- Set Redis URL in appsettings.json under `CacheSetting:RedisUrl` (e.g. `localhost:6379`).
- Run: `dotnet run --project Src/Services/IDPService/IDP.Api/IDP.Api.csproj`

Endpoint
- POST /api/v1/auth/RegisterAndSendOtp

Notes
- OTP expiry configured via `Otp:OtpTime` in appsettings.json.
- If OTP is not persisting, verify Redis connectivity (`redis-cli ping`) and the `CacheSetting` key.
