USE [master]
GO
/****** Object:  Database [spf_orders_db]    Script Date: 4/23/2025 5:19:18 PM ******/
CREATE DATABASE [spf_orders_db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'spf_orders_db', FILENAME = N'/var/opt/mssql/data/spf_orders_db.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'spf_orders_db_log', FILENAME = N'/var/opt/mssql/data/spf_orders_db_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [spf_orders_db] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [spf_orders_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [spf_orders_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [spf_orders_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [spf_orders_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [spf_orders_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [spf_orders_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [spf_orders_db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [spf_orders_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [spf_orders_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [spf_orders_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [spf_orders_db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [spf_orders_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [spf_orders_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [spf_orders_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [spf_orders_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [spf_orders_db] SET  ENABLE_BROKER 
GO
ALTER DATABASE [spf_orders_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [spf_orders_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [spf_orders_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [spf_orders_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [spf_orders_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [spf_orders_db] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [spf_orders_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [spf_orders_db] SET RECOVERY FULL 
GO
ALTER DATABASE [spf_orders_db] SET  MULTI_USER 
GO
ALTER DATABASE [spf_orders_db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [spf_orders_db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [spf_orders_db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [spf_orders_db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [spf_orders_db] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [spf_orders_db] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'spf_orders_db', N'ON'
GO
ALTER DATABASE [spf_orders_db] SET QUERY_STORE = ON
GO
ALTER DATABASE [spf_orders_db] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [spf_orders_db]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 4/23/2025 5:19:19 PM ******/
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
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 4/23/2025 5:19:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 4/23/2025 5:19:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[TotalPrice] [decimal](18, 0) NOT NULL,
	[OrderDate] [datetime2](7) NOT NULL,
	[OrderStatus] [tinyint] NOT NULL,
	[CustomerName] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[StoreId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250306102537_InitialMigration', N'8.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250310021741_AddStoreIdColumnForOrders', N'8.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250410103842_AddOnDeleteBehaviorForOrderDetails', N'8.0.12')
GO
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'8493559d-eb92-46d5-9396-014410d61db7', N'3d30a7b9-2711-4ae8-bb77-7845c21d0c99', N'332fc7da-b0be-4533-bf67-2909020ff689', N'Combo Cơm Trưa Phi-lê Gà Quay', 1, CAST(59000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'2dec0cc8-8d2f-43ef-9455-0562a146f430', N'8950b287-87ea-4943-a273-b82315067e0a', N'2f5dbe05-3b35-4b1f-9e37-9c46f2cb473c', N'Combo Deal Hời 55k', 8, CAST(79000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'614dc27f-b7bc-491d-a465-06fbe5bc1080', N'f4f25bda-b43d-4268-9e25-c013df9fc79d', N'2f5dbe05-3b35-4b1f-9e37-9c46f2cb473c', N'Combo Deal Hời 55k', 1, CAST(79000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'1cb143d4-1d59-4a7e-b627-13a6ff9a5370', N'f5cc6653-e949-478c-bc98-d466a3248314', N'332fc7da-b0be-4533-bf67-2909020ff689', N'Combo Cơm Trưa Phi-lê Gà Quay', 1, CAST(59000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'c053a34f-da52-49cd-9fae-20631bd08bd1', N'b0a40de0-06c6-4534-b5e0-fa38fdaa990a', N'2f5dbe05-3b35-4b1f-9e37-9c46f2cb473c', N'Combo Deal Hời 55k', 1, CAST(79000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'e713e7b9-5d8f-4910-bd02-2d980a29b14f', N'ffc6b82e-ff23-41e6-8f54-c1153ad0f10b', N'2f5dbe05-3b35-4b1f-9e37-9c46f2cb473c', N'Combo Deal Hời 55k', 1, CAST(79000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'9ed4fdf8-9291-489e-82ae-31cbfd9d582d', N'fbfba446-db18-4aa1-ab65-c5b9e10b9a58', N'2f5dbe05-3b35-4b1f-9e37-9c46f2cb473c', N'Combo Deal Hời 55k', 1, CAST(79000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'191bab3f-15e0-469d-aabc-372a1ac874cd', N'966ac98e-5364-4c4c-ab5f-941fb81177d0', N'332fc7da-b0be-4533-bf67-2909020ff689', N'Combo Cơm Trưa Phi-lê Gà Quay', 1, CAST(59000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'13c03474-5926-4afc-9da3-4c19e82d45b0', N'da729d55-af16-471e-976c-6f6302213c9f', N'2f5dbe05-3b35-4b1f-9e37-9c46f2cb473c', N'Combo Deal Hời 55k', 2, CAST(79000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'c022d9cf-f1e9-457c-ad51-86270767363a', N'84d9553c-15a2-4ab2-a557-37bcd334b4c9', N'2f5dbe05-3b35-4b1f-9e37-9c46f2cb473c', N'Combo Deal Hời 55k', 1, CAST(79000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'4b4bfa9d-effb-44b3-bc8a-8e0780d920b6', N'bd6db35b-7a7a-450d-806c-9752c229534d', N'2f5dbe05-3b35-4b1f-9e37-9c46f2cb473c', N'Combo Deal Hời 55k', 1, CAST(79000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'49c3dc8b-daa1-4130-81b8-b4f68aebf32f', N'a5ff88d8-3c59-4841-93ea-99a073f74379', N'2f5dbe05-3b35-4b1f-9e37-9c46f2cb473c', N'Combo Deal Hời 55k', 1, CAST(79000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'd66de6dc-a6d7-426b-b49f-b9d12266c43b', N'4f5ea4cd-0a5f-4247-9949-2c666ff86443', N'2f5dbe05-3b35-4b1f-9e37-9c46f2cb473c', N'Combo Deal Hời 55k', 1, CAST(79000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'05155e4d-4dac-4c75-adeb-b9dbf1742fe9', N'792aeaf4-91eb-47fd-a817-86bc598faa11', N'2f5dbe05-3b35-4b1f-9e37-9c46f2cb473c', N'Combo Deal Hời 55k', 1, CAST(79000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'4fd1ed46-2e5c-440d-bc7c-b9e0b889d13a', N'a5ff88d8-3c59-4841-93ea-99a073f74379', N'332fc7da-b0be-4533-bf67-2909020ff689', N'Combo Cơm Trưa Phi-lê Gà Quay', 1, CAST(59000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'aa41b604-3e2a-42c1-a82b-c84d671a3856', N'8950b287-87ea-4943-a273-b82315067e0a', N'332fc7da-b0be-4533-bf67-2909020ff689', N'Combo Cơm Trưa Phi-lê Gà Quay', 13, CAST(59000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'028d6695-d687-4b30-ab91-deb83303d97f', N'33312318-a0c0-4a38-9a30-60402c849595', N'2f5dbe05-3b35-4b1f-9e37-9c46f2cb473c', N'Combo Deal Hời 55k', 2, CAST(79000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'e9bdd6b8-f27b-48b4-af31-e0397629d03e', N'5ece946c-eaa0-4cb4-8875-013e70b8619b', N'332fc7da-b0be-4533-bf67-2909020ff689', N'Combo Cơm Trưa Phi-lê Gà Quay', 1, CAST(59000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'58952e40-d6a4-4871-97db-f02ffedd6195', N'0096fb1d-8b52-4502-8f9c-d395d82b3597', N'2f5dbe05-3b35-4b1f-9e37-9c46f2cb473c', N'Combo Deal Hời 55k', 2, CAST(79000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'379c137c-5afa-40af-93bd-f12a3f9c25d4', N'966ac98e-5364-4c4c-ab5f-941fb81177d0', N'2f5dbe05-3b35-4b1f-9e37-9c46f2cb473c', N'Combo Deal Hời 55k', 1, CAST(79000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [Price]) VALUES (N'3dc5e8ad-96a6-41a9-a07c-f771ea68c078', N'f4f25bda-b43d-4268-9e25-c013df9fc79d', N'332fc7da-b0be-4533-bf67-2909020ff689', N'Combo Cơm Trưa Phi-lê Gà Quay', 1, CAST(59000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Orders] ([Id], [CustomerId], [TotalPrice], [OrderDate], [OrderStatus], [CustomerName], [PhoneNumber], [Address], [StoreId]) VALUES (N'5ece946c-eaa0-4cb4-8875-013e70b8619b', N'8d018a26-28b6-4767-11cb-08dd772ff774', CAST(59000 AS Decimal(18, 0)), CAST(N'2025-04-15T04:53:19.3020657' AS DateTime2), 0, N'John Ng tttt', N'0236548972', N'12 NVT, Thạnh Lộc, Quận 12, Hồ Chí Minh', N'906bf960-417d-4697-9bbb-5d351e455bfe')
INSERT [dbo].[Orders] ([Id], [CustomerId], [TotalPrice], [OrderDate], [OrderStatus], [CustomerName], [PhoneNumber], [Address], [StoreId]) VALUES (N'4f5ea4cd-0a5f-4247-9949-2c666ff86443', N'8d018a26-28b6-4767-11cb-08dd772ff774', CAST(55000 AS Decimal(18, 0)), CAST(N'2025-04-15T03:57:11.6502903' AS DateTime2), 0, N'John Ng Testttt', N'0236548972', N'12 NVT, Trúc Bạch, Ba Đình, Hà Nội', N'906bf960-417d-4697-9bbb-5d351e455bfe')
INSERT [dbo].[Orders] ([Id], [CustomerId], [TotalPrice], [OrderDate], [OrderStatus], [CustomerName], [PhoneNumber], [Address], [StoreId]) VALUES (N'84d9553c-15a2-4ab2-a557-37bcd334b4c9', N'8d018a26-28b6-4767-11cb-08dd772ff774', CAST(55000 AS Decimal(18, 0)), CAST(N'2025-04-10T11:11:47.9884173' AS DateTime2), 0, N'John Ng', N'0236548972', N'12 NVT, Thạnh Xuân, Quận 12, Hồ Chí Minh', N'906bf960-417d-4697-9bbb-5d351e455bfe')
INSERT [dbo].[Orders] ([Id], [CustomerId], [TotalPrice], [OrderDate], [OrderStatus], [CustomerName], [PhoneNumber], [Address], [StoreId]) VALUES (N'33312318-a0c0-4a38-9a30-60402c849595', N'8d018a26-28b6-4767-11cb-08dd772ff774', CAST(110000 AS Decimal(18, 0)), CAST(N'2025-04-10T11:26:33.1663277' AS DateTime2), 0, N'John Ng', N'0236548972', N'12 NVT, Thạnh Lộc, Quận 12, Hồ Chí Minh', N'906bf960-417d-4697-9bbb-5d351e455bfe')
INSERT [dbo].[Orders] ([Id], [CustomerId], [TotalPrice], [OrderDate], [OrderStatus], [CustomerName], [PhoneNumber], [Address], [StoreId]) VALUES (N'da729d55-af16-471e-976c-6f6302213c9f', N'8d018a26-28b6-4767-11cb-08dd772ff774', CAST(110000 AS Decimal(18, 0)), CAST(N'2025-04-14T05:34:27.8932519' AS DateTime2), 0, N'John Ng Wife', N'0236548972', N'rtyhwretuh, Thạnh Xuân, Quận 12, Hồ Chí Minh', N'906bf960-417d-4697-9bbb-5d351e455bfe')
INSERT [dbo].[Orders] ([Id], [CustomerId], [TotalPrice], [OrderDate], [OrderStatus], [CustomerName], [PhoneNumber], [Address], [StoreId]) VALUES (N'3d30a7b9-2711-4ae8-bb77-7845c21d0c99', N'8d018a26-28b6-4767-11cb-08dd772ff774', CAST(59000 AS Decimal(18, 0)), CAST(N'2025-04-10T10:57:29.0958790' AS DateTime2), 0, N'John Ng', N'0236548972', N'sdSasd, Thạnh Xuân, Quận 12, Hồ Chí Minh', N'906bf960-417d-4697-9bbb-5d351e455bfe')
INSERT [dbo].[Orders] ([Id], [CustomerId], [TotalPrice], [OrderDate], [OrderStatus], [CustomerName], [PhoneNumber], [Address], [StoreId]) VALUES (N'792aeaf4-91eb-47fd-a817-86bc598faa11', N'8d018a26-28b6-4767-11cb-08dd772ff774', CAST(55000 AS Decimal(18, 0)), CAST(N'2025-04-15T03:46:57.4075247' AS DateTime2), 0, N'My test Ng', N'0236548972', N'12 NVT, Ngọc Thụy, Long Biên, Hà Nội', N'906bf960-417d-4697-9bbb-5d351e455bfe')
INSERT [dbo].[Orders] ([Id], [CustomerId], [TotalPrice], [OrderDate], [OrderStatus], [CustomerName], [PhoneNumber], [Address], [StoreId]) VALUES (N'966ac98e-5364-4c4c-ab5f-941fb81177d0', N'8d018a26-28b6-4767-11cb-08dd772ff774', CAST(114000 AS Decimal(18, 0)), CAST(N'2025-04-15T08:31:34.2067842' AS DateTime2), 0, N'John Ng', N'0236548972', N'12 NVT, Đồng Xuân, Hoàn Kiếm, Hà Nội', N'906bf960-417d-4697-9bbb-5d351e455bfe')
INSERT [dbo].[Orders] ([Id], [CustomerId], [TotalPrice], [OrderDate], [OrderStatus], [CustomerName], [PhoneNumber], [Address], [StoreId]) VALUES (N'bd6db35b-7a7a-450d-806c-9752c229534d', N'8d018a26-28b6-4767-11cb-08dd772ff774', CAST(55000 AS Decimal(18, 0)), CAST(N'2025-04-10T11:17:05.5946796' AS DateTime2), 0, N'John Ng', N'0236548972', N'12 NVT, Thạnh Lộc, Quận 12, Hồ Chí Minh', N'906bf960-417d-4697-9bbb-5d351e455bfe')
INSERT [dbo].[Orders] ([Id], [CustomerId], [TotalPrice], [OrderDate], [OrderStatus], [CustomerName], [PhoneNumber], [Address], [StoreId]) VALUES (N'a5ff88d8-3c59-4841-93ea-99a073f74379', N'8d018a26-28b6-4767-11cb-08dd772ff774', CAST(114000 AS Decimal(18, 0)), CAST(N'2025-04-15T08:27:10.6200300' AS DateTime2), 0, N'John Ng', N'0236548972', N'12 NVT, Phường Phường17, Gò Vấp, Hồ Chí Minh', N'906bf960-417d-4697-9bbb-5d351e455bfe')
INSERT [dbo].[Orders] ([Id], [CustomerId], [TotalPrice], [OrderDate], [OrderStatus], [CustomerName], [PhoneNumber], [Address], [StoreId]) VALUES (N'8950b287-87ea-4943-a273-b82315067e0a', N'8d018a26-28b6-4767-11cb-08dd772ff774', CAST(1207000 AS Decimal(18, 0)), CAST(N'2025-04-15T09:56:45.3133288' AS DateTime2), 0, N'John Ng', N'0236548972', N'sdSasd, Lũng Cú, Đồng Văn, Hà Giang', N'906bf960-417d-4697-9bbb-5d351e455bfe')
INSERT [dbo].[Orders] ([Id], [CustomerId], [TotalPrice], [OrderDate], [OrderStatus], [CustomerName], [PhoneNumber], [Address], [StoreId]) VALUES (N'f4f25bda-b43d-4268-9e25-c013df9fc79d', N'8d018a26-28b6-4767-11cb-08dd772ff774', CAST(114000 AS Decimal(18, 0)), CAST(N'2025-04-15T08:34:49.2198684' AS DateTime2), 0, N'John Ng :>:>:>', N'0236548972', N'12 NVT, Hàng Mã, Hoàn Kiếm, Hà Nội', N'906bf960-417d-4697-9bbb-5d351e455bfe')
INSERT [dbo].[Orders] ([Id], [CustomerId], [TotalPrice], [OrderDate], [OrderStatus], [CustomerName], [PhoneNumber], [Address], [StoreId]) VALUES (N'ffc6b82e-ff23-41e6-8f54-c1153ad0f10b', N'8d018a26-28b6-4767-11cb-08dd772ff774', CAST(55000 AS Decimal(18, 0)), CAST(N'2025-04-11T10:22:08.1569791' AS DateTime2), 0, N'John Ng', N'0236548972', N'12 NVT, PhườngPhường16, Gò Vấp, Hồ Chí Minh', N'906bf960-417d-4697-9bbb-5d351e455bfe')
INSERT [dbo].[Orders] ([Id], [CustomerId], [TotalPrice], [OrderDate], [OrderStatus], [CustomerName], [PhoneNumber], [Address], [StoreId]) VALUES (N'fbfba446-db18-4aa1-ab65-c5b9e10b9a58', N'8d018a26-28b6-4767-11cb-08dd772ff774', CAST(55000 AS Decimal(18, 0)), CAST(N'2025-04-15T07:26:36.4431579' AS DateTime2), 0, N'John Ng Hehe', N'0236548972', N'12 NVT, Phường Phường17, Gò Vấp, Hồ Chí Minh', N'906bf960-417d-4697-9bbb-5d351e455bfe')
INSERT [dbo].[Orders] ([Id], [CustomerId], [TotalPrice], [OrderDate], [OrderStatus], [CustomerName], [PhoneNumber], [Address], [StoreId]) VALUES (N'0096fb1d-8b52-4502-8f9c-d395d82b3597', N'8d018a26-28b6-4767-11cb-08dd772ff774', CAST(110000 AS Decimal(18, 0)), CAST(N'2025-04-11T02:44:27.9187505' AS DateTime2), 0, N'John Ng', N'0236548972', N'sdSasd, Ngọc Thụy, Long Biên, Hà Nội', N'906bf960-417d-4697-9bbb-5d351e455bfe')
INSERT [dbo].[Orders] ([Id], [CustomerId], [TotalPrice], [OrderDate], [OrderStatus], [CustomerName], [PhoneNumber], [Address], [StoreId]) VALUES (N'f5cc6653-e949-478c-bc98-d466a3248314', N'8d018a26-28b6-4767-11cb-08dd772ff774', CAST(59000 AS Decimal(18, 0)), CAST(N'2025-04-15T04:30:08.0683087' AS DateTime2), 0, N'John Ng teeeeee', N'0236548972', N'12 NVT, Phường Phường12, Gò Vấp, Hồ Chí Minh', N'906bf960-417d-4697-9bbb-5d351e455bfe')
INSERT [dbo].[Orders] ([Id], [CustomerId], [TotalPrice], [OrderDate], [OrderStatus], [CustomerName], [PhoneNumber], [Address], [StoreId]) VALUES (N'b0a40de0-06c6-4534-b5e0-fa38fdaa990a', N'8d018a26-28b6-4767-11cb-08dd772ff774', CAST(55000 AS Decimal(18, 0)), CAST(N'2025-04-15T04:33:00.1341625' AS DateTime2), 0, N'John Ng ttttttt', N'0236548972', N'12 NVT, Tứ Liên, Tây Hồ, Hà Nội', N'906bf960-417d-4697-9bbb-5d351e455bfe')
GO
/****** Object:  Index [IX_OrderDetails_OrderId]    Script Date: 4/23/2025 5:19:19 PM ******/
CREATE NONCLUSTERED INDEX [IX_OrderDetails_OrderId] ON [dbo].[OrderDetails]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [StoreId]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders_OrderId]
GO
USE [master]
GO
ALTER DATABASE [spf_orders_db] SET  READ_WRITE 
GO
