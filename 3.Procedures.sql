/*
* CARTERAS
*
*
**/
if object_id('AnexoYapamotors') is not null  drop procedure dbo.AnexoYapamotors;
go
CREATE procedure [dbo].[AnexoYapamotors] --363,2
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

if object_id('ResumenYapamotors') is not null  drop procedure dbo.ResumenYapamotors;
go
create procedure [dbo].[ResumenYapamotors] --363,2
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
GO
if object_id('CalificacionesCSV') is not null  drop procedure dbo.CalificacionesCSV;
go
CREATE procedure [dbo].[CalificacionesCSV] 
	@carteraid int,
	@ProductoID int
as
select /*
	case 
		when cc.repro <> 0 then left(cc.nCodCred + '-' + cc.repro + space(20), 20)
		else left(convert(nvarchar,cc.nCodCred) + space(20),20)
	end ncodcred,*/
	
	left(((case 
		when ltrim(rtrim(c.cCodCta)) = '' then convert(nvarchar(6),c.nCodCred)
		else ltrim(rtrim(c.cCodCta)) 
	end) + space(20)),20) ncodcred,

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
if object_id('CreditosCSV') is not null  drop procedure dbo.CreditosCSV;
go
CREATE procedure [dbo].[CreditosCSV] --[dbo].[CreditosCSV] 363, 2
	@carteraid int,
	@ProductoID int
as
select
	left((case when ltrim(rtrim(c.cCodCta)) = '' then convert(nvarchar(6),c.nCodCred) else ltrim(rtrim(c.cCodCta)) end)+space(20),20) CodigoCredito,

	case when p.nTipoPersona = 1 then 'DNI' else 'RUC' end TipoDocumento,

	left((case when p.nTipoPersona = 1 then n.cDNI else j.cRuc end)+space(8),8) NroDocumento, --'41537590'

	right(space(15) + convert(nvarchar,c.nPrestamo),15) Importe,

	case 
		when c.nCodCred = 122809 then left(CONVERT(nvarchar, (select sum(amortizacion) from cronogramasalternativos)) + space(15),15)
		else left(CONVERT(nvarchar, precios.precio) + space(15),15) 
	end Precio,

	'A' PeriodoTasa,

	left(convert(nvarchar,CONVERT(DECIMAL(10,6),c.ntasacomp)) + space(11),11) Interes,

	left(convert(nvarchar,CONVERT(DECIMAL(10,6),c.nTasaMor)) + space(11),11) InteresMoratorio,

	left((case when ltrim(rtrim(c.cCodCta)) = '' then convert(nvarchar(6),c.nCodCred) else ltrim(rtrim(c.cCodCta)) end)+space(20),20) CodigoCredito2

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
if object_id('CronogramasCSV') is not null  drop procedure dbo.CronogramasCSV;
go
CREATE procedure [dbo].[CronogramasCSV] --303,1
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
    

	declare @res table(
		ncodcred nvarchar(100),
		cuotaFecha nvarchar(100),
		nCapital nvarchar(100),
		nInteres nvarchar(100),
		nPerGracia nvarchar(100),
		encaje nvarchar(100),
		total nvarchar(100)
	)

	insert into @res
	select 
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

	delete from @res where ncodcred like '%122770%'

	insert into @res
	select 
		ltrim(rtrim(nCodCred)) ncodcred,
		left(convert(nvarchar,nNroCuota) + FORMAT(dFecPago, 'yyyyMMdd') + space(17),17) cuotaFecha,
		left(convert(nvarchar,convert(decimal(10,2),amortizacion)) + space(15),15)  nCapital,
		left(convert(nvarchar,convert(decimal(10,2),interes)) + space(17),17) nInteres,
		left(convert(nvarchar,(convert(decimal(10,2), periodoGracia))) + space(17),17) nPerGracia,
		left('0.00' + space(11),11) encaje,
		left(convert(nvarchar,convert(decimal(10,2), (totalCuota))) + space(8),8) total	
	from cronogramasalternativos

	select * from @res

