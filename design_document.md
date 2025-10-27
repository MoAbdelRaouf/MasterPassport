# Master Passport Project Design Document

## 1. Introduction

This document outlines the design for the Master Passport user registration system, which will serve as a centralized database for users of information systems and other applications within an organization. The system will be developed using .NET 9.0, SQL Server, Clean Architecture, Bootstrap/Tailwind CSS for the UI, and JWT for authentication. The solution will consist of two main projects: a Web API and an MVC Core 9 application.

## 2. System Overview

The user registration process will involve two main stages:
1.  **Temporary Registration**: User data is initially recorded in temporary tables.
2.  **Approval and Permanent Storage**: After review and approval by a system administrator, the user data is moved to permanent tables.

The user interface for registration will be divided into several steps:
*   Personal Information
*   Contact Information
*   Workplace Information
*   Educational Qualifications
*   Experience Fields
*   Languages
*   Attachments
*   Login Credentials

## 3. Technology Stack

*   **Backend**: .NET 9.0 (C#)
*   **Database**: SQL Server
*   **Architecture**: Clean Architecture
*   **Web Frameworks**: ASP.NET Core Web API, ASP.NET Core MVC 9
*   **Frontend**: Bootstrap/Tailwind CSS
*   **Authentication**: JWT (JSON Web Tokens)
*   **IDE**: Visual Studio 2022

## 4. Database Schema Design

The following SQL Server database schema is provided and will be used as the foundation for the data model. The tables are categorized into temporary and permanent tables, along with lookup tables.

### 4.1. Temporary Tables

These tables hold user data during the initial registration phase before administrator approval.

*   **TempUsers**
    *   `TempUserID` (PK, INT, IDENTITY)
    *   `NationalityID` (INT, FK to Nationalities)
    *   `NationalID` (NVARCHAR(14)) - For Egyptians (NationalityID = 1)
    *   `PassportNumber` (NVARCHAR(50)) - For non-Egyptians (NationalityID != 1)
    *   `TitleID` (INT, FK to Titles)
    *   `FirstNameAr` (NVARCHAR(50))
    *   `SecondNameAr` (NVARCHAR(50))
    *   `ThirdNameAr` (NVARCHAR(50))
    *   `FourthNameAr` (NVARCHAR(50))
    *   `FullNameEn` (NVARCHAR(200))
    *   `DateOfBirth` (DATE)
    *   `GenderID` (INT, FK to Genders)
    *   `PersonalImage` (NVARCHAR(MAX))
    *   `Address` (NVARCHAR(200))
    *   `CityID` (INT, FK to Cities)
    *   `Email` (NVARCHAR(100))
    *   `MobileNumber` (NVARCHAR(20))
    *   `AlternativeMobile` (NVARCHAR(20))
    *   `RegistrationDate` (DATETIME)
    *   `IsApproved` (BIT)
    *   `ApprovedBy` (NVARCHAR(100))
    *   `ApprovedDate` (DATETIME)

*   **TempWorkPlaces**
    *   `TempWorkPlaceID` (PK, INT, IDENTITY)
    *   `TempUserID` (INT, FK to TempUsers)
    *   `WorkPlaceTypeID` (INT, FK to WorkPlaceTypes)
    *   `JobTitle` (NVARCHAR(100))
    *   `WorkPlaceName` (NVARCHAR(200))
    *   `StartDate` (DATE)
    *   `EndDate` (DATE, NULLABLE, if `IsCurrent` is true)
    *   `CityID` (INT, FK to Cities)
    *   `UniversityID` (INT, FK to Universities, conditional)
    *   `UniversityOtherName` (NVARCHAR(200), conditional)
    *   `CollegeID` (INT, FK to College, conditional)
    *   `CollegeOtherName` (NVARCHAR(200), conditional)
    *   `IsCurrent` (BIT)

*   **TempScientificDegrees**
    *   `TempScientificDegreeID` (PK, INT, IDENTITY)
    *   `TempUserID` (INT, FK to TempUsers)
    *   `QualificationTypeID` (INT, FK to QualificationTypes)
    *   `UniversityID` (INT, FK to Universities)
    *   `UniversityOtherName` (NVARCHAR(200), conditional)
    *   `CollegeID` (INT, FK to College)
    *   `CollegeOtherName` (NVARCHAR(200), conditional)
    *   `QualificationMajorID` (INT, FK to QualificationMajor)
    *   `MajorOtherName` (NVARCHAR(200), conditional)
    *   `QualificationMinorID` (INT, FK to QualificationMinor)
    *   `MinorOtherName` (NVARCHAR(200), conditional)
    *   `GraduationDate` (DATE)
    *   `Grade` (NVARCHAR(50))

*   **TempUserExperienceFields**
    *   `TempUserExperienceFieldID` (PK, INT, IDENTITY)
    *   `TempUserID` (INT, FK to TempUsers)
    *   `ExperienceFieldID` (INT, FK to ExperienceField)

*   **TempUserLanguages**
    *   `TempUserLanguageID` (PK, INT, IDENTITY)
    *   `TempUserID` (INT, FK to TempUsers)
    *   `LanguageID` (INT, FK to Language)
    *   `LanguageOtherName` (NVARCHAR(100), conditional)
    *   `ProficiencyLevelID` (INT, FK to ProficiencyLevel)

*   **TempUserAttachments**
    *   `TempUserAttachmentID` (PK, INT, IDENTITY)
    *   `TempUserID` (INT, FK to TempUsers)
    *   `AttachmentTypeID` (INT, FK to AttachmentTypes)
    *   `FileName` (NVARCHAR(255))
    *   `FilePath` (NVARCHAR(MAX))
    *   `FileSize` (INT)
    *   `ContentType` (NVARCHAR(100))
    *   `UploadedAt` (DATETIME)

*   **TempUserCredentials**
    *   `TempUserCredentialID` (PK, INT, IDENTITY)
    *   `TempUserID` (INT, FK to TempUsers)
    *   `Username` (NVARCHAR(100), unique, typically Email)
    *   `PasswordHash` (NVARCHAR(256))
    *   `PasswordSalt` (NVARCHAR(256))

### 4.2. Permanent Tables

These tables store approved user data.

*   **Users**
    *   `UserID` (PK, INT, IDENTITY)
    *   `NationalityID` (INT, FK to Nationalities)
    *   `NationalID` (NVARCHAR(14))
    *   `PassportNumber` (NVARCHAR(50))
    *   `TitleID` (INT, FK to Titles)
    *   `FirstNameAr` (NVARCHAR(50))
    *   `SecondNameAr` (NVARCHAR(50))
    *   `ThirdNameAr` (NVARCHAR(50))
    *   `FourthNameAr` (NVARCHAR(50))
    *   `FullNameEn` (NVARCHAR(200))
    *   `DateOfBirth` (DATE)
    *   `GenderID` (INT, FK to Genders)
    *   `PersonalImage` (NVARCHAR(MAX))
    *   `Address` (NVARCHAR(200))
    *   `CityID` (INT, FK to Cities)
    *   `Email` (NVARCHAR(100))
    *   `MobileNumber` (NVARCHAR(20))
    *   `AlternativeMobile` (NVARCHAR(20))
    *   `RegistrationDate` (DATETIME)
    *   `IsActive` (BIT)
    *   `CreatedAt` (DATETIME)
    *   `UpdatedAt` (DATETIME)

*   **WorkPlaces**
    *   `WorkPlaceID` (PK, INT, IDENTITY)
    *   `UserID` (INT, FK to Users)
    *   `WorkPlaceTypeID` (INT, FK to WorkPlaceTypes)
    *   `JobTitle` (NVARCHAR(100))
    *   `WorkPlaceName` (NVARCHAR(200))
    *   `StartDate` (DATE)
    *   `EndDate` (DATE, NULLABLE)
    *   `CityID` (INT, FK to Cities)
    *   `UniversityID` (INT, FK to Universities)
    *   `UniversityOtherName` (NVARCHAR(200))
    *   `CollegeID` (INT, FK to College)
    *   `CollegeOtherName` (NVARCHAR(200))
    *   `IsCurrent` (BIT)

*   **ScientificDegrees**
    *   `ScientificDegreeID` (PK, INT, IDENTITY)
    *   `UserID` (INT, FK to Users)
    *   `QualificationTypeID` (INT, FK to QualificationTypes)
    *   `UniversityID` (INT, FK to Universities)
    *   `UniversityOtherName` (NVARCHAR(200))
    *   `CollegeID` (INT, FK to College)
    *   `CollegeOtherName` (NVARCHAR(200))
    *   `QualificationMajorID` (INT, FK to QualificationMajor)
    *   `MajorOtherName` (NVARCHAR(200))
    *   `QualificationMinorID` (INT, FK to QualificationMinor)
    *   `MinorOtherName` (NVARCHAR(200))
    *   `GraduationDate` (DATE)
    *   `Grade` (NVARCHAR(50))

*   **UserExperienceFields**
    *   `UserExperienceFieldID` (PK, INT, IDENTITY)
    *   `UserID` (INT, FK to Users)
    *   `ExperienceFieldID` (INT, FK to ExperienceField)

*   **UserLanguages**
    *   `UserLanguageID` (PK, INT, IDENTITY)
    *   `UserID` (INT, FK to Users)
    *   `LanguageID` (INT, FK to Language)
    *   `LanguageOtherName` (NVARCHAR(100))
    *   `ProficiencyLevelID` (INT, FK to ProficiencyLevel)

*   **UserAttachments**
    *   `UserAttachmentID` (PK, INT, IDENTITY)
    *   `UserID` (INT, FK to Users)
    *   `AttachmentTypeID` (INT, FK to AttachmentTypes)
    *   `FileName` (NVARCHAR(255))
    *   `FilePath` (NVARCHAR(MAX))
    *   `FileSize` (INT)
    *   `ContentType` (NVARCHAR(100))
    *   `UploadedAt` (DATETIME)

*   **UserCredentials**
    *   `UserCredentialID` (PK, INT, IDENTITY)
    *   `UserID` (INT, FK to Users)
    *   `Username` (NVARCHAR(100), unique)
    *   `PasswordHash` (NVARCHAR(256))
    *   `PasswordSalt` (NVARCHAR(256))
    *   `LastLogin` (DATETIME)
    *   `IsLockedOut` (BIT)
    *   `LockoutEndDate` (DATETIME)

### 4.3. Lookup Tables

These tables provide reference data for various fields.

*   **AttachmentTypes**
    *   `AttachmentTypeID` (PK, INT, IDENTITY)
    *   `TypeName` (NVARCHAR(100))
    *   `Description` (NVARCHAR(500))
    *   `MaxFileSizeMB` (INT)
    *   `AllowedExtensions` (NVARCHAR(200))
    *   `AllowedCount` (INT)
    *   `IsRequired` (BIT)
    *   `CreatedAt` (DATETIME)
    *   `IsActive` (BIT)

*   **Cities**
    *   `CityID` (PK, INT, IDENTITY)
    *   `CityNameAr` (NVARCHAR(50))
    *   `GovernorateID` (INT, FK to Governorates)
    *   `IsActive` (BIT)
    *   `CDate` (DATETIME)

*   **College**
    *   `CollegeID` (PK, INT, IDENTITY)
    *   `UniversityID` (INT, FK to Universities)
    *   `CollegeName` (NVARCHAR(100))
    *   `IsActive` (BIT)
    *   `CDate` (DATETIME)

*   **Countries**
    *   `CountryID` (PK, INT, IDENTITY)
    *   `CountryNameAr` (NVARCHAR(50))
    *   `IsActive` (BIT)
    *   `CDate` (DATETIME)

*   **ExperienceCategory**
    *   `ExperienceCatID` (PK, INT, IDENTITY)
    *   `ExperienceCatName` (NVARCHAR(50))
    *   `ParentID` (INT, NULLABLE, self-referencing FK)
    *   `IsActive` (BIT)
    *   `CDate` (DATETIME)

*   **ExperienceField**
    *   `ExperienceFieldID` (PK, INT, IDENTITY)
    *   `ExperienceCatID` (INT, FK to ExperienceCategory)
    *   `ExperienceFieldName` (NVARCHAR(50))
    *   `IsActive` (BIT)
    *   `CDate` (DATETIME)

*   **Genders**
    *   `GenderID` (PK, INT, IDENTITY)
    *   `GenderNameAr` (NVARCHAR(10))
    *   `IsActive` (BIT)
    *   `CDate` (DATETIME)

*   **Governorates**
    *   `GovernorateID` (PK, INT, IDENTITY)
    *   `GovernorateNameAr` (NVARCHAR(50))
    *   `CountryID` (INT, FK to Countries)

*   **InformationSystems**
    *   `SystemID` (PK, INT, IDENTITY)
    *   `SystemName` (NVARCHAR(100))
    *   `SystemCode` (NVARCHAR(50))
    *   `Description` (NVARCHAR(500))
    *   `CreatedAt` (DATETIME)
    *   `UpdatedAt` (DATETIME)
    *   `IsActive` (BIT)

*   **Language**
    *   `LanguageID` (PK, INT, IDENTITY)
    *   `LanguageName` (NVARCHAR(50))
    *   `IsActive` (BIT)
    *   `CDate` (DATETIME)

*   **LoginRequests**
    *   `RequestID` (PK, INT, IDENTITY)
    *   `UserID` (INT, FK to Users, NULLABLE)
    *   `Username` (NVARCHAR(100))
    *   `SystemID` (INT, FK to InformationSystems)
    *   `RequestDate` (DATETIME)
    *   `IPAddress` (NVARCHAR(50))
    *   `UserAgent` (NVARCHAR(500))
    *   `IsSuccessful` (BIT)
    *   `FailureReason` (NVARCHAR(200))
    *   `SessionID` (NVARCHAR(100))

*   **ModificationTypes**
    *   `ModificationTypeID` (PK, INT, IDENTITY)
    *   `ModificationTypeName` (NVARCHAR(50))
    *   `Description` (NVARCHAR(200))

*   **PasswordHistory**
    *   `HistoryID` (PK, INT, IDENTITY)
    *   `UserID` (INT, FK to Users)
    *   `PasswordHash` (NVARCHAR(256))
    *   `PasswordSalt` (NVARCHAR(256))
    *   `ChangedAt` (DATETIME)
    *   `ChangedBy` (NVARCHAR(100))

*   **ProficiencyLevel**
    *   `ProficiencyLevelID` (PK, INT, IDENTITY)
    *   `ProficiencyLevelName` (NVARCHAR(50))
    *   `IsActive` (BIT)
    *   `CDate` (DATETIME)

*   **QualificationMajor**
    *   `QualificationMajorID` (PK, INT, IDENTITY)
    *   `MajorName` (NVARCHAR(100))
    *   `IsActive` (BIT)
    *   `CDate` (DATETIME)

*   **QualificationMinor**
    *   `QualificationMinorID` (PK, INT, IDENTITY)
    *   `QualificationMajorID` (INT, FK to QualificationMajor)
    *   `MinorName` (NVARCHAR(100))
    *   `IsActive` (BIT)
    *   `CDate` (DATETIME)

*   **QualificationType**
    *   `QualificationTypeID` (PK, INT, IDENTITY)
    *   `TypeName` (NVARCHAR(100))
    *   `IsPostgraduate` (BIT)
    *   `IsActive` (BIT)
    *   `CDate` (DATETIME)

*   **Roles**
    *   `RoleID` (PK, INT, IDENTITY)
    *   `RoleName` (NVARCHAR(50))
    *   `Description` (NVARCHAR(200))
    *   `CreatedAt` (DATETIME)
    *   `IsActive` (BIT)

*   **Titles**
    *   `TitleID` (PK, INT, IDENTITY)
    *   `TitleNameAr` (NVARCHAR(50))
    *   `IsActive` (BIT)
    *   `CDate` (DATETIME)

*   **Universities**
    *   `UniversityID` (PK, INT, IDENTITY)
    *   `UniversityName` (NVARCHAR(100))
    *   `IsActive` (BIT)
    *   `CDate` (DATETIME)

*   **UserRoles**
    *   `UserRoleID` (PK, INT, IDENTITY)
    *   `UserID` (INT, FK to Users)
    *   `RoleID` (INT, FK to Roles)
    *   `AssignedAt` (DATETIME)

*   **WorkPlaceTypes**
    *   `WorkPlaceTypeID` (PK, INT, IDENTITY)
    *   `TypeName` (NVARCHAR(100))
    *   `IsActive` (BIT)
    *   `CDate` (DATETIME)

## 5. Clean Architecture Layers

The project will adhere to Clean Architecture principles, separating concerns into distinct layers:

*   **Domain Layer**: Contains enterprise-wide business rules. This layer will include entities, value objects, and interfaces that define the core business logic and data structures. It should be independent of any external frameworks or databases.

*   **Application Layer**: Orchestrates the domain objects to perform specific application-level tasks. This layer will contain application-specific business rules, DTOs (Data Transfer Objects), services, and interfaces for repositories defined in the Domain Layer. It depends on the Domain Layer but not on the Infrastructure Layer.

*   **Infrastructure Layer**: Handles external concerns such as database access, file system operations, and external API integrations. This layer will implement the interfaces defined in the Domain and Application Layers. It depends on the Domain and Application Layers.

*   **Presentation Layer (Web API & MVC)**: The outermost layer, responsible for handling user interactions and presenting information. This includes the ASP.NET Core Web API project for exposing RESTful endpoints and the ASP.NET Core MVC 9 project for the web user interface. Both will depend on the Application Layer.

## 6. Project Structure

The solution will be structured as follows:

```
MasterPassport.sln
├── src
│   ├── MasterPassport.Domain
│   │   ├── Entities
│   │   ├── Enums
│   │   ├── Interfaces
│   │   └── ValueObjects
│   ├── MasterPassport.Application
│   │   ├── DTOs
│   │   ├── Features
│   │   │   ├── Users
│   │   │   │   ├── Commands
│   │   │   │   └── Queries
│   │   │   └── ... (other features)
│   │   ├── Interfaces
│   │   └── Services
│   ├── MasterPassport.Infrastructure
│   │   ├── Persistence
│   │   │   ├── Contexts
│   │   │   ├── Migrations
│   │   │   └── Repositories
│   │   ├── Services
│   │   └── Identity
│   ├── MasterPassport.WebAPI
│   │   ├── Controllers
│   │   ├── Extensions
│   │   ├── Filters
│   │   ├── Models
│   │   └── Program.cs
│   └── MasterPassport.WebMVC
│       ├── Controllers
│       ├── Views
│       ├── Models
│       └── Program.cs
└── tests
    ├── MasterPassport.Domain.Tests
    ├── MasterPassport.Application.Tests
    └── MasterPassport.Infrastructure.Tests
```

*(Note: Unit tests are omitted as requested, but the structure allows for their inclusion later.)*

## 7. Authentication and Authorization

JWT (JSON Web Tokens) will be used for authentication. The Web API will issue tokens upon successful login, and both the Web API and MVC application will use these tokens for subsequent authenticated requests. Role-based authorization will be implemented to control access to different parts of the application.

## 8. UI/UX Considerations

The MVC application will utilize Bootstrap and/or Tailwind CSS for a responsive and modern user interface. The registration process will be guided by a multi-step form, ensuring a user-friendly experience.
