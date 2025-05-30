USE [master]
GO
/****** Object:  Database [spf_products_db]    Script Date: 4/23/2025 5:18:08 PM ******/
CREATE DATABASE [spf_products_db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'spf_products_db', FILENAME = N'/var/opt/mssql/data/spf_products_db.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'spf_products_db_log', FILENAME = N'/var/opt/mssql/data/spf_products_db_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [spf_products_db] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [spf_products_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [spf_products_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [spf_products_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [spf_products_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [spf_products_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [spf_products_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [spf_products_db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [spf_products_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [spf_products_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [spf_products_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [spf_products_db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [spf_products_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [spf_products_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [spf_products_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [spf_products_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [spf_products_db] SET  ENABLE_BROKER 
GO
ALTER DATABASE [spf_products_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [spf_products_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [spf_products_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [spf_products_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [spf_products_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [spf_products_db] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [spf_products_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [spf_products_db] SET RECOVERY FULL 
GO
ALTER DATABASE [spf_products_db] SET  MULTI_USER 
GO
ALTER DATABASE [spf_products_db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [spf_products_db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [spf_products_db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [spf_products_db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [spf_products_db] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [spf_products_db] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'spf_products_db', N'ON'
GO
ALTER DATABASE [spf_products_db] SET QUERY_STORE = ON
GO
ALTER DATABASE [spf_products_db] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [spf_products_db]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 4/23/2025 5:18:08 PM ******/
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
/****** Object:  Table [dbo].[MenuProduct]    Script Date: 4/23/2025 5:18:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuProduct](
	[MenusId] [uniqueidentifier] NOT NULL,
	[ProductsId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_MenuProduct] PRIMARY KEY CLUSTERED 
(
	[MenusId] ASC,
	[ProductsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menus]    Script Date: 4/23/2025 5:18:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[Id] [uniqueidentifier] NOT NULL,
	[StoreId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[State] [tinyint] NOT NULL,
	[ConcurrencyStamp] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[LastUpdatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 4/23/2025 5:18:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [uniqueidentifier] NOT NULL,
	[StoreId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[AvailableStock] [int] NOT NULL,
	[BookingCount] [int] NOT NULL,
	[CoverImagePath] [nvarchar](max) NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[ConcurrencyStamp] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[LastUpdatedAt] [datetime2](7) NULL,
	[State] [tinyint] NOT NULL,
	[Discount] [decimal](18, 2) NOT NULL,
	[RateCount] [int] NOT NULL,
	[Rating] [float] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250304045615_InitialMigration', N'8.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250321095315_AdjustProductsColumn', N'8.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250327065733_AdjustColumnsContraints', N'8.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250327083855_AddColumnDiscountForProducts', N'8.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250328023735_AddMenusTable', N'8.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250328083546_AddRatingColumnsForProducts', N'8.0.12')
GO
INSERT [dbo].[MenuProduct] ([MenusId], [ProductsId]) VALUES (N'c06a6b35-7345-4f61-a0da-a072cabf36f0', N'68295306-0bdd-44d8-b1b9-17590cfd86f1')
INSERT [dbo].[MenuProduct] ([MenusId], [ProductsId]) VALUES (N'9b2ea81a-d1b8-4079-86c0-912dc49afea9', N'332fc7da-b0be-4533-bf67-2909020ff689')
INSERT [dbo].[MenuProduct] ([MenusId], [ProductsId]) VALUES (N'0c70ac3b-952d-487a-ae4f-7f4cdbbdd942', N'a18eb11b-18b4-4b30-9660-3141ac8f5bc6')
INSERT [dbo].[MenuProduct] ([MenusId], [ProductsId]) VALUES (N'c06a6b35-7345-4f61-a0da-a072cabf36f0', N'73c4dc76-0f35-4508-8c7d-31cfea16d437')
INSERT [dbo].[MenuProduct] ([MenusId], [ProductsId]) VALUES (N'c06a6b35-7345-4f61-a0da-a072cabf36f0', N'a80bd1ab-f4cc-4c8e-bc5d-5510b702b8c6')
INSERT [dbo].[MenuProduct] ([MenusId], [ProductsId]) VALUES (N'12ccf311-67f7-4250-aa22-715b88eb45ed', N'98c82a05-f06a-430f-9ac9-64e1afbee619')
INSERT [dbo].[MenuProduct] ([MenusId], [ProductsId]) VALUES (N'12ccf311-67f7-4250-aa22-715b88eb45ed', N'64140c4c-1d9f-47b7-a1f2-6ca306631d3f')
INSERT [dbo].[MenuProduct] ([MenusId], [ProductsId]) VALUES (N'dba69c53-2ffe-4d28-a883-b37eaf73553d', N'2f5dbe05-3b35-4b1f-9e37-9c46f2cb473c')
INSERT [dbo].[MenuProduct] ([MenusId], [ProductsId]) VALUES (N'0c70ac3b-952d-487a-ae4f-7f4cdbbdd942', N'd487a8c4-e410-49a5-b808-b3f2e7558eb1')
INSERT [dbo].[MenuProduct] ([MenusId], [ProductsId]) VALUES (N'12ccf311-67f7-4250-aa22-715b88eb45ed', N'118e43ae-81b7-4013-b7ec-c5f7b9e747b0')
INSERT [dbo].[MenuProduct] ([MenusId], [ProductsId]) VALUES (N'7112e06a-531a-46d1-bf27-2acecbdc347d', N'8040f001-ee6e-4b5e-8bed-e1670e984f8d')
INSERT [dbo].[MenuProduct] ([MenusId], [ProductsId]) VALUES (N'0c70ac3b-952d-487a-ae4f-7f4cdbbdd942', N'dc795c3b-db5f-46b9-bb6b-f2dd2bceea5d')
INSERT [dbo].[MenuProduct] ([MenusId], [ProductsId]) VALUES (N'0c70ac3b-952d-487a-ae4f-7f4cdbbdd942', N'7139b605-57e5-4474-95d3-f8db0ef90fb8')
GO
INSERT [dbo].[Menus] ([Id], [StoreId], [Title], [State], [ConcurrencyStamp], [CreatedDate], [LastUpdatedAt]) VALUES (N'7112e06a-531a-46d1-bf27-2acecbdc347d', N'3e7d2b56-371b-406c-9c76-faa4b7006a1a', N'Trio Deal Chỉ 98K - Mua 2 Được 3', 0, N'6b22a1c4-b92d-4494-98c1-270bf24782c0', CAST(N'2025-04-23T02:51:24.4700109' AS DateTime2), CAST(N'2025-04-23T02:52:44.9423302' AS DateTime2))
INSERT [dbo].[Menus] ([Id], [StoreId], [Title], [State], [ConcurrencyStamp], [CreatedDate], [LastUpdatedAt]) VALUES (N'12ccf311-67f7-4250-aa22-715b88eb45ed', N'3e7d2b56-371b-406c-9c76-faa4b7006a1a', N'Gà Sốt Bơ Tỏi Thảo Mộc', 0, N'ab0359b3-fd46-44d6-a352-129819fcd971', CAST(N'2025-04-23T03:04:16.6870033' AS DateTime2), CAST(N'2025-04-23T05:00:25.5094577' AS DateTime2))
INSERT [dbo].[Menus] ([Id], [StoreId], [Title], [State], [ConcurrencyStamp], [CreatedDate], [LastUpdatedAt]) VALUES (N'0c70ac3b-952d-487a-ae4f-7f4cdbbdd942', N'3e7d2b56-371b-406c-9c76-faa4b7006a1a', N'Món Ăn Kèm', 0, N'6424dfc1-c4eb-4299-8dcc-6071501e4308', CAST(N'2025-04-23T03:43:12.0890549' AS DateTime2), CAST(N'2025-04-23T04:23:33.0563915' AS DateTime2))
INSERT [dbo].[Menus] ([Id], [StoreId], [Title], [State], [ConcurrencyStamp], [CreatedDate], [LastUpdatedAt]) VALUES (N'9b2ea81a-d1b8-4079-86c0-912dc49afea9', N'906bf960-417d-4697-9bbb-5d351e455bfe', N'Trưa nay ăn gì', 0, N'3ecf31c1-5925-4da7-ad56-a5f45f2703ec', CAST(N'2025-03-28T04:20:42.6063910' AS DateTime2), CAST(N'2025-03-28T06:55:19.3405738' AS DateTime2))
INSERT [dbo].[Menus] ([Id], [StoreId], [Title], [State], [ConcurrencyStamp], [CreatedDate], [LastUpdatedAt]) VALUES (N'c06a6b35-7345-4f61-a0da-a072cabf36f0', N'3e7d2b56-371b-406c-9c76-faa4b7006a1a', N'Gà Giòn Truyền Thống', 0, N'0bb695c9-167c-4358-90c1-325e440f301b', CAST(N'2025-04-23T03:12:33.1261894' AS DateTime2), CAST(N'2025-04-23T03:32:55.8961133' AS DateTime2))
INSERT [dbo].[Menus] ([Id], [StoreId], [Title], [State], [ConcurrencyStamp], [CreatedDate], [LastUpdatedAt]) VALUES (N'dba69c53-2ffe-4d28-a883-b37eaf73553d', N'906bf960-417d-4697-9bbb-5d351e455bfe', N'Món đang giảm', 0, N'8f829d13-1ec4-4e58-8c6c-4bfd944758ec', CAST(N'2025-03-28T04:20:32.7083458' AS DateTime2), CAST(N'2025-03-28T06:48:24.0096572' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [StoreId], [Name], [Description], [AvailableStock], [BookingCount], [CoverImagePath], [Price], [ConcurrencyStamp], [CreatedAt], [LastUpdatedAt], [State], [Discount], [RateCount], [Rating]) VALUES (N'68295306-0bdd-44d8-b1b9-17590cfd86f1', N'3e7d2b56-371b-406c-9c76-faa4b7006a1a', N'Gà rán có xương (1 miếng)', N'1 Miếng gà có xương 1 Tương ớt + 1 tương cà', 0, 0, N'/stores/3e7d2b56-371b-406c-9c76-faa4b7006a1a/products/68295306-0bdd-44d8-b1b9-17590cfd86f1/cover-img.jpg', CAST(39000 AS Decimal(18, 0)), N'1f772e53-4228-4b50-8fde-ff4ab7faf846', CAST(N'2025-04-23T03:10:47.4502773' AS DateTime2), NULL, 0, CAST(0.00 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[Products] ([Id], [StoreId], [Name], [Description], [AvailableStock], [BookingCount], [CoverImagePath], [Price], [ConcurrencyStamp], [CreatedAt], [LastUpdatedAt], [State], [Discount], [RateCount], [Rating]) VALUES (N'332fc7da-b0be-4533-bf67-2909020ff689', N'906bf960-417d-4697-9bbb-5d351e455bfe', N'Combo Cơm Trưa Phi-lê Gà Quay', N'1 Cơm Phi-lê Gà Quay/Cơm Phi-lê Gà Quay Tiêu + 1 ly Pepsi (Vừa)', 0, 0, N'/stores/906bf960-417d-4697-9bbb-5d351e455bfe/products/332fc7da-b0be-4533-bf67-2909020ff689/cover-img.jpg', CAST(59000 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', CAST(N'2025-03-27T08:20:21.3722984' AS DateTime2), NULL, 0, CAST(0.00 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[Products] ([Id], [StoreId], [Name], [Description], [AvailableStock], [BookingCount], [CoverImagePath], [Price], [ConcurrencyStamp], [CreatedAt], [LastUpdatedAt], [State], [Discount], [RateCount], [Rating]) VALUES (N'a18eb11b-18b4-4b30-9660-3141ac8f5bc6', N'3e7d2b56-371b-406c-9c76-faa4b7006a1a', N'Khoai Tây Chiên Phô Mai', N'Khoai tây chiên nóng giòn kèm sốt phô mai béo ngậy', 0, 0, N'/stores/3e7d2b56-371b-406c-9c76-faa4b7006a1a/products/a18eb11b-18b4-4b30-9660-3141ac8f5bc6/cover-img.jpg', CAST(49000 AS Decimal(18, 0)), N'737d3db0-9a9c-4cec-a8c7-29a67593bb8c', CAST(N'2025-04-23T04:06:07.1151954' AS DateTime2), NULL, 0, CAST(0.00 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[Products] ([Id], [StoreId], [Name], [Description], [AvailableStock], [BookingCount], [CoverImagePath], [Price], [ConcurrencyStamp], [CreatedAt], [LastUpdatedAt], [State], [Discount], [RateCount], [Rating]) VALUES (N'73c4dc76-0f35-4508-8c7d-31cfea16d437', N'3e7d2b56-371b-406c-9c76-faa4b7006a1a', N'Gà giòn không xương (6 miếng)', N'6 Miếng gà giòn không xương , 1 sốt mù tạt mật ong, 2 Tương ớt + 2 tương cà', 0, 0, N'/stores/3e7d2b56-371b-406c-9c76-faa4b7006a1a/products/73c4dc76-0f35-4508-8c7d-31cfea16d437/cover-img.jpg', CAST(95000 AS Decimal(18, 0)), N'106a8a96-11d6-461c-9ace-cc86391e2246', CAST(N'2025-04-23T03:30:51.8305345' AS DateTime2), NULL, 0, CAST(0.00 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[Products] ([Id], [StoreId], [Name], [Description], [AvailableStock], [BookingCount], [CoverImagePath], [Price], [ConcurrencyStamp], [CreatedAt], [LastUpdatedAt], [State], [Discount], [RateCount], [Rating]) VALUES (N'a80bd1ab-f4cc-4c8e-bc5d-5510b702b8c6', N'3e7d2b56-371b-406c-9c76-faa4b7006a1a', N'Khuỷu cánh gà cay (6 miếng)', N'6 Khuỷu cánh gà cay , 2 Tương ớt + 2 tương cà', 0, 0, N'/stores/3e7d2b56-371b-406c-9c76-faa4b7006a1a/products/a80bd1ab-f4cc-4c8e-bc5d-5510b702b8c6/cover-img.jpg', CAST(95000 AS Decimal(18, 0)), N'795de344-cb5b-4ec9-b974-23f5a60990f6', CAST(N'2025-04-23T03:21:15.1343760' AS DateTime2), NULL, 0, CAST(0.00 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[Products] ([Id], [StoreId], [Name], [Description], [AvailableStock], [BookingCount], [CoverImagePath], [Price], [ConcurrencyStamp], [CreatedAt], [LastUpdatedAt], [State], [Discount], [RateCount], [Rating]) VALUES (N'98c82a05-f06a-430f-9ac9-64e1afbee619', N'3e7d2b56-371b-406c-9c76-faa4b7006a1a', N'Combo B - Gà Sốt Bơ Tỏi Và Thảo Mộc', N'- 2 Miếng gà sốt bơ tỏi và thảo mộc - 2 Miếng gà có xương (lựa chọn vị cay/ không cay) - 1 Món ăn kèm cỡ vừa tùy chọn (Bắp cải trộn/ Khoai tây nghiền/ Khoai tây chiên) - 2 Nước ngọt', 0, 0, N'/stores/3e7d2b56-371b-406c-9c76-faa4b7006a1a/products/98c82a05-f06a-430f-9ac9-64e1afbee619/cover-img.jpg', CAST(257000 AS Decimal(18, 0)), N'131a0028-622d-498b-b5fd-4cc03129e6cc', CAST(N'2025-04-23T04:53:37.1100407' AS DateTime2), NULL, 0, CAST(0.00 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[Products] ([Id], [StoreId], [Name], [Description], [AvailableStock], [BookingCount], [CoverImagePath], [Price], [ConcurrencyStamp], [CreatedAt], [LastUpdatedAt], [State], [Discount], [RateCount], [Rating]) VALUES (N'64140c4c-1d9f-47b7-a1f2-6ca306631d3f', N'3e7d2b56-371b-406c-9c76-faa4b7006a1a', N'1 Miếng gà sốt bơ tỏi và thảo mộc', N'- 1 Miếng gà sốt bơ tỏi và thảo mộc - 1 Tương ớt', 0, 0, N'/stores/3e7d2b56-371b-406c-9c76-faa4b7006a1a/products/64140c4c-1d9f-47b7-a1f2-6ca306631d3f/cover-img.jpg', CAST(49000 AS Decimal(18, 0)), N'0ccd7680-d879-42c3-b14a-de7afa6a719e', CAST(N'2025-04-23T02:58:35.4099933' AS DateTime2), NULL, 0, CAST(0.00 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[Products] ([Id], [StoreId], [Name], [Description], [AvailableStock], [BookingCount], [CoverImagePath], [Price], [ConcurrencyStamp], [CreatedAt], [LastUpdatedAt], [State], [Discount], [RateCount], [Rating]) VALUES (N'2f5dbe05-3b35-4b1f-9e37-9c46f2cb473c', N'906bf960-417d-4697-9bbb-5d351e455bfe', N'Combo Deal Hời 55k', N'01 miếng Gà/01 Mì Ý Gà Viên + 01 Khoai Tây Chiên (Vừa)/01 Bắp Cải Trộn (Lớn) + 01 Bánh Trứng/02 Viên Khoai Môn Kim Sa', 0, 0, N'/stores/906bf960-417d-4697-9bbb-5d351e455bfe/products/2f5dbe05-3b35-4b1f-9e37-9c46f2cb473c/cover-img.jpg', CAST(79000 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', CAST(N'2025-03-27T08:40:48.8698677' AS DateTime2), CAST(N'2025-03-27T08:40:48.8698687' AS DateTime2), 0, CAST(24000.00 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[Products] ([Id], [StoreId], [Name], [Description], [AvailableStock], [BookingCount], [CoverImagePath], [Price], [ConcurrencyStamp], [CreatedAt], [LastUpdatedAt], [State], [Discount], [RateCount], [Rating]) VALUES (N'd487a8c4-e410-49a5-b808-b3f2e7558eb1', N'3e7d2b56-371b-406c-9c76-faa4b7006a1a', N'01 Bánh Quy Bơ Mật Ong', N'Bánh quy thơm ngậy mùi bơ và đường', 0, 0, N'/stores/3e7d2b56-371b-406c-9c76-faa4b7006a1a/products/d487a8c4-e410-49a5-b808-b3f2e7558eb1/cover-img.jpg', CAST(12000 AS Decimal(18, 0)), N'b48ba5a4-858e-47dd-ae16-04af897e8fa7', CAST(N'2025-04-23T03:41:08.1590576' AS DateTime2), NULL, 0, CAST(0.00 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[Products] ([Id], [StoreId], [Name], [Description], [AvailableStock], [BookingCount], [CoverImagePath], [Price], [ConcurrencyStamp], [CreatedAt], [LastUpdatedAt], [State], [Discount], [RateCount], [Rating]) VALUES (N'118e43ae-81b7-4013-b7ec-c5f7b9e747b0', N'3e7d2b56-371b-406c-9c76-faa4b7006a1a', N'04 Miếng Gà Sốt Bơ Tỏi Và Thảo Mộc', N'- 4 Miếng gà sốt bơ tỏi và thảo mộc - 3 Tương ớt + 1 tương cà', 0, 0, N'/stores/3e7d2b56-371b-406c-9c76-faa4b7006a1a/products/118e43ae-81b7-4013-b7ec-c5f7b9e747b0/cover-img.jpg', CAST(196000 AS Decimal(18, 0)), N'306e3024-e1be-43a7-b000-cc2f73348297', CAST(N'2025-04-23T04:40:45.9747081' AS DateTime2), NULL, 0, CAST(0.00 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[Products] ([Id], [StoreId], [Name], [Description], [AvailableStock], [BookingCount], [CoverImagePath], [Price], [ConcurrencyStamp], [CreatedAt], [LastUpdatedAt], [State], [Discount], [RateCount], [Rating]) VALUES (N'6c48ba52-7c01-4cd5-9cad-cee4f5de4a29', N'df958450-5c70-44ed-989d-448cb6cec6aa', N'producttest', N'verylongdescriptiontestforproducttest', 0, 0, NULL, CAST(20000 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 0, CAST(0.00 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[Products] ([Id], [StoreId], [Name], [Description], [AvailableStock], [BookingCount], [CoverImagePath], [Price], [ConcurrencyStamp], [CreatedAt], [LastUpdatedAt], [State], [Discount], [RateCount], [Rating]) VALUES (N'8040f001-ee6e-4b5e-8bed-e1670e984f8d', N'3e7d2b56-371b-406c-9c76-faa4b7006a1a', N'Trio Deal Chỉ 98K', N'- 01 Miếng gà sốt bơ tỏi và thảo mộc - 01 Bánh cuộn gà giòn không xương (cay/ không cay) - 01 Burger tôm (cay/ không cay) - 02 Tương ớt + 01 tương cà', 0, 0, N'/stores/3e7d2b56-371b-406c-9c76-faa4b7006a1a/products/8040f001-ee6e-4b5e-8bed-e1670e984f8d/cover-img.jpg', CAST(98000 AS Decimal(18, 0)), N'c01ae8fd-8a1e-434d-9998-ed7678f3694c', CAST(N'2025-04-22T11:10:26.9285019' AS DateTime2), NULL, 0, CAST(0.00 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[Products] ([Id], [StoreId], [Name], [Description], [AvailableStock], [BookingCount], [CoverImagePath], [Price], [ConcurrencyStamp], [CreatedAt], [LastUpdatedAt], [State], [Discount], [RateCount], [Rating]) VALUES (N'dc795c3b-db5f-46b9-bb6b-f2dd2bceea5d', N'3e7d2b56-371b-406c-9c76-faa4b7006a1a', N'05 Bánh Quy Bơ Mật Ong', N'Bánh quy thơm ngậy mùi bơ và đường', 0, 0, N'/stores/3e7d2b56-371b-406c-9c76-faa4b7006a1a/products/dc795c3b-db5f-46b9-bb6b-f2dd2bceea5d/cover-img.jpg', CAST(49000 AS Decimal(18, 0)), N'f9491f7f-6b84-4d71-9b96-5c7ddc52b9e3', CAST(N'2025-04-23T04:21:14.9279092' AS DateTime2), NULL, 0, CAST(0.00 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[Products] ([Id], [StoreId], [Name], [Description], [AvailableStock], [BookingCount], [CoverImagePath], [Price], [ConcurrencyStamp], [CreatedAt], [LastUpdatedAt], [State], [Discount], [RateCount], [Rating]) VALUES (N'7139b605-57e5-4474-95d3-f8db0ef90fb8', N'3e7d2b56-371b-406c-9c76-faa4b7006a1a', N'03 Bánh Quy Bơ Mật Ong', N'Bánh quy thơm ngậy mùi bơ và đường', 0, 0, N'/stores/3e7d2b56-371b-406c-9c76-faa4b7006a1a/products/7139b605-57e5-4474-95d3-f8db0ef90fb8/cover-img.jpg', CAST(29000 AS Decimal(18, 0)), N'f23c2325-4abe-4648-9bfa-7712f43688a4', CAST(N'2025-04-23T03:56:16.9655371' AS DateTime2), NULL, 0, CAST(0.00 AS Decimal(18, 2)), 0, 0)
GO
/****** Object:  Index [IX_MenuProduct_ProductsId]    Script Date: 4/23/2025 5:18:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_MenuProduct_ProductsId] ON [dbo].[MenuProduct]
(
	[ProductsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (CONVERT([tinyint],(0))) FOR [State]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ((0.0)) FOR [Discount]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ((0)) FOR [RateCount]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [Rating]
GO
ALTER TABLE [dbo].[MenuProduct]  WITH CHECK ADD  CONSTRAINT [FK_MenuProduct_Menus_MenusId] FOREIGN KEY([MenusId])
REFERENCES [dbo].[Menus] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MenuProduct] CHECK CONSTRAINT [FK_MenuProduct_Menus_MenusId]
GO
ALTER TABLE [dbo].[MenuProduct]  WITH CHECK ADD  CONSTRAINT [FK_MenuProduct_Products_ProductsId] FOREIGN KEY([ProductsId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MenuProduct] CHECK CONSTRAINT [FK_MenuProduct_Products_ProductsId]
GO
USE [master]
GO
ALTER DATABASE [spf_products_db] SET  READ_WRITE 
GO