GO
if object_id('CerrarCartera') is not null  drop procedure dbo.CerrarCartera;
go
CREATE procedure [dbo].[CerrarCartera]
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
if object_id('CrearCartera') is not null  drop procedure dbo.CrearCartera;
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
if object_id('EditarCartera') is not null  drop procedure dbo.EditarCartera;
go
CREATE procedure [dbo].[EditarCartera]
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
if object_id('EliminarCartera') is not null  drop procedure dbo.EliminarCartera;
go
create procedure  [dbo].[EliminarCartera]
	@CarteraID int,
	@ProductoID int
as
	delete from CarteraCredito where CarteraId in(
		select CarteraId from CarteraCredito where CarteraId = @CarteraID
	)

	delete from CreditosBloqueados where CarteraId = @CarteraID and ProductoID = @ProductoID

	delete from carteras where CarteraId = @carteraid;
GO
if object_id('EvaluaCreditos') is not null  drop procedure dbo.EvaluaCreditos;
go
CREATE procedure [dbo].[EvaluaCreditos]
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
if object_id('FindCartera') is not null  drop procedure dbo.FindCartera;
go
CREATE procedure [dbo].[FindCartera] --363,2
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
			case when cro.ncodcred = 122809 then  293388.23 
			else sum(cro.ncapital) end precio
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
GO
if object_id('FindCredito') is not null  drop procedure dbo.FindCredito;
go
CREATE procedure [dbo].[FindCredito]  --[dbo].[FindCredito] 'credito', '123494', '20200701', 1
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
			(select sum(monto) from PagoDetalle x where x.nCodCred = c.nCodCred and EsDeuda = 1) deuda,
			c.*,
			cf.FondeadorID,
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
			left join dbo.CreditoFondeador cf on cf.ncodcred = c.ncodcred
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
			(select sum(monto) from CuotaPagoDeuda x where x.nCodCred = c.nCodCred and EsDeuda = 1) deuda,
			c.*,
			cf.FondeadorID,
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
			left join dbo.CreditoFondeador cf on cf.ncodcred = c.ncodcred
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
			(select sum(monto) from CuotaPagoDeuda x where x.nCodCred = c.nCodCred and EsDeuda = 1) deuda,
			c.*,
			cf.FondeadorID,
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
			left join dbo.CreditoFondeador cf on cf.ncodcred = c.ncodcred
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
GO
if object_id('GetCarteras') is not null  drop procedure dbo.GetCarteras;
go
CREATE procedure [dbo].[GetCarteras]
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


--CONDICIÓN FORJADA PARA QUE APAREZCA UN PRECIO ESPECÍFICO EN LA CARTERA 308
UPDATE @PRECIOS SET PRECIO =  8185491.66 WHERE CARTERAID = 303;

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
GO

/*
* PAGOS
*
*
**/
if object_id('CerrarPago') is not null  drop procedure dbo.CerrarPago;
go
CREATE procedure [dbo].[CerrarPago]
	@PagoID int,
	@FechaCierre DateTime
as
	update pagos set FechaCierre = @FechaCierre where PagoID = @PagoID;
	/*
	actualizar estado de las cuotas en el cronograma del fondeador
	*/
