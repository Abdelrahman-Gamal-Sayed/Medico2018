USE [master]
GO
/****** Object:  Database [HoData]    Script Date: 01/10/2018 09:41:05 ******/
CREATE DATABASE [HoData] ON  PRIMARY 
( NAME = N'HoData', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\HoData.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HoData_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\HoData_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HoData].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HoData] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [HoData] SET ANSI_NULLS OFF
GO
ALTER DATABASE [HoData] SET ANSI_PADDING OFF
GO
ALTER DATABASE [HoData] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [HoData] SET ARITHABORT OFF
GO
ALTER DATABASE [HoData] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [HoData] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [HoData] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [HoData] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [HoData] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [HoData] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [HoData] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [HoData] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [HoData] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [HoData] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [HoData] SET  DISABLE_BROKER
GO
ALTER DATABASE [HoData] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [HoData] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [HoData] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [HoData] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [HoData] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [HoData] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [HoData] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [HoData] SET  READ_WRITE
GO
ALTER DATABASE [HoData] SET RECOVERY SIMPLE
GO
ALTER DATABASE [HoData] SET  MULTI_USER
GO
ALTER DATABASE [HoData] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [HoData] SET DB_CHAINING OFF
GO
USE [HoData]
GO
/****** Object:  Table [dbo].[Ray]    Script Date: 01/10/2018 09:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ray](
	[Ray_code] [int] IDENTITY(1,1) NOT NULL,
	[Ray_Aname] [varchar](500) NULL,
	[Ray_Ename] [varchar](500) NULL,
 CONSTRAINT [PK_Ray] PRIMARY KEY CLUSTERED 
(
	[Ray_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 01/10/2018 09:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Patient](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ssn] [varchar](14) NULL,
	[ename] [varchar](50) NULL,
	[aname] [nvarchar](100) NULL,
	[gender] [nvarchar](10) NULL,
	[phone] [nvarchar](11) NULL,
	[DateOfBirth] [date] NULL,
	[city] [nvarchar](20) NULL,
	[area] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Medicine]    Script Date: 01/10/2018 09:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Medicine](
	[Medication_code] [int] NOT NULL,
	[Medication_Aname] [varchar](1000) NULL,
	[Medication_Ename] [nvarchar](50) NULL,
	[PACK_SIZE] [int] NULL,
	[PACK_PRICE] [int] NULL,
	[UNIT_NO] [int] NULL,
	[UNIT_PRICE] [int] NULL,
	[Medication_FORM] [nvarchar](50) NULL,
	[Medication_GROUP] [varchar](30) NULL,
	[COMP_ID] [int] NULL,
	[BRANCH_CODE] [int] NULL,
	[LIC_TYPE] [varchar](30) NULL,
	[DOSAGE_FORM] [varchar](100) NULL,
	[M_TYPE] [varchar](30) NULL,
	[CON_MED] [varchar](30) NULL,
	[ACTIVE] [varchar](1) NULL,
	[UPDATE_BY] [varchar](100) NULL,
	[UPDATE_DATE] [date] NULL,
	[CREATED_BY] [varchar](100) NULL,
	[CREATED_DATE] [date] NULL,
	[GRN_CODE] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[Medication_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Reserve]    Script Date: 01/10/2018 09:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reserve](
	[serial] [int] IDENTITY(1,1) NOT NULL,
	[Patient_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[serial] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Re]    Script Date: 01/10/2018 09:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Re](
	[serial] [int] NOT NULL,
	[Patient_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[serial] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lab]    Script Date: 01/10/2018 09:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lab](
	[lab_code] [int] IDENTITY(1,1) NOT NULL,
	[lab_aname] [varchar](500) NULL,
	[lab_ename] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[lab_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Governorate]    Script Date: 01/10/2018 09:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Governorate](
	[Governorate_CODE] [int] NOT NULL,
	[Governorate_ANAME] [nvarchar](50) NULL,
	[Governorate_ENAME] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Governorate_CODE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Diagnosis]    Script Date: 01/10/2018 09:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Diagnosis](
	[Diagnosis_Code] [int] IDENTITY(1,1) NOT NULL,
	[Diagnosis_ANAME] [varchar](500) NULL,
	[Diagnosis_ENAME] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[Diagnosis_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AREA]    Script Date: 01/10/2018 09:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AREA](
	[AREA_CODE] [int] NOT NULL,
	[Governorate] [int] NULL,
	[AREA_ANAME] [nvarchar](50) NULL,
	[AREA_ENAME] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[AREA_CODE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 01/10/2018 09:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserType](
	[type_code] [int] IDENTITY(1,1) NOT NULL,
	[type_name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[type_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 01/10/2018 09:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[password] [varchar](50) NULL,
	[type] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Detection]    Script Date: 01/10/2018 09:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Detection](
	[Detection_id] [int] IDENTITY(1,1) NOT NULL,
	[patient_id] [int] NOT NULL,
	[detection_date] [date] NULL,
	[detection_notes] [varchar](1000) NULL,
	[new_date] [date] NULL,
 CONSTRAINT [PK_Detection] PRIMARY KEY CLUSTERED 
(
	[Detection_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RayDetection]    Script Date: 01/10/2018 09:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RayDetection](
	[ray_code] [int] NOT NULL,
	[det_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[det_id] ASC,
	[ray_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LabDetection]    Script Date: 01/10/2018 09:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LabDetection](
	[lab_code] [int] NOT NULL,
	[det_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[det_id] ASC,
	[lab_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicineDetection]    Script Date: 01/10/2018 09:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MedicineDetection](
	[Med_code] [int] NOT NULL,
	[det_id] [int] NOT NULL,
	[med_dose] [varchar](20) NULL,
	[med_duration] [varchar](20) NULL,
 CONSTRAINT [PK_MedicineDetection] PRIMARY KEY CLUSTERED 
(
	[Med_code] ASC,
	[det_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DiagnoseDetection]    Script Date: 01/10/2018 09:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiagnoseDetection](
	[diagnose_id] [int] NOT NULL,
	[det_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[diagnose_id] ASC,
	[det_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK__User__type__4AB81AF0]    Script Date: 01/10/2018 09:41:07 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([type])
REFERENCES [dbo].[UserType] ([type_code])
GO
/****** Object:  ForeignKey [FK__Detection__patie__4D94879B]    Script Date: 01/10/2018 09:41:07 ******/
ALTER TABLE [dbo].[Detection]  WITH CHECK ADD FOREIGN KEY([patient_id])
REFERENCES [dbo].[Patient] ([Id])
GO
/****** Object:  ForeignKey [FK__RayDetect__ray_c__5070F446]    Script Date: 01/10/2018 09:41:07 ******/
ALTER TABLE [dbo].[RayDetection]  WITH CHECK ADD FOREIGN KEY([ray_code])
REFERENCES [dbo].[Ray] ([Ray_code])
GO
/****** Object:  ForeignKey [FK__LabDetect__det_i__5812160E]    Script Date: 01/10/2018 09:41:07 ******/
ALTER TABLE [dbo].[LabDetection]  WITH CHECK ADD FOREIGN KEY([det_id])
REFERENCES [dbo].[Detection] ([Detection_id])
GO
/****** Object:  ForeignKey [FK__LabDetect__lab_c__571DF1D5]    Script Date: 01/10/2018 09:41:07 ******/
ALTER TABLE [dbo].[LabDetection]  WITH CHECK ADD FOREIGN KEY([lab_code])
REFERENCES [dbo].[Lab] ([lab_code])
GO
/****** Object:  ForeignKey [FK__MedicineD__det_i__534D60F1]    Script Date: 01/10/2018 09:41:07 ******/
ALTER TABLE [dbo].[MedicineDetection]  WITH CHECK ADD FOREIGN KEY([det_id])
REFERENCES [dbo].[Detection] ([Detection_id])
GO
/****** Object:  ForeignKey [FK__MedicineD__Med_c__5441852A]    Script Date: 01/10/2018 09:41:07 ******/
ALTER TABLE [dbo].[MedicineDetection]  WITH CHECK ADD FOREIGN KEY([Med_code])
REFERENCES [dbo].[Medicine] ([Medication_code])
GO
/****** Object:  ForeignKey [FK__DiagnoseD__det_i__5BE2A6F2]    Script Date: 01/10/2018 09:41:07 ******/
ALTER TABLE [dbo].[DiagnoseDetection]  WITH CHECK ADD FOREIGN KEY([det_id])
REFERENCES [dbo].[Detection] ([Detection_id])
GO
/****** Object:  ForeignKey [FK__DiagnoseD__diagn__5AEE82B9]    Script Date: 01/10/2018 09:41:07 ******/
ALTER TABLE [dbo].[DiagnoseDetection]  WITH CHECK ADD FOREIGN KEY([diagnose_id])
REFERENCES [dbo].[Diagnosis] ([Diagnosis_Code])
GO
