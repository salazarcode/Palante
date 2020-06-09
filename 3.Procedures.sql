--Stored Procedures

if object_id('ResumenYapamotors') is not null 
	drop procedure dbo.ResumenYapamotors;
go
create procedure dbo.[ResumenYapamotors] --1032
	@CarteraID int,
	@ProductoID int
as
--declare @CarteraID int  = 365;
select 
	case when ltrim(rtrim(c.cCodCta)) = '' then convert(nvarchar(6),c.nCodCred)
	else ltrim(rtrim(c.cCodCta)) end operacion,
	/*c.nCodCred operacion,*/

	case when p.nTipoPersona = 1 then n.cApePat + ' ' + n.cApeMat + ' ' + n.cNombres
	else j.cRazonSocial end cliente,

	case when p.nTipoPersona = 1 then n.cDNI
	else j.cRUC end identificacion,

	'' fechaVenta,

	case when c.nMoneda = 1 then 'Soles'
	else 'Dólares' end moneda,

	conc.cNomCod consecionario,

	marca.cNomCod marca,

	vso.cNombreModelo modelo,

	convert(decimal(10,2),ocprecio.nMonto) valorAutomovilUsd,

	convert(decimal(10,2),vi.nMonto / 3.33) cuotaInicialUds,

	convert(decimal(10,2),vi.nMonto * 100 / vi.nPrecioSinDscto) porcIncial,

	convert(decimal(10,2),(v.nGps) +  (( v.nGastosLegales + v.nSetame + v.nSoat + Round(c.nPrestamo-(((vi.iConcesionario+v.nGps)*v.nTipoCambio)+v.nGastosLegales+v.nSoat+v.nSetame),2))/v.nTipoCambio))  gpsSegurosYOtrosUsd,

	c.nPrestamo montoConcedido,

	sentinel.cNomCod cal,

	'Taxi' tipoCliente,

	convert(decimal(10,2),v.nGastosLegales) notariaSoles,

	convert(decimal(10,2),v.nGps) gpsUsd,

	convert(decimal(10,2),v.nSetame) setaca,

	convert(decimal(10,2), v.nSoat) soatSoles,

	'' encajeSoles,

	'' totalSoles,

	v.nTipoCambio tc,

	'' totalUsd

from
	creditos c
	inner join credvehicular v on v.nCodCred = c.nCodCred
	inner join CarteraCredito cc on cc.nCodCred = c.nCodCred
	inner join carteras ca on ca.CarteraID = cc.CarteraId and ca.ProductoID = @ProductoID and ca.CarteraId = @CarteraID
	inner join CredPersonas cp on cp.nCodCred = c.nCodCred
	inner join persona p on p.nCodPers = cp.nCodPers
	left join PersonaNat n on n.nCodPers = p.nCodPers
	left join PersonaJur j on j.nCodPers = p.nCodPers
	inner join Vehicularsolicitud vs on vs.nCodSolicitud = v.nCodsolicitud
	inner join VehicularSolicitudInformacionAuto vi on vi.nCodSolicitud = vs.nCodSolicitud
	inner join [VehicularObjetivosCred] ocgps on ocgps.nSolicitud = vs.nCodSolicitud and ocgps.nCodigo = 2 and ocgps.nmoneda = 2
	inner join [VehicularObjetivosCred] ocsetame on ocsetame.nSolicitud = vs.nCodSolicitud and ocsetame.nCodigo = 3  and ocsetame.nmoneda = 2
	inner join [VehicularObjetivosCred] ocprecio on ocprecio.nSolicitud = vs.nCodSolicitud and ocprecio.nCodigo = 1  and ocsetame.nmoneda = 2
	inner join [VehicularSolicitudPrecioSoat] soat on soat.iTipoVehiculo = vi.ITipoVehiculo and soat.iModelo = vi.iModeloAuto and soat.iMarca = vi.iMarcaAuto
	inner join CatalogoCodigos marca on marca.ncodigo = 6423 and vi.iMarcaAuto = marca.nValor
	inner join VehicularSolicitudPrecioSoat vso on vso.iMarca = vi.iMarcaAuto and vi.iModeloAuto = vso.iModelo
	inner join CatalogoCodigos conc on conc.nCodigo=6422 and vi.iConcesionario = conc.nValor
	inner join VehicularsolicitudProspectos vp on vp.nCodSolicitud = v.nCodsolicitud 
	inner join CatalogoCodigos sentinel on sentinel.ncodigo = 4409 and sentinel.nvalor = vp.iPeorEstadoSentinel
go


if object_id('AnexoYapamotors') is not null 
	drop procedure dbo.AnexoYapamotors;
go
CREATE procedure dbo.AnexoYapamotors --363,2
	@CarteraID int,
	@ProductoID int
	as