GO
if object_id('CrearPago') is not null  drop procedure dbo.CrearPago;
go
CREATE procedure [dbo].[CrearPago]( --[dbo].[CrearPago] 1,2,'krobles','123549,1,1,370,0;123549,1,2,10.00,1;'
	@FondeadorID int,
	@ProductoID int,
	@CreadoPor nvarchar(100),
	@Pagos nvarchar(max)
)
as
begin
	declare @PagoID int;

	insert into dbo.Pagos(FondeadorID, ProductoID, CreadoPor, Creado, Modificado, FechaCierre)
	values(@FondeadorID, @ProductoID, @CreadoPor, getdate(), getdate(), null);

	set @PagoID = @@identity;

	declare @pagodetalles table(
		#row int,
		credito int,
		calendario int,
		cuota int,
		monto decimal(10,2),
		esdeuda bit
	);

	-- METO EL INPUT DEL USUARIO EN UNA TABLA AUXILIAR
	insert into @pagodetalles
	select ROW_NUMBER() OVER (Order by nCodCred, nNroCalendario, nNroCuota) AS RowNumber, * 
	from dbo.CuotaDeudaToTable(@Pagos)

	-- CREO VARIABLES AUXILIARES

	declare @capital decimal(10,2);
	declare @intereses decimal(10,2);
	declare @gracia decimal(10,2);

	declare @capital_meta decimal(10,2);
	declare @interes_meta decimal(10,2);
	declare @gracia_meta decimal(10,2);

	declare @credito decimal(10,2);
	declare @calendario decimal(10,2);
	declare @cuota decimal(10,2);
	declare @monto decimal(10,2);
	declare @esdeuda bit;

	declare @contador int = 1;
	declare @longitud int = (select count(*) from @pagodetalles);

	while @contador <= @longitud
	begin
		-- IDENTIFICO EL MONTO DISPONIBLE PARA EL PAGO QUE VIENE DEL INPUT
		select 
			@credito = credito, 
			@calendario = calendario, 
			@cuota = cuota,
			@monto = monto,
			@esdeuda = esdeuda
		from @pagodetalles 
		where #row = @contador;

		if(@esdeuda = 1)
		begin
			insert into dbo.PagoDetalle(PagoID, nCodCred, nNroCalendario, nNroCuota, PagoConceptoID, Monto, EsDeuda)
			values(@PagoID, @credito, @calendario, @cuota, null, @monto, 1);
		end
		else
		begin 
			-- IDENTIFICO LA DEUDA ESPECIFICA DE ESA CUOTA 
			select 
				@capital_meta = ncapital,
				@interes_meta = ninterescomp,
				@gracia_meta = npergracia
			from credcronograma 
			where ncodcred = @credito 
			and nnrocalendario = @calendario
			and nnrocuota = @cuota

			if(@monto <= @gracia_meta) -- select * from pagoconceptos
			begin
				insert into dbo.PagoDetalle(PagoID, nCodCred, nNroCalendario, nNroCuota, PagoConceptoID, Monto, EsDeuda)
				values(@PagoID, @credito, @calendario, @cuota, 1, @monto, 0);
				set @monto = 0;
			end
			else
			begin			
				insert into dbo.PagoDetalle(PagoID, nCodCred, nNroCalendario, nNroCuota, PagoConceptoID, Monto, EsDeuda)
				values(@PagoID, @credito, @calendario, @cuota, 1, @capital_meta, 0);
				set @monto = @monto - @gracia_meta;

				if(@monto > 0)
				begin			
					if(@monto <= @interes_meta)
					begin
						insert into dbo.PagoDetalle(PagoID, nCodCred, nNroCalendario, nNroCuota, PagoConceptoID, Monto, EsDeuda)
						values(@PagoID, @credito, @calendario, @cuota, 2, @monto, 0);
						set @monto = 0;
					end
					else
					begin			
						insert into dbo.PagoDetalle(PagoID, nCodCred, nNroCalendario, nNroCuota, PagoConceptoID, Monto, EsDeuda)
						values(@PagoID, @credito, @calendario, @cuota, 2, @interes_meta, 0);
						set @monto = @monto - @interes_meta;

						if(@monto > 0)
						begin
							if(@monto <= @capital_meta)
							begin
								insert into dbo.PagoDetalle(PagoID, nCodCred, nNroCalendario, nNroCuota, PagoConceptoID, Monto, EsDeuda)
								values(@PagoID, @credito, @calendario, @cuota, 3, @monto, 0);
								set @monto = 0;
							end
							else
							begin			
								insert into dbo.PagoDetalle(PagoID, nCodCred, nNroCalendario, nNroCuota, PagoConceptoID, Monto, EsDeuda)
								values(@PagoID, @credito, @calendario, @cuota, 3, @gracia_meta, 0);
								set @monto = @monto - @capital_meta;
							end
						end
					end		
				end
			end
		end

		set @contador = @contador + 1;
	end
end
GO
if object_id('EditarPago') is not null  drop procedure dbo.EditarPago;
go
create procedure [dbo].[EditarPago]
	@PagoID int,
	@Fondeador int,
	@cuotas nvarchar(max)
as
GO
if object_id('EliminarPago') is not null  drop procedure dbo.EliminarPago;
go
CREATE procedure [dbo].[EliminarPago](
	@pagoID int
)
as
begin
	delete from dbo.Pagos where pagoid = @pagoID;
	delete from dbo.CuotaPagoDeuda where pagoid = @pagoID;
