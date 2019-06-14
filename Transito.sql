USE [Transito]
GO
/****** Object:  User [transito]    Script Date: 14/06/2019 03:08:54 p. m. ******/
CREATE USER [transito] FOR LOGIN [transito] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [transito]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [transito]
GO
/****** Object:  Table [dbo].[Catalogo]    Script Date: 14/06/2019 03:08:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Catalogo](
	[idCatalogo] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](100) NULL,
	[tipoCatalogo] [int] NULL,
 CONSTRAINT [PK_Catalogo] PRIMARY KEY CLUSTERED 
(
	[idCatalogo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 14/06/2019 03:08:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[idCliente] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](150) NULL,
	[apellidos] [varchar](150) NULL,
	[fechaNacimiento] [date] NULL,
	[numeroLicencia] [varchar](50) NULL,
	[telefono] [varchar](20) NULL,
	[contrasenia] [varchar](100) NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[idCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dictamen]    Script Date: 14/06/2019 03:08:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dictamen](
	[idDictamen] [int] IDENTITY(1,1) NOT NULL,
	[personal] [int] NULL,
	[reporte] [int] NULL,
	[folio] [varchar](100) NULL,
	[descripcion] [text] NULL,
	[fechaHora] [datetime] NULL,
	[nombrePerito] [varchar](200) NULL,
 CONSTRAINT [PK_Dictamen] PRIMARY KEY CLUSTERED 
(
	[idDictamen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Foto]    Script Date: 14/06/2019 03:08:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Foto](
	[idFoto] [int] IDENTITY(1,1) NOT NULL,
	[foto] [varchar](200) NULL,
	[reporte] [int] NULL,
 CONSTRAINT [PK_Foto] PRIMARY KEY CLUSTERED 
(
	[idFoto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personal]    Script Date: 14/06/2019 03:08:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personal](
	[idPersonal] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](150) NULL,
	[apellidos] [varchar](150) NULL,
	[catalogo] [int] NULL,
	[nombreUsuario] [varchar](100) NULL,
	[contrasenia] [varchar](100) NULL,
 CONSTRAINT [PK_Personal] PRIMARY KEY CLUSTERED 
(
	[idPersonal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reporte]    Script Date: 14/06/2019 03:08:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reporte](
	[idReporte] [int] IDENTITY(1,1) NOT NULL,
	[vehiculo] [int] NULL,
	[lugar] [varchar](200) NULL,
	[nombreConductorAgeno] [varchar](200) NULL,
	[cliente] [int] NULL,
	[estatusReporte] [int] NULL,
	[vehiculoAgeno] [int] NULL,
 CONSTRAINT [PK_Reporte] PRIMARY KEY CLUSTERED 
(
	[idReporte] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehiculo]    Script Date: 14/06/2019 03:08:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehiculo](
	[idVehiculo] [int] IDENTITY(1,1) NOT NULL,
	[cliente] [int] NULL,
	[marca] [varchar](100) NULL,
	[modelo] [varchar](50) NULL,
	[color] [varchar](50) NULL,
	[anio] [varchar](10) NULL,
	[nombreAseguradora] [varchar](150) NULL,
	[numeroPoliza] [varchar](150) NULL,
	[numeroPlacas] [varchar](150) NULL,
 CONSTRAINT [PK_Vehiculo] PRIMARY KEY CLUSTERED 
(
	[idVehiculo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VehiculoAgeno]    Script Date: 14/06/2019 03:08:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VehiculoAgeno](
	[idVehiculoAgeno] [int] IDENTITY(1,1) NOT NULL,
	[marca] [varchar](100) NULL,
	[modelo] [varchar](50) NULL,
	[color] [varchar](50) NULL,
	[anio] [varchar](10) NULL,
	[nombreAseguradora] [varchar](150) NULL,
	[numeroPoliza] [varchar](150) NULL,
	[numeroPlacas] [varchar](150) NULL,
 CONSTRAINT [PK_VehiculoAgeno] PRIMARY KEY CLUSTERED 
(
	[idVehiculoAgeno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Catalogo] ON 

INSERT [dbo].[Catalogo] ([idCatalogo], [nombre], [tipoCatalogo]) VALUES (1, N'Personal', NULL)
INSERT [dbo].[Catalogo] ([idCatalogo], [nombre], [tipoCatalogo]) VALUES (2, N'Administrador', 1)
INSERT [dbo].[Catalogo] ([idCatalogo], [nombre], [tipoCatalogo]) VALUES (3, N'Perito', 1)
INSERT [dbo].[Catalogo] ([idCatalogo], [nombre], [tipoCatalogo]) VALUES (4, N'Soporte', 1)
INSERT [dbo].[Catalogo] ([idCatalogo], [nombre], [tipoCatalogo]) VALUES (5, N'Atención', 1)
INSERT [dbo].[Catalogo] ([idCatalogo], [nombre], [tipoCatalogo]) VALUES (6, N'Estatus Reporte', NULL)
INSERT [dbo].[Catalogo] ([idCatalogo], [nombre], [tipoCatalogo]) VALUES (7, N'Dictaminado', 6)
INSERT [dbo].[Catalogo] ([idCatalogo], [nombre], [tipoCatalogo]) VALUES (8, N'En espera', 6)
SET IDENTITY_INSERT [dbo].[Catalogo] OFF
SET IDENTITY_INSERT [dbo].[Cliente] ON 

INSERT [dbo].[Cliente] ([idCliente], [nombre], [apellidos], [fechaNacimiento], [numeroLicencia], [telefono], [contrasenia]) VALUES (1, N'Josafat', N'Murillo Hernández', CAST(N'1998-11-06' AS Date), N'1234567', N'2717141311', N'PinkieDash5')
INSERT [dbo].[Cliente] ([idCliente], [nombre], [apellidos], [fechaNacimiento], [numeroLicencia], [telefono], [contrasenia]) VALUES (2, N'Adolfo', N'De La Cruz Díaz', CAST(N'1998-05-05' AS Date), N'7654321', N'2717141113', N'adolfo')
SET IDENTITY_INSERT [dbo].[Cliente] OFF
SET IDENTITY_INSERT [dbo].[Dictamen] ON 

INSERT [dbo].[Dictamen] ([idDictamen], [personal], [reporte], [folio], [descripcion], [fechaHora], [nombrePerito]) VALUES (1, 1, 1, N'12345Folio', N'Choque', CAST(N'2019-06-12T21:56:20.407' AS DateTime), N'Josafat')
SET IDENTITY_INSERT [dbo].[Dictamen] OFF
SET IDENTITY_INSERT [dbo].[Personal] ON 

INSERT [dbo].[Personal] ([idPersonal], [nombre], [apellidos], [catalogo], [nombreUsuario], [contrasenia]) VALUES (1, N'Josafat', N'Murillo Hernández', 3, N'Dash', N'PinkieDash5')
INSERT [dbo].[Personal] ([idPersonal], [nombre], [apellidos], [catalogo], [nombreUsuario], [contrasenia]) VALUES (2, N'Adolfo', N'De La Cruz Díaz', 3, N'adolfo', N'adolfo')
SET IDENTITY_INSERT [dbo].[Personal] OFF
SET IDENTITY_INSERT [dbo].[Reporte] ON 

INSERT [dbo].[Reporte] ([idReporte], [vehiculo], [lugar], [nombreConductorAgeno], [cliente], [estatusReporte], [vehiculoAgeno]) VALUES (1, 1, N'Centro', N'Juan Perez', 1, 8, NULL)
SET IDENTITY_INSERT [dbo].[Reporte] OFF
SET IDENTITY_INSERT [dbo].[Vehiculo] ON 

INSERT [dbo].[Vehiculo] ([idVehiculo], [cliente], [marca], [modelo], [color], [anio], [nombreAseguradora], [numeroPoliza], [numeroPlacas]) VALUES (1, 1, N'Chevrolet', N'2019', N'Azul-Dorado', N'2019', N'SEP', N'123456', N'Códoba123')
INSERT [dbo].[Vehiculo] ([idVehiculo], [cliente], [marca], [modelo], [color], [anio], [nombreAseguradora], [numeroPoliza], [numeroPlacas]) VALUES (2, 2, N'Toyota', N'2019', N'Verde', N'2019', NULL, NULL, N'Xalapa123')
SET IDENTITY_INSERT [dbo].[Vehiculo] OFF
SET IDENTITY_INSERT [dbo].[VehiculoAgeno] ON 

INSERT [dbo].[VehiculoAgeno] ([idVehiculoAgeno], [marca], [modelo], [color], [anio], [nombreAseguradora], [numeroPoliza], [numeroPlacas]) VALUES (1, N'Toyota', N'1998', N'Azul', N'1998', N'CEMARNAT', N'1234567', N'Córdoba123')
SET IDENTITY_INSERT [dbo].[VehiculoAgeno] OFF
ALTER TABLE [dbo].[Dictamen] ADD  CONSTRAINT [DF_Dictamen_fechaHora]  DEFAULT (getdate()) FOR [fechaHora]
GO