select 
	'' codSbs,
	case when p.nTipoPersona = 1 then n.cDNI
	else j.cRUC end ideDeudor ,
	case when p.nTipoPersona = 1 then n.cApePat + ' ' + n.cApeMat + ' ' + n.cNombres
	else j.cRazonSocial end apellidosNombres ,
	case when ltrim(rtrim(c.cCodCta)) = '' then convert(nvarchar(6),c.nCodCred)
	else ltrim(rtrim(c.cCodCta)) end credito,
	/*c.nCodCred credito,*/
	'MICRO EMPRESA' tipoCredito,
	c.nPrestamo monto,
	case when c.nMoneda = 1 then 'Soles'
	else 'Dólares' end  moneda ,
	(  POWER((1+c.nTasaComp/100), 1/12) - 1  ) * 100 temPorc,
	c.nTasaComp teaPorc,
	'' fechaVencimiento,
	c.nNroCuotas nroCuotas ,
	c.nNroCuotas nroCuotasPend,
	0 clasif ,
	'VIGENTE' sitGarantia ,
	'SIN GARANTÍA' tipoGarantia ,
	vi.nPrecioSinDscto valGarantia ,
	'-' intCapitalizado ,
	c.nPrestamo saldoCapital,
	c.nPrestamo saldoCapitalEnLaVenta ,
	'-' saldoInteres ,
	convert(decimal(10,2), c.nPrestamo / 1000) provConst ,
	convert(decimal(10,2), c.nPrestamo / 1000) provRequer,
	convert(decimal(10,2), c.nPrestamo - c.nPrestamo / 1000) valContable,
	c.nPrestamo precioVenta,
	'CONTADO' modalidad,
	'' utilidad ,
	f.Nombre empresa
from
	creditos c
	inner join CarteraCredito cc on cc.nCodCred = c.nCodCred
	inner join carteras ca on ca.CarteraID = cc.CarteraId and ca.ProductoID = @ProductoID and ca.CarteraID = @CarteraID
	inner join carteras car on car.CarteraId = cc.CarteraId
	inner join fondeadores f on f.fondeadorid = car.FondeadorId
	inner join CredPersonas cp on cp.nCodCred = c.nCodCred
	inner join persona p on p.nCodPers = cp.nCodPers
	left join PersonaNat n on n.nCodPers = p.nCodPers
	left join PersonaJur j on j.nCodPers = p.nCodPers
	inner join Vehicularsolicitud vs on vs.nPersCod = p.nCodPers
	inner join VehicularSolicitudInformacionAuto vi on vi.nCodSolicitud = vs.nCodSolicitud
	inner join VehicularObjetivosCred ocgps on ocgps.nSolicitud = vs.nCodSolicitud and ocgps.nCodigo = 2 and ocgps.nmoneda = 2
	inner join VehicularObjetivosCred ocsetame on ocsetame.nSolicitud = vs.nCodSolicitud and ocsetame.nCodigo = 3  and ocsetame.nmoneda = 2
	inner join VehicularObjetivosCred ocprecio on ocprecio.nSolicitud = vs.nCodSolicitud and ocprecio.nCodigo = 1  and ocsetame.nmoneda = 2
	inner join VehicularSolicitudPrecioSoat soat on soat.iTipoVehiculo = vi.ITipoVehiculo and soat.iModelo = vi.iModeloAuto and soat.iMarca = vi.iMarcaAuto
GO


if object_id('CronogramasCSV') is not null 
	drop procedure dbo.CronogramasCSV;
go
CREATE procedure dbo.[CronogramasCSV] --1011
	@carteraid int,
	@ProductoID int
as
	declare @table table(
		nCodCred int,
		mayor int
	)

	insert into @table
	select 
		cro.nCodCred,
		max(cro.nnrocalendario) mayor
	from 	
		credcronograma cro
		inner join carteracredito cc on cc.nCodCred = cro.nCodCred
	where 
		cc.CarteraId = @carteraid
		and cc.ProductoID = @ProductoID	
	group by 
		cro.nCodCred

	select 
	/*
		case 
			when cc.repro <> 0 then left(cc.nCodCred + '-' + cc.repro + space(20),20)
			else left(convert(nvarchar,cc.nCodCred) + space(20),20)
		end ncodcred,
		*/
		case when ltrim(rtrim(c.cCodCta)) = '' then convert(nvarchar(6),c.nCodCred)
		else ltrim(rtrim(c.cCodCta)) end ncodcred,

		left(convert(nvarchar,cro.nNroCuota) + FORMAT(cro.dFecVcto, 'yyyyMMdd') + space(17),17) cuotaFecha,
		left(convert(nvarchar,convert(decimal(10,2),cro.nCapital)) + space(15),15)  nCapital,
		left(convert(nvarchar,convert(decimal(10,2),cro.nInteres)) + space(17),17) nInteres,
		left(convert(nvarchar,(convert(decimal(10,2), cro.nPerGracia))) + space(17),17) nPerGracia,
		left('0.00' + space(11),11) encaje,
		left(convert(nvarchar,convert(decimal(10,2), (cro.nCapital + cro.nInteres + cro.nPerGracia))) + space(8),8) total
	from 	
		credcronograma cro
		inner join carteracredito cc on cc.nCodCred = cro.nCodCred
		inner join creditos c on c.nCodCred = cc.nCodCred
		inner join @table a on a.nCodCred = cro.nCodCred
	where 
		cc.CarteraId = @carteraid
		and cc.ProductoID = @ProductoID
		and cro.nNroCalendario = a.mayor
		and 	
		(
			(
				year(cro.dFecVcto) = year(getdate()) 
				and month(cro.dFecVcto) > month(getdate())
			) 
			or 
			(
				year(cro.dFecVcto) > year(getdate())
			)
		)
	order by 
		cro.nCodCred, cro.dFecVcto
GO

