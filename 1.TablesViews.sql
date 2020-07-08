if object_id('carteracredito') is not null		drop table carteracredito;
if object_id('carteras') is not null			drop table carteras;
if object_id('creditorecompra') is not null		drop table creditorecompra;
if object_id('cuotapago') is not null			drop table cuotapago;
if object_id('fondeadores') is not null			drop table fondeadores;
if object_id('pagos') is not null				drop table pagos;
if object_id('recompras') is not null			drop table recompras;
if object_id('roles') is not null				drop table roles;
if object_id('rolusuario') is not null			drop table rolusuario;
if object_id('usuarios') is not null			drop table usuarios;
if object_id('CreditosBloqueados') is not null  drop table CreditosBloqueados;
if object_id('creditosparaventacartera') is not null drop view dbo.creditosparaventacartera;
if object_id('CronogramasAlternativos') is not null  drop table CronogramasAlternativos;
go

CREATE TABLE [CarteraCredito](
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

CREATE TABLE [Carteras](
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

CREATE TABLE [CreditoRecompra](
	[RecompraID] [int] NOT NULL,
	[nCodCred] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RecompraID] ASC,
	[nCodCred] ASC
))
GO

CREATE TABLE [CuotaPago](
	[PagoID] [int] NOT NULL,
	[nCodCred] [int] NOT NULL,
	[nNroCuota] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PagoID] ASC,
	[nCodCred] ASC,
	[nNroCuota] ASC
))
GO

CREATE TABLE [Fondeadores](
	[FondeadorID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Color] [varchar](50) NULL,
	[evaluador] [varchar](100) NULL)
GO

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

CREATE TABLE [Roles](
	[RolID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Creado] [datetime] NULL)
GO

CREATE TABLE [RolUsuario](
	[RolID] [int] NOT NULL,
	[UsuarioID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RolID] ASC,
	[UsuarioID] ASC
))
GO

CREATE TABLE [Usuarios](
	[UsuarioID] [int] IDENTITY(1,1) NOT NULL,
	[NombreCompleto] [varchar](max) NULL,
	[Sexo] bit,
	[Nombre] [varchar](100) NULL,
	[Clave] [varchar](100) NULL,
	[SesionToken] [varchar](100) NULL,
	[Creado] [datetime] NULL
)
GO

CREATE TABLE [CreditosBloqueados](
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

CREATE view [CreditosParaVentaCartera] as
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