# 📚 Library Management System API

A production-ready RESTful API for digital library management with authentication, book management, reading analytics, and payment processing.

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=.net&logoColor=white)](https://dotnet.microsoft.com/)
[![Azure](https://img.shields.io/badge/Azure-Cloud-0078D4?logo=microsoftazure&logoColor=white)](https://azure.microsoft.com/)
[![Stripe](https://img.shields.io/badge/Stripe-Payment-008CDD?logo=stripe&logoColor=white)](https://stripe.com/)
[![License](https://img.shields.io/badge/License-Proprietary-red.svg)](LICENSE)

---

## 🎯 Overview

ASP.NET Core 9.0 API with Clean Architecture, JWT authentication, bilingual support (EN/AR), Azure cloud integration, Stripe payments, and Serilog logging.

**Key Features**:
- 🔐 JWT authentication with role-based access (Admin, Staff, User)
- 📚 Complete book management with PDF upload to Azure Blob Storage
- 📊 Reading analytics and tracking
- 💳 Stripe payment integration + cash payments
- 🌍 English and Arabic localization
- 📝 Structured logging (Console, File, Azure Blob)
- ✅ FluentValidation and global error handling (RFC 7807)

---

## 📡 API Documentation

**📘 [Complete API Documentation (Postman)](https://documenter.getpostman.com/view/33498662/2sB3WqszWj)**

**27 Total Endpoints**:
- 🔐 Authentication (9): Register, Login, Email confirmation, Password reset, User blocking
- 📚 Categories (5): CRUD operations
- 📖 Books (5): CRUD with PDF upload and advanced filters
- 📊 Reading Tracking (4): User reading history and analytics
- 💳 Publisher/Checkout (4): Stripe and cash payment processing

**Postman Collection**: Import `📚 Library Management System API - Complete Collection (All 27 Endpoints).postman_collection.json` for all endpoints with examples and auto-saved JWT tokens.

---

## 🚀 Quick Start

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server (Azure or LocalDB)
- Azure Storage Account
- Stripe test account
- Gmail with App Password

### Installation

```bash
# Clone repository
git clone https://github.com/s0if/Library.git
cd Library/Library.API

# Configure appsettings.json with your credentials:
# - ConnectionStrings:DefaultConnection (SQL Server)
# - jwt:secretkey (32+ character secret)
# - AzureStorage:ConnectionString
# - Stripe:SecretKey
# - EmailSettings (Gmail SMTP)

# Apply migrations
dotnet ef database update

# Run
dotnet run
```

**API Available**: `https://localhost:7085`

---

## 🔐 Default Users

| Role  | Email              | Password   | Access                     |
|-------|--------------------|------------|----------------------------|
| Admin | `admin@saif.com`   | `@Admin12` | Full system access         |
| Staff | `staff@saif.com`   | `@Staff12` | Manage books/categories    |
| User  | `user@saif.com`    | `@User12`  | View books, make purchases |

---

## ⚙️ Configuration

**Required Settings** (`appsettings.json`):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=LibraryDB;..."
  },
  "jwt": {
    "secretkey": "your-32-character-secret-key",
    "issuer": "SaifLibrary",
    "audience": "Library",
    "DurationInDays": 30
  },
  "AzureStorage": {
    "ConnectionString": "DefaultEndpointsProtocol=https;...",
    "ContainerName": "pdf-files",
    "LogsContainerName": "logs"
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

**Security**: Use `dotnet user-secrets` for local development. Never commit credentials.

**Localization**: Set `Accept-Language: en` or `ar` header for English/Arabic responses.

---

## 📁 Project Structure

```
Library/
├── Library.API/              # Controllers, Middleware, Configuration
├── Library.APPLICATION/      # Services, DTOs, Validation, Mapping
├── Library.DOMAIN/           # Entities, Interfaces
└── Library.PERSISTENCE/      # DbContext, Repositories, Migrations
```

**Clean Architecture**: 4-layer separation with dependency inversion.

---

## 🚢 Deployment

**Build**:
```bash
dotnet publish -c Release -o ./publish
```

**Options**:
- **Azure App Service**: `az webapp up --name your-app --resource-group your-rg`
- **Docker**: Use included Dockerfile
- **IIS**: Install .NET 9.0 Hosting Bundle
- **AWS**: Elastic Beanstalk deployment

**Production Checklist**:
- Configure `appsettings.Production.json`
- Use Azure Key Vault for secrets
- Enable HTTPS and restrict CORS
- Set up Application Insights
- Apply database migrations

---

## 📄 License

**Proprietary License** - Copyright (c) 2025 Saif. All rights reserved.

**Terms Summary**:
- ✅ Use for personal, educational, or non-commercial purposes
- ✅ Study and review the source code
- ✅ Contribute via pull requests to new branches
- ❌ **Cannot** use code in your own projects without explicit attribution to Saif
- ❌ **Cannot** sell or commercialize the software
- ❌ **Cannot** redistribute or fork outside the original repository

**Violations will result in legal action.** See [LICENSE](LICENSE) for full terms.

---

## 📞 Support

- **API Docs**: [Postman Documentation](https://documenter.getpostman.com/view/33498662/2sB3WqszWj)
- **Issues**: [GitHub Issues](https://github.com/s0if/Library/issues)
- **Repository**: [github.com/s0if/Library](https://github.com/s0if/Library)

---

<div align="center">

**Made with ❤️ using ASP.NET Core 9.0**

_Last Updated: November 7, 2025_

</div>
