EXEC sp_dropserver 'cof', 'droplogins';		

EXEC master.sp_addlinkedserver   
 @server = 'cof',
 @srvproduct=N'sql_server',
 @provider=N'SQLOLEDB',
 @datasrc=N'192.168.200.248\srvym'
go

------------------------------------------------------------------------

EXEC master.sp_addlinkedsrvlogin 
	@rmtsrvname='cof', 
	@useself=N'False', 
	@locallogin=NULL, 
	@rmtuser=N'Uexterno2', 
	@rmtpassword=N'Ext3rn0201'
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

create synonym dbo.CatalogoCodigos						for cof.[DBCore_VtaCartera].[dbo].CatalogoCodigos;						
create synonym dbo.personatelefono						for cof.[DBCore_VtaCartera].[dbo].personatelefono;					
create synonym dbo.PersonaNat							for cof.[DBCore_VtaCartera].[dbo].PersonaNat;								
create synonym dbo.PersonaJur							for cof.[DBCore_VtaCartera].[dbo].PersonaJur;								
create synonym dbo.persona								for cof.[DBCore_VtaCartera].[dbo].persona;									
create synonym dbo.CredPersonas							for cof.[DBCore_VtaCartera].[dbo].CredPersonas;								
create synonym dbo.credvehicular						for cof.[DBCore_VtaCartera].[dbo].credvehicular;							
create synonym dbo.creditos								for cof.[DBCore_VtaCartera].[dbo].creditos;									
create synonym dbo.CredCronograma						for cof.[DBCore_VtaCartera].[dbo].CredCronograma;							
create synonym dbo.Vehicularsolicitud					for cof.[DBCore_VtaCartera].[dbo].Vehicularsolicitud;						
create synonym dbo.VehicularSolicitudInformacionAuto	for cof.[DBCore_VtaCartera].[dbo].VehicularSolicitudInformacionAuto;		
create synonym dbo.VehicularsolicitudProspectos			for cof.[DBCore_VtaCartera].[dbo].VehicularsolicitudProspectos;				
create synonym dbo.VehicularObjetivosCred				for cof.[DBCore_VtaCartera].[dbo].VehicularObjetivosCred;					
create synonym dbo.VehicularSolicitudPrecioSoat			for cof.[DBCore_VtaCartera].[dbo].VehicularSolicitudPrecioSoat;				
create synonym dbo.InmobiliarioPersona					for cof.[DBCore_VtaCartera].[dbo].InmobiliarioPersona;		