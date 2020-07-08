/*

insert into Fondeadores values('Coopac', '', 'dbo.Coopac')
insert into Fondeadores values('Capital', '', 'dbo.Capital') 

insert into usuarios values('Kelly Robles', 0,'krobles', '12345', null, getdate())
insert into roles values('Administrador', getdate())
insert into RolUsuario values(1, 1)

--crear una cartera normal
exec dbo.CrearCartera 1, 2, 'krobles', '123529', '20200501';

--crear cartera de reprogramados
DECLARE @Result VARCHAR(MAX);

SELECT
    @Result = CASE
        WHEN @Result IS NULL
        THEN convert(nvarchar(20),c.nCodCred)
        ELSE @Result + ',' + convert(nvarchar(20),c.nCodCred) 
    END
FROM
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
					max(cro.nNroCalendario) topCalendario
				from 
					credcronograma cro
				where 
					((year(dFecVcto) = year(getdate()) and month(dFecVcto) > month(getdate())) or year(dFecVcto) > year(getdate()))
					--and ncodcred = 119180
				group by 
					cro.ncodcred
			) neo on neo.nCodCred = cro.ncodcred
		where 
			cro.nnroCalendario = neo.topCalendario
		group by 
			cro.nCodCred
	) beta on beta.nCodCred = c.nCodCred
where
	cod.ncodigo = 4029
	and c.nCodCred not in(select nCodCred from CreditosBloqueados)
	and cod.nvalor not in(3,4,5)
	and n.cDNI in(
		'46405593',
		'07723058',
		'10364047',
		'19081916',
		'41369819',
		'42843877',
		'32902214',
		'32990712',
		'40195955',
		'18034424',
		'76410784',
		'25440821',
		'40038853',
		'06073837',
		'09987308',
		'29659395',
		'26721171',
		'41993290',
		'33729901',
		'07610620',
		'25556898',
		'40487403',
		'45036408',
		'07031644',
		'72814501',
		'10178982',
		'44154802',
		'25825551',
		'44958620',
		'06042026',
		'42580388',
		'09749058',
		'06021269',
		'06229329',
		'42115391',
		'08848562',
		'70209068',
		'03898324',
		'41497635',
		'29563770',
		'22306500',
		'42523580',
		'70601957',
		'17901138',
		'25647149',
		'47124383',
		'32979505',
		'43033336',
		'10721071',
		'07525610',
		'41488075',
		'43134810',
		'44281283',
		'44567613',
		'00247589',
		'46473992',
		'09401524',
		'43239978',
		'43487933'
	)

	select @Result res

exec CrearCartera 1, 2, 'krobles', @Result, '20200501';

update carteracredito set repro = 1 where carteraid = 364

*/