if object_id('CalificacionesCSV') is not null 
	drop procedure dbo.CalificacionesCSV;
go
CREATE procedure dbo.[CalificacionesCSV] 
	@carteraid int,
	@ProductoID int
as
select /*
	case 
		when cc.repro <> 0 then left(cc.nCodCred + '-' + cc.repro + space(20), 20)
		else left(convert(nvarchar,cc.nCodCred) + space(20),20)
	end ncodcred,*/
	
	case when ltrim(rtrim(c.cCodCta)) = '' then convert(nvarchar(6),c.nCodCred)
	else ltrim(rtrim(c.cCodCta)) end ncodcred,
	case
		when ccp.nvalor in(7,8,9,2) then '09'
		when ccp.nvalor = 6 then '12'
		when ccp.nvalor = 10 then '03'
		when ccp.nvalor = 1 then '99'
	end sbs
from 
	creditos c 
	inner join CarteraCredito cc on cc.nCodCred = c.nCodCred
	inner join carteras ca on ca.CarteraID = cc.CarteraID
	inner join credpersonas cp on cp.nCodCred = c.nCodCred
	left join VehicularSolicitudProspectos v on v.nCodPers = cp.nCodPers and v.iResultado = 1
	left join CatalogoCodigos cco on ncodigo = 4409 and v.iPeorEstadoSentinel = cco.nValor
	left join catalogocodigos ccp on ccp.ncodigo = 4029 and c.nSubProd = ccp.nValor
	left join InmobiliarioPersona i on i.nCodPers = cp.nCodPers 
where 
	ca.CarteraID = @carteraid
	and ca.ProductoID = @ProductoID
GO

if object_id('ClientesCSV') is not null 
	drop procedure dbo.ClientesCSV;
go
CREATE procedure dbo.[ClientesCSV] 
	@carteraid int,
	@ProductoID int
as
select
	case 
	when p.nTipoPersona = 1 
		then left('DNI' + n.cDNI + space(14), 14)
		else left('RUC' + j.cRUC + space(14), 14)
	end  id,
	
	case 
	when p.nTipoPersona = 1 
		then left(n.cApePat + space(45), 45) + left(n.cApeMat + space(45), 45) + left(n.cnombres + space(45), 45)
		else left(j.cRazonSocial + space(135), 135)
	end nombre,

	FORMAT(n.dFecNac, 'yyyyMMdd') dFecNac,
	case when n.nSexo = 1 then 'M' else 'F' end nSexo,
	substring((select cNomCod from CatalogoCodigos cc where ncodigo = 100 and nvalor = n.nEstadoCivil), 1, 1) nEstadoCivil,
	'000000' ubigeo,
	left(p.cDirValor1 + p.cDirValor2 + p.cDirValor3 + p.cDirValor4 + space(54), 54) direccion,
	left(pt.cTelefono + space(9),9) cTelefono
from 
	carteracredito cc
	inner join creditos c on c.ncodcred = cc.nCodCred
	inner join credpersonas cp on cp.nCodCred = c.nCodCred
	inner join persona p on p.nCodPers= cp.nCodPers
	left join personatelefono pt on pt.nCodPers = p.nCodPers	
	left join personanat n on n.ncodpers = p.ncodpers
	left join personajur j on j.ncodpers = p.ncodpers
where 
	CarteraId = @carteraid
	and ProductoID = @ProductoID
GO

if object_id('CreditosCSV') is not null 
	drop procedure dbo.CreditosCSV;
go
CREATE procedure dbo.[CreditosCSV] --1016
	@carteraid int,
	@ProductoID int
as
select
/*
	case 
		when cc.repro <> 0 then left(cc.nCodCred + '-' + cc.repro + space(20), 20)
		else left(convert(nvarchar,cc.nCodCred) + space(20),20)
	end ncodcred,
	*/
	case when ltrim(rtrim(c.cCodCta)) = '' then convert(nvarchar(6),c.nCodCred)
	else ltrim(rtrim(c.cCodCta)) end ncodcred,
	case when p.nTipoPersona = 1 then left('DNI'+n.cDNI + space(19),19) else left('RUC'+ j.cRuc+space(19),19) end id,
	left(convert(nvarchar,c.nPrestamo) + space(15),15) importe,
	left(CONVERT(nvarchar, precios.precio) + 'A' + space(13),13) precio,
	left(convert(nvarchar,CONVERT(DECIMAL(10,6),c.ntasacomp)) + space(12),12) ntasacomp,
	left(convert(nvarchar,CONVERT(DECIMAL(10,6),c.nTasaMor)) + space(8),8) interesMoratorio,
	case 
		when cc.repro <> 0 then left(cc.nCodCred + '-' + cc.repro + space(20), 20)
		else left(convert(nvarchar,cc.nCodCred) + space(20),20)
	end ncodcred2
