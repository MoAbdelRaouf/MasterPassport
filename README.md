# Master Passport - User Registration System

## Overview

Master Passport is a centralized user registration and management system designed to serve as a central database for users across multiple information systems and applications within an organization. The system implements a two-stage registration process with administrator approval before user data is permanently stored.

## Technology Stack

- **.NET 9.0** - Latest version of the .NET framework
- **ASP.NET Core Web API** - RESTful API for backend services
- **ASP.NET Core MVC 9** - Web application with server-side rendering
- **SQL Server** - Relational database management system
- **Entity Framework Core** - Object-relational mapping (ORM)
- **Clean Architecture** - Separation of concerns and dependency inversion
- **JWT Authentication** - Secure token-based authentication
- **Bootstrap/Tailwind CSS** - Modern, responsive UI framework

## Architecture

The solution follows Clean Architecture principles with clear separation of concerns:

### 1. Domain Layer (`MasterPassport.Domain`)
Contains enterprise-wide business rules and entities:
- **Entities**: Core business objects (User, TempUser, WorkPlace, etc.)
- **Interfaces**: Repository and service contracts
- **Value Objects**: Immutable objects representing domain concepts

### 2. Application Layer (`MasterPassport.Application`)
Orchestrates domain objects for application-specific tasks:
- **DTOs**: Data Transfer Objects for API communication
- **Interfaces**: Service contracts for business operations
- **Features**: Organized by use cases (Registration, Authentication, etc.)

### 3. Infrastructure Layer (`MasterPassport.Infrastructure`)
Handles external concerns:
- **Persistence**: Database context, repositories, and configurations
- **Services**: Password hashing, JWT token generation
- **Identity**: Authentication and authorization implementation

### 4. Presentation Layer
- **Web API** (`MasterPassport.WebAPI`): RESTful endpoints for client applications
- **Web MVC** (`MasterPassport.WebMVC`): User interface for registration and administration

## Database Schema

The database consists of three main categories of tables:

### Temporary Tables
User data is initially stored in temporary tables during registration:
- `TempUsers` - Personal and contact information
- `TempWorkPlaces` - Employment history
- `TempScientificDegrees` - Educational qualifications
- `TempUserExperienceFields` - Areas of expertise
- `TempUserLanguages` - Language proficiency
- `TempUserAttachments` - Supporting documents
- `TempUserCredentials` - Login credentials

### Permanent Tables
After administrator approval, data is moved to permanent tables:
- `Users` - Approved user information
- `WorkPlaces` - Verified employment history
- `ScientificDegrees` - Verified educational qualifications
- `UserExperienceFields` - Confirmed areas of expertise
- `UserLanguages` - Confirmed language proficiency
- `UserAttachments` - Approved supporting documents
- `UserCredentials` - Active login credentials

### Lookup Tables
Reference data for dropdowns and validation:
- `Countries`, `Governorates`, `Cities` - Geographic hierarchy
- `Genders`, `Titles` - Personal information options
- `Universities`, `Colleges` - Educational institutions
- `WorkPlaceTypes` - Types of employment
- `QualificationTypes`, `QualificationMajor`, `QualificationMinor` - Educational qualifications
- `ExperienceCategory`, `ExperienceField` - Areas of expertise (hierarchical)
- `Language`, `ProficiencyLevel` - Language skills
- `AttachmentTypes` - Document categories
- `Roles` - User roles for authorization

## Registration Process

The user registration is divided into multiple steps:

1. **Personal Information**
   - Nationality (Egyptian or Foreign)
   - National ID (for Egyptians) or Passport Number (for foreigners)
   - Title, Full Name (Arabic and English)
   - Date of Birth, Gender
   - Personal Photo

2. **Contact Information**
   - Address
   - City (selected via Country → Governorate → City hierarchy)
   - Email Address
   - Mobile Number (primary and alternative)

3. **Workplace Information**
   - Multiple workplaces can be added
   - Workplace Type, Job Title, Workplace Name
   - Start Date, End Date (optional for current position)
   - City
   - University and College (conditional, for higher education workplaces)

4. **Educational Qualifications**
   - Multiple degrees can be added
   - At least one non-postgraduate qualification required
   - Qualification Type, University, College
   - Major and Minor specialization
   - Graduation Date, Grade

5. **Experience Fields**
   - Multiple fields can be selected
   - Hierarchical selection: Category → Field

