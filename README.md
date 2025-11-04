# 📚 Library Management System API

A comprehensive RESTful API for managing a digital library system with book management, user authentication, reading tracking, and integrated payment processing.

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=.net)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-9.0-512BD4)](https://docs.microsoft.com/en-us/aspnet/core/)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core%209.0-512BD4)](https://docs.microsoft.com/en-us/ef/core/)
[![Azure](https://img.shields.io/badge/Azure-Blob%20Storage-0078D4?logo=microsoftazure)](https://azure.microsoft.com/)
[![Postman](https://img.shields.io/badge/Postman-Collection-FF6C37?logo=postman)](https://www.postman.com/)

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Technology Stack](#-technology-stack)
- [Architecture](#-architecture)
- [Getting Started](#-getting-started)
- [API Documentation](#-api-documentation)
- [Authentication](#-authentication)
- [API Endpoints](#-api-endpoints)
- [Postman Collection](#-postman-collection)
- [Configuration](#-configuration)
- [Database](#-database)
- [External Services](#-external-services)
- [Testing](#-testing)
- [Localization](#-localization)
- [Project Structure](#-project-structure)
- [Contributing](#-contributing)
- [License](#-license)

---

## 🎯 Overview

The Library Management System API is a modern, feature-rich RESTful API built with ASP.NET Core 9.0. It provides comprehensive functionality for managing a digital library, including user authentication, book catalog management, reading activity tracking, and integrated payment processing via Stripe.

### Key Highlights

- **Clean Architecture** - Organized in 4 distinct layers (API, Application, Domain, Persistence)
- **JWT Authentication** - Secure token-based authentication with role-based access control
- **Bilingual Support** - Full support for English and Arabic languages
- **Azure Cloud Storage** - PDF book storage using Azure Blob Storage
- **Payment Integration** - Stripe payment gateway for book purchases
- **Email Notifications** - Automated email system for confirmations and password resets

---

## ✨ Features

### 🔐 User Management
- User registration with email confirmation
- Secure login with JWT token generation
- Password change and reset functionality
- Email verification system
- User blocking/unblocking (Admin only)
- Role-based access control (Admin, Staff, User)

### 📖 Book Management
- Complete CRUD operations for books
- PDF file upload to Azure Blob Storage
- Advanced filtering (by title, author, ISBN, date, price range)
- Category-based organization
- Public browsing with protected modifications

### 📚 Category Management
- Create, read, update, delete categories
- Public access for viewing
- Protected operations for staff/admin

### 📊 Reading Tracking
- Track user reading activity
- View reading history by user
- Analyze book popularity statistics
- Admin/Staff can assign reading to users

### 💳 Purchase System
- Stripe payment integration
- Cash payment option
- Purchase history tracking
- Multi-book transactions

### 🌍 Internationalization
- English (en) and Arabic (ar) support
- Localized responses based on Accept-Language header
- Bilingual error messages

---

## 🛠 Technology Stack

### Backend Framework
- **ASP.NET Core 9.0** - Modern web framework
- **C# 12** - Programming language
- **Entity Framework Core 9.0** - ORM for database operations

### Database
- **SQL Server** - Primary database
- **Entity Framework Core** - Code-first migrations

### Authentication & Security
- **ASP.NET Core Identity** - User management
- **JWT Bearer Tokens** - Stateless authentication
- **BCrypt** - Password hashing

### External Services
- **Azure Blob Storage** - Cloud storage for PDF files
- **Stripe** - Payment processing gateway
- **Gmail SMTP** - Email service

### Architecture Patterns
- **Clean Architecture** - Separation of concerns
- **Repository Pattern** - Data access abstraction
- **Use Case Pattern** - Business logic encapsulation
- **DTO Pattern** - Data transfer optimization

---

## 🏗 Architecture

The project follows Clean Architecture principles with 4 distinct layers:

```
Library.sln
│
├── Library.API              # Presentation Layer
│   ├── Controllers          # API endpoints
│   ├── Middleware           # Request/response handling
│   └── Program.cs           # Application entry point
│
├── Library.APPLICATION      # Application Layer
│   ├── UseCase              # Business logic
│   ├── DTO                  # Data transfer objects
│   └── Services             # Application services
│
├── Library.DOMAIN           # Domain Layer
│   ├── Entity               # Domain entities
│   ├── Interface            # Repository contracts
│   └── Enum                 # Domain enumerations
│
└── Library.PERSISTENCE      # Infrastructure Layer
    ├── Repository           # Data access implementations
    ├── Migrations           # Database migrations
    └── ApplicationDbContext # Database context
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB or Express)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [Postman](https://www.postman.com/downloads/) (for API testing)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/library-management-api.git
   cd library-management-api
   ```

2. **Configure the database connection**

   Update `appsettings.json` in `Library.API` project:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LibraryDB;Trusted_Connection=true;TrustServerCertificate=true"
     }
   }
   ```

3. **Configure external services**

   Add your credentials to `appsettings.json`:
   ```json
   {
     "AzureStorage": {
       "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=your-account-name;AccountKey=your-account-key;EndpointSuffix=core.windows.net",
       "ContainerName": "books"
     },
     "Stripe": {
       "SecretKey": "your-stripe-secret-key",
       "PublishableKey": "your-stripe-publishable-key"
     },
     "EmailSettings": {
       "Host": "smtp.gmail.com",
       "Port": 587,
       "Username": "your-email@gmail.com",
       "Password": "your-app-password"
     }
   }
   ```

4. **Apply database migrations**
   ```bash
   cd Library.API
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access the API**

   The API will be available at: `https://localhost:7085`

---

## 📚 API Documentation

### Complete Documentation Files

The project includes comprehensive documentation:

- **`Library-API-Collection-COMPLETE.json`** - Postman collection with all 27 endpoints
- **`Library-API-Environment.json`** - Postman environment variables
- **`API-Documentation-Complete.md`** - Full markdown documentation
- **`README-API-Documentation.md`** - Quick start guide
- **`POSTMAN-COLLECTION-SUMMARY.md`** - Detailed endpoint summary

### Base URL

```
https://localhost:7085
```

### API Versioning

Currently using implicit versioning. All endpoints are accessed directly from the base URL.

---

## 🔐 Authentication

### JWT Token Authentication

The API uses JWT Bearer tokens for authentication. After successful login, include the token in all protected requests:

```http
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

### Getting a Token

**Endpoint:** `POST /Auth/Login`

**Request:**
```json
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

### Default User Accounts

The system comes pre-seeded with three user accounts:

| Role | Email | Password | Permissions |
|------|-------|----------|-------------|
| **Admin** | admin@saif.com | @Admin12 | Full system access + user management |
| **Staff** | staff@saif.com | @Staff12 | Book/category management + analytics |
| **User** | user@saif.com | @User12 | Read books + make purchases |

### Role-Based Access Control

- **Public** - View books and categories (no authentication required)
- **User** - All public + start reading + purchase books
- **Staff** - All User + manage books/categories + view statistics
- **Admin** - All Staff + user management + block/unblock users

---

## 📡 API Endpoints

### Summary (27 Total Endpoints)

#### 🔐 Authentication (9 endpoints)
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| POST | `/Auth/Register` | Register new user | No |
| POST | `/Auth/Login` | Login user | No |
| GET | `/Auth/ConfirmEmail` | Confirm email address | No |
| PUT | `/Auth/ChangePassword` | Change password | Yes |
| POST | `/Auth/GenerateRestPassword` | Request password reset | No |
| GET | `/Auth/ResetPassword` | Get reset form | No |
| POST | `/Auth/ResetPassword` | Submit new password | No |
| POST | `/Auth/ISBlock/:ID` | Block user | Admin |
| POST | `/Auth/UNBlock/:ID` | Unblock user | Admin |

#### 📚 Categories (5 endpoints)
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | `/Category` | Get all categories | No |
| GET | `/Category/ById/:Id` | Get category by ID | No |
| POST | `/Category/Add` | Create category | Staff/Admin |
| PUT | `/Category/Update/:Id` | Update category | Staff/Admin |
| DELETE | `/Category/Delete/:Id` | Delete category | Staff/Admin |

#### 📖 Books (5 endpoints)
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | `/Book` | Get all books (with filters) | No |
| GET | `/Book/ById/:Id` | Get book by ID | No |
| POST | `/Book/Add` | Add book with PDF | Staff/Admin |
| PUT | `/Book/Update/:Id` | Update book | Staff/Admin |
| DELETE | `/Book/Delete/:Id` | Delete book | Staff/Admin |

#### 📊 Reading Tracking (4 endpoints)
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| POST | `/Read/start/:BookId` | Start reading | User |
| POST | `/Read/start/:UserId/:BookId` | Start reading for user | Staff/Admin |
| GET | `/Read/ByBooks/:BookId` | Get readers by book | Staff/Admin |
| GET | `/Read/ByUser/:UserId` | Get books by user | Staff/Admin |

#### 💳 Publisher/Checkout (4 endpoints)
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| POST | `/Publisher/Add` | Create purchase | User |
| GET | `/Publisher/Get` | Get all purchases | No |
| GET | `/Publisher/GetById/:Id` | Get purchase by ID | No |
| DELETE | `/Publisher/Delete/:Id` | Delete purchase | Staff/Admin |

### Advanced Book Filtering

The `GET /Book` endpoint supports multiple filters:

```http
GET /Book?Title=Clean&Author=Martin&LowestPrice=20&HighestPrice=100
```

**Available Filters:**
- `Title` - Partial match on book title
- `Author` - Partial match on author name
- `PublishedDate` - Exact date match (YYYY-MM-DD)
- `ISBN` - Partial match on ISBN
- `LowestPrice` - Minimum price filter
- `HighestPrice` - Maximum price filter

---

## 📮 Postman Collection

### Import Collection

1. **Open Postman**
2. Click **"Import"**
3. Select both files:
   - `Library-API-Collection-COMPLETE.json`
   - `Library-API-Environment.json`
4. Click **"Import"**

### Select Environment

1. Click environment dropdown (top right)
2. Select **"Library API Environment"**

### Test the API

1. Open **"🔐 Authentication"** folder
2. Run **"🔐 Login - Authenticate User"**
3. Token auto-saves to `{{token}}` variable
4. Test any endpoint!

### Collection Features

- ✅ All 27 endpoints documented
- ✅ Professional format with emojis
- ✅ Auto-save token on login
- ✅ Pre-configured authentication
- ✅ Request/response examples
- ✅ Error scenarios included
- ✅ Usage instructions
- ✅ Field descriptions

---

## ⚙️ Configuration

### appsettings.json Structure

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=LibraryDB;..."
  },
  "Jwt": {
    "Key": "your-super-secret-key-minimum-32-characters",
    "Issuer": "LibraryAPI",
    "Audience": "LibraryAPIUsers",
    "DurationInMinutes": 60
  },
  "AzureStorage": {
    "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=your-account-name;AccountKey=your-account-key;EndpointSuffix=core.windows.net",
    "ContainerName": "books"
  },
  "Stripe": {
    "SecretKey": "sk_test_...",
    "PublishableKey": "pk_test_..."
  },
  "EmailSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "Username": "your-email@gmail.com",
    "Password": "your-app-password",
    "From": "Library System <noreply@library.com>"
  }
}
```

### Environment Variables

You can also use environment variables for sensitive data:

```bash
export ConnectionStrings__DefaultConnection="Server=..."
export Jwt__Key="your-secret-key"
export AzureStorage__ConnectionString="DefaultEndpointsProtocol=https;..."
export Stripe__SecretKey="sk_test_..."
```

---

## 🗄️ Database

### Schema Overview

**Main Tables:**
- `AspNetUsers` - User accounts
- `AspNetRoles` - User roles (Admin, Staff, User)
- `Categories` - Book categories
- `Books` - Book catalog
- `Reads` - Reading activity tracking
- `Publishers` - Purchase transactions
- `BookPublishers` - Many-to-many relationship

### Migrations

**Create new migration:**
```bash
dotnet ef migrations add MigrationName --project Library.PERSISTENCE --startup-project Library.API
```

**Update database:**
```bash
dotnet ef database update --project Library.PERSISTENCE --startup-project Library.API
```

**Remove last migration:**
```bash
dotnet ef migrations remove --project Library.PERSISTENCE --startup-project Library.API
```

### Seed Data

The database is automatically seeded with:
- 3 user accounts (Admin, Staff, User)
- 5 book categories (Fiction, Science, Technology, Biography, History)
- Sample books (if configured)

---

## 🌐 External Services

### Azure Blob Storage Setup

1. **Create an Azure account** at [azure.microsoft.com](https://azure.microsoft.com/)
2. **Create a Storage Account:**
   - Navigate to Azure Portal
   - Click "Create a resource" → "Storage account"
   - Choose a unique storage account name
   - Select your region and performance tier (Standard/Premium)
   - Click "Review + create"

3. **Create a Blob Container:**
   - Open your Storage Account
   - Go to "Containers" under Data storage
   - Click "+ Container"
   - Name it "books" (or your preferred name)
   - Set access level to "Private"
   - Click "Create"

4. **Get Connection String:**
   - In your Storage Account, go to "Access keys"
   - Copy the "Connection string" value
   - Add it to your `appsettings.json`

5. **Configure in application:**
   ```json
   {
     "AzureStorage": {
       "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=youraccount;AccountKey=yourkey;EndpointSuffix=core.windows.net",
       "ContainerName": "books"
     }
   }
   ```

**Used for:**
- PDF book file storage
- Secure file access with SAS tokens
- Scalable cloud storage (up to 5 PB)
- 99.9% availability SLA
- Automatic redundancy and backups
- Global content delivery with Azure CDN (optional)

### Stripe Setup

1. Create account at [stripe.com](https://stripe.com/)
2. Get your test keys from Dashboard
3. Add to `appsettings.json`

**Used for:**
- Credit card payment processing
- Checkout session creation
- Payment webhooks (optional)

### Gmail SMTP Setup

1. Enable 2-Factor Authentication on Gmail
2. Generate App Password
3. Add credentials to `appsettings.json`

**Used for:**
- Registration confirmation emails
- Password reset emails
- Email notifications

---

## 🧪 Testing

### Manual Testing with Postman

1. Import the Postman collection
2. Select the environment
3. Run the **Login** endpoint
4. Test endpoints in order:
   - Public endpoints (no auth)
   - User endpoints (with token)
   - Staff/Admin endpoints (with appropriate role)

### Test Different Roles

```bash
# Test as Admin
Login: admin@saif.com / @Admin12
Can: All operations

# Test as Staff
Login: staff@saif.com / @Staff12
Can: Manage books/categories, view analytics

# Test as User
Login: user@saif.com / @User12
Can: Read books, make purchases
```

### Test Workflows

**Registration Flow:**
```
1. POST /Auth/Register
2. Check email for confirmation link
3. GET /Auth/ConfirmEmail (from email link)
4. POST /Auth/Login
5. Use token for protected endpoints
```

**Purchase Flow (Stripe):**
```
1. GET /Book (browse books)
2. POST /Publisher/Add (with Stripe payment method)
3. Redirect to Stripe checkout URL
4. Complete payment
5. User redirected back to app
```

---

## 🌍 Localization

### Supported Languages

- **English (en)** - Default
- **Arabic (ar)** - Full RTL support

### Setting Language

Add the `Accept-Language` header to any request:

```http
Accept-Language: en
```

or

```http
Accept-Language: ar
```

### Localized Responses

**English:**
```json
{
  "Message": "Book added successfully"
}
```

**Arabic:**
```json
{
  "Message": "تمت إضافة الكتاب بنجاح"
}
```

### In Postman

Use the environment variable:
```
{{Lang}} = en  (or ar)
```

---

## 📁 Project Structure

```
Library/
│
├── Library.API/                          # Presentation Layer
│   ├── Controllers/
│   │   ├── AuthController.cs           # Authentication endpoints
│   │   ├── BookController.cs           # Book management
│   │   ├── CategoryController.cs       # Category management
│   │   ├── ReadController.cs           # Reading tracking
│   │   └── PublisherController.cs      # Purchase/checkout
│   ├── Middleware/
│   ├── Program.cs                      # Application startup
│   └── appsettings.json                # Configuration
│
├── Library.APPLICATION/                 # Application Layer
│   ├── UseCase/
│   │   ├── Book/
│   │   ├── Category/
│   │   └── Read/
│   ├── DTO/
│   └── Services/
│
├── Library.DOMAIN/                      # Domain Layer
│   ├── Entity/
│   │   ├── ApplicationUser.cs
│   │   ├── Book.cs
│   │   ├── Category.cs
│   │   ├── Read.cs
│   │   └── Publisher.cs
│   ├── Interface/
│   └── Enum/
│
├── Library.PERSISTENCE/                 # Infrastructure Layer
│   ├── Repository/
│   │   ├── GeneralsRepository.cs       # Generic repository
│   │   ├── BookRepository.cs
│   │   ├── CategoryRepository.cs
│   │   └── ...
│   ├── Migrations/                     # EF Core migrations
│   ├── Utils/
│   │   └── SeedData.cs                # Database seeding
│   └── ApplicationDbContext.cs
│
├── Documentation/
│   ├── Library-API-Collection-COMPLETE.json
│   ├── Library-API-Environment.json
│   ├── API-Documentation-Complete.md
│   └── README-API-Documentation.md
│
└── Library.sln                          # Solution file
```

---

## 🔒 Security Best Practices

### Implemented Security Measures

- ✅ JWT token authentication
- ✅ Password hashing with BCrypt
- ✅ Email confirmation required
- ✅ Role-based authorization
- ✅ HTTPS enforcement
- ✅ CORS configuration
- ✅ Input validation
- ✅ SQL injection prevention (EF Core)
- ✅ XSS protection

### Recommendations for Production

1. **Use strong JWT secret keys** (32+ characters)
2. **Enable HTTPS only** in production
3. **Use environment variables** for secrets
4. **Implement rate limiting** to prevent abuse
5. **Add request logging** for audit trails
6. **Enable CORS** for specific origins only
7. **Use API keys** for external services
8. **Implement refresh tokens** for long-lived sessions
9. **Add input sanitization** for user content
10. **Regular security audits** and updates

---

## 🚀 Deployment

### Prerequisites for Production

- SQL Server database
- HTTPS certificate
- Azure Storage Account (production tier)
- Stripe account (live keys)
- SMTP service (production email)

### Steps

1. **Build the application**
   ```bash
   dotnet publish -c Release -o ./publish
   ```

2. **Update production configuration**
   - Use production connection strings
   - Use production API keys
   - Enable HTTPS enforcement
   - Configure CORS for your domain

3. **Deploy to hosting service**
   - Azure App Service
   - AWS Elastic Beanstalk
   - IIS on Windows Server
   - Linux with Nginx/Apache

4. **Run database migrations**
   ```bash
   dotnet ef database update --connection "ProductionConnectionString"
   ```

5. **Verify deployment**
   - Test all endpoints
   - Check logs
   - Monitor performance

---

## 📊 API Response Format

### Success Response

```json
{
  "Message": "Operation successful",
  "Data": {
    // Response data
  }
}
```

### Error Response

```json
{
  "message": "Error description",
  "errors": [
    "Validation error 1",
    "Validation error 2"
  ]
}
```

### HTTP Status Codes

- `200 OK` - Request successful
- `201 Created` - Resource created
- `400 Bad Request` - Invalid input
- `401 Unauthorized` - Missing/invalid token
- `403 Forbidden` - Insufficient permissions
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server error

---

## 🤝 Contributing

### How to Contribute

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Coding Standards

- Follow C# naming conventions
- Use async/await for I/O operations
- Write XML documentation for public APIs
- Add unit tests for new features
- Update documentation

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 👨‍💻 Author

**Your Name**
- GitHub: [@yourusername](https://github.com/yourusername)
- Email: your.email@example.com

---

## 🙏 Acknowledgments

- ASP.NET Core team for the excellent framework
- Entity Framework Core team
- Stripe for payment processing
- Microsoft Azure for cloud storage
- Community contributors

---

## 📞 Support

For questions and support:

- **Documentation:** See `API-Documentation-Complete.md`
- **Issues:** [GitHub Issues](https://github.com/yourusername/library-management-api/issues)
- **Email:** support@yourlibrary.com

---

## 🗺️ Roadmap

### Planned Features

- [ ] Book recommendations based on reading history
- [ ] Book reviews and ratings
- [ ] Advanced search with Elasticsearch
- [ ] Real-time notifications with SignalR
- [ ] Book reservation system
- [ ] Multi-currency support
- [ ] Subscription plans
- [ ] Mobile app integration
- [ ] GraphQL API endpoint
- [ ] Microservices architecture

---

## 📝 Changelog

### Version 1.0.0 (Current)

- ✅ Complete authentication system
- ✅ Book management with PDF upload
- ✅ Category management
- ✅ Reading tracking
- ✅ Stripe payment integration
- ✅ Bilingual support (EN/AR)
- ✅ Complete Postman collection
- ✅ Comprehensive documentation

---

**Made with ❤️ using ASP.NET Core**

---

*Last Updated: November 4, 2025*