end
GO
if object_id('FindPago') is not null  drop procedure dbo.FindPago;
go
CREATE procedure [dbo].[FindPago](
	@PagoID int
)
as
begin
	select
	 p.*,
	 c.*,
	 f.*,
	 pro.*
	from dbo.pagos p
	inner join dbo.cuotapagodeuda c on c.PagoID = p.PagoID
	inner join Fondeadores f on p.FondeadorID = f.FondeadorID
	inner join CatalogoCodigos pro on p.ProductoID = pro.nValor and pro.ncodigo = 4029
	where p.PagoID = @PagoID
end
GO
if object_id('GetPagos') is not null  drop procedure dbo.GetPagos;
go
CREATE procedure [dbo].[GetPagos]
as
begin
	select 
	 p.*,
	 c.*,
	 f.*,
	 pro.*,
	 con.*
	from dbo.pagos p
	inner join dbo.PagoDetalle c on c.PagoID = p.PagoID
	left join dbo.PagoConceptos con on c.PagoConceptoID = con.PagoConceptoID
	inner join Fondeadores f on p.FondeadorID = f.FondeadorID
	inner join CatalogoCodigos pro on p.ProductoID = pro.nValor and pro.ncodigo = 4029
end
GO
if object_id('PagosExcel') is not null  drop procedure dbo.PagosExcel;
go
CREATE procedure [dbo].[PagosExcel]
	@pagoID int
as

select 
	distinct
	case when p.nTipoPersona = 1 then n.cDNI else j.cRUC end Identificacion,
	case when p.nTipoPersona = 1 then n.capepat else '' end ApellidoPat,
	case when p.nTipoPersona = 1 then n.capemat else '' end ApellidoMat,
	case when p.nTipoPersona = 1 then n.cnombres else j.crazonsocial end Nombres,
	case when c.cCodCta != null and c.cCodCta != '' then c.cCodCta else c.nCodCred end CodigoCredito,
	d.nNroCuota NroCuota,
	CONVERT(varchar, pagos.creado,103) FechaPago,
	z.monto PeriodoGracia,
	y.monto Interes,
	x.monto Amortizacion,
	0 Encaje,
	x.monto+y.monto+z.monto TotalCuota
from 
	pagodetalle d
	inner join pagos on pagos.pagoid = d.pagoid
	inner join (
		select distinct d.ncodcred, d.nNroCalendario, d.nNroCuota, d.Monto
		from pagodetalle d
		where pagoid = 4 and d.PagoConceptoID = 1
	) x on x.nCodCred = d.nCodCred and x.nNroCalendario = d.nNroCalendario and x.nNroCuota = d.nNroCuota
	inner join (
		select distinct d.ncodcred, d.nNroCalendario, d.nNroCuota, d.Monto
		from pagodetalle d
		where pagoid = 4 and d.PagoConceptoID = 2
	) y on x.nCodCred = d.nCodCred and y.nNroCalendario = d.nNroCalendario and y.nNroCuota = d.nNroCuota
	inner join (
		select distinct d.ncodcred, d.nNroCalendario, d.nNroCuota, d.Monto
		from pagodetalle d
		where pagoid = 4 and d.PagoConceptoID = 3
	) z on x.nCodCred = d.nCodCred and z.nNroCalendario = d.nNroCalendario and z.nNroCuota = d.nNroCuota
	inner join credcronograma cro on cro.ncodcred = d.ncodcred 
		and cro.nnrocalendario = d.nNroCalendario 
		and cro.nnrocuota = d.nnrocuota		
	inner join creditos c on c.ncodcred = cro.ncodcred
	inner join credpersonas cp on c.ncodcred = cp.ncodcred
	inner join persona p on cp.ncodpers = p.ncodpers
	left join personajur j on j.ncodpers = p.ncodpers
	left join personanat n on n.ncodpers = p.ncodpers
where 
	pagos.pagoid = @pagoID
GO


/*
* RECOMPRAS
*
*
**/
if object_id('CerrarRecompra') is not null  drop procedure dbo.CerrarRecompra;
go
CREATE procedure [dbo].[CerrarRecompra]
	@RecompraID int,
	@FechaCierre DateTime
as
	update recompras set FechaCierre = @FechaCierre where RecompraID = @RecompraID;
	/*
	actualizar estado de las cuotas en el cronograma del fondeador
	*/