from 
	creditos c
	inner join (
		select 
			cro.ncodcred,
			sum(cro.ncapital) precio
		from 
			credcronograma cro
			inner join carteracredito cc on cc.nCodCred = cro.ncodcred
			inner join (
				select cro.ncodcred, max(cro.nNroCalendario) mayor
				from credcronograma cro
				group by cro.ncodcred	
			) my on my.ncodcred = cro.ncodcred and my.mayor = cro.nNroCalendario
		where
			cc.CarteraID = @carteraid
			and cc.ProductoID = @ProductoID
			and ( ( year(cro.dFecVcto) = year(getdate()) and month(cro.dFecVcto) > month(getdate()) ) or ( year(cro.dFecVcto) > year(getdate()) ) )
		group by 
			cro.ncodcred
	) precios on precios.ncodcred = c.ncodcred	
	inner join carteracredito cc on cc.ncodcred = c.ncodcred
	inner join credpersonas cp on cp.nCodCred = c.nCodCred
	inner join persona p on p.nCodPers= cp.nCodPers
	left join personanat n on n.ncodpers = p.ncodpers
	left join personajur j on j.ncodpers = p.ncodpers
where 
	cc.CarteraId = @carteraid
	and cc.ProductoID = @ProductoID
GO
------------------------------------------------------------------------------------
if object_id('CerrarCartera') is not null 
	drop procedure CerrarCartera;
go
CREATE procedure dbo.[CerrarCartera]
	@CarteraID int,
	@ProductoID int,
	@FechaCierre DateTime
as
	update carteras set FechaDesembolso = @FechaCierre where CarteraId = @CarteraID and ProductoID = @ProductoID;
	
	insert into CreditosBloqueados
	select nCodCred, @CarteraID, @ProductoID, getdate() from CarteraCredito where CarteraId = @CarteraID; 

	delete from CarteraCredito where carteraid != @CarteraID and ncodcred in(
		select nCodCred from CarteraCredito where CarteraID = @CarteraID
	)
GO

if object_id('CerrarPago') is not null 
	drop procedure CerrarPago;
go
CREATE procedure dbo.[CerrarPago]
	@PagoID int,
	@FechaCierre DateTime
as
	update pagos set FechaCierre = @FechaCierre where PagoID = @PagoID;
	/*
	actualizar estado de las cuotas en el cronograma del fondeador
	*/
GO

if object_id('CerrarRecompra') is not null 
	drop procedure CerrarRecompra;
go
CREATE procedure dbo.[CerrarRecompra]
	@RecompraID int,
	@FechaCierre DateTime
as
	update recompras set FechaCierre = @FechaCierre where RecompraID = @RecompraID;
	/*
	actualizar estado de las cuotas en el cronograma del fondeador
	*/
GO

if object_id('CerrarSesionSP') is not null 
	drop procedure CerrarSesionSP;
go
create procedure dbo.[CerrarSesionSP]
	@token varchar(100)
as
	update Usuarios set SesionToken = '' where SesionToken = @token;
GO


if object_id('CrearCartera') is not null 
	drop procedure CrearCartera;
go
CREATE procedure [dbo].[CrearCartera] --'emartinez', 1,  '991,992,993'
	@FondeadorID int,
	@ProductoID int,
	@CreadoPor varchar(200),
	@creditos nvarchar(max),
	@creado datetime
as
	/*

	ALGORITMO
	si (hay una cartera con el mismo producto) entonces
		n = es la ultima cartera con este producto
		si (esa cartera tiene creditos) entonces
			n = n + 1
			se crea la cartera
			devuelve n
		sino
			devolver n
		fin si
	sino
		n = inicial
		se crea la cartera con los creditos especificados si los hay
	fin si


	*/
	declare @CarteraID int = 0;
	declare @inicial int;

	--VALORES INICIALES PARA CADA CARTERA SEGÚN PRODUCTO
	if @ProductoID = 2	set @inicial = 363;			--yapamotors
	else if @ProductoID = 10 set @inicial = 4;		--pavivir
	else if @ProductoID = 1  set @inicial = 302;	--papymes
	else set @inicial = 1;							--default

	if(exists(select * from carteras where ProductoID = @ProductoID))
	begin
		set @CarteraID = ( select max(carteraid) from carteras where ProductoID = @ProductoID );
		if(exists(select * from carteracredito where productoid = @ProductoID and carteraid = @CarteraID))
		begin
			set @CarteraID = @CarteraID + 1
			insert into Carteras values(
				@CarteraID,
				@ProductoID,
				@FondeadorID,
				@creado,
				null,
				null,
				@CreadoPor
			)

			insert into CarteraCredito 
			select 
				@CarteraID, 
				@ProductoID,
				a.tuple,
				case when dbo.EsRepro(a.tuple) = 1 then dbo.CuantasRepro(a.tuple)
				else 0 end repro
			from 
				dbo.split_string(@creditos, ',') a;

			select @CarteraID cartera;
		end
		else
			select @CarteraID cartera;
	end
	else
	begin
		set @CarteraID = @inicial;
		insert into Carteras values(
			@CarteraID,
			@ProductoID,
			@FondeadorID,
			@creado,
			null,
			null,
			@CreadoPor
		)

		insert into CarteraCredito 
		select 
			@CarteraID, 
			@ProductoID,
			a.tuple,
			case when dbo.EsRepro(a.tuple) = 1 then dbo.CuantasRepro(a.tuple)
			else 0 end repro
		from 
			dbo.split_string(@creditos, ',') a;

		select @CarteraID cartera;
	end
GO

if object_id('CrearPago') is not null 
	drop procedure CrearPago;
go
CREATE procedure dbo.[CrearPago] --'emartinez', 1,  '991,992,993'
	@CreadoPor varchar(200),
	@Fondeadora int,
	@cuotas nvarchar(max)
as

GO

