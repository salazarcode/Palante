insert into Fondeadores values('Coopac', '', 'dbo.Coopac')
insert into Fondeadores values('Capital', '', 'dbo.Capital') 

insert into usuarios values('Kelly Robles', 0,'krobles', '12345', null, getdate())
insert into roles values('Administrador', getdate())
insert into RolUsuario values(1, 1)

