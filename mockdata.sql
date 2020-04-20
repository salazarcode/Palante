﻿IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'cliente'))
BEGIN
	insert into Cliente values('Andrés Eduardo Salazar Martínez')
	insert into Cliente values('Sergio Samuel Salazar Martínez')
	insert into Cliente values('Santiago Isaac Salazar Martínez')
END

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Credito'))
BEGIN
	insert into Credito values(4515.22, 1)
	insert into Credito values(11512.22, 1)
	insert into Credito values(965.22, 2)
	insert into Credito values(54.22, 2)
	insert into Credito values(98651.22, 3)
	insert into Credito values(1235.22, 3)
END