if object_id('CrearRecompra') is not null 
	drop procedure CrearRecompra;
go
CREATE procedure dbo.[CrearRecompra] --'emartinez', 1,  '991,992,993'
	@CreadoPor varchar(200),
	@Fondeadora int,
	@creditos nvarchar(max)
as
	declare @recompra int;

	insert into Recompras values(
		@Fondeadora,
		getdate(),
		getdate(),
		0,
		@CreadoPor
	)

	set @recompra = @@identity;

	insert into CreditoRecompra 
	select @recompra, * 
	from split_string(@creditos, ',');
GO




if object_id('EditarCartera') is not null 
	drop procedure EditarCartera;
go
CREATE procedure dbo.[EditarCartera]
	@CarteraID int,
	@ProductoID int,
	@FondeadorID int,
	@creditos nvarchar(max),
	@creado datetime
as
	delete from CarteraCredito where CarteraID = @CarteraID;

	update 
		Carteras 
	set 
		FondeadorID = @FondeadorID, 
		ProductoID = @ProductoID, 
		Modificado = getdate(), 
		Creado = @creado
	where 
		CarteraID = @CarteraID 
		and ProductoID = @ProductoID;

	insert into CarteraCredito 
	select @CarteraID, @ProductoID, f.tuple, 1 
	from dbo.split_string(@creditos, ',') f
GO

if object_id('EditarPago') is not null 
	drop procedure EditarPago;
go
create procedure dbo.[EditarPago]
	@PagoID int,
	@Fondeador int,
	@cuotas nvarchar(max)
as
GO

if object_id('EditarRecompra') is not null 
	drop procedure EditarRecompra;
go
create procedure dbo.[EditarRecompra]
	@RecompraID int,
	@Fondeador int,
	@creditos nvarchar(max)
as
	delete from Recompras where RecompraID = @RecompraID;

	update Recompras set FondeadorID = @Fondeador, Modificado = getdate() where RecompraID = @RecompraID;

	insert into CreditoRecompra 
	select @RecompraID, * 
	from split_string(@creditos, ',')
GO

if object_id('EliminarCartera') is not null 
	drop procedure EliminarCartera;
go
create procedure  dbo.[EliminarCartera]
	@CarteraID int,
	@ProductoID int
as
	delete from CarteraCredito where CarteraId in(
		select CarteraId from CarteraCredito where CarteraId = @CarteraID
	)

	delete from CreditosBloqueados where CarteraId = @CarteraID and ProductoID = @ProductoID

	delete from carteras where CarteraId = @carteraid;
GO

if object_id('EliminarPago') is not null 
	drop procedure EliminarPago;
go
create procedure  dbo.[EliminarPago]
	@PagoID int
as
	delete from CuotaPago where PagoID = @PagoID;

	delete from pagos where PagoID = @PagoID;
GO

if object_id('EliminarRecompra') is not null 
	drop procedure EliminarRecompra;
go
create procedure  dbo.[EliminarRecompra]
	@RecompraID int
as
	delete from CreditoRecompra where RecompraID = @RecompraID;

	delete from recompras where RecompraID = @RecompraID
GO

if object_id('EvaluaCreditos') is not null 
	drop procedure EvaluaCreditos;
go
CREATE procedure dbo.[EvaluaCreditos]
	@FondeadorID int,
	@creditos nvarchar(max)
as
begin 	
	--declare @FondeadorID int;
	--declare @creditos nvarchar(max) = '120778,120099,120077,120061,119800';
	declare @Funcion nvarchar(100);

	select @Funcion = evaluador from Fondeadores where FondeadorID = @FondeadorID;

	declare @sql nvarchar(max) = N'select c.nCodCred from creditos c where '+ @Funcion +'(c.nCodCred) = 1 and c.nCodCred in('+@creditos+')';

	exec sp_executesql @sql;
end
GO

if object_id('LoginSP') is not null 
	drop procedure LoginSP;
go
CREATE procedure dbo.[LoginSP] --'asalazar', '12345'
	@usuario varchar(100),
	@clave varchar(100)
as
	declare @token varchar(100);
	declare @usuarioID int;
	if(exists(
		select 
			u.*,
			r.*
		from Usuarios u
		inner join RolUsuario ru on ru.UsuarioID = u.UsuarioID
		inner join Roles r on r.RolID = ru.RolID
		where
			u.Nombre = @usuario 
			and u.Clave = @clave	
	))
	begin
		select 
			@usuarioID = u.UsuarioID
		from Usuarios u
		inner join RolUsuario ru on ru.UsuarioID = u.UsuarioID
		inner join Roles r on r.RolID = ru.RolID
		where
			u.Nombre = @usuario 
			and u.Clave = @clave
		
		select @token = substring(replace(newid(), '-', ''), 1, 100);

		update Usuarios set SesionToken = @token where UsuarioID = @usuarioID;

		select 
			u.*,
			r.*
		from Usuarios u
		inner join RolUsuario ru on ru.UsuarioID = u.UsuarioID
		inner join Roles r on r.RolID = ru.RolID
		where
			u.Nombre = @usuario 
			and u.Clave = @clave
	end
	else
			select 
			u.*,
			r.*
		from Usuarios u
		inner join RolUsuario ru on ru.UsuarioID = u.UsuarioID
		inner join Roles r on r.RolID = ru.RolID
		where
			u.Nombre = @usuario 
			and u.Clave = @clave