GO
if object_id('CrearRecompra') is not null  drop procedure dbo.CrearRecompra;
go
CREATE procedure [dbo].[CrearRecompra] --'emartinez', 1,  '991,992,993'
	@CreadoPor varchar(200),
	@FondeadorID int,
	@ProductoID int,
	@creditos nvarchar(max)
as
	declare @recompra int;
	--select * from recompras
	insert into Recompras values(
		@FondeadorID,
		@ProductoID,
		getdate(),
		getdate(),
		null,
		@CreadoPor
	)

	set @recompra = @@identity;

	insert into CreditoRecompra 
	select @recompra, * 
	from split_string(@creditos, ',');
GO
if object_id('EditarRecompra') is not null  drop procedure dbo.EditarRecompra;
go
CREATE procedure [dbo].[EditarRecompra]
	@RecompraID int,
	@FondeadorID int,
	@ProductoID int,
	@creditos nvarchar(max)
as
	update Recompras set 
		FondeadorID = @FondeadorID, 
		ProductoID = @ProductoID, 
		Modificado = getdate() 
	where RecompraID = @RecompraID;

	delete from CreditoRecompra where RecompraID = @RecompraID;

	insert into CreditoRecompra 
	select @RecompraID, * 
	from split_string(@creditos, ',')
GO
if object_id('EliminarRecompra') is not null  drop procedure dbo.EliminarRecompra;
go
create procedure  [dbo].[EliminarRecompra]
	@RecompraID int
as
	delete from CreditoRecompra where RecompraID = @RecompraID;

	delete from recompras where RecompraID = @RecompraID
GO
if object_id('FindDeuda') is not null  drop procedure dbo.FindDeuda;
go
CREATE procedure [dbo].[FindDeuda] --123550
(
	@nCodCred int
)
as
begin
	select
	 p.*,
	 c.*,
	 f.*,
	 pro.*
	from dbo.pagos p
	inner join dbo.PagoDetalle c on c.PagoID = p.PagoID
	inner join Fondeadores f on p.FondeadorID = f.FondeadorID
	inner join CatalogoCodigos pro on p.ProductoID = pro.nValor and pro.ncodigo = 4029
	where c.nCodCred = @nCodCred
	and c.EsDeuda = 1
end
GO
if object_id('GetCronogramas') is not null  drop procedure dbo.GetCronogramas;
go
CREATE procedure [dbo].[GetCronogramas] --[dbo].[GetCronogramas] 1, '115273',1
	@tipo int,
	@codigo nvarchar(100), 
	@ultimo bit = 0
as
if @tipo = 1
begin
--COF
	select 
		c.ccodcta CodCta,
		cro.nNroCalendario,
		cro.nCodCred CodigoCredito,
		cro.nnrocuota NumeroCuota,
		format(cro.dFecVcto, 'dd-MM-yyyy') FechaPago,
		cro.nCapital Amortizacion,
		cro.nInteres Interes,
		cro.nPerGracia PeriodoGracia,
		0.00 Encaje,
		cro.nCapital + cro.nInteres + cro.nPerGracia TotalCuota,
		cro.nEstado,
		cro.nEstadoCuota
	from 
		credcronograma cro
		inner join creditos c on c.ncodcred = cro.ncodcred
	where 
		( ( isnumeric(@codigo) = 1 and c.ncodcred = try_convert(int,@codigo) ) or c.ccodcta like '%'+@codigo+'%' )
		and (( cro.nnrocalendario = (select top 1 nnrocalendario from credcronograma where ncodcred = c.ncodcred order by nnrocalendario desc) and @ultimo = 1) or @ultimo = 0)
end
else
begin
--FONDEADOR
	select 
		c.ccodcta CodCta,
		cro.nNroCalendario,
		cro.nCodCred CodigoCredito,
		cro.nnrocuota NumeroCuota,
		format(cro.dFecVcto, 'dd-MM-yyyy') FechaPago,
		cro.nCapital Amortizacion,
		cro.nInteres Interes,
		cro.nPerGracia PeriodoGracia,
		0.00 Encaje,
		cro.nCapital + cro.nInteres + cro.nPerGracia TotalCuota,
		cro.nEstado,
		cro.nEstadoCuota
	from 
		credcronograma cro
		inner join creditos c on c.ncodcred = cro.ncodcred
	where 
		( ( isnumeric(@codigo) = 1 and c.ncodcred = try_convert(int,@codigo) ) or c.ccodcta like '%'+@codigo+'%' )
		and (( cro.nnrocalendario = (select top 1 nnrocalendario from credcronograma where ncodcred = c.ncodcred order by nnrocalendario desc) and @ultimo = 1) or @ultimo = 0)
