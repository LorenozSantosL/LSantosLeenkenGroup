------ Leenken group

--Base 
CREATE DATABASE LSantosLeenkenGroup;
-----------------------------Tablas
-----------Tabla Estado
CREATE TABLE EntidadFederativa
(
IdEstado INT IDENTITY(1,1) PRIMARY KEY,
Estado VARCHAR(100) NOT NULL
)

--------Tabla Empleado
CREATE TABLE Empleado
(
IdEmpleado INT IDENTITY(1,1) PRIMARY KEY,
NumeroNomina VARCHAR(10),
Nombre VARCHAR(100),
ApellidoPaterno VARCHAR(100),
ApellidoMaterno VARCHAR(100),
IdEstado INT FOREIGN KEY REFERENCES EntidadFederativa(IdEstado)
)

---------
INSERT INTO EntidadFederativa (Estado) Values('Yucatán')
SELECT * FROM EntidadFederativa
go

INSERT INTO Empleado (Nombre, ApellidoPaterno, ApellidoMaterno, IdEstado)  VALUES ('Daniela', 'Juarez', 'Lopez', 5)
SELECT * FROM Empleado
----------------------
CREATE TRIGGER EmpleadoTigger
	ON Empleado
		AFTER INSERT 
		AS
			BEGIN 
				DECLARE @NumeroNomina VARCHAR(10)
				DECLARE @IdEmpleado INT

				SET @NumeroNomina =
				(
				SELECT CONVERT(VARCHAR, IdEmpleado) +'-'+ CONVERT(VARCHAR, IdEstado) FROM inserted
				)

				SET @IdEmpleado = 
				(
					SELECT IdEmpleado FROM inserted
				)

				UPDATE Empleado 
					SET NumeroNomina = @NumeroNomina
					WHERE IdEmpleado = @IdEmpleado
			END
go
-------------------------- procedures

CREATE PROCEDURE EmpleadoAdd
@Nombre VARCHAR(100),
@ApellidoPaterno VARCHAR(100),
@ApellidoMaterno VARCHAR(100),
@IdEstado INT
AS 
	INSERT INTO Empleado (Nombre, ApellidoPaterno, ApellidoMaterno, IdEstado)  VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @IdEstado)
go

ALTER PROCEDURE EmpleadoUpdate 5, 'Santiago', 'Lopez', 'Rojas', 4
@IdEmpleado INT,
@Nombre VARCHAR(100),
@ApellidoPaterno VARCHAR(100),
@ApellidoMaterno VARCHAR(100),
@IdEstado INT

AS 
	UPDATE Empleado SET 
	Nombre = @Nombre,
	ApellidoPaterno = @ApellidoPaterno,
	ApellidoMaterno = @ApellidoMaterno,
	IdEstado = @IdEstado
	WHERE IdEmpleado = @IdEmpleado

GO


CREATE PROCEDURE EmpleadoGetAll
AS 
	SELECT Empleado.IdEmpleado, 
			Empleado.NumeroNomina, 
			Empleado.Nombre, 
			Empleado.ApellidoPaterno, 
			Empleado.ApellidoMaterno, 
			Empleado.IdEstado, 
			EntidadFederativa.Estado AS NombreEstado
			FROM Empleado

			INNER JOIN EntidadFederativa ON Empleado.IdEstado = EntidadFederativa.IdEstado
go


Alter PROCEDURE EmpleadoGetById 5
@IdEmpleado INT

AS 
	SELECT Empleado.IdEmpleado, 
			Empleado.NumeroNomina, 
			Empleado.Nombre, 
			Empleado.ApellidoPaterno, 
			Empleado.ApellidoMaterno, 
			Empleado.IdEstado, 
			EntidadFederativa.Estado AS NombreEstado
			FROM Empleado

			INNER JOIN EntidadFederativa ON Empleado.IdEstado = EntidadFederativa.IdEstado
	WHERE IdEmpleado = @IdEmpleado
go

CREATE PROCEDURE EmpleadoDelete 3
@IdEmpleado INT
AS 
	DELETE FROM Empleado WHERE IdEmpleado = @IdEmpleado

go


CREATE PROCEDURE EntidadFederativaGetAll
AS
	SELECT IdEstado, Estado FROM EntidadFederativa

GO