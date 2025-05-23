USE [master]
GO
/****** Object:  Database [spf_users_db]    Script Date: 4/23/2025 5:16:58 PM ******/
CREATE DATABASE [spf_users_db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'spf_users_db', FILENAME = N'/var/opt/mssql/data/spf_users_db.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'spf_users_db_log', FILENAME = N'/var/opt/mssql/data/spf_users_db_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [spf_users_db] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [spf_users_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [spf_users_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [spf_users_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [spf_users_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [spf_users_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [spf_users_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [spf_users_db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [spf_users_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [spf_users_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [spf_users_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [spf_users_db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [spf_users_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [spf_users_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [spf_users_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [spf_users_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [spf_users_db] SET  ENABLE_BROKER 
GO
ALTER DATABASE [spf_users_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [spf_users_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [spf_users_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [spf_users_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [spf_users_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [spf_users_db] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [spf_users_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [spf_users_db] SET RECOVERY FULL 
GO
ALTER DATABASE [spf_users_db] SET  MULTI_USER 
GO
ALTER DATABASE [spf_users_db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [spf_users_db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [spf_users_db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [spf_users_db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [spf_users_db] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [spf_users_db] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'spf_users_db', N'ON'
GO
ALTER DATABASE [spf_users_db] SET QUERY_STORE = ON
GO
ALTER DATABASE [spf_users_db] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [spf_users_db]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 4/23/2025 5:16:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppRoles]    Script Date: 4/23/2025 5:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppRoles](
	[Id] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](100) NULL,
	[Name] [nvarchar](30) NOT NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AppRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppUsers]    Script Date: 4/23/2025 5:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUsers](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](30) NULL,
	[LastName] [nvarchar](50) NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[Address] [nvarchar](max) NULL,
 CONSTRAINT [PK_AppUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 4/23/2025 5:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 4/23/2025 5:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 4/23/2025 5:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 4/23/2025 5:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 4/23/2025 5:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [uniqueidentifier] NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshTokens]    Script Date: 4/23/2025 5:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshTokens](
	[Id] [uniqueidentifier] NOT NULL,
	[AppUserId] [uniqueidentifier] NOT NULL,
	[Token] [nvarchar](200) NOT NULL,
	[ExpireTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_RefreshTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250226082905_InitialMigration', N'8.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250404075426_AddAddressColumnForUsers', N'8.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250409062414_UpdateRefreshTokensConstraints', N'8.0.12')
GO
INSERT [dbo].[AppRoles] ([Id], [Description], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'9e75b1d1-58a1-4a9a-ada9-39a545d6e5e5', N'Vendor role', N'Vendor', N'VENDOR', N'00000000-0000-0000-0000-000000000000')
INSERT [dbo].[AppRoles] ([Id], [Description], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'913ae6d5-7bef-43e8-8db3-8a162a71518f', N'Customer role', N'Customer', N'CUSTOMER', N'00000000-0000-0000-0000-000000000000')
INSERT [dbo].[AppRoles] ([Id], [Description], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'ceaf2fd0-ab94-43e4-9a50-d8f37b7377c9', N'Administrator role', N'Admin', N'ADMIN', N'00000000-0000-0000-0000-000000000000')
GO
INSERT [dbo].[AppUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address]) VALUES (N'8a3943a5-df25-4687-11ca-08dd772ff774', N'Adriel', N'Nguyen', N'supermainadmin', N'SUPERMAINADMIN', N'supermainadmin@example.com', N'SUPERMAINADMIN@EXAMPLE.COM', 0, N'AQAAAAIAAYagAAAAEJrU8advWnDVVF7vtDwH4rySAZUWAGNpxZBNWYkGHXWOKPGEh5GTd8+fjYcqcT2zPQ==', N'RA2NF6PIGXTU6CDKQYPGKX5UVPZP4C67', N'184ec851-a092-4bb0-9d3a-efc570f52557', N'0236321542', 0, 0, NULL, 1, 0, NULL)
INSERT [dbo].[AppUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address]) VALUES (N'8d018a26-28b6-4767-11cb-08dd772ff774', N'John', N'Ng', N'customer1', N'CUSTOMER1', N'customer1@example.com', N'CUSTOMER1@EXAMPLE.COM', 0, N'AQAAAAIAAYagAAAAEBkVgfcKAoOojlm3rQPGkfydodzfoJD6EpNmcyBZmjJnPBidEIKLvhblcVwY8A3R2w==', N'PYL5QCYTZXV627J2UM77QNBIOXUHQNSH', N'a3fe96e1-2128-4637-b859-98b350906821', N'0258741452', 0, 0, NULL, 1, 0, NULL)
INSERT [dbo].[AppUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address]) VALUES (N'c4330029-35c4-4ba1-67be-08dd78aa5700', N'John', N'Greg', N'vendor1', N'VENDOR1', N'vendor1@example.com', N'VENDOR1@EXAMPLE.COM', 0, N'AQAAAAIAAYagAAAAEGvRCyq3kw1HsDYOAEP5elpf/eeOdme0iXn4JG/4eXnTS+9OhrWAoK+nV4Y+wykBdQ==', N'VQXKFCU3RVY4MKB65HCRMWNNYFUAQWJ2', N'cd516b6a-9d46-4fe6-a680-a691c099f0d3', N'0254879963', 0, 0, NULL, 1, 0, NULL)
INSERT [dbo].[AppUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address]) VALUES (N'4f8a9993-3482-4fcd-5f8c-08dd7b3f5063', N'Denny', N'Dumand', N'customer2', N'CUSTOMER2', N'customer2@example.com', N'CUSTOMER2@EXAMPLE.COM', 0, N'AQAAAAIAAYagAAAAEEpz1aeNzWbxU5jK2qI4pnOMKVaUO2pmMeUGoh/WNr1cSB4CI9IkVuKjCb51CJimjg==', N'CLJALPZ5ZIJLTDYMPWZU6RA2IXWQZEM6', N'4d1845ba-a695-4bd2-bf56-668992371931', N'0236548633', 0, 0, NULL, 1, 0, NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c4330029-35c4-4ba1-67be-08dd78aa5700', N'9e75b1d1-58a1-4a9a-ada9-39a545d6e5e5')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8d018a26-28b6-4767-11cb-08dd772ff774', N'913ae6d5-7bef-43e8-8db3-8a162a71518f')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4f8a9993-3482-4fcd-5f8c-08dd7b3f5063', N'913ae6d5-7bef-43e8-8db3-8a162a71518f')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8a3943a5-df25-4687-11ca-08dd772ff774', N'ceaf2fd0-ab94-43e4-9a50-d8f37b7377c9')
GO
INSERT [dbo].[RefreshTokens] ([Id], [AppUserId], [Token], [ExpireTime]) VALUES (N'598ffb44-2e27-449f-871d-373ba2169f95', N'4f8a9993-3482-4fcd-5f8c-08dd7b3f5063', N'4AqJi3CY+8FyLX5eVSGGHfbLu+JZSGNr9h0KMefy5u4=', CAST(N'2025-04-22T07:25:18.9211123' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [AppUserId], [Token], [ExpireTime]) VALUES (N'2c8e00bb-70d4-45a8-862e-c806230de0c0', N'8a3943a5-df25-4687-11ca-08dd772ff774', N'QX5wMZUJz323va/6TG5IZqB/sHLeNuA9+veFOGuKP4o=', CAST(N'2025-04-30T08:07:09.6575037' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [AppUserId], [Token], [ExpireTime]) VALUES (N'd08b96c9-3cd5-48b5-9ffd-ed2ecc282e34', N'8d018a26-28b6-4767-11cb-08dd772ff774', N'rtX9UbK4KJmVX0Etf/3uOeActfibhgLAKg/clXtAUE0=', CAST(N'2025-04-25T09:37:29.7660948' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [AppUserId], [Token], [ExpireTime]) VALUES (N'2f677436-4a31-467e-97f6-ef4099a2a908', N'c4330029-35c4-4ba1-67be-08dd78aa5700', N'2IoMTfMradZoB5Dl0K7FF4qBL/t7j4rZwIjHUSXoyaY=', CAST(N'2025-04-30T10:02:06.1674418' AS DateTime2))
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AppRoles_Name]    Script Date: 4/23/2025 5:16:59 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_AppRoles_Name] ON [dbo].[AppRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 4/23/2025 5:16:59 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AppRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 4/23/2025 5:16:59 PM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AppUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 4/23/2025 5:16:59 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AppUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 4/23/2025 5:16:59 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 4/23/2025 5:16:59 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 4/23/2025 5:16:59 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 4/23/2025 5:16:59 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RefreshTokens_AppUserId]    Script Date: 4/23/2025 5:16:59 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_RefreshTokens_AppUserId] ON [dbo].[RefreshTokens]
(
	[AppUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RefreshTokens_Token]    Script Date: 4/23/2025 5:16:59 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_RefreshTokens_Token] ON [dbo].[RefreshTokens]
(
	[Token] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AppRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AppRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AppRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AppUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AppUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AppUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AppUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AppUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AppUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AppRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AppRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AppRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AppUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AppUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AppUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AppUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AppUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AppUsers_UserId]
GO
ALTER TABLE [dbo].[RefreshTokens]  WITH CHECK ADD  CONSTRAINT [FK_RefreshTokens_AppUsers_AppUserId] FOREIGN KEY([AppUserId])
REFERENCES [dbo].[AppUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RefreshTokens] CHECK CONSTRAINT [FK_RefreshTokens_AppUsers_AppUserId]
GO
USE [master]
GO
ALTER DATABASE [spf_users_db] SET  READ_WRITE 
GO