6. **Languages**
   - Multiple languages can be added
   - Language and Proficiency Level
   - Option to specify "Other" language

7. **Attachments**
   - Upload supporting documents
   - Attachment Type, File Name, File Path

8. **Login Credentials**
   - Username (typically the email address)
   - Password (hashed with unique salt)

## Getting Started

### Prerequisites

- .NET 9.0 SDK
- SQL Server 2014 or later
- Visual Studio 2022 (recommended) or Visual Studio Code

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/MoAbdelRaouf/MasterPassport.git
   cd MasterPassport
   ```

2. **Update connection strings**
   
   Edit `appsettings.json` in both `MasterPassport.WebAPI` and `MasterPassport.WebMVC` projects:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=MasterPassport;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

3. **Create the database**
   
   Run the SQL script provided in the `Database` folder to create the database schema.

4. **Apply Entity Framework migrations** (if using Code-First approach)
   ```bash
   dotnet ef migrations add InitialCreate --project src/MasterPassport.Infrastructure --startup-project src/MasterPassport.WebAPI
   dotnet ef database update --project src/MasterPassport.Infrastructure --startup-project src/MasterPassport.WebAPI
   ```

5. **Build the solution**
   ```bash
   dotnet build
   ```

6. **Run the applications**
   
   Start the Web API:
   ```bash
   cd src/MasterPassport.WebAPI
   dotnet run
   ```
   
   Start the MVC application (in a separate terminal):
   ```bash
   cd src/MasterPassport.WebMVC
   dotnet run
   ```

## Configuration

### JWT Settings

Configure JWT authentication in `appsettings.json`:

```json
"JwtSettings": {
  "SecretKey": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
  "Issuer": "MasterPassport",
  "Audience": "MasterPassportUsers",
  "ExpirationMinutes": "60"
}
```

**Important**: Change the `SecretKey` to a strong, unique value in production.

### API Settings (for MVC project)

Configure the Web API base URL in `appsettings.json` of the MVC project:

```json
"ApiSettings": {
  "BaseUrl": "https://localhost:7001"
}
```

## API Endpoints

### Authentication
- `POST /api/auth/login` - User login
- `POST /api/auth/change-password` - Change password
- `POST /api/auth/reset-password` - Reset password

### Registration
- `POST /api/registration/register` - Submit new registration
- `GET /api/registration/pending` - Get pending registrations (Admin)
- `POST /api/registration/approve/{id}` - Approve registration (Admin)
- `POST /api/registration/reject/{id}` - Reject registration (Admin)

### Lookups
- `GET /api/lookup/countries` - Get all countries
- `GET /api/lookup/governorates/{countryId}` - Get governorates by country
- `GET /api/lookup/cities/{governorateId}` - Get cities by governorate
- `GET /api/lookup/universities` - Get all universities
- `GET /api/lookup/colleges/{universityId}` - Get colleges by university
- And more...

## Security Features

- **Password Hashing**: Passwords are hashed using SHA-256 with unique salts
- **JWT Authentication**: Stateless authentication using JSON Web Tokens
- **Role-Based Authorization**: Different access levels for users and administrators
- **Password History**: Track password changes for security compliance
- **Login Auditing**: Log all login attempts for security monitoring

## Development Guidelines

### Adding New Entities

1. Create the entity in `MasterPassport.Domain/Entities`
2. Add DbSet to `ApplicationDbContext`
3. Configure entity relationships in `OnModelCreating`
4. Create repository interface in `MasterPassport.Domain/Interfaces`
5. Implement repository in `MasterPassport.Infrastructure/Persistence/Repositories`
6. Add repository to `UnitOfWork`

### Adding New Features

1. Create DTOs in `MasterPassport.Application/DTOs`
2. Define service interface in `MasterPassport.Application/Interfaces`
3. Implement service in `MasterPassport.Application/Services`
4. Create controller in `MasterPassport.WebAPI/Controllers`
5. Create views in `MasterPassport.WebMVC/Views` (if needed)

## Contributing

Contributions are welcome! Please follow these guidelines:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is proprietary software. All rights reserved.

## Contact

For questions or support, please contact the development team.

## Acknowledgments

- Built with .NET 9.0 and ASP.NET Core
- Follows Clean Architecture principles by Robert C. Martin
- Implements JWT authentication best practices
- Uses Entity Framework Core for data access
