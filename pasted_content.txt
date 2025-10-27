انشاء تطبيق لتسجيل بيانات المستخدمين بحيث يستخدم كقاعدة بيانات مركزيه لمستخدمي نظم المعلومات والتطبيقات الاخرى بالمؤسسةMaster Passport  وذلك باستخدام قاعدة بيانات SQL Server وMVC Core 9 وذلك بحيث يتكون من مشروعين بنفس الحل البرمجي أحدهم Web API والاخر MVC Core 9  
وتتم عملية التسجيل على مرحلتين أساسيتين الأولى تسجيل بيانات المستخدم بجداول مؤقته وبعد فحص البيانات من قبل مدير النظام يتم قبول بيانات المستخدم فيتم نقل بياناته الى الجداول الدائمة كما هو موضح بتصميم قاعدة البيانات المذكور لاحقا 
وعند تصميم واجهة المستخدم يجب ان يتم تقسيم عملية التسجيل الى عدة خطوات مثل البيانات الشخصية وبيانات التواصل وبيانات جهة العمل وبيانات المؤهلات الدراسية ومجالات الخبرة واللغات المتقنة والمرفقات وبيانات تسجيل الدخول وفيما يلي مزيد من التفصيل عن كل خطوة:
1.	البيانات الشخصية: جدول TempUsers
الجنسية NationalityID وفي حالة كان الكود يساوي 1 فهو مصري وأي كود اخر أكبر من 1 يكون أجنبي
الرقم القومي NationalID ويتكون من 14 رقم ويكون متاح فقط في حالة اختيار الجنسية مصري = 1
رقم جواز السفر PassportNumber ويكون متاح للأجانب فقط في حالة اختيار الجنسية غير مصري < 1
اللقب TitleID ويتم اختياره من خلال عرض قائمة بالألقاب المتاحة
الاسم الأول باللغة العربية FirstNameAr
الاسم الثاني باللغة العربية SecondNameAr
الاسم الثالث باللغة العربية ThirdNameAr
الاسم الرابع باللغة العربية FourthNameAr
الاسم بالكامل باللغة الإنجليزية FullNameEn
تاريخ الميلاد DateOfBirth
النوع GenderID
الصورة الشخصية PersonalImage

2.	بيانات التواصل: جدول TempUsers
العنوان Address
المدينة CityID ويتم عرض قائمة بالدول وبعد اختيار أحد الدول يتم عرض قائمة بالمحافظات وبعد اختيار أحد المحافظات يتم عرض قائمة بالمدن ليتم اختيار أحد المدن وتسجيل الكود الخاص بها
البريد الالكتروني Email
رقم الموبيل MobileNumber
رقم الموبيل البديل AlternativeMobile

3.	بيانات جهة العمل: جدول TempWorkPlaces
يمكن إضافة أكثر من جهة عمل لكل مستخدم
نوع جهة العمل WorkPlaceTypeID 
المسمى الوظيفي JobTitle
أسم جهة العمل WorkPlaceName
تاريخ بداية العمل StartDate
تاريخ نهاية العمل EndDate ويمكن ان يكون فارغا فقط في حالة اختيار العمل الحالي IsCurrent = true
المدينة CityID ويتم عرض قائمة بالدول وبعد اختيار أحد الدول يتم عرض قائمة بالمحافظات وبعد اختيار أحد المحافظات يتم عرض قائمة بالمدن ليتم اختيار أحد المدن وتسجيل الكود الخاص بها
في حالة اختيار نوع جهة العمل بالتعليم العالي =1 يجب ان تظهر القائمة الخاصة بالجامعات وعند اختيار أحدها يتم عرض الكليات الخاصة بها ويتم اختيار أحد الكليات لحفظ الكود الخاص بها CollegeID ويجب ان لا تظهر هذه القائمة في حالة اختيار أي نوع اخر من أنواع جهة العمل
الوظيفة الحالية IsCurrent
في حالة اختيار الجامعة أخرى = 0 يتم اتاحة إمكانية كتابة اسم الجامعة مباشرتا UniversityOtherName
في حالة اختيار الكلية أخرى = 0 يتم اتاحة إمكانية كتابة اسم الكلية مباشرتا CollegeOtherName

4.	بيانات المؤهلات الدراسية: جدول TempScientificDegrees
يمكن إضافة أكثر من مؤهل دراسي لكل مستخدم على ان يشتمل على مؤهل واحد على الأقل يكون نوعه بجدول QualificationType ISPostgraduate = falsed
نوع الطلب QualificationTypeID
عرض القائمة الخاصة بالجامعات وعند اختيار أحدها يتم عرض الكليات الخاصة بها ويتم اختيار أحد الكليات لحفظ الكود الخاص بها CollegeID
وفي حالة اختيار أخرى CollegeID=0 او أخرى من قائمة الجامعات يتم عرض  input لتسجيل اسم الجامعة UniversityOtherName واسم الكلية CollegeOtherName
يتم عرض قائمة المؤهلات الرئيسية من جدول QualificationMajor ثم عرض قائمة المؤهلات التخصصية من جدول QualificationMinor بناء على اختيار المؤهل الرئيسي و حفظ الاختيار في QualificationMinorID و في حالة اختيار أخرى =0 يتم عرض input  لتسجيل اسم المؤهل الرئيسي MajorOtherName و اسم المؤهل التخصصي MinorOtherName

5.	مجالات الخبرة: جدول TempUserExperienceFields
يمكن لكل مستخدم تسجيل أكثر من مجال من مجالات الخبرة ExperienceFieldID  و ذلك من خلال عرض قائمة بفئة الخبرة من الجدول ExperienceCategory في شكل شجرة منسدلة من القائمة و عند اختيار احد الفئات تعرض قائمة أخرى بمجالات الخبرة المرتبطة بها من الجدول ExperienceField و اتاحة الفرصة للمستخدم لاختيار مجال او اكثر من مجالات الخبرة و اضافتها لمجالات الخبرة الخاصة به و التي تظهر في grid

6.	اللغات المتقنة: جدول TempUserLanguages
يمكن لكل مستخدم تسجيل اكثر من لغة TempLanguageID و درجة الاتقان ProficiencyLevelID و عند اختيار لغة أخرى =0 يعرض input  لادخال لغة أخرى LanguageOtherName

7.	المرفقات: جدول TempUserAttachments
نوع المرفق AttachmentTypeID
أسم المرفق FileName
مسار المرفق FilePath
حجم المرفق FileSize
نوع الملف المرفق ContentType
تاريخ و توقيت الرفع UploadedAt

8.	بيانات تسجيل الدخول: جدول TempUserCredentials
أسم المستخدم و هو يساوي البريد الالكتروني الخاص بالمستخدم Username
كلمة المرور المشفرة PasswordHash باستخدام PasswordSalt و المختارة بشكل عشوائي يختلف من مستخدم لاخر

please use Visual Studio 2022 Dotnet core 9.0 , connecting to an existing database, use Clean Architecture, use Bootstrap/Tailwind CSS, and JWT for authentication. omit unit tests as requested
يجب الاخذ في الاعتبار تصميم قاعدة البيانات التالي:

