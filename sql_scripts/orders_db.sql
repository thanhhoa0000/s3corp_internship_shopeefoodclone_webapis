USE [master]
GO
/****** Object:  Database [spf_orders_db]    Script Date: 3/24/2025 5:31:27 PM ******/
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
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/24/2025 5:31:27 PM ******/
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
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 3/24/2025 5:31:27 PM ******/
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
/****** Object:  Table [dbo].[Orders]    Script Date: 3/24/2025 5:31:27 PM ******/
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
GO
/****** Object:  Index [IX_OrderDetails_OrderId]    Script Date: 3/24/2025 5:31:27 PM ******/
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
