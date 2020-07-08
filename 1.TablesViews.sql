/*
* CARTERAS
*
*
**/
/*
if object_id('CarteraCredito') is not null		drop table CarteraCredito;
CREATE TABLE dbo.[CarteraCredito](
	[CarteraId] [int] NOT NULL,
	[ProductoID] [int] NOT NULL,
	[nCodCred] [int] NOT NULL,
	[repro] [int] NULL,	
PRIMARY KEY 
(
	CarteraId,
	ProductoID,
	nCodCred
))
GO

if object_id('Carteras') is not null		drop table Carteras;
CREATE TABLE dbo.[Carteras](
	[CarteraID] [int],
	[ProductoID] [int],
	[FondeadorID] [int],

	[Creado] [datetime],
	[Modificado] [datetime] NULL,
	[FechaDesembolso] [datetime] NULL,
	[CreadoPor] [varchar](100) NULL

	primary key(CarteraID, ProductoID)
)
GO

if object_id('CreditoFondeador') is not null		drop table CreditoFondeador;
CREATE TABLE [dbo].[CreditoFondeador](
	[nCodCred] [int] NOT NULL,
	[FondeadorID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[nCodCred] ASC,
	[FondeadorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

if object_id('CreditosBloqueados') is not null		drop table CreditosBloqueados;
CREATE TABLE dbo.[CreditosBloqueados](
	[nCodCred] [int] NOT NULL,
	[CarteraID] [int] NOT NULL,
	[ProductoID] [int] NOT NULL,
	[fecha] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[nCodCred] ASC,
	[CarteraID] ASC
))
GO

if object_id('CronogramasAlternativos') is not null		drop table CronogramasAlternativos;
create table dbo.CronogramasAlternativos(
	nCodCred nvarchar(10),
	nNroCuota int,
	dFecPago datetime,
	amortizacion decimal(10,2),
	interes decimal(10,2),
	periodoGracia  decimal(10,2),
	encaje decimal(10,2),	
	totalCuota decimal(10,2)

	primary key(nCodCred, nNroCuota)
)
go

if object_id('CreditosParaVentaCartera') is not null		drop table CreditosParaVentaCartera
go
CREATE view dbo.[CreditosParaVentaCartera] as
select 
	n.cDNI dni,
	n.cnombres + ' '  +  n.cApePat + ' ' + n.cApeMat  nombres,
	j.cRuc ruc,
	j.cRazonSocial razonSocial,
	c.*,
	beta.precio,
	cod.*
from 
	creditos c
	inner join CatalogoCodigos cod on c.nSubProd = cod.nValor
	inner join CredPersonas cp on cp.nCodCred = c.nCodCred
	left join PersonaNat n on n.nCodPers = cp.nCodPers
	left join PersonaJur j on j.nCodPers = cp.nCodPers
	inner join (
		select 
			cro.nCodCred,
			sum(cro.nCapital) precio
		from credcronograma  cro
		where 
			((year(dFecVcto) >= year(getdate()) and month(dFecVcto) > month(getdate())) or year(dFecVcto) > year(getdate()))
		group by cro.ncodcred
	) beta on beta.nCodCred = c.nCodCred
where
	cod.ncodigo = 4029
	and c.nestado = 1
    and c.nCodCred not in(select nCodCred from CreditosBloqueados)
	and cod.nvalor not in(3,4,5)
GO

*/

/*
* PAGOS
*
*
**/
if object_id('PagoConceptos') is not null		drop table PagoConceptos;
CREATE TABLE [dbo].[PagoConceptos](
	[PagoConceptoID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[PagoConceptoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

if object_id('PagoDetalle') is not null		drop table PagoDetalle;
CREATE TABLE [dbo].[PagoDetalle](
	[PagoID] [int] NULL,
	[nCodCred] [int] NOT NULL,
	[nNroCalendario] [int] NOT NULL,
	[nNroCuota] [int] NOT NULL,
	[PagoConceptoID] [int] NULL,
	[Monto] [decimal](10, 2) NULL,
	[EsDeuda] [bit] NULL
) ON [PRIMARY]
GO

if object_id('Pagos') is not null		drop table Pagos;
CREATE TABLE [dbo].[Pagos](
	[PagoID] [int] IDENTITY(1,1) NOT NULL,
	[FondeadorID] [int] NULL,
	[ProductoID] [int] NULL,
	[CreadoPor] [varchar](100) NULL,
	[Creado] [datetime] NULL,
	[Modificado] [datetime] NULL,
	[FechaCierre] [datetime] NULL
) ON [PRIMARY]
GO


/*
* RECOMPRAS
*
*
**/
if object_id('CreditoRecompra') is not null		drop table CreditoRecompra;
CREATE TABLE [dbo].[CreditoRecompra](
	[RecompraID] [int] NOT NULL,
	[nCodCred] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RecompraID] ASC,
	[nCodCred] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

if object_id('Recompras') is not null		drop table Recompras;
CREATE TABLE [dbo].[Recompras](
	[RecompraID] [int] IDENTITY(1,1) NOT NULL,
	[FondeadorID] [int] NULL,
	[ProductoID] [int] NULL,
	[Creado] [datetime] NULL,
	[Modificado] [datetime] NULL,
	[FechaCierre] [datetime] NULL,
	[CreadoPor] [varchar](100) NULL
) ON [PRIMARY]
GO


/*
* AMORTIZACIONES
*
*
**/
if object_id('Amortizaciones') is not null		drop table Amortizaciones;
CREATE TABLE [dbo].[Amortizaciones](
	[AmortizacionID] [int] IDENTITY(1,1) NOT NULL,
	[Tasa] [decimal](10, 2) NULL,
	[SaldoCapital] [decimal](10, 2) NULL,
	[NuevoCapital] [decimal](10, 2) NULL,
	[UltimoVencimiento] [datetime] NULL,
	[Hoy] [datetime] NULL,
	[DiasTranscurridos] [int] NULL,
	[Factor] [decimal](10, 6) NULL,
	[InteresesTranscurridos] [decimal](10, 2) NULL,
	[KI] [decimal](10, 2) NULL,
	[nAmortizacion] [decimal](10, 2) NULL,
	[Capital] [decimal](10, 2) NULL,
	[Total] [decimal](10, 2) NULL,
	[nCodCred] [int] NULL,
	[NroCalendarioCOF] [int] NULL,
	[Confirmacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[AmortizacionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/*
* FONDEADORES
*
*
**/
if object_id('Fondeadores') is not null		drop table Fondeadores;
CREATE TABLE dbo.[Fondeadores](
	[FondeadorID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Color] [varchar](50) NULL,
	[evaluador] [varchar](100) NULL)
GO


/*
* AUTENTICACION
*
*
**/
if object_id('Roles') is not null		drop table Roles;
CREATE TABLE dbo.[Roles](
	[RolID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Creado] [datetime] NULL)
GO

if object_id('RolUsuario') is not null		drop table RolUsuario;
CREATE TABLE dbo.[RolUsuario](
	[RolID] [int] NOT NULL,
	[UsuarioID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RolID] ASC,
	[UsuarioID] ASC
))
GO

if object_id('Usuarios') is not null		drop table Usuarios;
CREATE TABLE dbo.[Usuarios](
	[UsuarioID] [int] IDENTITY(1,1) NOT NULL,
	[NombreCompleto] [varchar](max) NULL,
	[Sexo] bit,
	[Nombre] [varchar](100) NULL,
	[Clave] [varchar](100) NULL,
	[SesionToken] [varchar](100) NULL,
	[Creado] [datetime] NULL
)
GO