USE [master]
GO
/****** Object:  Database [MasterPassport]    Script Date: 10/6/2025 10:50:06 PM ******/
CREATE DATABASE [MasterPassport]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MasterPassport', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\MasterPassport.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MasterPassport_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\MasterPassport_log.ldf' , SIZE = 6912KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MasterPassport] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MasterPassport].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MasterPassport] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MasterPassport] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MasterPassport] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MasterPassport] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MasterPassport] SET ARITHABORT OFF 
GO
ALTER DATABASE [MasterPassport] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MasterPassport] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MasterPassport] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MasterPassport] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MasterPassport] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MasterPassport] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MasterPassport] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MasterPassport] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MasterPassport] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MasterPassport] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MasterPassport] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MasterPassport] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MasterPassport] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MasterPassport] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MasterPassport] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MasterPassport] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MasterPassport] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MasterPassport] SET RECOVERY FULL 
GO
ALTER DATABASE [MasterPassport] SET  MULTI_USER 
GO
ALTER DATABASE [MasterPassport] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MasterPassport] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MasterPassport] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MasterPassport] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [MasterPassport] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MasterPassport', N'ON'
GO
USE [MasterPassport]
GO
/****** Object:  Table [dbo].[AttachmentTypes]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttachmentTypes](
	[AttachmentTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[MaxFileSizeMB] [int] NULL,
	[AllowedExtensions] [nvarchar](200) NULL,
	[AllowedCount] [int] NOT NULL,
	[IsRequired] [bit] NULL,
	[CreatedAt] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK__Attachme__5C63AB44E55739D1] PRIMARY KEY CLUSTERED 
(
	[AttachmentTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cities]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[CityID] [int] IDENTITY(1,1) NOT NULL,
	[CityNameAr] [nvarchar](50) NOT NULL,
	[GovernorateID] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[College]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[College](
	[CollegeID] [int] IDENTITY(1,1) NOT NULL,
	[UniversityID] [int] NOT NULL,
	[CollegeName] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CDate] [datetime] NOT NULL,
 CONSTRAINT [PK_College] PRIMARY KEY CLUSTERED 
(
	[CollegeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Countries]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[CountryID] [int] IDENTITY(1,1) NOT NULL,
	[CountryNameAr] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[CountryNameAr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ExperienceCategory]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExperienceCategory](
	[ExperienceCatID] [int] IDENTITY(1,1) NOT NULL,
	[ExperienceCatName] [nvarchar](50) NOT NULL,
	[ParentID] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ExperienceCategory] PRIMARY KEY CLUSTERED 
(
	[ExperienceCatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ExperienceField]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExperienceField](
	[ExperienceFieldID] [int] IDENTITY(1,1) NOT NULL,
	[ExperienceCatID] [int] NOT NULL,
	[ExperienceFieldName] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ExperienceField] PRIMARY KEY CLUSTERED 
(
	[ExperienceFieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Genders]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genders](
	[GenderID] [int] IDENTITY(1,1) NOT NULL,
	[GenderNameAr] [nvarchar](10) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GenderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[GenderNameAr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Governorates]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Governorates](
	[GovernorateID] [int] IDENTITY(1,1) NOT NULL,
	[GovernorateNameAr] [nvarchar](50) NOT NULL,
	[CountryID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GovernorateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InformationSystems]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InformationSystems](
	[SystemID] [int] IDENTITY(1,1) NOT NULL,
	[SystemName] [nvarchar](100) NOT NULL,
	[SystemCode] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK__Informat__9394F6AA30055328] PRIMARY KEY CLUSTERED 
(
	[SystemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Informat__0936B4326599D2C3] UNIQUE NONCLUSTERED 
(
	[SystemCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Informat__B40377A83D7277FF] UNIQUE NONCLUSTERED 
(
	[SystemName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Language]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Language](
	[LanguageID] [int] IDENTITY(1,1) NOT NULL,
	[LanguageName] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[LanguageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoginRequests]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginRequests](
	[RequestID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[Username] [nvarchar](100) NOT NULL,
	[SystemID] [int] NOT NULL,
	[RequestDate] [datetime] NULL,
	[IPAddress] [nvarchar](50) NOT NULL,
	[UserAgent] [nvarchar](500) NULL,
	[IsSuccessful] [bit] NOT NULL,
	[FailureReason] [nvarchar](200) NULL,
	[SessionID] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[RequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ModificationTypes]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModificationTypes](
	[ModificationTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ModificationTypeName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[ModificationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ModificationTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PasswordHistory]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PasswordHistory](
	[HistoryID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[PasswordHash] [nvarchar](256) NOT NULL,
	[PasswordSalt] [nvarchar](256) NOT NULL,
	[ChangedAt] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[HistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProficiencyLevel]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProficiencyLevel](
	[ProficiencyLevelID] [int] IDENTITY(1,1) NOT NULL,
	[ProficiencyLevelName] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProficiencyLevel] PRIMARY KEY CLUSTERED 
(
	[ProficiencyLevelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QualificationMajor]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QualificationMajor](
	[QualificationMajorID] [int] IDENTITY(1,1) NOT NULL,
	[QualificationMajorName] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CDate] [datetime] NOT NULL,
 CONSTRAINT [PK_QualificationMajor] PRIMARY KEY CLUSTERED 
(
	[QualificationMajorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QualificationMinor]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QualificationMinor](
	[QualificationMinorID] [int] IDENTITY(1,1) NOT NULL,
	[QualificationMajorID] [int] NOT NULL,
	[QualificationMinorName] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CDate] [datetime] NOT NULL,
 CONSTRAINT [PK_QualificationMainor] PRIMARY KEY CLUSTERED 
(
	[QualificationMinorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QualificationType]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QualificationType](
	[QualificationTypeID] [int] IDENTITY(1,1) NOT NULL,
	[QualificationTypeName] [nvarchar](100) NOT NULL,
	[ISPostgraduate] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_QualificationType_IsActive]  DEFAULT ((1)),
	[CDate] [datetime] NOT NULL CONSTRAINT [DF_QualificationType_CDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_QualificationType] PRIMARY KEY CLUSTERED 
(
	[QualificationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ScientificDegrees]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScientificDegrees](
	[DegreeID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[QualificationTypeID] [int] NOT NULL,
	[CollegeID] [int] NOT NULL,
	[QualificationMinorID] [int] NOT NULL,
	[QualificationYear] [int] NOT NULL,
	[UniversityOtherName] [nvarchar](100) NULL,
	[CollegeOtherName] [nvarchar](100) NULL,
	[MajorOtherName] [nvarchar](100) NULL,
	[MinorOtherName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CDate] [datetime] NOT NULL,
 CONSTRAINT [PK__Scientif__4D9492CE2FDA6596] PRIMARY KEY CLUSTERED 
(
	[DegreeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Status]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[StatusID] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_StatusID] PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TempScientificDegrees]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempScientificDegrees](
	[TempDegreeID] [int] IDENTITY(1,1) NOT NULL,
	[TempUserID] [int] NOT NULL,
	[QualificationTypeID] [int] NOT NULL,
	[CollegeID] [int] NOT NULL,
	[QualificationMinorID] [int] NOT NULL,
	[UniversityOtherName] [nvarchar](100) NULL,
	[CollegeOtherName] [nvarchar](100) NULL,
	[MajorOtherName] [nvarchar](100) NULL,
	[MinorOtherName] [nvarchar](100) NULL,
	[StatusID] [int] NOT NULL,
 CONSTRAINT [PK__Staging___DE1912D2FC2570F1] PRIMARY KEY CLUSTERED 
(
	[TempDegreeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TempUserAttachments]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempUserAttachments](
	[TempAttachmentID] [int] IDENTITY(1,1) NOT NULL,
	[TempUserID] [int] NOT NULL,
	[AttachmentTypeID] [int] NOT NULL,
	[FileName] [nvarchar](255) NOT NULL,
	[FilePath] [nvarchar](500) NOT NULL,
	[FileSize] [bigint] NOT NULL,
	[ContentType] [nvarchar](100) NOT NULL,
	[UploadedAt] [datetime] NULL,
	[IsVerified] [bit] NULL,
	[VerifiedBy] [int] NULL,
	[VerifiedAt] [datetime] NULL,
	[Notes] [nvarchar](max) NULL,
	[RejectionReason] [nvarchar](500) NULL,
	[StatusID] [int] NOT NULL,
 CONSTRAINT [PK__Staging___0DC59E41E5FA3142] PRIMARY KEY CLUSTERED 
(
	[TempAttachmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TempUserCredentials]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempUserCredentials](
	[TempCredentialID] [int] IDENTITY(1,1) NOT NULL,
	[TempUserID] [int] NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[PasswordHash] [nvarchar](256) NOT NULL,
	[PasswordSalt] [nvarchar](256) NOT NULL,
	[LastPasswordChangeDate] [datetime] NOT NULL,
	[FailedLoginAttempts] [int] NULL,
	[AccountLockedUntil] [datetime] NULL,
	[CreatedAt] [datetime] NULL,
 CONSTRAINT [PK__Staging___E2982F0A3E97E9BD] PRIMARY KEY CLUSTERED 
(
	[TempCredentialID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Staging___E9438D6F75323107] UNIQUE NONCLUSTERED 
(
	[TempUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TempUserExperienceFields]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempUserExperienceFields](
	[TempUserID] [int] NOT NULL,
	[ExperienceFieldID] [int] NOT NULL,
	[StatusID] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CDate] [datetime] NOT NULL,
 CONSTRAINT [PK_TempUserExperienceFields] PRIMARY KEY CLUSTERED 
(
	[TempUserID] ASC,
	[ExperienceFieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TempUserLanguages]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempUserLanguages](
	[TempLanguageID] [int] IDENTITY(1,1) NOT NULL,
	[TempUserID] [int] NOT NULL,
	[LanguageOtherName] [nvarchar](50) NOT NULL,
	[ProficiencyLevelID] [int] NOT NULL,
	[StatusID] [int] NOT NULL,
 CONSTRAINT [PK__Staging___28A4C6E620BD7CAE] PRIMARY KEY CLUSTERED 
(
	[TempLanguageID] ASC,
	[TempUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TempUsers]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TempUsers](
	[TempUserID] [int] IDENTITY(1,1) NOT NULL,
	[NationalID] [nvarchar](14) NULL,
	[PassportNumber] [nvarchar](20) NULL,
	[TitleID] [int] NOT NULL,
	[FirstNameAr] [nvarchar](50) NOT NULL,
	[SecondNameAr] [nvarchar](50) NOT NULL,
	[ThirdNameAr] [nvarchar](50) NOT NULL,
	[FourthNameAr] [nvarchar](50) NOT NULL,
	[FullNameEn] [nvarchar](500) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[MobileNumber] [nvarchar](20) NOT NULL,
	[AlternativeMobile] [nvarchar](20) NULL,
	[DateOfBirth] [date] NOT NULL,
	[NationalityID] [int] NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
	[CityID] [int] NOT NULL,
	[GenderID] [int] NOT NULL,
	[UserExperience] [nvarchar](max) NULL,
	[PersonalImage] [varbinary](max) NULL,
	[VerifyEmailDate] [datetime] NULL,
	[InternalFileNumber] [nvarchar](50) NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[ReviewedBy] [nvarchar](100) NULL,
	[ReviewedAt] [datetime] NULL,
	[RejectionReason] [nvarchar](500) NULL,
	[UserID] [int] NULL,
	[StatusID] [int] NOT NULL,
 CONSTRAINT [PK__Staging___E9438D6E2D73F48A] PRIMARY KEY CLUSTERED 
(
	[TempUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TempWorkPlaces]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempWorkPlaces](
	[TempWorkPlaceID] [int] IDENTITY(1,1) NOT NULL,
	[TempUserID] [int] NOT NULL,
	[WorkPlaceTypeID] [int] NOT NULL,
	[JobTitle] [nvarchar](100) NOT NULL,
	[WorkPlaceName] [nvarchar](100) NULL,
	[CityID] [int] NOT NULL,
	[CollegeID] [int] NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[IsCurrent] [bit] NOT NULL,
	[UniversityOtherName] [nvarchar](100) NULL,
	[CollegeOtherName] [nvarchar](100) NULL,
	[StatusID] [int] NOT NULL,
 CONSTRAINT [PK__Staging___567A072778C336C6] PRIMARY KEY CLUSTERED 
(
	[TempWorkPlaceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Titles]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Titles](
	[TitleID] [int] IDENTITY(1,1) NOT NULL,
	[TitleNameAr] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_Titles_IsActive]  DEFAULT ((1)),
	[CDate] [datetime] NOT NULL CONSTRAINT [DF_Titles_CDate]  DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[TitleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[TitleNameAr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[University]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[University](
	[UniversityID] [int] IDENTITY(1,1) NOT NULL,
	[UniversityName] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CDate] [datetime] NOT NULL,
 CONSTRAINT [PK_University] PRIMARY KEY CLUSTERED 
(
	[UniversityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserAttachments]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAttachments](
	[AttachmentID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[AttachmentTypeID] [int] NOT NULL,
	[FileName] [nvarchar](255) NOT NULL,
	[FilePath] [nvarchar](500) NOT NULL,
	[FileSize] [bigint] NOT NULL,
	[ContentType] [nvarchar](100) NOT NULL,
	[UploadedAt] [datetime] NULL,
	[IsVerified] [bit] NULL,
	[VerifiedBy] [int] NULL,
	[VerifiedAt] [datetime] NULL,
	[Notes] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK__UserAtta__442C64DECDA464F5] PRIMARY KEY CLUSTERED 
(
	[AttachmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserCredentials]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCredentials](
	[CredentialID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[PasswordHash] [nvarchar](256) NOT NULL,
	[PasswordSalt] [nvarchar](256) NOT NULL,
	[LastPasswordChangeDate] [datetime] NOT NULL,
	[FailedLoginAttempts] [int] NULL,
	[AccountLockedUntil] [datetime] NULL,
	[IsEnabled] [bit] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[CredentialID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserExperienceFields]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserExperienceFields](
	[UserID] [int] NOT NULL,
	[ExperienceFieldID] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserExperienceFields] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[ExperienceFieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserLanguages]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLanguages](
	[LanguageID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[LanguageOtherName] [nvarchar](50) NOT NULL,
	[ProficiencyLevelID] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CDate] [datetime] NOT NULL,
 CONSTRAINT [PK__UserLang__B938558B3F3272A0] PRIMARY KEY CLUSTERED 
(
	[LanguageID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserModificationLog]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserModificationLog](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ModifiedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModificationTypeID] [int] NOT NULL,
	[TableName] [nvarchar](100) NOT NULL,
	[RecordID] [int] NOT NULL,
	[OldValues] [nvarchar](max) NULL,
	[NewValues] [nvarchar](max) NULL,
	[IPAddress] [nvarchar](50) NULL,
	[SystemID] [int] NULL,
 CONSTRAINT [PK__UserModi__5E5499A85700814D] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[NationalID] [nvarchar](14) NULL,
	[PassportNumber] [nvarchar](20) NULL,
	[TitleID] [int] NOT NULL,
	[FirstNameAr] [nvarchar](50) NOT NULL,
	[SecondNameAr] [nvarchar](50) NOT NULL,
	[ThirdNameAr] [nvarchar](50) NOT NULL,
	[FourthNameAr] [nvarchar](50) NOT NULL,
	[FullNameEn] [nvarchar](500) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[MobileNumber] [nvarchar](20) NOT NULL,
	[AlternativeMobile] [nvarchar](20) NULL,
	[DateOfBirth] [date] NOT NULL,
	[NationalityID] [int] NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
	[CityID] [int] NOT NULL,
	[GenderID] [int] NOT NULL,
	[UserExperience] [nvarchar](max) NULL,
	[PersonalImage] [varbinary](max) NULL,
	[VerifyEmailDate] [datetime] NULL,
	[InternalFileNumber] [nvarchar](50) NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[TempUserID] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK__Users__1788CCAC5A52C218] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Users_NationalID] UNIQUE NONCLUSTERED 
(
	[NationalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Users__45809E711062ED11] UNIQUE NONCLUSTERED 
(
	[PassportNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Users__A9D10534278CE7D9] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Users__E58799CC3167AD37] UNIQUE NONCLUSTERED 
(
	[InternalFileNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Users__E9AA321A5905C9F8] UNIQUE NONCLUSTERED 
(
	[NationalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WorkPlaces]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkPlaces](
	[WorkPlaceID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[WorkPlaceTypeID] [int] NOT NULL,
	[JobTitle] [nvarchar](100) NOT NULL,
	[WorkPlaceName] [nvarchar](100) NULL,
	[CityID] [int] NOT NULL,
	[CollegeID] [int] NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[IsCurrent] [bit] NOT NULL,
	[UniversityOtherName] [nvarchar](100) NULL,
	[CollegeOtherName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CDate] [datetime] NOT NULL,
 CONSTRAINT [PK__WorkPlac__0C3674E686154966] PRIMARY KEY CLUSTERED 
(
	[WorkPlaceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WorkPlaceType]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkPlaceType](
	[WorkPlaceTypeID] [int] IDENTITY(1,1) NOT NULL,
	[WorkPlaceTypeName] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_WorkPlaceType_IsActive]  DEFAULT ((1)),
	[CDate] [datetime] NOT NULL CONSTRAINT [DF_WorkPlaceType_CDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_WorkPlaceType] PRIMARY KEY CLUSTERED 
(
	[WorkPlaceTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [IX_LoginRequests_RequestTime]    Script Date: 10/6/2025 10:50:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_LoginRequests_RequestTime] ON [dbo].[LoginRequests]
(
	[RequestDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LoginRequests_SystemID]    Script Date: 10/6/2025 10:50:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_LoginRequests_SystemID] ON [dbo].[LoginRequests]
(
	[SystemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LoginRequests_UserID]    Script Date: 10/6/2025 10:50:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_LoginRequests_UserID] ON [dbo].[LoginRequests]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ScientificDegrees_UserID]    Script Date: 10/6/2025 10:50:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_ScientificDegrees_UserID] ON [dbo].[ScientificDegrees]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Staging_ScientificDegrees_StagingUserID]    Script Date: 10/6/2025 10:50:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_Staging_ScientificDegrees_StagingUserID] ON [dbo].[TempScientificDegrees]
(
	[TempUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Staging_UserAttachments_StagingUserID]    Script Date: 10/6/2025 10:50:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_Staging_UserAttachments_StagingUserID] ON [dbo].[TempUserAttachments]
(
	[TempUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Staging_UserLanguages_StagingUserID]    Script Date: 10/6/2025 10:50:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_Staging_UserLanguages_StagingUserID] ON [dbo].[TempUserLanguages]
(
	[TempUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Staging_WorkPlaces_StagingUserID]    Script Date: 10/6/2025 10:50:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_Staging_WorkPlaces_StagingUserID] ON [dbo].[TempWorkPlaces]
(
	[TempUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserAttachments_AttachmentTypeID]    Script Date: 10/6/2025 10:50:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserAttachments_AttachmentTypeID] ON [dbo].[UserAttachments]
(
	[AttachmentTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserAttachments_UserID]    Script Date: 10/6/2025 10:50:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserAttachments_UserID] ON [dbo].[UserAttachments]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserCredentials_IsEnabled]    Script Date: 10/6/2025 10:50:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserCredentials_IsEnabled] ON [dbo].[UserCredentials]
(
	[IsEnabled] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserCredentials_Username]    Script Date: 10/6/2025 10:50:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserCredentials_Username] ON [dbo].[UserCredentials]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserLanguages_UserID]    Script Date: 10/6/2025 10:50:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserLanguages_UserID] ON [dbo].[UserLanguages]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserModificationLog_ModifiedAt]    Script Date: 10/6/2025 10:50:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserModificationLog_ModifiedAt] ON [dbo].[UserModificationLog]
(
	[ModifiedAt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserModificationLog_UserID]    Script Date: 10/6/2025 10:50:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserModificationLog_UserID] ON [dbo].[UserModificationLog]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Users_Email]    Script Date: 10/6/2025 10:50:06 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Email] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Users_InternalFileNumber]    Script Date: 10/6/2025 10:50:06 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_InternalFileNumber] ON [dbo].[Users]
(
	[InternalFileNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_WorkPlaces_UserID]    Script Date: 10/6/2025 10:50:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_WorkPlaces_UserID] ON [dbo].[WorkPlaces]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AttachmentTypes] ADD  CONSTRAINT [DF__Attachmen__MaxFi__21B6055D]  DEFAULT ((5)) FOR [MaxFileSizeMB]
GO
ALTER TABLE [dbo].[AttachmentTypes] ADD  CONSTRAINT [DF__Attachmen__Allow__22AA2996]  DEFAULT ('.pdf,.jpg,.jpeg,.png') FOR [AllowedExtensions]
GO
ALTER TABLE [dbo].[AttachmentTypes] ADD  CONSTRAINT [DF_AttachmentTypes_AllowedCount]  DEFAULT ((1)) FOR [AllowedCount]
GO
ALTER TABLE [dbo].[AttachmentTypes] ADD  CONSTRAINT [DF__Attachmen__IsReq__239E4DCF]  DEFAULT ((0)) FOR [IsRequired]
GO
ALTER TABLE [dbo].[AttachmentTypes] ADD  CONSTRAINT [DF__Attachmen__Creat__24927208]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[AttachmentTypes] ADD  CONSTRAINT [DF_AttachmentTypes_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Cities] ADD  CONSTRAINT [DF_Cities_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Cities] ADD  CONSTRAINT [DF_Cities_CDate]  DEFAULT (getdate()) FOR [CDate]
GO
ALTER TABLE [dbo].[College] ADD  CONSTRAINT [DF_College_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[College] ADD  CONSTRAINT [DF_College_CDate]  DEFAULT (getdate()) FOR [CDate]
GO
ALTER TABLE [dbo].[Countries] ADD  CONSTRAINT [DF_Countries_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Countries] ADD  CONSTRAINT [DF_Countries_CDate]  DEFAULT (getdate()) FOR [CDate]
GO
ALTER TABLE [dbo].[ExperienceCategory] ADD  CONSTRAINT [DF_ExperienceCategory_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ExperienceCategory] ADD  CONSTRAINT [DF_ExperienceCategory_CDate]  DEFAULT (getdate()) FOR [CDate]
GO
ALTER TABLE [dbo].[ExperienceField] ADD  CONSTRAINT [DF_ExperienceField_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ExperienceField] ADD  CONSTRAINT [DF_ExperienceField_CDate]  DEFAULT (getdate()) FOR [CDate]
GO
ALTER TABLE [dbo].[Genders] ADD  CONSTRAINT [DF_Genders_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Genders] ADD  CONSTRAINT [DF_Genders_CDate]  DEFAULT (getdate()) FOR [CDate]
GO
ALTER TABLE [dbo].[InformationSystems] ADD  CONSTRAINT [DF__Informati__Creat__2A4B4B5E]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[InformationSystems] ADD  CONSTRAINT [DF__Informati__Updat__2B3F6F97]  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[InformationSystems] ADD  CONSTRAINT [DF__Informati__IsAct__29572725]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Language] ADD  CONSTRAINT [DF_Language_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Language] ADD  CONSTRAINT [DF_Language_CDate]  DEFAULT (getdate()) FOR [CDate]
GO
ALTER TABLE [dbo].[LoginRequests] ADD  DEFAULT (getdate()) FOR [RequestDate]
GO
ALTER TABLE [dbo].[PasswordHistory] ADD  DEFAULT (getdate()) FOR [ChangedAt]
GO
ALTER TABLE [dbo].[ProficiencyLevel] ADD  CONSTRAINT [DF_ProficiencyLevel_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ProficiencyLevel] ADD  CONSTRAINT [DF_ProficiencyLevel_CDate]  DEFAULT (getdate()) FOR [CDate]
GO
ALTER TABLE [dbo].[QualificationMajor] ADD  CONSTRAINT [DF_QualificationMajor_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[QualificationMajor] ADD  CONSTRAINT [DF_QualificationMajor_CDate]  DEFAULT (getdate()) FOR [CDate]
GO
ALTER TABLE [dbo].[QualificationMinor] ADD  CONSTRAINT [DF_QualificationMainor_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[QualificationMinor] ADD  CONSTRAINT [DF_QualificationMainor_CDate]  DEFAULT (getdate()) FOR [CDate]
GO
ALTER TABLE [dbo].[ScientificDegrees] ADD  CONSTRAINT [DF_ScientificDegrees_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ScientificDegrees] ADD  CONSTRAINT [DF_ScientificDegrees_CDate]  DEFAULT (getdate()) FOR [CDate]
GO
ALTER TABLE [dbo].[TempUserAttachments] ADD  CONSTRAINT [DF_TempUserAttachments_UploadedAt]  DEFAULT (getdate()) FOR [UploadedAt]
GO
ALTER TABLE [dbo].[TempUserAttachments] ADD  CONSTRAINT [DF_TempUserAttachments_IsVerified]  DEFAULT ((0)) FOR [IsVerified]
GO
ALTER TABLE [dbo].[TempUserCredentials] ADD  CONSTRAINT [DF_TempUserCredentials_FailedLoginAttempts]  DEFAULT ((0)) FOR [FailedLoginAttempts]
GO
ALTER TABLE [dbo].[TempUserCredentials] ADD  CONSTRAINT [DF__Staging_U__Creat__6B24EA82]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[TempUserExperienceFields] ADD  CONSTRAINT [DF_TempUserExperienceFields_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[TempUserExperienceFields] ADD  CONSTRAINT [DF_TempUserExperienceFields_CDate]  DEFAULT (getdate()) FOR [CDate]
GO
ALTER TABLE [dbo].[TempUsers] ADD  CONSTRAINT [DF__Staging_U__Creat__60A75C0F]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[TempWorkPlaces] ADD  CONSTRAINT [DF_TempWorkPlaces_IsCurrent]  DEFAULT ((1)) FOR [IsCurrent]
GO
ALTER TABLE [dbo].[University] ADD  CONSTRAINT [DF_University_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[University] ADD  CONSTRAINT [DF_University_CDate]  DEFAULT (getdate()) FOR [CDate]
GO
ALTER TABLE [dbo].[UserAttachments] ADD  CONSTRAINT [DF__UserAttac__Uploa__4BAC3F29]  DEFAULT (getdate()) FOR [UploadedAt]
GO
ALTER TABLE [dbo].[UserAttachments] ADD  CONSTRAINT [DF__UserAttac__IsVer__4CA06362]  DEFAULT ((0)) FOR [IsVerified]
GO
ALTER TABLE [dbo].[UserAttachments] ADD  CONSTRAINT [DF_UserAttachments_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[UserCredentials] ADD  DEFAULT ((0)) FOR [FailedLoginAttempts]
GO
ALTER TABLE [dbo].[UserCredentials] ADD  DEFAULT ((1)) FOR [IsEnabled]
GO
ALTER TABLE [dbo].[UserCredentials] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[UserCredentials] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[UserExperienceFields] ADD  CONSTRAINT [DF_UserExperienceFields_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[UserExperienceFields] ADD  CONSTRAINT [DF_UserExperienceFields_CDate]  DEFAULT (getdate()) FOR [CDate]
GO
ALTER TABLE [dbo].[UserLanguages] ADD  CONSTRAINT [DF_UserLanguages_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[UserLanguages] ADD  CONSTRAINT [DF_UserLanguages_CDate]  DEFAULT (getdate()) FOR [CDate]
GO
ALTER TABLE [dbo].[UserModificationLog] ADD  CONSTRAINT [DF__UserModif__Modif__06CD04F7]  DEFAULT (getdate()) FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__CreatedAt__34C8D9D1]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__UpdatedAt__35BCFE0A]  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[WorkPlaces] ADD  CONSTRAINT [DF_WorkPlaces_IsCurrent]  DEFAULT ((1)) FOR [IsCurrent]
GO
ALTER TABLE [dbo].[WorkPlaces] ADD  CONSTRAINT [DF_WorkPlaces_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[WorkPlaces] ADD  CONSTRAINT [DF_WorkPlaces_CDate]  DEFAULT (getdate()) FOR [CDate]
GO
ALTER TABLE [dbo].[Cities]  WITH CHECK ADD FOREIGN KEY([GovernorateID])
REFERENCES [dbo].[Governorates] ([GovernorateID])
GO
ALTER TABLE [dbo].[College]  WITH CHECK ADD  CONSTRAINT [FK_College_University] FOREIGN KEY([UniversityID])
REFERENCES [dbo].[University] ([UniversityID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[College] CHECK CONSTRAINT [FK_College_University]
GO
ALTER TABLE [dbo].[ExperienceCategory]  WITH CHECK ADD  CONSTRAINT [FK_ExperienceCategory_ExperienceCategory] FOREIGN KEY([ParentID])
REFERENCES [dbo].[ExperienceCategory] ([ExperienceCatID])
GO
ALTER TABLE [dbo].[ExperienceCategory] CHECK CONSTRAINT [FK_ExperienceCategory_ExperienceCategory]
GO
ALTER TABLE [dbo].[ExperienceCategory]  WITH CHECK ADD  CONSTRAINT [FK_ExperienceCategory_ExperienceCategory1] FOREIGN KEY([ParentID])
REFERENCES [dbo].[ExperienceCategory] ([ExperienceCatID])
GO
ALTER TABLE [dbo].[ExperienceCategory] CHECK CONSTRAINT [FK_ExperienceCategory_ExperienceCategory1]
GO
ALTER TABLE [dbo].[ExperienceField]  WITH CHECK ADD  CONSTRAINT [FK_ExperienceField_ExperienceCategory] FOREIGN KEY([ExperienceCatID])
REFERENCES [dbo].[ExperienceCategory] ([ExperienceCatID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[ExperienceField] CHECK CONSTRAINT [FK_ExperienceField_ExperienceCategory]
GO
ALTER TABLE [dbo].[Governorates]  WITH CHECK ADD FOREIGN KEY([CountryID])
REFERENCES [dbo].[Countries] ([CountryID])
GO
ALTER TABLE [dbo].[LoginRequests]  WITH CHECK ADD  CONSTRAINT [FK__LoginRequ__Syste__0D7A0286] FOREIGN KEY([SystemID])
REFERENCES [dbo].[InformationSystems] ([SystemID])
GO
ALTER TABLE [dbo].[LoginRequests] CHECK CONSTRAINT [FK__LoginRequ__Syste__0D7A0286]
GO
ALTER TABLE [dbo].[LoginRequests]  WITH CHECK ADD  CONSTRAINT [FK__LoginRequ__UserI__0C85DE4D] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[LoginRequests] CHECK CONSTRAINT [FK__LoginRequ__UserI__0C85DE4D]
GO
ALTER TABLE [dbo].[PasswordHistory]  WITH CHECK ADD  CONSTRAINT [FK__PasswordH__UserI__48CFD27E] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[PasswordHistory] CHECK CONSTRAINT [FK__PasswordH__UserI__48CFD27E]
GO
ALTER TABLE [dbo].[QualificationMinor]  WITH CHECK ADD  CONSTRAINT [FK_QualificationMinor_QualificationMajor] FOREIGN KEY([QualificationMajorID])
REFERENCES [dbo].[QualificationMajor] ([QualificationMajorID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[QualificationMinor] CHECK CONSTRAINT [FK_QualificationMinor_QualificationMajor]
GO
ALTER TABLE [dbo].[ScientificDegrees]  WITH CHECK ADD  CONSTRAINT [FK__Scientifi__UserI__5441852A] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[ScientificDegrees] CHECK CONSTRAINT [FK__Scientifi__UserI__5441852A]
GO
ALTER TABLE [dbo].[ScientificDegrees]  WITH CHECK ADD  CONSTRAINT [FK_ScientificDegrees_College] FOREIGN KEY([CollegeID])
REFERENCES [dbo].[College] ([CollegeID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[ScientificDegrees] CHECK CONSTRAINT [FK_ScientificDegrees_College]
GO
ALTER TABLE [dbo].[ScientificDegrees]  WITH CHECK ADD  CONSTRAINT [FK_ScientificDegrees_QualificationMinor] FOREIGN KEY([QualificationMinorID])
REFERENCES [dbo].[QualificationMinor] ([QualificationMinorID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[ScientificDegrees] CHECK CONSTRAINT [FK_ScientificDegrees_QualificationMinor]
GO
ALTER TABLE [dbo].[ScientificDegrees]  WITH CHECK ADD  CONSTRAINT [FK_ScientificDegrees_QualificationType] FOREIGN KEY([QualificationTypeID])
REFERENCES [dbo].[QualificationType] ([QualificationTypeID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[ScientificDegrees] CHECK CONSTRAINT [FK_ScientificDegrees_QualificationType]
GO
ALTER TABLE [dbo].[TempScientificDegrees]  WITH CHECK ADD  CONSTRAINT [FK__Staging_S__Stagi__797309D9] FOREIGN KEY([TempUserID])
REFERENCES [dbo].[TempUsers] ([TempUserID])
GO
ALTER TABLE [dbo].[TempScientificDegrees] CHECK CONSTRAINT [FK__Staging_S__Stagi__797309D9]
GO
ALTER TABLE [dbo].[TempScientificDegrees]  WITH CHECK ADD  CONSTRAINT [FK_TempScientificDegrees_College] FOREIGN KEY([CollegeID])
REFERENCES [dbo].[College] ([CollegeID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[TempScientificDegrees] CHECK CONSTRAINT [FK_TempScientificDegrees_College]
GO
ALTER TABLE [dbo].[TempScientificDegrees]  WITH CHECK ADD  CONSTRAINT [FK_TempScientificDegrees_QualificationMinor] FOREIGN KEY([QualificationMinorID])
REFERENCES [dbo].[QualificationMinor] ([QualificationMinorID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[TempScientificDegrees] CHECK CONSTRAINT [FK_TempScientificDegrees_QualificationMinor]
GO
ALTER TABLE [dbo].[TempScientificDegrees]  WITH CHECK ADD  CONSTRAINT [FK_TempScientificDegrees_QualificationType] FOREIGN KEY([QualificationTypeID])
REFERENCES [dbo].[QualificationType] ([QualificationTypeID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[TempScientificDegrees] CHECK CONSTRAINT [FK_TempScientificDegrees_QualificationType]
GO
ALTER TABLE [dbo].[TempScientificDegrees]  WITH CHECK ADD  CONSTRAINT [FK_TempScientificDegrees_StatusID] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([StatusID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[TempScientificDegrees] CHECK CONSTRAINT [FK_TempScientificDegrees_StatusID]
GO
ALTER TABLE [dbo].[TempUserAttachments]  WITH CHECK ADD  CONSTRAINT [FK__Staging_U__Stagi__70DDC3D8] FOREIGN KEY([TempUserID])
REFERENCES [dbo].[TempUsers] ([TempUserID])
GO
ALTER TABLE [dbo].[TempUserAttachments] CHECK CONSTRAINT [FK__Staging_U__Stagi__70DDC3D8]
GO
ALTER TABLE [dbo].[TempUserAttachments]  WITH CHECK ADD  CONSTRAINT [FK_TempUserAttachments_AttachmentTypes] FOREIGN KEY([AttachmentTypeID])
REFERENCES [dbo].[AttachmentTypes] ([AttachmentTypeID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[TempUserAttachments] CHECK CONSTRAINT [FK_TempUserAttachments_AttachmentTypes]
GO
ALTER TABLE [dbo].[TempUserAttachments]  WITH CHECK ADD  CONSTRAINT [FK_TempUserAttachments_StatusID] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([StatusID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[TempUserAttachments] CHECK CONSTRAINT [FK_TempUserAttachments_StatusID]
GO
ALTER TABLE [dbo].[TempUserCredentials]  WITH CHECK ADD  CONSTRAINT [FK__Staging_U__Stagi__6C190EBB] FOREIGN KEY([TempUserID])
REFERENCES [dbo].[TempUsers] ([TempUserID])
GO
ALTER TABLE [dbo].[TempUserCredentials] CHECK CONSTRAINT [FK__Staging_U__Stagi__6C190EBB]
GO
ALTER TABLE [dbo].[TempUserExperienceFields]  WITH CHECK ADD  CONSTRAINT [FK_TempUserExperienceFields_ExperienceField] FOREIGN KEY([ExperienceFieldID])
REFERENCES [dbo].[ExperienceField] ([ExperienceFieldID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[TempUserExperienceFields] CHECK CONSTRAINT [FK_TempUserExperienceFields_ExperienceField]
GO
ALTER TABLE [dbo].[TempUserExperienceFields]  WITH CHECK ADD  CONSTRAINT [FK_TempUserExperienceFields_TempUsers] FOREIGN KEY([TempUserID])
REFERENCES [dbo].[TempUsers] ([TempUserID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[TempUserExperienceFields] CHECK CONSTRAINT [FK_TempUserExperienceFields_TempUsers]
GO
ALTER TABLE [dbo].[TempUserLanguages]  WITH CHECK ADD  CONSTRAINT [FK__Staging_U__Stagi__03F0984C] FOREIGN KEY([TempUserID])
REFERENCES [dbo].[TempUsers] ([TempUserID])
GO
ALTER TABLE [dbo].[TempUserLanguages] CHECK CONSTRAINT [FK__Staging_U__Stagi__03F0984C]
GO
ALTER TABLE [dbo].[TempUserLanguages]  WITH CHECK ADD  CONSTRAINT [FK_TempUserLanguages_ProficiencyLevel] FOREIGN KEY([ProficiencyLevelID])
REFERENCES [dbo].[ProficiencyLevel] ([ProficiencyLevelID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[TempUserLanguages] CHECK CONSTRAINT [FK_TempUserLanguages_ProficiencyLevel]
GO
ALTER TABLE [dbo].[TempUserLanguages]  WITH CHECK ADD  CONSTRAINT [FK_TempUserLanguages_StatusID] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([StatusID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[TempUserLanguages] CHECK CONSTRAINT [FK_TempUserLanguages_StatusID]
GO
ALTER TABLE [dbo].[TempUsers]  WITH CHECK ADD  CONSTRAINT [FK_TempUsers_Cities] FOREIGN KEY([CityID])
REFERENCES [dbo].[Cities] ([CityID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[TempUsers] CHECK CONSTRAINT [FK_TempUsers_Cities]
GO
ALTER TABLE [dbo].[TempUsers]  WITH CHECK ADD  CONSTRAINT [FK_TempUsers_Countries] FOREIGN KEY([NationalityID])
REFERENCES [dbo].[Countries] ([CountryID])
GO
ALTER TABLE [dbo].[TempUsers] CHECK CONSTRAINT [FK_TempUsers_Countries]
GO
ALTER TABLE [dbo].[TempUsers]  WITH CHECK ADD  CONSTRAINT [FK_TempUsers_Genders] FOREIGN KEY([GenderID])
REFERENCES [dbo].[Genders] ([GenderID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[TempUsers] CHECK CONSTRAINT [FK_TempUsers_Genders]
GO
ALTER TABLE [dbo].[TempUsers]  WITH CHECK ADD  CONSTRAINT [FK_TempUsers_StatusID] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([StatusID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[TempUsers] CHECK CONSTRAINT [FK_TempUsers_StatusID]
GO
ALTER TABLE [dbo].[TempUsers]  WITH CHECK ADD  CONSTRAINT [FK_TempUsers_Titles] FOREIGN KEY([TitleID])
REFERENCES [dbo].[Titles] ([TitleID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[TempUsers] CHECK CONSTRAINT [FK_TempUsers_Titles]
GO
ALTER TABLE [dbo].[TempWorkPlaces]  WITH CHECK ADD  CONSTRAINT [FK__Staging_W__Stagi__7D439ABD] FOREIGN KEY([TempUserID])
REFERENCES [dbo].[TempUsers] ([TempUserID])
GO
ALTER TABLE [dbo].[TempWorkPlaces] CHECK CONSTRAINT [FK__Staging_W__Stagi__7D439ABD]
GO
ALTER TABLE [dbo].[TempWorkPlaces]  WITH CHECK ADD  CONSTRAINT [FK_TempWorkPlaces_Cities] FOREIGN KEY([CityID])
REFERENCES [dbo].[Cities] ([CityID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[TempWorkPlaces] CHECK CONSTRAINT [FK_TempWorkPlaces_Cities]
GO
ALTER TABLE [dbo].[TempWorkPlaces]  WITH CHECK ADD  CONSTRAINT [FK_TempWorkPlaces_College] FOREIGN KEY([CollegeID])
REFERENCES [dbo].[College] ([CollegeID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[TempWorkPlaces] CHECK CONSTRAINT [FK_TempWorkPlaces_College]
GO
ALTER TABLE [dbo].[TempWorkPlaces]  WITH CHECK ADD  CONSTRAINT [FK_TempWorkPlaces_StatusID] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([StatusID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[TempWorkPlaces] CHECK CONSTRAINT [FK_TempWorkPlaces_StatusID]
GO
ALTER TABLE [dbo].[TempWorkPlaces]  WITH CHECK ADD  CONSTRAINT [FK_TempWorkPlaces_WorkPlaceType] FOREIGN KEY([WorkPlaceTypeID])
REFERENCES [dbo].[WorkPlaceType] ([WorkPlaceTypeID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[TempWorkPlaces] CHECK CONSTRAINT [FK_TempWorkPlaces_WorkPlaceType]
GO
ALTER TABLE [dbo].[UserAttachments]  WITH CHECK ADD  CONSTRAINT [FK__UserAttac__Attac__4E88ABD4] FOREIGN KEY([AttachmentTypeID])
REFERENCES [dbo].[AttachmentTypes] ([AttachmentTypeID])
GO
ALTER TABLE [dbo].[UserAttachments] CHECK CONSTRAINT [FK__UserAttac__Attac__4E88ABD4]
GO
ALTER TABLE [dbo].[UserAttachments]  WITH CHECK ADD  CONSTRAINT [FK__UserAttac__UserI__4D94879B] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UserAttachments] CHECK CONSTRAINT [FK__UserAttac__UserI__4D94879B]
GO
ALTER TABLE [dbo].[UserCredentials]  WITH CHECK ADD  CONSTRAINT [FK__UserCrede__UserI__44FF419A] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UserCredentials] CHECK CONSTRAINT [FK__UserCrede__UserI__44FF419A]
GO
ALTER TABLE [dbo].[UserExperienceFields]  WITH CHECK ADD  CONSTRAINT [FK_UserExperienceFields_ExperienceField] FOREIGN KEY([ExperienceFieldID])
REFERENCES [dbo].[ExperienceField] ([ExperienceFieldID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[UserExperienceFields] CHECK CONSTRAINT [FK_UserExperienceFields_ExperienceField]
GO
ALTER TABLE [dbo].[UserExperienceFields]  WITH CHECK ADD  CONSTRAINT [FK_UserExperienceFields_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[UserExperienceFields] CHECK CONSTRAINT [FK_UserExperienceFields_Users]
GO
ALTER TABLE [dbo].[UserLanguages]  WITH CHECK ADD  CONSTRAINT [FK__UserLangu__UserI__5CD6CB2B] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UserLanguages] CHECK CONSTRAINT [FK__UserLangu__UserI__5CD6CB2B]
GO
ALTER TABLE [dbo].[UserLanguages]  WITH CHECK ADD  CONSTRAINT [FK_UserLanguages_Language] FOREIGN KEY([LanguageID])
REFERENCES [dbo].[Language] ([LanguageID])
GO
ALTER TABLE [dbo].[UserLanguages] CHECK CONSTRAINT [FK_UserLanguages_Language]
GO
ALTER TABLE [dbo].[UserLanguages]  WITH CHECK ADD  CONSTRAINT [FK_UserLanguages_ProficiencyLevel] FOREIGN KEY([ProficiencyLevelID])
REFERENCES [dbo].[ProficiencyLevel] ([ProficiencyLevelID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[UserLanguages] CHECK CONSTRAINT [FK_UserLanguages_ProficiencyLevel]
GO
ALTER TABLE [dbo].[UserModificationLog]  WITH CHECK ADD  CONSTRAINT [FK__UserModif__Syste__08B54D69] FOREIGN KEY([SystemID])
REFERENCES [dbo].[InformationSystems] ([SystemID])
GO
ALTER TABLE [dbo].[UserModificationLog] CHECK CONSTRAINT [FK__UserModif__Syste__08B54D69]
GO
ALTER TABLE [dbo].[UserModificationLog]  WITH CHECK ADD  CONSTRAINT [FK__UserModif__UserI__07C12930] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UserModificationLog] CHECK CONSTRAINT [FK__UserModif__UserI__07C12930]
GO
ALTER TABLE [dbo].[UserModificationLog]  WITH CHECK ADD  CONSTRAINT [FK_UserModificationLog_ModificationTypes] FOREIGN KEY([ModificationTypeID])
REFERENCES [dbo].[ModificationTypes] ([ModificationTypeID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[UserModificationLog] CHECK CONSTRAINT [FK_UserModificationLog_ModificationTypes]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK__Users__CityID__3A81B327] FOREIGN KEY([CityID])
REFERENCES [dbo].[Cities] ([CityID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK__Users__CityID__3A81B327]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK__Users__GenderID__3B75D760] FOREIGN KEY([GenderID])
REFERENCES [dbo].[Genders] ([GenderID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK__Users__GenderID__3B75D760]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK__Users__TitleID__36B12243] FOREIGN KEY([TitleID])
REFERENCES [dbo].[Titles] ([TitleID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK__Users__TitleID__36B12243]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Countries] FOREIGN KEY([NationalityID])
REFERENCES [dbo].[Countries] ([CountryID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Countries]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_TempUsers] FOREIGN KEY([TempUserID])
REFERENCES [dbo].[TempUsers] ([TempUserID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_TempUsers]
GO
ALTER TABLE [dbo].[WorkPlaces]  WITH CHECK ADD  CONSTRAINT [FK__WorkPlace__UserI__571DF1D5] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[WorkPlaces] CHECK CONSTRAINT [FK__WorkPlace__UserI__571DF1D5]
GO
ALTER TABLE [dbo].[WorkPlaces]  WITH CHECK ADD  CONSTRAINT [FK_WorkPlaces_Cities1] FOREIGN KEY([CityID])
REFERENCES [dbo].[Cities] ([CityID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[WorkPlaces] CHECK CONSTRAINT [FK_WorkPlaces_Cities1]
GO
ALTER TABLE [dbo].[WorkPlaces]  WITH CHECK ADD  CONSTRAINT [FK_WorkPlaces_College] FOREIGN KEY([CollegeID])
REFERENCES [dbo].[College] ([CollegeID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[WorkPlaces] CHECK CONSTRAINT [FK_WorkPlaces_College]
GO
ALTER TABLE [dbo].[WorkPlaces]  WITH CHECK ADD  CONSTRAINT [FK_WorkPlaces_WorkPlaceType] FOREIGN KEY([WorkPlaceTypeID])
REFERENCES [dbo].[WorkPlaceType] ([WorkPlaceTypeID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[WorkPlaces] CHECK CONSTRAINT [FK_WorkPlaces_WorkPlaceType]
GO
/****** Object:  StoredProcedure [dbo].[AddStagingUser]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddStagingUser]
    @NationalID NVARCHAR(20),
    @PassportNumber NVARCHAR(20) = NULL,
    @FirstNameAr NVARCHAR(50),
    @SecondNameAr NVARCHAR(50),
    @ThirdNameAr NVARCHAR(50) = NULL,
    @FourthNameAr NVARCHAR(50) = NULL,
    @FullNameEn NVARCHAR(100),
    @Email NVARCHAR(100),
    @MobileNumber NVARCHAR(20),
    @AlternativeMobile NVARCHAR(20) = NULL,
    @TitleID INT,
    @NationalityID INT,
    @CountryID INT,
    @GovernorateID INT,
    @CityID INT,
    @PersonalImage VARBINARY(MAX) = NULL,
    @DateOfBirth DATE,
    @GenderID INT,
    @InternalFileNumber NVARCHAR(50) = NULL,
    @CreatedBy NVARCHAR(100)
AS
BEGIN
    INSERT INTO Staging_Users (
        NationalID, PassportNumber, FirstNameAr, SecondNameAr, 
        ThirdNameAr, FourthNameAr, FullNameEn, Email, MobileNumber, 
        AlternativeMobile, TitleID, NationalityID, CountryID, 
        GovernorateID, CityID, PersonalImage, DateOfBirth, 
        GenderID, InternalFileNumber, CreatedBy
    )
    VALUES (
        @NationalID, @PassportNumber, @FirstNameAr, @SecondNameAr, 
        @ThirdNameAr, @FourthNameAr, @FullNameEn, @Email, @MobileNumber, 
        @AlternativeMobile, @TitleID, @NationalityID, @CountryID, 
        @GovernorateID, @CityID, @PersonalImage, @DateOfBirth, 
        @GenderID, @InternalFileNumber, @CreatedBy
    );
    
    SELECT SCOPE_IDENTITY() AS StagingUserID;
END;

GO
/****** Object:  StoredProcedure [dbo].[AddStagingUserAttachment]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddStagingUserAttachment]
    @StagingUserID INT,
    @AttachmentTypeID INT,
    @FileName NVARCHAR(255),
    @FilePath NVARCHAR(500),
    @FileContent VARBINARY(MAX) = NULL,
    @FileSize BIGINT,
    @ContentType NVARCHAR(100)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Staging_Users WHERE StagingUserID = @StagingUserID)
    BEGIN
        RAISERROR('المستخدم المؤقت غير موجود', 16, 1);
        RETURN;
    END
    
    IF NOT EXISTS (SELECT 1 FROM AttachmentTypes WHERE AttachmentTypeID = @AttachmentTypeID)
    BEGIN
        RAISERROR('نوع المرفق غير موجود', 16, 1);
        RETURN;
    END
    
    DECLARE @MaxFileSize BIGINT;
    SELECT @MaxFileSize = MaxFileSizeMB * 1024 * 1024 
    FROM AttachmentTypes 
    WHERE AttachmentTypeID = @AttachmentTypeID;
    
    IF @FileSize > @MaxFileSize
    BEGIN
        RAISERROR('حجم الملف يتجاوز الحد المسموح به', 16, 1);
        RETURN;
    END
    
    DECLARE @AllowedExtensions NVARCHAR(200);
    SELECT @AllowedExtensions = AllowedExtensions 
    FROM AttachmentTypes 
    WHERE AttachmentTypeID = @AttachmentTypeID;
    
    DECLARE @FileExtension NVARCHAR(10) = RIGHT(@FileName, CHARINDEX('.', REVERSE(@FileName)) - 1);
    IF ',' + @AllowedExtensions + ',' NOT LIKE '%,' + @FileExtension + ',%'
    BEGIN
        RAISERROR('نوع الملف غير مسموح به', 16, 1);
        RETURN;
    END
    
    INSERT INTO Staging_UserAttachments (
        StagingUserID, AttachmentTypeID, FileName, FilePath, 
        FileContent, FileSize, ContentType
    )
    VALUES (
        @StagingUserID, @AttachmentTypeID, @FileName, @FilePath, 
        @FileContent, @FileSize, @ContentType
    );
END;

GO
/****** Object:  StoredProcedure [dbo].[ApproveUser]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[ApproveUser]
    @StagingUserID INT,
    @ReviewedBy NVARCHAR(100),
    @InitialPassword NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        IF NOT EXISTS (SELECT 1 FROM Staging_Users WHERE StagingUserID = @StagingUserID AND Status = 'Pending')
        BEGIN
            RAISERROR('المستخدم غير موجود أو تمت مراجعته مسبقاً', 16, 1);
            RETURN;
        END
        
        DECLARE @NationalID NVARCHAR(20), @Email NVARCHAR(100), @InternalFileNumber NVARCHAR(50);
        
        SELECT 
            @NationalID = NationalID,
            @Email = Email,
            @InternalFileNumber = InternalFileNumber
        FROM Staging_Users
        WHERE StagingUserID = @StagingUserID;
        
        IF EXISTS (
            SELECT 1 FROM Users 
            WHERE NationalID = @NationalID 
               OR Email = @Email 
               OR InternalFileNumber = @InternalFileNumber
        )
        BEGIN
            RAISERROR('بيانات مكررة (رقم قومي/بريد إلكتروني/رقم ملف)', 16, 1);
            RETURN;
        END
        
        DECLARE @NewUserID INT;
        
        -- استخدام طريقة بديلة للحصول على معرف المستخدم الجديد
        INSERT INTO Users (
            NationalID, PassportNumber, FirstNameAr, SecondNameAr, 
            ThirdNameAr, FourthNameAr, FullNameEn, Email, MobileNumber, 
            AlternativeMobile, TitleID, NationalityID, CountryID, 
            GovernorateID, CityID, PersonalImage, DateOfBirth, 
            GenderID, InternalFileNumber
        )
        SELECT 
            NationalID, PassportNumber, FirstNameAr, SecondNameAr, 
            ThirdNameAr, FourthNameAr, FullNameEn, Email, MobileNumber, 
            AlternativeMobile, TitleID, NationalityID, CountryID, 
            GovernorateID, CityID, PersonalImage, DateOfBirth, 
            GenderID, InternalFileNumber
        FROM Staging_Users
        WHERE StagingUserID = @StagingUserID;
        
        -- الحصول على معرف المستخدم الجديد
        SET @NewUserID = SCOPE_IDENTITY();
        
        -- التحقق من نجاح الإدراج
        IF @NewUserID IS NULL
        BEGIN
            RAISERROR('فشل في إنشاء المستخدم الرئيسي', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
        
        INSERT INTO BasicQualifications (
            UserID, QualificationType, University, College, Major, SubMajor
        )
        SELECT 
            @NewUserID, QualificationType, University, College, Major, SubMajor
        FROM Staging_BasicQualifications
        WHERE StagingUserID = @StagingUserID AND Status = 'Pending';
        
        INSERT INTO ScientificDegrees (
            UserID, DegreeType, University, College, Major, SubMajor
        )
        SELECT 
            @NewUserID, DegreeType, University, College, Major, SubMajor
        FROM Staging_ScientificDegrees
        WHERE StagingUserID = @StagingUserID AND Status = 'Pending';
        
        INSERT INTO WorkPlaces (
            UserID, CurrentJobTitle, WorkPlaceName, WorkCountryID, 
            WorkGovernorateID, WorkCityID, WorkPlaceType, University, College
        )
        SELECT 
            @NewUserID, CurrentJobTitle, WorkPlaceName, WorkCountryID, 
            WorkGovernorateID, WorkCityID, WorkPlaceType, University, College
        FROM Staging_WorkPlaces
        WHERE StagingUserID = @StagingUserID AND Status = 'Pending';
        
        INSERT INTO UserLanguages (
            UserID, LanguageName, ProficiencyLevel
        )
        SELECT 
            @NewUserID, LanguageName, ProficiencyLevel
        FROM Staging_UserLanguages
        WHERE StagingUserID = @StagingUserID AND Status = 'Pending';
        
        INSERT INTO UserAttachments (
            UserID, AttachmentTypeID, FileName, FilePath, FileContent, 
            FileSize, ContentType, UploadedBy
        )
        SELECT 
            @NewUserID, AttachmentTypeID, FileName, FilePath, FileContent, 
            FileSize, ContentType, 'System'
        FROM Staging_UserAttachments
        WHERE StagingUserID = @StagingUserID AND Status = 'Pending';
        
        IF EXISTS (SELECT 1 FROM Staging_UserCredentials WHERE StagingUserID = @StagingUserID)
        BEGIN
            DECLARE @TempPassword NVARCHAR(256), @TempSalt NVARCHAR(256);
            
            SELECT 
                @TempPassword = PasswordHash,
                @TempSalt = PasswordSalt
            FROM Staging_UserCredentials
            WHERE StagingUserID = @StagingUserID;
            
            INSERT INTO UserCredentials (
                UserID, Username, PasswordHash, PasswordSalt, 
                IsTemporaryPassword, PasswordExpiryDate, LastPasswordChangeDate
            )
            VALUES (
                @NewUserID, @Email, @TempPassword, @TempSalt, 
                1, DATEADD(DAY, 30, GETDATE()), GETDATE()
            );
                
            INSERT INTO PasswordHistory (
                UserID, PasswordHash, PasswordSalt, ChangedBy
            )
            VALUES (
                @NewUserID, @TempPassword, @TempSalt, @ReviewedBy
            );
                
            DELETE FROM Staging_UserCredentials
            WHERE StagingUserID = @StagingUserID;
        END
        ELSE IF @InitialPassword IS NOT NULL
        BEGIN
            DECLARE @Salt NVARCHAR(256) = CONVERT(NVARCHAR(256), NEWID());
            DECLARE @Hash NVARCHAR(256) = HASHBYTES('SHA2_512', @InitialPassword + @Salt);
            
            INSERT INTO UserCredentials (
                UserID, Username, PasswordHash, PasswordSalt, 
                IsTemporaryPassword, PasswordExpiryDate, LastPasswordChangeDate
            )
            VALUES (
                @NewUserID, @Email, @Hash, @Salt, 
                1, DATEADD(DAY, 30, GETDATE()), GETDATE()
            );
                
            INSERT INTO PasswordHistory (
                UserID, PasswordHash, PasswordSalt, ChangedBy
            )
            VALUES (
                @NewUserID, @Hash, @Salt, @ReviewedBy
            );
        END
        
        UPDATE Staging_Users
        SET 
            Status = 'Approved',
            ReviewedBy = @ReviewedBy,
            ReviewedAt = GETDATE()
        WHERE StagingUserID = @StagingUserID;
        
        UPDATE Staging_BasicQualifications SET Status = 'Approved' WHERE StagingUserID = @StagingUserID;
        UPDATE Staging_ScientificDegrees SET Status = 'Approved' WHERE StagingUserID = @StagingUserID;
        UPDATE Staging_WorkPlaces SET Status = 'Approved' WHERE StagingUserID = @StagingUserID;
        UPDATE Staging_UserLanguages SET Status = 'Approved' WHERE StagingUserID = @StagingUserID;
        UPDATE Staging_UserAttachments SET Status = 'Approved' WHERE StagingUserID = @StagingUserID;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END;

GO
/****** Object:  StoredProcedure [dbo].[ChangePassword]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChangePassword]
    @UserID INT,
    @OldPassword NVARCHAR(100),
    @NewPassword NVARCHAR(100),
    @ChangedBy NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @CurrentHash NVARCHAR(256), @CurrentSalt NVARCHAR(256);
    SELECT 
        @CurrentHash = PasswordHash,
        @CurrentSalt = PasswordSalt
    FROM UserCredentials
    WHERE UserID = @UserID;
    
    IF HASHBYTES('SHA2_512', @OldPassword + @CurrentSalt) <> @CurrentHash
    BEGIN
        UPDATE UserCredentials
        SET FailedLoginAttempts = FailedLoginAttempts + 1
        WHERE UserID = @UserID;
        
        RAISERROR('كلمة المرور الحالية غير صحيحة', 16, 1);
        RETURN;
    END
    
    DECLARE @NewSalt NVARCHAR(256) = CONVERT(NVARCHAR(256), NEWID());
    DECLARE @NewHash NVARCHAR(256) = HASHBYTES('SHA2_512', @NewPassword + @NewSalt);
    
    UPDATE UserCredentials
    SET 
        PasswordHash = @NewHash,
        PasswordSalt = @NewSalt,
        IsTemporaryPassword = 0,
        PasswordExpiryDate = DATEADD(DAY, 90, GETDATE()),
        LastPasswordChangeDate = GETDATE(),
        FailedLoginAttempts = 0,
        AccountLocked = 0,
        AccountLockedUntil = NULL
    WHERE UserID = @UserID;
    
    INSERT INTO PasswordHistory (
        UserID, PasswordHash, PasswordSalt, ChangedBy
    )
    VALUES (
        @UserID, @NewHash, @NewSalt, @ChangedBy
    );
END;

GO
/****** Object:  StoredProcedure [dbo].[CreateStagingUserCredentials]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateStagingUserCredentials]
    @StagingUserID INT,
    @Password NVARCHAR(100),
    @PasswordExpiryDays INT = 7
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Staging_Users WHERE StagingUserID = @StagingUserID)
    BEGIN
        RAISERROR('المستخدم المؤقت غير موجود', 16, 1);
        RETURN;
    END
    
    IF EXISTS (SELECT 1 FROM Staging_UserCredentials WHERE StagingUserID = @StagingUserID)
    BEGIN
        RAISERROR('بيانات المصادقة موجودة مسبقاً', 16, 1);
        RETURN;
    END
    
    DECLARE @Email NVARCHAR(100);
    SELECT @Email = Email FROM Staging_Users WHERE StagingUserID = @StagingUserID;
    
    DECLARE @Salt NVARCHAR(256) = CONVERT(NVARCHAR(256), NEWID());
    DECLARE @Hash NVARCHAR(256) = HASHBYTES('SHA2_512', @Password + @Salt);
    DECLARE @ExpiryDate DATETIME = DATEADD(DAY, @PasswordExpiryDays, GETDATE());
    
    INSERT INTO Staging_UserCredentials (
        StagingUserID, Username, PasswordHash, PasswordSalt, 
        PasswordExpiryDate
    )
    VALUES (
        @StagingUserID, @Email, @Hash, @Salt, @ExpiryDate
    );
END;

GO
/****** Object:  StoredProcedure [dbo].[CreateUserSession]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateUserSession]
    @UserID INT,
    @SystemID INT,
    @SessionID NVARCHAR(100),
    @IPAddress NVARCHAR(50),
    @UserAgent NVARCHAR(500) = NULL
AS
BEGIN
    INSERT INTO UserSessions (
        SessionID, UserID, SystemID, LoginTime, LastActivity, IPAddress, UserAgent
    )
    VALUES (
        @SessionID, @UserID, @SystemID, GETDATE(), GETDATE(), @IPAddress, @UserAgent
    );
END;

GO
/****** Object:  StoredProcedure [dbo].[EndUserSession]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EndUserSession]
    @SessionID NVARCHAR(100)
AS
BEGIN
    UPDATE UserSessions
    SET 
        IsActive = 0,
        LogoutTime = GETDATE()
    WHERE SessionID = @SessionID;
END;

GO
/****** Object:  StoredProcedure [dbo].[LogLoginRequest]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LogLoginRequest]
    @Username NVARCHAR(100),
    @SystemID INT,
    @IPAddress NVARCHAR(50),
    @UserAgent NVARCHAR(500) = NULL,
    @IsSuccessful BIT,
    @FailureReason NVARCHAR(200) = NULL,
    @SessionID NVARCHAR(100) = NULL
AS
BEGIN
    DECLARE @UserID INT = NULL;
    
    IF @IsSuccessful = 1
    BEGIN
        SELECT @UserID = UserID 
        FROM UserCredentials 
        WHERE Username = @Username;
    END
    
    INSERT INTO LoginRequests (
        UserID, Username, SystemID, IPAddress, UserAgent,
        IsSuccessful, FailureReason, SessionID
    )
    VALUES (
        @UserID, @Username, @SystemID, @IPAddress, @UserAgent,
        @IsSuccessful, @FailureReason, @SessionID
    );
END;

GO
/****** Object:  StoredProcedure [dbo].[RejectUser]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RejectUser]
    @StagingUserID INT,
    @ReviewedBy NVARCHAR(100),
    @RejectionReason NVARCHAR(500)
AS
BEGIN
    UPDATE Staging_Users
    SET 
        Status = 'Rejected',
        ReviewedBy = @ReviewedBy,
        ReviewedAt = GETDATE(),
        RejectionReason = @RejectionReason
    WHERE StagingUserID = @StagingUserID AND Status = 'Pending';
    
    UPDATE Staging_BasicQualifications SET Status = 'Rejected' WHERE StagingUserID = @StagingUserID;
    UPDATE Staging_ScientificDegrees SET Status = 'Rejected' WHERE StagingUserID = @StagingUserID;
    UPDATE Staging_WorkPlaces SET Status = 'Rejected' WHERE StagingUserID = @StagingUserID;
    UPDATE Staging_UserLanguages SET Status = 'Rejected' WHERE StagingUserID = @StagingUserID;
    UPDATE Staging_UserAttachments SET Status = 'Rejected' WHERE StagingUserID = @StagingUserID;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_PromoteTempUserToPermanent]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_PromoteTempUserToPermanent]
    @TempUserID INT,
    @ApprovedBy NVARCHAR(100),
    @NewUserID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @TranName VARCHAR(32) = 'PromoteTempUser';
    DECLARE @Error INT = 0;
    DECLARE @ErrorMessage NVARCHAR(4000);
    
    -- التحقق من وجود المستخدم المؤقت
    IF NOT EXISTS (SELECT 1 FROM [dbo].[TempUsers] WHERE [TempUserID] = @TempUserID)
    BEGIN
        RAISERROR('المستخدم المؤقت المحدد غير موجود', 16, 1);
        RETURN -1;
    END
    
    -- التحقق من حالة المستخدم المؤقت (يجب أن يكون معتمدًا)
    -- نعتبر المستخدم معتمدًا إذا كان ReviewedBy و ReviewedAt ليسا NULL و RejectionReason هو NULL
    DECLARE @ReviewedBy NVARCHAR(100);
    DECLARE @ReviewedAt DATETIME;
    DECLARE @RejectionReason NVARCHAR(500);
    
    SELECT 
        @ReviewedBy = [ReviewedBy],
        @ReviewedAt = [ReviewedAt],
        @RejectionReason = [RejectionReason]
    FROM [dbo].[TempUsers]
    WHERE [TempUserID] = @TempUserID;
    
    IF @ReviewedBy IS NULL OR @ReviewedAt IS NULL OR @RejectionReason IS NOT NULL
    BEGIN
        RAISERROR('لا يمكن نقل المستخدم إلا إذا كان معتمدًا (ReviewedBy و ReviewedAt ليسا NULL و RejectionReason هو NULL)', 16, 1);
        RETURN -2;
    END
    
    BEGIN TRY
        BEGIN TRANSACTION @TranName;
        
        -- 1. إدخال البيانات الأساسية للمستخدم في جدول Users
        INSERT INTO [dbo].[Users] (
            [NationalID], [PassportNumber], [TitleID], [FirstNameAr], [SecondNameAr], 
            [ThirdNameAr], [FourthNameAr], [FullNameEn], [Email], [MobileNumber], 
            [AlternativeMobile], [DateOfBirth], [NationalityID], [Address], [CityID], 
            [GenderID], [QualificationTypeID], [QualificationCollegeID], 
            [QualificationMinorID], [QualificationYear], [QualificationOtherCollege], 
            [QualificationOtherUniversity], [QualificationOtherMajor], 
            [QualificationOtherMinor], [UserExperience], [PersonalImage], 
            [VerifiedEmailDate], [InternalFileNumber], [TempUserID], [IsActive]
        )
        SELECT 
            [NationalID], [PassportNumber], [TitleID], [FirstNameAr], [SecondNameAr], 
            [ThirdNameAr], [FourthNameAr], [FullNameEn], [Email], [MobileNumber], 
            [AlternativeMobile], [DateOfBirth], [NationalityID], [Address], [CityID], 
            [GenderID], [QualificationTypeID], [QualificationCollegeID], 
            [QualificationMinorID], [QualificationYear], [QualificationOtherCollege], 
            [QualificationOtherUniversity], [QualificationOtherMajor], 
            [QualificationOtherMinor], [UserExperience], [PersonalImage], 
            [VerifiedEmailDate], [InternalFileNumber], [TempUserID], 1
        FROM [dbo].[TempUsers]
        WHERE [TempUserID] = @TempUserID;
        
        -- الحصول على معرف المستخدم الجديد
        SET @NewUserID = SCOPE_IDENTITY();
        
        -- 2. نقل بيانات الاعتمادات
        IF EXISTS (SELECT 1 FROM [dbo].[TempUserCredentials] WHERE [TempUserID] = @TempUserID)
        BEGIN
            INSERT INTO [dbo].[UserCredentials] (
                [UserID], [Username], [PasswordHash], [PasswordSalt], 
                [LastPasswordChangeDate], [FailedLoginAttempts], [AccountLockedUntil], 
                [IsEnabled], [CreatedAt], [UpdatedAt]
            )
            SELECT 
                @NewUserID, [Username], [PasswordHash], [PasswordSalt], 
                [LastPasswordChangeDate], [FailedLoginAttempts], [AccountLockedUntil], 
                1, GETDATE(), GETDATE()
            FROM [dbo].[TempUserCredentials]
            WHERE [TempUserID] = @TempUserID;
        END
        
        -- 3. نقل بيانات اللغات
        IF EXISTS (SELECT 1 FROM [dbo].[TempUserLanguages] WHERE [TempUserID] = @TempUserID)
        BEGIN
            INSERT INTO [dbo].[UserLanguages] (
                [UserID], [LanguageOtherName], [ProficiencyLevelID], [IsActive], [CDate]
            )
            SELECT 
                @NewUserID, [LanguageOtherName], [ProficiencyLevelID], 1, GETDATE()
            FROM [dbo].[TempUserLanguages]
            WHERE [TempUserID] = @TempUserID;
        END
        
        -- 4. نقل بيانات المؤهلات العلمية
        IF EXISTS (SELECT 1 FROM [dbo].[TempScientificDegrees] WHERE [TempUserID] = @TempUserID)
        BEGIN
            INSERT INTO [dbo].[ScientificDegrees] (
                [UserID], [DegreeTypeID], [CollegeID], [QualificationMinorID], 
                [UniversityOtherName], [CollegeOtherName], [MajorOtherName], 
                [MinorOtherName], [IsActive], [CDate]
            )
            SELECT 
                @NewUserID, [DegreeTypeID], [CollegeID], [QualificationMinorID], 
                [UniversityOtherName], [CollegeOtherName], [MajorOtherName], 
                [MinorOtherName], 1, GETDATE()
            FROM [dbo].[TempScientificDegrees]
            WHERE [TempUserID] = @TempUserID;
        END
        
        -- 5. نقل بيانات المرفقات
        IF EXISTS (SELECT 1 FROM [dbo].[TempUserAttachments] WHERE [TempUserID] = @TempUserID)
        BEGIN
            INSERT INTO [dbo].[UserAttachments] (
                [UserID], [AttachmentTypeID], [FileName], [FilePath], [FileSize], 
                [ContentType], [UploadedAt], [IsVerified], [VerifiedBy], 
                [VerifiedAt], [Notes], [IsActive]
            )
            SELECT 
                @NewUserID, [AttachmentTypeID], [FileName], [FilePath], [FileSize], 
                [ContentType], [UploadedAt], [IsVerified], [VerifiedBy], 
                [VerifiedAt], [Notes], 1
            FROM [dbo].[TempUserAttachments]
            WHERE [TempUserID] = @TempUserID;
        END
        
        -- 6. نقل بيانات أماكن العمل
        IF EXISTS (SELECT 1 FROM [dbo].[TempWorkPlaces] WHERE [TempUserID] = @TempUserID)
        BEGIN
            INSERT INTO [dbo].[WorkPlaces] (
                [UserID], [WorkPlaceTypeID], [JobTitle], [WorkPlaceName], [CityID], 
                [CollegeID], [IsCurrent], [UniversityOtherName], [CollegeOtherName], 
                [IsActive], [CDate]
            )
            SELECT 
                @NewUserID, [WorkPlaceTypeID], [JobTitle], [WorkPlaceName], [CityID], 
                [CollegeID], [IsCurrent], [UniversityOtherName], [CollegeOtherName], 
                1, GETDATE()
            FROM [dbo].[TempWorkPlaces]
            WHERE [TempUserID] = @TempUserID;
        END
        
        -- 7. تحديث بيانات المراجعة للمستخدم المؤقت
        UPDATE [dbo].[TempUsers]
        SET [ReviewedBy] = @ApprovedBy,
            [ReviewedAt] = GETDATE(),
            [RejectionReason] = 'Transferred to permanent user'
        WHERE [TempUserID] = @TempUserID;
        
        -- 8. تسجيل العملية في سجل التعديلات
        INSERT INTO [dbo].[UserModificationLog] (
            [UserID], [ModifiedBy], [ModifiedAt], [ModificationTypeID], 
            [TableName], [RecordID], [OldValues], [NewValues], [IPAddress], [SystemID]
        )
        VALUES (
            @NewUserID, @ApprovedBy, GETDATE(), 
            (SELECT TOP 1 [ModificationTypeID] FROM [dbo].[ModificationTypes] 
             WHERE [ModificationTypeName] = 'User Promotion'),
            'Users', @NewUserID, 
            'TempUserID: ' + CAST(@TempUserID AS VARCHAR(10)), 
            'User promoted from temporary to permanent', NULL, NULL
        );
        
        COMMIT TRANSACTION @TranName;
        RETURN 0; -- نجاح العملية
    END TRY
    BEGIN CATCH
        SET @Error = ERROR_NUMBER();
        SET @ErrorMessage = ERROR_MESSAGE();
        
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION @TranName;
            
        -- تسجيل الخطأ
        INSERT INTO [dbo].[UserModificationLog] (
            [UserID], [ModifiedBy], [ModifiedAt], [ModificationTypeID], 
            [TableName], [RecordID], [OldValues], [NewValues], [IPAddress], [SystemID]
        )
        VALUES (
            NULL, @ApprovedBy, GETDATE(), 
            (SELECT TOP 1 [ModificationTypeID] FROM [dbo].[ModificationTypes] 
             WHERE [ModificationTypeName] = 'Error'),
            'TempUsers', @TempUserID, 
            @ErrorMessage, 'Failed to promote user', NULL, NULL
        );
        
        -- إعادة رمي الخطأ مع رسالة مخصصة
        RAISERROR('فشل في نقل المستخدم: %s', 16, 1, @ErrorMessage);
        RETURN @Error;
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[ValidateLogin]    Script Date: 10/6/2025 10:50:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ValidateLogin]
    @Username NVARCHAR(100),
    @Password NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @UserID INT, @Hash NVARCHAR(256), @Salt NVARCHAR(256), 
            @IsEnabled BIT, @AccountLocked BIT, @AccountLockedUntil DATETIME,
            @FailedAttempts INT, @PasswordExpiryDate DATETIME;
    
    SELECT 
        @UserID = uc.UserID,
        @Hash = uc.PasswordHash,
        @Salt = uc.PasswordSalt,
        @IsEnabled = uc.IsEnabled,
        @AccountLocked = uc.AccountLocked,
        @AccountLockedUntil = uc.AccountLockedUntil,
        @FailedAttempts = uc.FailedLoginAttempts,
        @PasswordExpiryDate = uc.PasswordExpiryDate
    FROM UserCredentials uc
    JOIN Users u ON uc.UserID = u.UserID
    WHERE uc.Username = @Username;
    
    IF @UserID IS NULL
    BEGIN
        RAISERROR('اسم المستخدم غير موجود', 16, 1);
        RETURN;
    END
    
    IF @IsEnabled = 0
    BEGIN
        RAISERROR('الحساب غير مفعل', 16, 1);
        RETURN;
    END
    
    IF @AccountLocked = 1 AND (@AccountLockedUntil IS NULL OR @AccountLockedUntil > GETDATE())
    BEGIN
        RAISERROR('الحساب مقفل مؤقتاً', 16, 1);
        RETURN;
    END
    
    IF HASHBYTES('SHA2_512', @Password + @Salt) <> @Hash
    BEGIN
        UPDATE UserCredentials
        SET FailedLoginAttempts = @FailedAttempts + 1
        WHERE UserID = @UserID;
        
        IF @FailedAttempts + 1 >= 5
        BEGIN
            UPDATE UserCredentials
            SET 
                AccountLocked = 1,
                AccountLockedUntil = DATEADD(MINUTE, 30, GETDATE())
            WHERE UserID = @UserID;
        END
        
        RAISERROR('كلمة المرور غير صحيحة', 16, 1);
        RETURN;
    END
    
    IF @PasswordExpiryDate < GETDATE()
    BEGIN
        RAISERROR('انتهت صلاحية كلمة المرور', 16, 1);
        RETURN;
    END
    
    UPDATE UserCredentials
    SET FailedLoginAttempts = 0
    WHERE UserID = @UserID;
    
    SELECT @UserID AS UserID;
END;

GO
USE [master]
GO
ALTER DATABASE [MasterPassport] SET  READ_WRITE 
GO
