if object_id('CreditoDias') is not null 
	drop function dbo.CreditoDias;
go
create function dbo.CreditoDias(@nCodCred int)
returns int as
begin
	declare @n int;
	select
		@n = datediff(day, min(cro.dfecvcto), max(cro.dfecvcto))
	from 
		creditos c
		inner join credcronograma cro on cro.nCodCred = c.ncodcred
	where 
		c.ncodcred = @nCodCred
	return @n;
end
go

if object_id('ObtenerPrecio') is not null 
	drop function dbo.ObtenerPrecio;
go
create function dbo.ObtenerPrecio(@nCodCred int)
returns float
as
begin
	declare @n float;
	select 
		@n = sum(cro.ncapital)
	from  
		CredCronograma cro
	where 
		cro.ncodcred = @nCodCred
		and cro.nNroCalendario = dbo.UltimoCalendario(cro.nCodCred)
		and cro.dfecvcto > getdate()
	return @n;
end
go

if object_id('UltimoCalendario') is not null 
	drop function dbo.UltimoCalendario;
go
create function dbo.UltimoCalendario(@nCodCred int)
returns int
as
begin
	declare @res int;
	select 
		@res = max(nnrocalendario)
	from  
		CredCronograma cro
	where 
		cro.ncodcred = @nCodCred
	return @res;
end
go

if object_id('Capital') is not null 
	drop function dbo.Capital;
go
create FUNCTION dbo.[Capital] (@CreditoID int)
RETURNS bit
AS BEGIN
	if(dbo.CreditoDias(@CreditoID) < 1825)
		return 1;
	return 0;
END
GO

if object_id('EsRepro') is not null 
	drop function dbo.EsRepro;
go
create FUNCTION dbo.[EsRepro] (@CreditoID int)
RETURNS bit
AS BEGIN
	declare @nTipoCrono int;
		
	select 
		@nTipoCrono = cro.nTipoCrono
	from 
		creditos c
		inner join CredCronograma cro on cro.nCodCred = c.nCodCred
	where
		cro.ntipocrono = 2
		and c.nCodCred = @CreditoID
	group by 
		c.nCodCred, cro.nTipoCrono

	if(@nTipoCrono = 2)
		return 1;
	
	return 0;
END
GO

if object_id('CuantasRepro') is not null 
	drop function dbo.CuantasRepro;
go
create FUNCTION dbo.CuantasRepro (@CreditoID int)
RETURNS bit
AS BEGIN
	declare @n int;

	select 
		@n = count(*)
	from 
		creditos c
		inner join CredCronograma cro on cro.nCodCred = c.nCodCred
	where
		cro.ntipocrono = 2
		and c.nCodCred = @CreditoID
	group by 
		c.nCodCred, cro.nTipoCrono

	return @n;
END
GO

if object_id('Coopac') is not null 
	drop function dbo.Coopac;
go
CREATE FUNCTION dbo.[Coopac] (@CreditoID int)
RETURNS bit
AS 
BEGIN
if(exists(
		select 
			convert(decimal(10,2),vi.nMonto * 100 / vi.nPrecioSinDscto) porcIncial,
			sentinel.cNomCod cal
		from
			creditos c
			inner join credvehicular v on v.nCodCred = c.nCodCred
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
		where 
			(
				c.ncodcred = @CreditoID
				and convert(decimal(10,2),vi.nMonto * 100 / vi.nPrecioSinDscto) >= 10
				and sentinel.nValor in(-1, 0, 1)
			) 
			or 
			(
				c.ncodcred = @CreditoID
				and vi.nmonto is null
				and vi.nPrecioSinDscto is null
				and sentinel.nValor is null
			)
	))
	return 1;
return 0;
END
GO

if object_id('Verificador') is not null 
	drop function dbo.Verificador;
go
create function dbo.[Verificador] (@CreditoID int, @Funcion nvarchar(100))
RETURNS bit
AS BEGIN
	declare @sql nvarchar(100) = N'select @res = ' + @Funcion + '(' + cast(@CreditoID as nvarchar(100)) + ')';
	declare @params nvarchar(150) = N'@res int out';

	declare @res bit;
	exec sp_executesql @sql, @params, @res out;
	return @res;
end
GO

if object_id('split_string') is not null 
	drop function dbo.split_string;
go
create function dbo.split_string( @in_string VARCHAR(MAX), @delimiter VARCHAR(1) )
RETURNS @list TABLE(tuple VARCHAR(100))
AS
BEGIN
        WHILE LEN(@in_string) > 0
        BEGIN
            INSERT INTO @list(tuple)
            SELECT left(@in_string, charindex(@delimiter, @in_string+',') -1) as tuple
    
            SET @in_string = stuff(@in_string, 1, charindex(@delimiter, @in_string + @delimiter), '')
        end
    RETURN 
END
go
