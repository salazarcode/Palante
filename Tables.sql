IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'cliente'))
BEGIN
	CREATE TABLE [dbo].[Cliente](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[Nombres] [varchar](max) NULL
	)
END

IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'prestamo'))
BEGIN
	create table prestamo(
		ID int identity(1,1) primary key,
		Capital float,
		ClienteID int FOREIGN KEY REFERENCES cliente(ID)
	)
END