end
GO


/*
* AMORTIZACIONES
*
*
**/
if object_id('GetAmortizacion') is not null  drop procedure dbo.GetAmortizacion;
go
CREATE procedure [dbo].[GetAmortizacion]
	@ReprogramacionID int = 0
as
begin
	if(@ReprogramacionID = 0)
		select * from dbo.Reprogramaciones
	else
		select * from dbo.Reprogramaciones where ReprogramacionID = @ReprogramacionID
end
GO
if object_id('GuardarAmortizacion') is not null  drop procedure dbo.GuardarAmortizacion;
go
CREATE procedure [dbo].[GuardarAmortizacion]
    @ReprogramacionID int,
    @Tasa decimal(10,2),
    @SaldoCapital decimal(10,2),
    @NuevoCapital decimal(10,2),
    @UltimoVencimiento datetime,
    @Hoy datetime,
    @DiasTranscurridos int,
    @Factor decimal(10,2),
    @InteresesTranscurridos decimal(10,2),
    @KI decimal(10,2),
    @Amortizacion decimal(10,2),
    @Capital decimal(10,2),
    @nCodCred int,
    @Total decimal(10,2),
	@NroCalendarioCOF int,
	@Confirmacion datetime
as
begin
	if(@ReprogramacionID = 0)
		insert into dbo.Reprogramaciones(Tasa, SaldoCapital, NuevoCapital, UltimoVencimiento, Hoy, DiasTranscurridos, Factor, InteresesTranscurridos, KI, Amortizacion, Capital, nCodCred, Total, NroCalendarioCOF)
		values(@Tasa, @SaldoCapital, @NuevoCapital, @UltimoVencimiento, @Hoy, @DiasTranscurridos, @Factor, @InteresesTranscurridos, @KI, @Amortizacion, @Capital, @nCodCred, @Total, @NroCalendarioCOF);
	else
		update dbo.Reprogramaciones 
		set 
			Tasa=@Tasa, 
			SaldoCapital=@SaldoCapital, 
			NuevoCapital=@NuevoCapital, 
			UltimoVencimiento=@UltimoVencimiento, 
			Hoy=@Hoy, 
			DiasTranscurridos=@DiasTranscurridos, 
			Factor=@Factor, 
			InteresesTranscurridos=@InteresesTranscurridos, 
			KI=@KI, 
			Amortizacion=@Amortizacion, 
			Capital=@Capital, 
			Total=@Total, 
			nCodCred = @nCodCred,
			NroCalendarioCOF=@NroCalendarioCOF, 
			Confirmacion=@Confirmacion	
		where 
			ReprogramacionID = @ReprogramacionID
end
GO


/*
* ANALISIS
*
*
**/
if object_id('GetCreditosPorEstado') is not null  drop procedure dbo.GetCreditosPorEstado;
go
CREATE procedure [dbo].[GetCreditosPorEstado] --'6,16'
	@estados nvarchar(100)
as
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
			and ((year(cro.dFecVcto) = year(getdate()) and month(cro.dFecVcto) > month(getdate())) or year(cro.dFecVcto) > year(getdate()))
		group by 
			cro.nCodCred
	) beta on beta.nCodCred = c.nCodCred
where
	cod.ncodigo = 4029
	and c.nCodCred not in(select nCodCred from CreditosBloqueados)
	and cod.nvalor not in(3,4,5)
	and c.nEstado in(select * from dbo.split_string(@estados,','))
GO


/*
* AUTENTICACIÓN
*
*
**/
if object_id('LoginSP') is not null  drop procedure dbo.LoginSP;
go
CREATE procedure [dbo].[LoginSP] --'asalazar', '12345'
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
if object_id('CerrarSesionSP') is not null  drop procedure dbo.CerrarSesionSP;
go
create procedure [dbo].[CerrarSesionSP]
	@token varchar(100)
as
	update Usuarios set SesionToken = '' where SesionToken = @token;
GO

