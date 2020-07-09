EXEC sp_dropserver 'cof', 'droplogins';		

EXEC sp_addlinkedserver   
 @server = 'cof',
 @srvproduct=N'sql_server',
 @provider=N'SQLOLEDB',
 @datasrc=N'192.168.200.248\srvym'
go

------------------------------------------------------------------------

EXEC sp_addlinkedsrvlogin 
	@rmtsrvname='cof', 
	@useself=N'False', 
	@locallogin=NULL, 
	@rmtuser=N'Uexterno2', 
	@rmtpassword=N'Java***174'
go

if exists(select * from sys.synonyms where name like '%CatalogoCodigos%') drop synonym CatalogoCodigos;
if exists(select * from sys.synonyms where name like '%personatelefono%') drop synonym personatelefono;
if exists(select * from sys.synonyms where name like '%PersonaNat%') drop synonym PersonaNat;
if exists(select * from sys.synonyms where name like '%PersonaJur%') drop synonym PersonaJur;
if exists(select * from sys.synonyms where name like '%persona%') drop synonym persona;
if exists(select * from sys.synonyms where name like '%CredPersonas%') drop synonym CredPersonas;
if exists(select * from sys.synonyms where name like '%credvehicular%') drop synonym credvehicular;
if exists(select * from sys.synonyms where name like '%creditos%') drop synonym creditos;
if exists(select * from sys.synonyms where name like '%CredCronograma%') drop synonym CredCronograma;
if exists(select * from sys.synonyms where name like '%Vehicularsolicitud%') drop synonym Vehicularsolicitud;
if exists(select * from sys.synonyms where name like '%VehicularSolicitudInformacionAuto%') drop synonym VehicularSolicitudInformacionAuto;
if exists(select * from sys.synonyms where name like '%VehicularsolicitudProspectos%') drop synonym VehicularsolicitudProspectos;
if exists(select * from sys.synonyms where name like '%VehicularObjetivosCred%') drop synonym VehicularObjetivosCred;
if exists(select * from sys.synonyms where name like '%VehicularSolicitudPrecioSoat%') drop synonym VehicularSolicitudPrecioSoat;
if exists(select * from sys.synonyms where name like '%InmobiliarioPersona%') drop synonym InmobiliarioPersona;

create synonym dbo.CatalogoCodigos						for cof.[DBCoreMigracion].[dbo].CatalogoCodigos;						
create synonym dbo.personatelefono						for cof.[DBCoreMigracion].[dbo].personatelefono;					
create synonym dbo.PersonaNat							for cof.[DBCoreMigracion].[dbo].PersonaNat;								
create synonym dbo.PersonaJur							for cof.[DBCoreMigracion].[dbo].PersonaJur;								
create synonym dbo.persona								for cof.[DBCoreMigracion].[dbo].persona;									
create synonym dbo.CredPersonas							for cof.[DBCoreMigracion].[dbo].CredPersonas;								
create synonym dbo.credvehicular						for cof.[DBCoreMigracion].[dbo].credvehicular;							
create synonym dbo.creditos								for cof.[DBCoreMigracion].[dbo].creditos;									
create synonym dbo.CredCronograma						for cof.[DBCoreMigracion].[dbo].CredCronograma;							
create synonym dbo.Vehicularsolicitud					for cof.[DBCoreMigracion].[dbo].Vehicularsolicitud;						
create synonym dbo.VehicularSolicitudInformacionAuto	for cof.[DBCoreMigracion].[dbo].VehicularSolicitudInformacionAuto;		
create synonym dbo.VehicularsolicitudProspectos			for cof.[DBCoreMigracion].[dbo].VehicularsolicitudProspectos;				
create synonym dbo.VehicularObjetivosCred				for cof.[DBCoreMigracion].[dbo].VehicularObjetivosCred;					
create synonym dbo.VehicularSolicitudPrecioSoat			for cof.[DBCoreMigracion].[dbo].VehicularSolicitudPrecioSoat;				
create synonym dbo.InmobiliarioPersona					for cof.[DBCoreMigracion].[dbo].InmobiliarioPersona;		