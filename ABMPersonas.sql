USE [TUPPI]
GO
/****** Object:  Table [dbo].[estado_civil]    Script Date: 3/6/2021 21:19:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[estado_civil](
	[id_estado_civil] [numeric](2, 0) NOT NULL,
	[n_estado_civil] [varchar](30) NULL,
 CONSTRAINT [PK_estado_civil] PRIMARY KEY CLUSTERED 
(
	[id_estado_civil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[personas]    Script Date: 3/6/2021 21:19:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[personas](
	[apellido] [varchar](30) NULL,
	[nombres] [varchar](30) NULL,
	[tipo_documento] [numeric](2, 0) NULL,
	[documento] [numeric](8, 0) NOT NULL,
	[estado_civil] [numeric](2, 0) NULL,
	[sexo] [numeric](1, 0) NULL,
	[fallecio] [bit] NULL,
 CONSTRAINT [PK_personas] PRIMARY KEY CLUSTERED 
(
	[documento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tipo_documento]    Script Date: 3/6/2021 21:19:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipo_documento](
	[id_tipo_documento] [numeric](2, 0) NOT NULL,
	[n_tipo_documento] [varchar](30) NULL,
 CONSTRAINT [PK_tipo_documento] PRIMARY KEY CLUSTERED 
(
	[id_tipo_documento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[estado_civil] ([id_estado_civil], [n_estado_civil]) VALUES (CAST(1 AS Numeric(2, 0)), N'Soltero')
INSERT [dbo].[estado_civil] ([id_estado_civil], [n_estado_civil]) VALUES (CAST(2 AS Numeric(2, 0)), N'Casado')
INSERT [dbo].[estado_civil] ([id_estado_civil], [n_estado_civil]) VALUES (CAST(3 AS Numeric(2, 0)), N'Viudo')
INSERT [dbo].[estado_civil] ([id_estado_civil], [n_estado_civil]) VALUES (CAST(4 AS Numeric(2, 0)), N'Separado')
GO
INSERT [dbo].[personas] ([apellido], [nombres], [tipo_documento], [documento], [estado_civil], [sexo], [fallecio]) VALUES (N'Perez', N'Juan', CAST(1 AS Numeric(2, 0)), CAST(123456 AS Numeric(8, 0)), CAST(1 AS Numeric(2, 0)), CAST(2 AS Numeric(1, 0)), 1)
GO
INSERT [dbo].[tipo_documento] ([id_tipo_documento], [n_tipo_documento]) VALUES (CAST(1 AS Numeric(2, 0)), N'DNI')
INSERT [dbo].[tipo_documento] ([id_tipo_documento], [n_tipo_documento]) VALUES (CAST(2 AS Numeric(2, 0)), N'LE')
INSERT [dbo].[tipo_documento] ([id_tipo_documento], [n_tipo_documento]) VALUES (CAST(3 AS Numeric(2, 0)), N'LC')
INSERT [dbo].[tipo_documento] ([id_tipo_documento], [n_tipo_documento]) VALUES (CAST(4 AS Numeric(2, 0)), N'Cedula')
INSERT [dbo].[tipo_documento] ([id_tipo_documento], [n_tipo_documento]) VALUES (CAST(5 AS Numeric(2, 0)), N'Pasaporte')
GO