GO

if object_id('Resumen') is not null 
	drop procedure Resumen;
go
CREATE procedure dbo.[Resumen] --1011
	@carteraid int
as
select
	'' as codsbs,
	case	
	when p.nTipoPersona = 1 
		then 'DNI' + n.cDNI
		else 'RUC' + j.cRUC
	end  iddeudor,

	n.cApePat+' '+n.cApeMat+' '+n.cnombres apellidos_nombres,
	c.nCodCred credito,
	'MICRO EMPRESA' tipoCredito,
	c.nPrestamo monto,
	case when c.nMoneda = 1 then 'S'
	else 'USD' end moneda,
	c.nTasaMor tem,
	c.nTasaComp tea,
	c.dFecCancel fvencim,
	c.nNroCuotas nrocuotas,
	c.nNroCuotas nrocuotaspend,
	0 clasif,
	'VIGENTE' sitcart,
	'SIN GARANTIA' tipogarantia,
	c.nPrestamo valgarantia,
	'MODALIDAD' contado,
	'' utilidad,
	f.Nombre empresa

from 
	creditos c
	inner join credpersonas cp on cp.nCodCred = c.nCodCred
	inner join persona p on p.nCodPers= cp.nCodPers
	left join personatelefono pt on pt.nCodPers = p.nCodPers
	inner join CarteraCredito cc on cc.CarteraId = @carteraid and cc.nCodCred = c.nCodCred
	inner join carteras car on car.CarteraId = cc.CarteraId
	inner join Fondeadores f on car.FondeadorId = f.FondeadorID
	left join personanat n on n.ncodpers = p.ncodpers
	left join personajur j on j.ncodpers = p.ncodpers
where 
	cp.nrelacion = 10
	and
	c.nEstado = 1
GO

if object_id('GetCreditosPaginados') is not null 
	drop procedure dbo.GetCreditosPaginados;
go
create procedure [dbo].[GetCreditosPaginados]
	@page int,
	@pagesize int,
	@producto int
as
--declare @page int = 1;
--declare @pagesize int = 10;
--declare @producto int = 2;
declare @totalpages int;

declare @start int = ( @page - 1 ) * @pagesize; 
declare @end int = @start + @pagesize; 

declare @numberedTable table(
	row# int,
	nCodcred int
)

insert into @numberedTable
select 
	ROW_NUMBER() OVER(ORDER BY c.ncodcred ASC) AS row#,
	c.ncodcred
from 
	creditos c
	inner join CatalogoCodigos cod on c.nSubProd = cod.nValor
where
	cod.ncodigo = 4029
	and c.nestado = 1
    and c.nCodCred not in(select nCodCred from CreditosBloqueados)
	and cod.nvalor not in(3,4,5)
	and 
	(
		( @producto = 1 and cod.nvalor = 1 ) 
		or
		( @producto = 10 and cod.nvalor = 10 ) 
		or
		( @producto = 2 and cod.nvalor in(9,8,7,6,2) ) 
		or
		( @producto = 0 )
	)

declare @module int;

