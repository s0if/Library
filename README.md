# 📚 Library Management System API

A comprehensive RESTful API for managing a digital library with book management, user authentication, reading tracking, and integrated payment processing.

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=.net)](https://dotnet.microsoft.com/)
[![Azure](https://img.shields.io/badge/Azure-Cloud-0078D4?logo=microsoftazure)](https://azure.microsoft.com/)

---

## 🎯 Overview

Modern ASP.NET Core 9.0 API with Clean Architecture, JWT authentication, bilingual support (EN/AR), Azure cloud storage, Stripe payments, and structured logging with Serilog.

### Key Features

- **Clean Architecture** - 4-layer separation (API, Application, Domain, Persistence)
- **JWT Authentication** - Role-based access control (Admin, Staff, User)
- **Bilingual Support** - English and Arabic with request localization
- **Azure Integration** - Blob Storage for PDFs and logs, Azure SQL Server
- **Stripe Payments** - Integrated payment gateway
- **Structured Logging** - Serilog (Console, File, Azure Blob Storage)
- **Global Error Handling** - RFC 7807 Problem Details format
- **FluentValidation** - Input validation

---

## 🛠 Technology Stack

- **ASP.NET Core 9.0** with Entity Framework Core
- **Azure SQL Server** - Cloud database
- **Azure Blob Storage** - PDF files (`pdf-files` container) & logs (`logs` container)
- **Serilog** - Structured logging with multiple sinks
- **JWT + Identity** - Authentication and authorization
- **Stripe** - Payment processing
- **Gmail SMTP** - Email notifications

---

## 🚀 Quick Start

### Prerequisites

- .NET 9.0 SDK
- SQL Server (Azure/LocalDB)
- Azure Storage Account
- Stripe account (test keys)

### Installation

1. **Clone repository**

   ```bash
   git clone https://github.com/s0if/Library.git
   cd library-management-api
   ```

2. **Configure `appsettings.json`**

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=tcp:your-server.database.windows.net,1433;..."
     },
     "jwt": {
       "secretkey": "your-32-character-secret-key",
       "issuer": "SaifLibrary",
       "audience": "Library"
     },
     "AzureStorage": {
       "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=...",
       "ContainerName": "pdf-files"
     },
     "Stripe": {
       "SecretKey": "sk_test_..."
     },
     "EmailSettings": {
       "SmtpServer": "smtp.gmail.com",
       "SmtpPort": 587,
       "SenderEmail": "your-email@gmail.com",
       "senderToken": "your-gmail-app-password"
     }
   }
   ```

3. **Apply migrations & run**

   ```bash
   cd Library.API
   dotnet ef database update
   dotnet run
   ```

4. **Access API**: `https://localhost:7085`

---

## 🔐 Authentication

### Default Users (Seeded)

| Role  | Email          | Password | Permissions             |
| ----- | -------------- | -------- | ----------------------- |
| Admin | admin@saif.com | @Admin12 | Full system access      |
| Staff | staff@saif.com | @Staff12 | Manage books/categories |
| User  | user@saif.com  | @User12  | Read & purchase books   |

### Getting Token

```http
POST /Auth/Login
Content-Type: application/json

{
  "email": "admin@saif.com",
  "password": "@Admin12"
}
```

**Response:**

```json
{
  "Message": "Login successful",
  "Token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

Use token in requests: `Authorization: Bearer {token}`

---

## 📡 API Endpoints (27 Total)

### 🔐 Authentication (9)

- `POST /Auth/Register` - Register new user
- `POST /Auth/Login` - Login & get JWT token
- `GET /Auth/ConfirmEmail` - Confirm email
- `PUT /Auth/ChangePassword` - Change password
- `POST /Auth/GenerateRestPassword` - Request password reset
- `POST /Auth/ResetPassword` - Reset password
- `POST /Auth/ISBlock/:ID` - Block user (Admin)
- `POST /Auth/UNBlock/:ID` - Unblock user (Admin)

### 📚 Categories (5)

- `GET /Category` - List all (Public)
- `GET /Category/ById/:Id` - Get by ID (Public)
- `POST /Category/Add` - Create (Staff/Admin)
- `PUT /Category/Update/:Id` - Update (Staff/Admin)
- `DELETE /Category/Delete/:Id` - Delete (Staff/Admin)

### 📖 Books (5)

- `GET /Book` - List with filters (Public)
- `GET /Book/ById/:Id` - Get by ID (Public)
- `POST /Book/Add` - Add with PDF upload (Staff/Admin)
- `PUT /Book/Update/:Id` - Update (Staff/Admin)
- `DELETE /Book/Delete/:Id` - Delete (Staff/Admin)

**Filters**: Title, Author, ISBN, PublishedDate, LowestPrice, HighestPrice

### 📊 Reading Tracking (4)

- `POST /Read/start/:BookId` - Start reading (User)
- `POST /Read/start/:UserId/:BookId` - Assign reading (Staff/Admin)
- `GET /Read/ByBooks/:BookId` - Get readers (Staff/Admin)
- `GET /Read/ByUser/:UserId` - Get user's reads (Staff/Admin)

### 💳 Publisher/Checkout (4)

- `POST /Publisher/Add` - Create purchase (User)
- `GET /Publisher/Get` - List all purchases (Public)
- `GET /Publisher/GetById/:Id` - Get purchase (Public)
- `DELETE /Publisher/Delete/:Id` - Delete purchase (Staff/Admin)

---

## 📮 Postman Collection

Import `📚 Library Management System API - Complete Collection (All 27 Endpoints).postman_collection.json` for:

- All 27 endpoints with examples
- Auto-save JWT token on login
- Pre-configured requests
- Error scenarios

---

## ⚙️ Configuration

### Serilog Logging

Logs are written to:

- **Console** (Development)
- **Local Files** (`Logs/log-YYYYMMDD.json`, 30-day retention, 10MB max)
- **Azure Blob Storage** (`logs/{yyyy}/{MM}/{dd}/log-{HH}.Json`)

All errors include `traceId` for correlation across logs.

### Azure Blob Storage Setup

1. Create Storage Account in Azure Portal
2. Create two containers:
   - `pdf-files` (Private) - For book PDFs
   - `logs` (Private) - For application logs
3. Copy connection string from Access Keys
4. Add to `appsettings.json` and Serilog configuration

### Gmail SMTP

1. Enable 2-Factor Authentication
2. Generate App Password
3. Use app password (not account password) in `senderToken`

---

## 📊 Error Response Format (RFC 7807)

All errors return standardized Problem Details format:

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "Validation Error",
  "status": 400,
  "detail": "Email is required",
  "traceId": "00-abc123...",
  "errors": {
    "Email": ["Email is required"]
  }
}
```

**Status Codes**: 200 (OK), 400 (Bad Request), 401 (Unauthorized), 403 (Forbidden), 404 (Not Found), 500 (Server Error)

---

## 🌍 Localization

Set language with `Accept-Language` header:

- `en` - English (default)
- `ar` - Arabic

All responses (success/error messages) are localized.

---

## 📁 Project Structure

```
Library/
├── Library.API/                          # Controllers, Middleware, Configuration
├── Library.APPLICATION/                 # Use Cases, DTOs, Services, Mapping
├── Library.DOMAIN/                      # Entities, Interfaces
└── Library.PERSISTENCE/                 # Repositories, DbContext, Migrations
```

---

## 🔒 Security Features

- JWT Bearer authentication
- Password hashing (BCrypt)
- Email confirmation required
- Role-based authorization
- HTTPS enforcement
- CORS configuration
- Input validation (FluentValidation)
- Global exception handling

---

## 📝 Database Schema

**Main Tables**: AspNetUsers, AspNetRoles, Books, Categories, Reads, Publishers, BookPublishers

**Migrations**:

```bash
# Create migration
dotnet ef migrations add MigrationName --project Library.PERSISTENCE --startup-project Library.API

# Apply migration
dotnet ef database update --project Library.PERSISTENCE --startup-project Library.API
```

---

## 🚢 Deployment

1. **Build**: `dotnet publish -c Release -o ./publish`
2. **Update production config** (connection strings, API keys)
3. **Deploy to**: Azure App Service, AWS, IIS, or Docker
4. **Apply migrations**: `dotnet ef database update`
5. **Verify**: Test endpoints and check logs

---

## 📄 License

MIT License - see LICENSE file for details.

---

## 📞 Support

- **Documentation**: See Postman collection
- **Issues**: [GitHub Issues](https://github.com/s0if/Library.git)

---

**Made with ❤️ using ASP.NET Core 9.0**

_Last Updated: November 5, 2025_