select @module = (max(row#) % @pagesize) from @numberedTable;

if(@module <> 0)
	select @totalpages = ((max(row#) / @pagesize) + 1) from @numberedTable;
else
	select @totalpages = (max(row#) / @pagesize) from @numberedTable;

select
	pag.row#,
	(n.cDNI) dni,
	n.cnombres + ' '  +  n.cApePat + ' ' + n.cApeMat  nombres,
	(j.cRuc) ruc,
	(j.cRazonSocial) razonSocial,
	c.*,
	@totalpages pages,
	cod.nvalor codigoProducto,
	cod.cNomCod nombreProducto,
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
	inner join @numberedTable pag on pag.ncodcred = c.ncodcred
where
	cod.ncodigo = 4029
	and c.nestado = 1
    and c.nCodCred not in(select nCodCred from CreditosBloqueados)
	and cod.nvalor not in(3,4,5)
	and 
	(
		( @producto = 1 and cod.nvalor = 1 ) 
		or
		( @producto = 10 and cod.nvalor = 10 ) 
		or
		( @producto = 2 and cod.nvalor in(9,8,7,6,2) ) 
		or
		( @producto = 0 )
	)
	and pag.row# > @start 
	and pag.row# <= @end
order by pag.row# desc
go


if object_id('GetCreditosPaginadosRepro') is not null 
	drop procedure dbo.GetCreditosPaginadosRepro;
go
create procedure dbo.GetCreditosPaginadosRepro
	@page int,
	@pagesize int,
	@producto int
as
--declare @page int = 1;
--declare @pagesize int = 10;
--declare @producto int = 2;

declare @totalpages int;

declare @start int = ( @page - 1 ) * @pagesize; 
declare @end int = @start + @pagesize; 

declare @numberedTable table(
	row# int,
	nCodcred int
)

declare @repros table(
	nCodcred int
)
insert into @repros
select distinct nCodCred
from credcronograma 
where 
nTipoCrono = 2


insert into @numberedTable
select 
	ROW_NUMBER() OVER(ORDER BY c.ncodcred ASC) AS row#,
	c.ncodcred
from 
	creditos c
	inner join CatalogoCodigos cod on c.nSubProd = cod.nValor
where
	cod.ncodigo = 4029
	and c.nCodCred in(select nCodCred from @repros)
    and c.nCodCred not in(select nCodCred from CreditosBloqueados)
	and cod.nvalor not in(3,4,5)
	and 
	(
		( @producto = 1 and cod.nvalor = 1 ) 
		or
		( @producto = 10 and cod.nvalor = 10 ) 
		or
		( @producto = 2 and cod.nvalor in(9,8,7,6,2) ) 
		or
		( @producto = 0 )
	)	

declare @module int;

select @module = (max(row#) % @pagesize) from @numberedTable;

if(@module <> 0)
	select @totalpages = ((max(row#) / @pagesize) + 1) from @numberedTable;
else
	select @totalpages = (max(row#) / @pagesize) from @numberedTable;

select
	pag.row#,
	(n.cDNI) dni,
	n.cnombres + ' '  +  n.cApePat + ' ' + n.cApeMat  nombres,
	(j.cRuc) ruc,
	(j.cRazonSocial) razonSocial,
	c.*,
	@totalpages pages,
	cod.nvalor codigoProducto,
	cod.cNomCod nombreProducto,
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
	inner join @numberedTable pag on pag.ncodcred = c.ncodcred
where
	cod.ncodigo = 4029
	and c.nCodCred in(select nCodCred from @repros)
    and c.nCodCred not in(select nCodCred from CreditosBloqueados)
	and cod.nvalor not in(3,4,5)
	and 
	(
		( @producto = 1 and cod.nvalor = 1 ) 
		or
		( @producto = 10 and cod.nvalor = 10 ) 
		or
		( @producto = 2 and cod.nvalor in(9,8,7,6,2) ) 
		or
		( @producto = 0 )
	)
	and pag.row# > @start 
	and pag.row# <= @end
order by pag.row# desc
go

if object_id('FindCredito') is not null 
	drop procedure dbo.FindCredito;
go
create procedure [dbo].[FindCredito]
	@tipo nvarchar(10),
	@q nvarchar(max),
	@fecha datetime
as
if @tipo = 'credito' 
		select
			(n.cDNI) dni,
			n.cnombres + ' '  +  n.cApePat + ' ' + n.cApeMat  nombres,
			(j.cRuc) ruc,
			(j.cRazonSocial) razonSocial,
			c.*,
			cod.nvalor codigoProducto,
			cod.cNomCod nombreProducto,
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
					sum(ncapital) precio 
				from 
					credcronograma cro
					inner join (
						select 
							cro.nCodCred,							
							case when cro.nCodCred = 117489 then 2
							else max(cro.nNroCalendario) end topCalendario
						from 
							credcronograma cro
						group by 
							cro.ncodcred
					) neo on neo.nCodCred = cro.ncodcred
				where
					cro.nnroCalendario = neo.topCalendario
					and ((year(cro.dFecVcto) = year(@fecha) and month(cro.dFecVcto) > month(@fecha)) or year(cro.dFecVcto) > year(@fecha))
				group by 
					cro.nCodCred
			) beta on beta.nCodCred = c.nCodCred
		where
			cod.ncodigo = 4029
			and c.nCodCred not in(select nCodCred from CreditosBloqueados)
			and cod.nvalor not in(3,4,5)
			and c.nCodCred in(select convert(int,tuple) tuple from dbo.split_string(@q, ','))-- = convert(int,@q)
if @tipo = 'dni' 
		select
			(n.cDNI) dni,
			n.cnombres + ' '  +  n.cApePat + ' ' + n.cApeMat  nombres,
			(j.cRuc) ruc,
			(j.cRazonSocial) razonSocial,
			c.*,
			cod.nvalor codigoProducto,
			cod.cNomCod nombreProducto,
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
					sum(ncapital) precio 
				from 
					credcronograma cro
					inner join (
						select 
							cro.nCodCred,
							case when cro.nCodCred = 117489 then 2
							else max(cro.nNroCalendario) end topCalendario
						from 
							credcronograma cro
						group by 
							cro.ncodcred
					) neo on neo.nCodCred = cro.ncodcred
				where 
					cro.nnroCalendario = neo.topCalendario
					and ((year(cro.dFecVcto) = year(@fecha) and month(cro.dFecVcto) > month(@fecha)) or year(cro.dFecVcto) > year(@fecha))
				group by 
					cro.nCodCred
			) beta on beta.nCodCred = c.nCodCred
		where
			cod.ncodigo = 4029
			and c.nCodCred not in(select nCodCred from CreditosBloqueados)
			and cod.nvalor not in(3,4,5)
			and n.cDNI in(select tuple from dbo.split_string(@q, ','))
if @tipo = 'ruc' 
		select
			(n.cDNI) dni,
			n.cnombres + ' '  +  n.cApePat + ' ' + n.cApeMat  nombres,
			(j.cRuc) ruc,
			(j.cRazonSocial) razonSocial,
			c.*,
			cod.nvalor codigoProducto,
			cod.cNomCod nombreProducto,
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
					sum(ncapital) precio 
				from 
					credcronograma cro
					inner join (
						select 
							cro.nCodCred,
							case when cro.nCodCred = 117489 then 2
							else max(cro.nNroCalendario) end topCalendario
						from 
							credcronograma cro
						group by 
							cro.ncodcred
					) neo on neo.nCodCred = cro.ncodcred
				where 
					cro.nnroCalendario = neo.topCalendario
					and ((year(cro.dFecVcto) = year(@fecha) and month(cro.dFecVcto) > month(@fecha)) or year(cro.dFecVcto) > year(@fecha))
				group by 
					cro.nCodCred
			) beta on beta.nCodCred = c.nCodCred
		where
			cod.ncodigo = 4029
			and c.nCodCred not in(select nCodCred from CreditosBloqueados)
			and cod.nvalor not in(3,4,5)
			and j.cRUC in(select tuple from dbo.split_string(@q, ','))
go


if object_id('FindCartera') is not null 
	drop procedure FindCartera;
go
create procedure dbo.FindCartera --363,2
	@cartera int,
	@producto int
as
declare @fecha datetime;

select @fecha = Creado from carteras where CarteraID = @cartera and ProductoID = @producto;

declare @table table(
	CarteraID int,
	ProductoID int,
	FondeadorID int,
	Nombre nvarchar(10),
	Creado datetime,
	Modificado Datetime,
	FechaDesembolso datetime,
	CreadoPor nvarchar(10),
	creditoID int,
	precio decimal(10,2)
)

insert into @table 
select 
	ca.CarteraID,
	ca.ProductoID,
	ca.FondeadorID,
	f.Nombre,
	ca.Creado,
	ca.Modificado,
	ca.FechaDesembolso,
	ca.CreadoPor,
	cc.nCodCred,
	a.precio
from 
	carteras ca
	inner join fondeadores f on ca.FondeadorID = f.FondeadorID
	inner join CarteraCredito cc on cc.CarteraId = ca.CarteraID and cc.ProductoID = ca.ProductoID
	inner join (
		select 
			cro.ncodcred,
			sum(cro.ncapital) precio
		from 
			credcronograma cro
			inner join carteracredito cc on cc.nCodCred = cro.ncodcred
			inner join (
				select cro.ncodcred, 
				case when cro.nCodCred = 117489 then 2
				else max(cro.nNroCalendario) end mayor
				from credcronograma cro
				group by cro.ncodcred	
			) my on my.ncodcred = cro.ncodcred and my.mayor = cro.nNroCalendario
		where
			cc.CarteraID = @cartera
			and cc.ProductoID = @producto
			and ( ( year(cro.dFecVcto) = year(@fecha) and month(cro.dFecVcto) > month(@fecha) ) or ( year(cro.dFecVcto) > year(@fecha) ) )
		group by 
			cro.ncodcred
	) a on a.ncodcred = cc.nCodCred
where ca.carteraID = @cartera
and ca.ProductoID = @producto

select 
	a.*,	
	(n.cDNI) dni,
	n.cnombres + ' '  +  n.cApePat + ' ' + n.cApeMat  nombres,
	(j.cRuc) ruc,
	(j.cRazonSocial) razonSocial,
	c.*,
	a.precio,
	a.FondeadorID,
	a.Nombre
from 
	carteracredito cc
	inner join creditos c on c.nCodCred = cc.nCodCred
	inner join credpersonas p on p.ncodcred = cc.ncodcred
	left join personanat n on n.ncodpers = p.ncodpers
	left join personajur j on j.ncodpers = p.ncodpers
	inner join @table a on a.creditoID = c.nCodCred
where 
	cc.carteraid = @cartera 
	and cc.productoid = @producto
go

if object_id('GetCarteras') is not null 
	drop procedure GetCarteras;
go
create procedure [dbo].[GetCarteras]
	@producto int
as
declare @precios table(
	CarteraID int,
	precio float
)

insert into @precios
select 
	ca.CarteraID,
	sum(cro.ncapital) precio
from 
	carteras ca
	inner join carteracredito cc on cc.CarteraId = ca.CarteraID and cc.ProductoID = ca.ProductoID
	inner join (
		select cro.ncodcred, 		
		case when cro.nCodCred = 117489 then 2
		else max(cro.nNroCalendario) end mayor
		from credcronograma cro
		group by cro.nCodCred
	) may on may.nCodcred = cc.nCodCred
	inner join credcronograma cro on cro.ncodcred = cc.nCodCred and cro.nNroCalendario = may.mayor 
where 
	ca.ProductoID = @producto
	and (( year(cro.dFecVcto) = year(ca.Creado) and month(cro.dFecVcto) > month(ca.Creado) ) or year(cro.dFecVcto) > year(ca.Creado))
group by 
	ca.CarteraID

select 
	ca.*,
	pr.precio,
	f.*,
	cod.*	
from 
	carteras ca 
	inner join @precios pr on pr.CarteraID = ca.CarteraID
	inner join Fondeadores f on f.FondeadorID = ca.FondeadorID
	inner join CatalogoCodigos cod on ca.ProductoID = cod.nValor and cod.ncodigo = 4029
go

if object_id('GetCronogramas') is not null 
	drop procedure GetCronogramas;
go
create procedure dbo.GetCronogramas
	@tipo int,
	@nCodCred int
as
if @tipo = 1
begin
	select 
		cro.nNroCalendario,
		cro.*	
	from 
		credcronograma cro
	where 
		cro.nCodCred = @nCodCred
end
else
begin
	select 
		cro.nNroCalendario,
		cro.*	
	from 
		credcronograma cro
	where 
		cro.nCodCred = @nCodCred
end