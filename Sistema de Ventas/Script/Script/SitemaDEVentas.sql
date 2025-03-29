USE master 

go 


if exists(SELECT * FROM sys.databases WHERE name = 'SistemaVentas')
BEGIN
	DROP DATABASE SistemaVentas
END
go
 


CREATE DATABASE SistemaVentas
go



USE SistemaVentas
go



-----ISS
USE master
GO

CREATE LOGIN [IIS APPPOOL\DefaultAppPool] FROM WINDOWS 
GO

USE SistemaVentas
GO

CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [IIS APPPOOL\DefaultAppPool]
GO

exec sys.sp_addrolemember 'db_owner', [IIS APPPOOL\DefaultAppPool]
go



--------------------Tablas-----------------------------------------


CREATE TABLE Empleado(
    UsuLog VARCHAR(20) PRIMARY KEY,
    Nombre VARCHAR(20) NOT NULL,
    PassUsu VARCHAR(20) NOT NULL 
);
GO

CREATE TABLE Categoria (
    Codigo_Cate Varchar(6) PRIMARY KEY CHECK (Codigo_Cate LIKE '[A-Za-z0-9][A-Za-z0-9][A-Za-z0-9][A-Za-z0-9][A-Za-z0-9][A-Za-z0-9]'),
    Nombre VARCHAR(50) NOT NULL,
    Activo BIT DEfAULT (1) NOT  Null
	
);
GO

CREATE TABLE Articulo (
    Codigo Varchar(10) PRIMARY KEY  CHECK (Codigo LIKE '[A-Za-z0-9][A-Za-z0-9][A-Za-z0-9][A-Za-z0-9][A-Za-z0-9][A-Za-z0-9][A-Za-z0-9][A-Za-z0-9][A-Za-z0-9][A-Za-z0-9]'),
    TipoPresentacion VARCHAR(15) NOT NULL CHECK (TipoPresentacion IN ('Unidad', 'Blister', 'Sobre', 'Frasco')),
    Nombre VARCHAR(50) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL CHECK (Precio > 0),
    Tamaño INT NOT NULL  CHECK (Tamaño > 0),
    FechaVenc DATE NOT NULL check (FechaVenc > GetDate()),
    Codigo_Cate Varchar(6) NOT NULL,
    Activo BIT DEfAULT (1) NOT  Null,
    FOREIGN KEY (Codigo_Cate) REFERENCES Categoria(Codigo_Cate)
);
GO

CREATE TABLE Clientes (
    CiCli CHAR(8) PRIMARY KEY CHECK (CiCli LIKE '[1-6][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
    Nombre VARCHAR(100) NOT NULL,
    NumTarj CHAR(16) NOT NULL CHECK (NumTarj NOT LIKE '%[^0-9]%'),
    Telefono CHAR(9) NOT NULL CHECK (Telefono NOT LIKE '%[^0-9]%')
);
GO

CREATE TABLE EstadoVenta (
    IdEstado INT PRIMARY KEY Not Null,
    NombreEstado VARCHAR(50) NOT NULL
);
GO

CREATE TABLE Venta (
    NumVenta INT PRIMARY KEY IDENTITY(1,1),
    MontoTotal DECIMAL(10, 2) NOT NULL CHECK (MontoTotal > 0),
    FechaVenta DATETIME DEFAULT GETDATE() NOT NULL,
    DirEnvio VARCHAR(50) NOT NULL,
    CiCli CHAR(8) NOT NULL,
    UsuLog VARCHAR(20) Not null,
    FOREIGN KEY (CiCli) REFERENCES Clientes(CiCli),
    FOREIGN KEY (UsuLog) REFERENCES Empleado(UsuLog)
);
GO

CREATE TABLE VentaArticulo (
    NumVenta INT NOT NULL,
    CodArt Varchar(10) NOT NULL,
    CantArticulos INT NOT NULL CHECK (CantArticulos > 0),
    FOREIGN KEY (NumVenta) REFERENCES Venta(NumVenta),
    FOREIGN KEY (CodArt) REFERENCES Articulo(Codigo),
    PRIMARY KEY (NumVenta, CodArt)
);
GO

CREATE TABLE EstadoGenerado
(
T_Estado INT Not Null,
NVenta int Not Null,
Fecha DATETIME NOT NULL DEFAULT GETDATE(),
Primary Key (T_Estado, NVenta),
FOREIGN KEY (T_Estado) References EstadoVenta(IdEstado),
FOREIGN KEY (NVenta) References Venta(NumVenta)

)
GO




-------------Datos de Pruebas-------------------

--12 Empleados
INSERT INTO Empleado (UsuLog, Nombre, PassUsu) VALUES
('Alberto01', 'Alberto', 'ABC123'),
('Abigail01', 'Abigail', 'DEF456'),
('Benjamin01', 'Benjamin', 'GHI789'),
('Danielle01', 'Danielle','JKL012'),
('Eleonora01','Eleonora', 'MNO345'),
('Ferdinan01', 'Ferdinan', 'PQR678'),
('Gabriela01','Gabriela', 'STU901'),
('Isabella01','Isabella', 'VWX234'),
('Jonathan01','Jonathan', 'YZA567'),
('Kimberly01','Kimberly', 'BCD890'),
('Leonardo01','Leonardo', 'EFG123'),
('Margarit01','Margarit', 'HIJ456');
GO
---Acceso a los Empleado
-----------------------------------------
CREATE LOGIN [Alberto01] WITH PASSWORD = 'ABC123';
GO
USE SistemaVentas;
GO
CREATE USER [Alberto01] FOR LOGIN [Alberto01];
GRANT EXECUTE TO [Alberto01];
GO
--------------------------------------
CREATE LOGIN [Abigail01] WITH PASSWORD = 'DEF456';
GO
USE SistemaVentas;
GO
CREATE USER [Abigail01] FOR LOGIN [Abigail01];
GRANT EXECUTE TO [Abigail01];
GO
--------------------------------------------
CREATE LOGIN [Benjamin01] WITH PASSWORD = 'GHI789';
GO
USE SistemaVentas;
GO
CREATE USER [Benjamin01] FOR LOGIN [Benjamin01];
GRANT EXECUTE TO [Benjamin01];
GO
--------------------------------------------
CREATE LOGIN [Danielle01] WITH PASSWORD = 'JKL012';
GO
USE SistemaVentas;
GO
CREATE USER [Danielle01] FOR LOGIN [Danielle01];
GRANT EXECUTE TO [Danielle01];
GO
--------------------------------------------
CREATE LOGIN [Eleonora01] WITH PASSWORD = 'MNO345';
GO
USE SistemaVentas;
GO
CREATE USER [Eleonora01] FOR LOGIN [Eleonora01];
GRANT EXECUTE TO [Eleonora01];
GO
----------------------------------------------
CREATE LOGIN [Ferdinan01] WITH PASSWORD = 'PQR678';
GO
USE SistemaVentas;
GO
CREATE USER [Ferdinan01] FOR LOGIN [Ferdinan01];
GRANT EXECUTE TO [Ferdinan01];
GO
------------------------------------------------
CREATE LOGIN [Gabriela01] WITH PASSWORD = 'STU901';
GO
USE SistemaVentas;
GO
CREATE USER [Gabriela01] FOR LOGIN [Gabriela01];
GRANT EXECUTE TO [Gabriela01];
GO
----------------------------------------------
CREATE LOGIN [Isabella01] WITH PASSWORD = 'VWX234';
GO
USE SistemaVentas;
GO
CREATE USER [Isabella01] FOR LOGIN [Isabella01];
GRANT EXECUTE TO [Isabella01];
GO
------------------------------------------------
CREATE LOGIN [Jonathan01] WITH PASSWORD = 'YZA567';
GO
USE SistemaVentas;
GO
CREATE USER [Jonathan01] FOR LOGIN [Jonathan01];
GRANT EXECUTE TO [Jonathan01];
GO
-----------------------------------------------
CREATE LOGIN [Kimberly01] WITH PASSWORD = 'BCD890';
GO
USE SistemaVentas;
GO
CREATE USER [Kimberly01] FOR LOGIN [Kimberly01];
GRANT EXECUTE TO [Kimberly01];
GO
------------------------------------------------------------

CREATE LOGIN [Leonardo01] WITH PASSWORD = 'EFG123';
GO
USE SistemaVentas;
GO
CREATE USER [Leonardo01] FOR LOGIN [Leonardo01];
GRANT EXECUTE TO [Leonardo01];
GO

----------------------------------------------------------
CREATE LOGIN [Margarit01] WITH PASSWORD = 'HIJ456';
GO
USE SistemaVentas;
GO
CREATE USER [Margarit01] FOR LOGIN [Margarit01];
GRANT EXECUTE TO [Margarit01];
GO



--10 categorias
INSERT INTO Categoria (Codigo_Cate, Nombre) VALUES
('HIGB01', 'Higiene Bucal'),
('MEDI02', 'Medicamentos'),
('MATE03', 'Productos de Maternidad'),
('SUPL04', 'Suplementos Alimenticios'),
('DERM05', 'Dermatologia'),
('COSM06', 'Cosmeticos'),
('PERF07', 'Perfumeria'),
('BEBE08', 'Cuidado Infantil'),
('HOGA09', 'Cuidado del Hogar'),
('MASC10', 'Cuidado de Mascotas');
GO

--4 Estados de Venta
INSERT INTO EstadoVenta (IdEstado, NombreEstado) VALUES
('1', 'Armado'),
('2', 'Envio'),
('3', 'Entregado'),
('4', 'Cancelado');
GO


INSERT INTO Clientes (CiCli, Nombre, NumTarj, Telefono) VALUES
('12345678', 'Juan', '1234567890123456', '123456789'),
('23456789', 'Maria', '2345678901234567', '987654321'),
('34567891', 'Carlos', '3456789012345678', '986754321'),
('45678912', 'Ana', '4567890123456789', '987645321'),
('56789123', 'Luis', '5678901234567890', '399452732'),
('67891234', 'Sofia', '6789012345678901', '499452723'),
('12345670', 'Pedro', '7890123456789012', '199542732'),
('23456701', 'Laura', '8901234567890123', '699254732'),
('34567012', 'Diego', '9012345678901234', '794452732'),
('45670123', 'Carmen', '0123456789012345', '498542732'),
('56701234', 'Andrea', '1122334455667788', '297346512'),
('67012345', 'Daniel', '2233445566778899', '992311456'),
('12345671', 'Monica', '3344556677889900', '496762345'),
('23456712', 'Jose', '4455667788990011', '797654534'),
('34567123', 'Valeria', '5566778899001122', '899453212'),
('45671234', 'Raul', '6677889900112233', '999876758'), 
('56712345', 'Fernanda', '7788990011223344', '499992343'),
('67123456', 'Jorge', '8899001122334455', '693467654'),
('12345672', 'Isabel', '9900112233445566', '498754893'),
('23456723', 'Oscar', '0011223344556677', '992234236'),
('34567234', 'Natalia', '1122334455667789', '393256475'),
('45672345', 'Victor', '2233445566778890', '499161432'),
('56723456', 'Elena', '3344556677889901', '993256475'),
('67234567', 'Ricardo', '4455667788990012', '199856475'),
('12345673', 'Paula', '5566778899001122', '691346475'),
('23456734', 'Mario', '6677889900112233', '495423647'), 
('34567345', 'Luisa', '7788990011223344', '799765647'), 
('45673456', 'Emilio', '8899001122334455', '193246475'),
('56734567', 'Clara', '9900112233445566', '295426475'),
('67345678', 'Tomas', '0011223344556677', '698475325')
GO

--80 Articulos
INSERT INTO Articulo (Codigo, TipoPresentacion, Nombre, Precio, Tamaño, FechaVenc, Codigo_Cate) VALUES
('Cepillo001', 'Unidad', 'Cepillo Dental', 150, 1, '2026-12-31', 'HIGB01'),
('Pasta00002', 'Unidad', 'Pasta Dental', 120, 100, '2025-06-30', 'HIGB01'),
('HiloDen003', 'Frasco', 'Hilo Dental', 90, 10, '2026-01-15', 'HIGB01'),
('Enjague004', 'Frasco', 'Enjuague Bucal', 320, 500, '2025-09-20', 'HIGB01'),
('Parace0005', 'Unidad', 'Paracetamol', 450, 10, '2025-12-15', 'MEDI02'),
('Antihis006', 'Blister', 'Antihistaminico', 500, 20, '2026-03-01', 'MEDI02'),
('Jarabe0007', 'Frasco', 'Jarabe Tos', 700, 200, '2025-08-10', 'MEDI02'),
('Unguent008', 'Unidad', 'Ungüento Antibiotico', 290, 15, '2026-10-05', 'MEDI02'),
('SueroOr009', 'Sobre', 'Suero Oral', 60, 25, '2027-01-20', 'MEDI02'),
('AceiteB010', 'Frasco', 'Aceite Bebé', 650, 250, '2026-04-15', 'MATE03'),
('ToallaB011', 'Unidad', 'Toallas para Bebe', 300, 20, '2027-02-01', 'MATE03'),
('Pañales012', 'Unidad', 'Pañales', 1500, 50, '2025-12-31', 'MATE03'),
('CremaE0013', 'Unidad', 'Crema Estrías', 950, 200, '2026-08-10', 'MATE03'),
('Protect014', 'Unidad', 'Protector Solar', 780, 100, '2027-05-20', 'MATE03'),
('VitaD00015', 'Frasco', 'Vitamina D3', 1200, 100, '2026-06-30', 'SUPL04'),
('VitaC00016', 'Frasco', 'Vitamina C', 500, 30, '2025-11-15', 'SUPL04'),
('Omega00017', 'Frasco', 'Omega 3', 800, 90, '2026-09-01', 'SUPL04'),
('Calcio0018', 'Unidad', 'Calcio Vit. D', 600, 60, '2025-07-25', 'SUPL04'),
('Prote00019', 'Unidad', 'Proteína Polvo', 1800, 1000, '2026-03-15', 'SUPL04'),
('CremaH0020', 'Frasco', 'Crema Hidratante', 420, 200, '2026-11-30', 'DERM05'),
('AloeV00021', 'Frasco', 'Aloe Vera Gel', 380, 150, '2025-10-20', 'DERM05'),
('AntiA00022', 'Frasco', 'Crema Antiage', 2000, 100, '2027-03-10', 'DERM05'),
('Locio00023', 'Unidad', 'Loción Corporal', 650, 500, '2026-12-25', 'DERM05'),
('CremaQ0024', 'Frasco', 'Crema Quemaduras', 450, 50, '2025-06-01', 'DERM05'),
('Labial0025', 'Unidad', 'Labial', 290, 10, '2027-04-15', 'COSM06'),
('Esmal00026', 'Unidad', 'Esmalte Uñas', 190, 15, '2026-02-28', 'COSM06'),
('Base000027', 'Unidad', 'Base', 950, 30, '2025-07-10', 'COSM06'),
('Polvo00028', 'Unidad', 'Polvo Compacto', 550, 20, '2026-10-05', 'COSM06'),
('Deline0029', 'Unidad', 'Delineador Líquido', 400, 10, '2027-01-20', 'COSM06'),
('PerfFl0030', 'Frasco', 'Perfume Floral', 1600, 50, '2026-09-01', 'PERF07'),
('PerfCi0031', 'Frasco', 'Perfume Cítrico', 1700, 75, '2025-11-30', 'PERF07'),
('PerfAm0032', 'Frasco', 'Perfume Amaderado', 2000, 100, '2026-05-20', 'PERF07'),
('BodyS00033', 'Frasco', 'Body Splash', 700, 200, '2027-02-25', 'PERF07'),
('ColinaI034', 'Frasco', 'Colonia Infantil', 900, 150, '2026-07-15', 'PERF07'),
('CremaB0035', 'Frasco', 'Crema Bebé', 280, 100, '2025-08-30', 'BEBE08'),
('TalcoB0036', 'Frasco', 'Talco Bebé', 250, 200, '2026-03-05', 'BEBE08'),
('ShampB0037', 'Frasco', 'Shampoo Bebé', 420, 500, '2025-10-15', 'BEBE08'),
('JabonB0038', 'Frasco', 'Jabón Líquido Bebé', 350, 300, '2026-01-05', 'BEBE08'),
('Toalli0039', 'Unidad', 'Toallitas Bebé', 250, 80, '2027-06-15', 'BEBE08'),
('CremaA0040', 'Unidad', 'Crema Antipolvo', 180, 100, '2025-12-31', 'BEBE08'),
('AceitB0041', 'Frasco', 'Aceite Bebé', 320, 150, '2026-04-15', 'BEBE08'),
('PañalC0042', 'Unidad', 'Pañales Chico', 1800, 60, '2027-02-25', 'BEBE08'),
('PañalM0043', 'Unidad', 'Pañales Medio', 1500, 50, '2025-08-10', 'BEBE08'),
('PañalG0044', 'Unidad', 'Pañales Grande', 1600, 55, '2026-12-01', 'BEBE08'),
('CremaP0045', 'Frasco', 'Crema Postparto', 950, 200, '2025-07-15', 'MATE03'),
('AceitM0046', 'Frasco', 'Aceite Masajes', 380, 150, '2026-10-20', 'MATE03'),
('CintaL0047', 'Unidad', 'Cinta Lactancia', 210, 1, '2027-04-30', 'MATE03'),
('JabonN0048', 'Unidad', 'Jabón Natural', 300, 250, '2026-09-10', 'BEBE08'),
('Pomad00049', 'Frasco', 'Crema Rojeces', 400, 75, '2025-12-01', 'BEBE08'),
('CremaA0050', 'Unidad', 'Crema Antipicazón', 550, 100, '2025-11-20', 'BEBE08'),
('Shamp00051', 'Unidad', 'Shampoo', 450, 500, '2026-02-01', 'HOGA09'),
('Deterg0052', 'Frasco', 'Detergente Líquido', 350, 1000, '2026-05-10', 'HOGA09'),
('Lavand0053', 'Frasco', 'Lavandina', 180, 900, '2025-12-25', 'HOGA09'),
('LimpiP0054', 'Unidad', 'Limpiador de Piso', 290, 1000, '2026-01-30', 'HOGA09'),
('LimpiF0055', 'Unidad', 'Limpiador de Piso Flotante', 220, 500, '2025-08-15', 'HOGA09'),
('Broom00056', 'Unidad', 'Escoba', 350, 1, '2026-11-20', 'HOGA09'),
('Trapez0057', 'Unidad', 'Trapeador', 180, 1, '2027-03-10', 'HOGA09'),
('Papel00058', 'Unidad', 'Papel Higiénico', 150, 12, '2026-07-25', 'HOGA09'),
('Repue00059', 'Unidad', 'Respuesto Trapeador', 120, 1, '2025-06-05', 'HOGA09'),
('Trapo00060', 'Unidad', 'Trapo', 80, 1, '2025-10-10', 'HOGA09'),
('Guante0061', 'Unidad', 'Guantes de Plástico', 120, 2, '2026-04-01', 'HOGA09'),
('Escoba0062', 'Unidad', 'Escoba', 180, 1, '2027-07-10', 'HOGA09'),
('Basura0063', 'Unidad', 'Basura', 300, 20, '2025-09-20', 'HOGA09'),
('Balde00064', 'Unidad', 'Balde de Plástico', 200, 1, '2026-11-25', 'HOGA09'),
('Plumero065', 'Unidad', 'Plumero', 150, 1, '2025-07-30', 'HOGA09'),
('Rociador66', 'Unidad', 'Rociador de Agua', 80, 1, '2026-06-05', 'HOGA09'),
('Cepillo067', 'Unidad', 'Cepillo de Piso', 250, 1, '2027-04-20', 'HOGA09'),
('Mopa000068', 'Unidad', 'Mopa', 200, 1, '2025-11-10', 'HOGA09'),
('Cera000069', 'Unidad', 'Cera Líquida', 450, 500, '2026-03-25', 'HOGA09'),
('Lustra0070', 'Unidad', 'Lustramuebles', 100, 250, '2025-08-01', 'HOGA09'),
('Lavava0071', 'Unidad', 'Lavavajillas', 300, 1000, '2026-12-31', 'HOGA09'),
('Desen00072', 'Unidad', 'Desengrasante', 150, 750, '2025-05-30', 'HOGA09'),
('Horno00073', 'Unidad', 'Limpia Hornos', 180, 1000, '2026-07-15', 'HOGA09'),
('Estufa0074', 'Unidad', 'Limpia Estufas', 80, 300, '2027-05-01', 'HOGA09'),
('Cafete0075', 'Unidad', 'Limpiador de Cafetera', 80, 300, '2027-05-01', 'HOGA09'),
('AceroI0076', 'Unidad', 'Limpiadora de Acero Inoxidable', 80, 300, '2027-05-01', 'HOGA09'),
('Servi00077', 'Unidad', 'Servilleta', 80, 300, '2027-05-01', 'HOGA09'),
('Pañuelo078', 'Unidad', 'Pañuelo', 80, 300, '2027-05-01', 'HOGA09'),
('Insect0079', 'Unidad', 'Anti Pulgas', 80, 300, '2027-05-01', 'MASC10'),
('Esponja080', 'Unidad', 'Esponja', 80, 300, '2027-05-01', 'HOGA09');
GO


--100 Ventas
INSERT INTO Venta (MontoTotal, FechaVenta, DirEnvio, CiCli, UsuLog) VALUES
(2000.00, '2025-08-03T09:00:00', 'Calle Rios 145', '12345678', 'Alberto01'),
(350.00, '2025-01-02T09:15:00', 'Calle Rios 146', '23456789', 'Abigail01'),
(450.00, '2025-01-03T10:30:00', 'Calle Rios 147', '34567891', 'Benjamin01'),
(1800.00, '2025-01-04T11:45:00', 'Calle Rios 148', '45678912', 'Danielle01'),
(250.00, '2025-01-05T12:00:00', 'Calle Rios 149', '56789123', 'Eleonora01'),
(300.00, '2025-01-06T13:30:00', 'Calle Rios 150', '67891234', 'Ferdinan01'),
(1000.00, '2025-01-07T14:45:00', 'Calle Rios 151', '12345670', 'Gabriela01'),
(150.00, '2025-01-08T15:00:00', 'Calle Rios 152', '23456701', 'Isabella01'),
(1350.00, '2025-01-09T16:10:00', 'Calle Rios 153', '34567012', 'Jonathan01'),
(250.00, '2025-01-10T17:00:00', 'Calle Rios 154', '45670123', 'Kimberly01'),
(2100.00, '2025-01-11T18:20:00', 'Calle Rios 155', '56701234', 'Leonardo01'),
(1300.00, '2025-01-12T19:10:00', 'Calle Rios 156', '67012345', 'Margarit01'),
(500.00, '2025-01-13T20:00:00', 'Calle Rios 157', '12345671', 'Alberto01'),
(600.00, '2025-01-14T21:30:00', 'Calle Rios 158', '23456712', 'Abigail01'),
(350.00, '2025-01-15T22:00:00', 'Calle Rios 159', '34567123', 'Benjamin01'),
(2200.00, '2025-01-16T23:45:00', 'Calle Rios 160', '45671234', 'Danielle01'),
(950.00, '2025-01-17T08:05:00', 'Calle Rios 161', '56712345', 'Eleonora01'),
(1800.00, '2025-01-18T09:25:00', 'Calle Rios 162', '67123456', 'Ferdinan01'),
(2100.00, '2025-01-19T10:30:00', 'Calle Rios 163', '12345672', 'Gabriela01'),
(3000.00, '2025-01-20T11:00:00', 'Calle Rios 164', '23456723', 'Isabella01'),
(250.00, '2025-01-21T12:30:00', 'Calle Rios 165', '34567234', 'Jonathan01'),
(1200.00, '2025-01-22T13:00:00', 'Calle Rios 166', '45672345', 'Kimberly01'),
(700.00, '2025-01-23T14:20:00', 'Calle Rios 167', '56723456', 'Leonardo01'),
(1800.00, '2025-01-24T15:30:00', 'Calle Rios 168', '67234567', 'Margarit01'),
(1200.00, '2025-01-25T16:00:00', 'Calle Rios 169', '12345673', 'Alberto01'),
(550.00, '2025-01-26T17:30:00', 'Calle Rios 170', '23456734', 'Abigail01'),
(2200.00, '2025-01-27T18:10:00', 'Calle Rios 171', '34567345', 'Benjamin01'),
(370.00, '2025-01-28T19:45:00', 'Calle Rios 172', '45673456', 'Danielle01'),
(1000.00, '2025-01-29T20:00:00', 'Calle Rios 173', '56734567', 'Eleonora01'),
(1800.00, '2025-01-30T21:10:00', 'Calle Rios 174', '67345678', 'Ferdinan01'),
(1450.00, '2025-02-01T09:00:00', 'Calle Rios 175', '12345678', 'Gabriela01'),
(900.00, '2025-02-02T10:30:00', 'Calle Rios 176', '23456789', 'Isabella01'),
(800.00, '2025-02-03T11:45:00', 'Calle Rios 177', '34567891', 'Jonathan01'),
(1750.00, '2025-02-04T12:10:00', 'Calle Rios 178', '45678912', 'Kimberly01'),
(1000.00, '2025-02-05T13:20:00', 'Calle Rios 179', '56789123', 'Leonardo01'),
(500.00, '2025-02-06T14:40:00', 'Calle Rios 180', '67891234', 'Margarit01'),
(450.00, '2025-02-07T15:00:00', 'Calle Rios 181', '12345670', 'Alberto01'),
(1250.00, '2025-02-08T16:30:00', 'Calle Rios 182', '23456701', 'Abigail01'),
(2200.00, '2025-02-09T17:15:00', 'Calle Rios 183', '34567012', 'Benjamin01'),
(3000.00, '2025-02-10T18:50:00', 'Calle Rios 184', '45670123', 'Danielle01'),
(600.00, '2025-02-11T19:10:00', 'Calle Rios 185', '56701234', 'Eleonora01'),
(2200.00, '2025-02-12T20:25:00', 'Calle Rios 186', '67012345', 'Ferdinan01'),
(1200.00, '2025-02-13T21:00:00', 'Calle Rios 187', '12345671', 'Gabriela01'),
(1500.00, '2025-02-14T22:30:00', 'Calle Rios 188', '23456712', 'Isabella01'),
(800.00, '2025-02-15T23:40:00', 'Calle Rios 189', '34567123', 'Jonathan01'),
(3000.00, '2025-02-16T08:20:00', 'Calle Rios 190', '45671234', 'Kimberly01'),
(1750.00, '2025-02-17T09:35:00', 'Calle Rios 191', '56712345', 'Leonardo01'),
(400.00, '2025-02-18T10:50:00', 'Calle Rios 192', '67123456', 'Margarit01'),
(2000.00, '2025-02-19T11:00:00', 'Calle Rios 193', '12345672', 'Alberto01'),
(950.00, '2025-02-20T12:15:00', 'Calle Rios 194', '23456723', 'Abigail01'),
(2100.00, '2025-02-21T08:00:00', 'Calle Rios 195', '34567234', 'Benjamin01'),
(350.00, '2025-02-22T09:15:00', 'Calle Rios 196', '45672345', 'Danielle01'),
(950.00, '2025-02-23T10:30:00', 'Calle Rios 197', '56723456', 'Eleonora01'),
(1300.00, '2025-02-24T11:45:00', 'Calle Rios 198', '67234567', 'Ferdinan01'),
(500.00, '2025-02-25T12:00:00', 'Calle Rios 199', '12345673', 'Gabriela01'),
(700.00, '2025-02-26T13:30:00', 'Calle Rios 200', '23456734', 'Isabella01'),
(1500.00, '2025-02-27T14:45:00', 'Calle Rios 201', '34567345', 'Jonathan01'),
(2200.00, '2025-02-28T15:00:00', 'Calle Rios 202', '45673456', 'Kimberly01'),
(800.00, '2025-03-01T16:10:00', 'Calle Rios 203', '56734567', 'Leonardo01'),
(1250.00, '2025-03-02T17:00:00', 'Calle Rios 204', '67345678', 'Margarit01'),
(1800.00, '2025-03-03T18:20:00', 'Calle Rios 205', '12345678', 'Alberto01'),
(950.00, '2025-03-04T19:10:00', 'Calle Rios 206', '23456789', 'Abigail01'),
(2200.00, '2025-03-05T20:30:00', 'Calle Rios 207', '34567891', 'Benjamin01'),
(2500.00, '2025-03-06T21:40:00', 'Calle Rios 208', '45678912', 'Danielle01'),
(3000.00, '2025-03-07T08:10:00', 'Calle Rios 209', '56789123', 'Eleonora01'),
(1200.00, '2025-03-08T09:00:00', 'Calle Rios 210', '67891234', 'Ferdinan01'),
(500.00, '2025-03-09T10:30:00', 'Calle Rios 211', '12345670', 'Gabriela01'),
(350.00, '2025-03-10T11:15:00', 'Calle Rios 212', '23456701', 'Isabella01'),
(1500.00, '2025-03-11T12:20:00', 'Calle Rios 213', '34567012', 'Jonathan01'),
(700.00, '2025-03-12T13:50:00', 'Calle Rios 214', '45670123', 'Kimberly01'),
(1250.00, '2025-03-13T15:10:00', 'Calle Rios 215', '56701234', 'Leonardo01'),
(2200.00, '2025-03-14T16:25:00', 'Calle Rios 216', '67012345', 'Margarit01'),
(1800.00, '2025-03-15T17:30:00', 'Calle Rios 217', '12345671', 'Alberto01'),
(950.00, '2025-03-16T18:00:00', 'Calle Rios 218', '23456712', 'Abigail01'),
(2200.00, '2025-03-17T19:15:00', 'Calle Rios 219', '34567123', 'Benjamin01'),
(2500.00, '2025-03-18T20:30:00', 'Calle Rios 220', '45671234', 'Danielle01'),
(3000.00, '2025-03-19T21:00:00', 'Calle Rios 221', '56712345', 'Eleonora01'),
(1200.00, '2025-03-20T08:45:00', 'Calle Rios 222', '67123456', 'Ferdinan01'),
(500.00, '2025-03-21T10:00:00', 'Calle Rios 223', '12345672', 'Gabriela01'),
(350.00, '2025-03-22T11:30:00', 'Calle Rios 224', '23456723', 'Isabella01'),
(1500.00, '2025-03-23T12:15:00', 'Calle Rios 225', '34567234', 'Jonathan01'),
(700.00, '2025-03-24T13:40:00', 'Calle Rios 226', '45672345', 'Kimberly01'),
(1250.00, '2025-03-25T15:05:00', 'Calle Rios 227', '56723456', 'Leonardo01'),
(2200.00, '2025-03-26T16:30:00', 'Calle Rios 228', '67234567', 'Margarit01'),
(1800.00, '2025-03-27T17:40:00', 'Calle Rios 229', '12345673', 'Alberto01'),
(950.00, '2025-03-28T18:15:00', 'Calle Rios 230', '23456734', 'Abigail01'),
(2200.00, '2025-03-29T19:20:00', 'Calle Rios 231', '34567345', 'Benjamin01'),
(2500.00, '2025-03-30T20:25:00', 'Calle Rios 232', '45673456', 'Danielle01'),
(3000.00, '2025-03-31T21:00:00', 'Calle Rios 233', '56734567', 'Eleonora01'),
(1200.00, '2025-04-01T08:50:00', 'Calle Rios 234', '67345678', 'Ferdinan01');
GO


-----Estados----

INSERT INTO EstadoGenerado (T_Estado, NVenta) VALUES
(1,1),(2,2),(2,3),(1,4);

GO
INSERT INTO EstadoGenerado (T_Estado, NVenta) VALUES
 (2,5),(3,6), (1,7), (1,8), (1,9), (1,10);

GO

INSERT INTO EstadoGenerado (T_Estado, NVenta) VALUES
(1,11),(2,12),(1,13),(1,14), (1,15),(1,16), (1,17), (1,18), (1,19), (1,20);

GO
INSERT INTO EstadoGenerado (T_Estado, NVenta) VALUES
(1,21),(1,22),(1,23),(4,24), (1,25),(3,26), (2,27), (1,28), (4,29), (1,30);

GO

INSERT INTO EstadoGenerado (T_Estado, NVenta) VALUES
(1,31),(1,32),(1,33),(1,34), (1,35),(3,36), (2,37), (1,38), (4,39), (1,40);

GO

INSERT INTO EstadoGenerado (T_Estado, NVenta) VALUES
(1,41),(2,42),(3,43),(4,44), (1,45),(3,46), (2,47), (1,48), (4,49), (1,50);

GO

INSERT INTO EstadoGenerado (T_Estado, NVenta) VALUES
(1,51),(2,52),(3,53),(4,54), (1,55),(3,56), (2,57), (1,58), (4,59), (1,60);

GO
INSERT INTO EstadoGenerado (T_Estado, NVenta) VALUES
(1,61),(2,62),(3,63),(4,64), (1,65),(3,66), (2,67), (1,68), (4,69), (1,70);

GO

INSERT INTO EstadoGenerado (T_Estado, NVenta) VALUES
(1,71),(2,72),(3,73),(4,74), (1,75),(3,76), (2,77), (1,78), (4,79), (1,80);

GO
INSERT INTO EstadoGenerado (T_Estado, NVenta) VALUES
(1,81),(2,82),(3,83),(4,84), (1,85),(3,86), (2,87), (1,88), (4,89), (1,90);

GO



-----Venta Articulo

INSERT INTO VentaArticulo (NumVenta, CodArt, CantArticulos) VALUES
(1, 'Cepillo001', 2),
(2, 'HiloDen003', 3),
(3, 'Parace0005', 5),
(4, 'Jarabe0007', 1),
(5, 'SueroOr009', 3),
(6, 'Cepillo001', 1),
(7, 'HiloDen003', 2),
(8, 'Parace0005', 3),
(9, 'Jarabe0007', 1),
(10, 'SueroOr009', 1),
(11, 'Cepillo001', 1),
(12, 'HiloDen003', 1),
(13, 'Parace0005', 2),
(14, 'Jarabe0007', 1),
(15, 'SueroOr009', 1),
(16, 'Cepillo001', 1),
(17, 'HiloDen003', 1),
(18, 'Parace0005', 1),
(19, 'Jarabe0007', 1),
(20, 'SueroOr009', 1),
(21, 'Cepillo001', 1),
(22, 'HiloDen003', 1),
(23, 'Parace0005', 2),
(24, 'Jarabe0007', 1),
(25, 'SueroOr009', 1),
(26, 'Cepillo001', 1),
(27, 'HiloDen003', 1),
(28, 'Parace0005', 1),
(29, 'Jarabe0007', 1),
(30, 'SueroOr009', 1),
(31, 'Cepillo001', 1),
(32, 'Enjague004', 1),
(33, 'Jarabe0007', 1),
(34, 'Parace0005', 1),
(35, 'Cepillo001', 1),
(36, 'Enjague004', 1),
(37, 'Jarabe0007', 1),
(38, 'Parace0005', 1),
(39, 'Cepillo001', 1),
(40, 'Enjague004', 1),
(41, 'Cepillo001', 1),
(42, 'Parace0005', 1),
(43, 'SueroOr009', 1),
(44, 'Cepillo001', 1),
(45, 'Parace0005', 1),
(46, 'SueroOr009', 1),
(47, 'Cepillo001', 1),
(48, 'Parace0005', 1),
(49, 'SueroOr009', 1),
(50, 'Cepillo001', 1),
(51, 'Cepillo001', 1),
(52, 'Parace0005', 1),
(53, 'SueroOr009', 1),
(54, 'Cepillo001', 1),
(55, 'Parace0005', 1),
(56, 'SueroOr009', 1),
(57, 'Cepillo001', 1),
(58, 'Parace0005', 1),
(59, 'SueroOr009', 1),
(60, 'Cepillo001', 1),
(61, 'Cepillo001', 1),
(62, 'Pasta00002', 1),
(63, 'HiloDen003', 1),
(64, 'Enjague004', 1),
(65, 'Parace0005', 1),
(66, 'Antihis006', 1),
(67, 'Jarabe0007', 1),
(68, 'Unguent008', 1),
(69, 'SueroOr009', 1),
(70, 'Parace0005', 1),
(71, 'Antihis006', 1),
(72, 'Jarabe0007', 1),
(73, 'Cepillo001', 1),
(74, 'Pasta00002', 1),
(75, 'HiloDen003', 1),
(76, 'Enjague004', 1),
(77, 'Parace0005', 1),
(78, 'Antihis006', 1),
(79, 'Jarabe0007', 1),
(80, 'Unguent008', 1),
(81, 'SueroOr009', 1),
(82, 'Parace0005', 1),
(83, 'Antihis006', 1),
(84, 'Jarabe0007', 1),
(85, 'Cepillo001', 1),
(86, 'Pasta00002', 1),
(87, 'HiloDen003', 1),
(88, 'Enjague004', 1),
(89, 'Parace0005', 1),
(90, 'Antihis006', 1);
GO



----------Logueo Empleado


CREATE PROCEDURE LogueoEmpleado 
		@UsuLog Varchar (20),
		@PassUsu Varchar(20)
		AS
		BEGIN
		
		SELECT * FROM Empleado
		WHERE UsuLog = @UsuLog AND PassUsu = @PassUsu

END
GO

CREATE PROCEDURE BuscarEmp 
@UsuLog varchar(20) 
AS
BEGIN
	Select * 
	From Empleado
	where UsuLog = @UsuLog
END
GO




--------Cliente--------------------------------
CREATE PROCEDURE AltaCliente
    @CiCli CHAR(8),
    @Nombre VARCHAR(100),
    @NumTarj CHAR(16),
    @Telefono CHAR(15)
    
    
AS
BEGIN
    -- Primero, consulta si el Cliente ya existe
    IF EXISTS (SELECT * FROM Clientes WHERE CiCli = @CiCli)
    BEGIN
        RETURN -1 
        
    END

    -- Si no existe, inserta el nuevo Cliente
    INSERT INTO Clientes(CiCli, Nombre, NumTarj,Telefono)
    VALUES (@CiCli, @Nombre, @NumTarj,@Telefono)
    
    RETURN 1 -- Alta exitosa
END
GO


CREATE PROCEDURE ModificarCliente
    @CiCli CHAR(8),
    @Nombre VARCHAR(100),
    @NumTarj CHAR(16),
    @Telefono CHAR(15)
    
AS
BEGIN
    -- Primero, consulta si el Cliente existe
    IF NOT EXISTS (SELECT * FROM Clientes WHERE CiCli = @CiCli)
    BEGIN
       
        RETURN -1
    END

    -- Si existe, es posible modificarlo
    UPDATE Clientes
    SET
            Nombre = @Nombre,
            NumTarj = @NumTarj,
            Telefono = @Telefono
    WHERE @CiCli = CiCli
    
    RETURN 1 -- Modificacion exitosa
END
GO


CREATE PROCEDURE ListarCliente
AS
BEGIN
	SELECT * FROM Clientes
	ORDER BY Nombre
END 
GO



--EXEC ListarCliente


CREATE PROCEDURE BuscarCliente 
@CiCli int 
AS
BEGIN
	Select * 
	From Clientes
	where CiCli = @CiCli
END
GO

--exec BuscarCliente 12345678






---------------CATEGORIAS---------------------

CREATE PROCEDURE BuscarCategoria
@CodCat Varchar(6)

AS
BEGIN
	SELECT * FROM Categoria WHERE Codigo_Cate = @CodCat
	END
GO


CREATE PROCEDURE BuscarActivo
@CodCat Varchar(6)
AS
BEGIN
	SELECT * FROM Categoria WHERE Codigo_Cate = @CodCat AND Activo = 1
	
END
GO


CREATE PROCEDURE AltaCategoria
    @Codigo_Cate Varchar(6),
    @Nombre VARCHAR(100)
   
AS
BEGIN
    -- Verificar si la categoría ya existe y está activa
    IF EXISTS (SELECT 1 FROM Categoria WHERE Codigo_Cate = @Codigo_Cate AND Activo = 1)
    BEGIN
        RETURN -1; -- La categoría ya existe y está activa
    END
    
    -- Verificar si la categoría existe pero está inactiva
    IF EXISTS (SELECT 1 FROM Categoria WHERE Codigo_Cate = @Codigo_Cate AND Activo = 0)
    BEGIN
        -- Reactivar la categoría y actualizar el nombre
        UPDATE Categoria
        SET Nombre = @Nombre, Activo = 1
        WHERE Codigo_Cate = @Codigo_Cate;

        RETURN -2; -- Categoría reactivada
    END

    -- Si la categoría no existe, insertarla
    INSERT INTO Categoria (Codigo_Cate, Nombre, Activo)
    VALUES (@Codigo_Cate, @Nombre, 1);

    RETURN 1; -- Alta exitosa
END
GO
	
    

CREATE PROCEDURE ModificarCategoria
	@Codigo_Cate Varchar(6),
    @Nombre VARCHAR(100)
    
    
AS
BEGIN
    -- Primero, consulta si la Categoria existe
    IF  NOT EXISTS (SELECT * FROM Categoria WHERE Codigo_Cate = @Codigo_Cate AND Activo=1)
    BEGIN
       
        RETURN -1
    END
	ELSE
	Begin
    -- Si existe, modifica la Categoria
    UPDATE Categoria
    SET  
            Nombre = @Nombre
            
   WHERE Codigo_Cate = @Codigo_Cate
    
    If (@@ERROR = 0)
    RETURN 1 -- Modificacion exitosa

   else
	return -3
	END
	
		
END
GO



CREATE PROCEDURE BajaCategoria
  @Codigo_Cate VARCHAR(6)
AS
BEGIN
    -- Verificar si la categoría existe
    IF NOT EXISTS (SELECT 1 FROM Categoria WHERE Codigo_Cate = @Codigo_Cate)
    BEGIN
        RETURN -1; -- La categoría no existe
    END

    -- Si la categoría tiene artículos asociados, solo se desactiva
    IF EXISTS (SELECT 1 FROM Articulo WHERE Codigo_Cate = @Codigo_Cate)
    BEGIN
        UPDATE Categoria  
        SET Activo = 0 
        WHERE Codigo_Cate = @Codigo_Cate;

        RETURN 1; -- Categoría desactivada
    END
    ELSE
    BEGIN
        -- Si no tiene artículos, se elimina
        DELETE FROM Categoria WHERE Codigo_Cate = @Codigo_Cate;
        
        IF @@ERROR = 0
            RETURN 1; -- Eliminación exitosa
        ELSE
            RETURN -3; -- Error en la eliminación
    END
END
GO



--exec BajaCategoria MEDI02 

CREATE PROCEDURE ListarCategoria
AS
BEGIN
    SELECT * 
    FROM Categoria 
    WHERE Activo = 1
    ORDER BY Codigo_Cate
END
GO



--EXEC  ListarCategoria


---------------ARTICULO----------------------
CREATE PROCEDURE AltaArticulo
    @Codigo CHAR(10),
    @TipoPresentacion VARCHAR(50),
    @Nombre VARCHAR(100),
    @Precio DECIMAL(10, 2),
    @Tamaño INT,
    @FechaVenc DATE,
    @Codigo_Cate Varchar(6)
    --@UsuLog VARCHAR(50)
        
AS
BEGIN
    -- Verificamos que no exista un artículo con el mismo código y que esté activo
    IF EXISTS (SELECT * FROM Articulo WHERE Codigo = @Codigo AND Activo = 1)
    BEGIN
        RETURN -1
    END

    -- Verificamos que la categoría exista y esté activa en la tabla Categoria
    IF NOT EXISTS (SELECT * FROM Categoria WHERE Codigo_Cate = @Codigo_Cate AND Activo = 1)
    BEGIN
        RETURN -2
    END

    -- Si el artículo existe pero está inactivo, lo actualizamos
    IF EXISTS (SELECT 1 FROM Articulo WHERE Codigo = @Codigo AND Activo = 0)
    BEGIN
        UPDATE Articulo
        SET Nombre = @Nombre,
            Precio = @Precio,
            TipoPresentacion = @TipoPresentacion,
            Tamaño = @Tamaño,
            FechaVenc = @FechaVenc,
            Codigo_Cate = @Codigo_Cate,
            Activo = 1
        WHERE Codigo = @Codigo

        RETURN -3
    END

    -- Si el artículo no existe, lo insertamos
    INSERT INTO Articulo (Codigo, Nombre, Precio, TipoPresentacion, Tamaño, FechaVenc, Codigo_Cate, Activo)
    VALUES (@Codigo, @Nombre, @Precio, @TipoPresentacion, @Tamaño, @FechaVenc, @Codigo_Cate, 1)

    RETURN 1
END
GO


CREATE PROCEDURE ModificarArticulo

	@Codigo CHAR(10),
    @TipoPresentacion VARCHAR(50),
    @Nombre VARCHAR(100),
    @Precio DECIMAL(10, 2),
    @Tamaño INT,
    @FechaVenc DATE ,
    @Codigo_Cate Varchar(6)
   
    
AS
BEGIN
    -- Primero, consulta si el Articulo existe
    IF  NOT EXISTS (SELECT * FROM Articulo WHERE Codigo = @Codigo AND Activo =1)
    BEGIN
        
        RETURN -1
    END
    
    If NOT EXISTS (SELECT 1 FROM Categoria WHERE Codigo_Cate = @Codigo_Cate AND Activo = 1)
    BEGIN
		RETURN -2--- Categoria no Existe o no esta activa
		END
		
		
    -- Si existe, modifica el Articulo
    UPDATE Articulo
    SET
            TipoPresentacion = @TipoPresentacion,
            Nombre = @Nombre,
            Precio = @Precio,
            Tamaño = @Tamaño,
            FechaVenc = @FechaVenc,
            Codigo_Cate = @Codigo_Cate
            
    WHERE @Codigo = Codigo
    
    RETURN 1 -- Modificación exitosa
END
GO

CREATE PROCEDURE BajaArticulo
    @CodArt Varchar(10)
AS
BEGIN
    -- Verificar si el artículo existe
    IF NOT EXISTS (SELECT 1 FROM Articulo WHERE Codigo = @CodArt)
    BEGIN
        RETURN -1;
    END
    
    -- Si el artículo tiene ventas asociadas, realizar baja lógica
    IF EXISTS (SELECT 1 FROM VentaArticulo WHERE CodArt = @CodArt)
    BEGIN
        UPDATE Articulo
        SET Activo = 0
        WHERE Codigo = @CodArt;

        IF @@ERROR <> 0
        BEGIN
            RETURN -3;
        END
        
        RETURN 2; -- Baja lógica 
    END
    
    
    DELETE FROM Articulo WHERE Codigo = @CodArt;

    IF @@ERROR <> 0
    BEGIN
        RETURN -3;
    END

    RETURN 1; -- Eliminación exitosa
END
GO


CREATE PROCEDURE BuscarArticuloActivo
@CodArt Varchar(10)
AS
BEGIN
    SELECT * FROM Articulo WHERE Codigo = @CodArt AND Activo = 1;
END;
GO	
	

CREATE Procedure BuscarArticulo
@CodArt Varchar (10)
AS
BEGIN

	SELECT * FROM Articulo WHERE Codigo = @CodArt
	END
	GO	


CREATE PROCEDURE ListarArticulo
AS
BEGIN
	SELECT  * FROM Articulo 
	where Activo = 1  
	ORDER BY Nombre
	
END 
GO



---------------VENTA--------------------------


CREATE PROCEDURE AltaVenta
    @MontoTotal DECIMAL(10, 2),
    @DirEnvio VARCHAR(255),
    @CiCli CHAR(8),
    @UsuLog VARCHAR(50),
    @NumVenta INT OUTPUT  
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar Cliente
    IF NOT EXISTS (SELECT 1 FROM Clientes WHERE CiCli = @CiCli)
        RETURN -1; -- El cliente no existe

    -- Validar Empleado
    IF NOT EXISTS (SELECT 1 FROM Empleado WHERE UsuLog = @UsuLog)
        RETURN -2; -- El empleado no está logueado

   
    INSERT INTO Venta (MontoTotal, DirEnvio, CiCli, UsuLog)
    VALUES (@MontoTotal, @DirEnvio, @CiCli, @UsuLog);

    
    SET @NumVenta = SCOPE_IDENTITY();

    RETURN 2; 
END;
GO



CREATE PROCEDURE ListarVenta
AS
BEGIN
	SELECT * FROM Venta
	ORDER BY NumVenta
END 
GO

--EXEC  ListarVenta




----Venta de Articulo

CREATE PROCEDURE AltaVentaArt 
    @NumVenta INT,
    @CodArt CHAR(10),
    @CantArticulos INT
    
AS
BEGIN
    -- Verificar si la venta existe
    IF NOT EXISTS (SELECT 1 FROM Venta WHERE NumVenta = @NumVenta)
    BEGIN 
        RETURN -1;
    END

    -- Verificar si el artículo existe
    IF NOT EXISTS (SELECT 1 FROM Articulo WHERE Codigo = @CodArt AND Activo = 1)
    BEGIN
        RETURN -2;
    END
    

    
    --Para no duplicar la PK
    IF EXISTS (SELECT 1 FROM VentaArticulo WHERE NumVenta = @NumVenta AND CodArt = @CodArt)
    BEGIN
        RETURN -4;  -- Ya existe ese artículo en la venta
    END

    -- Insertar en VentaArticulo
    INSERT INTO VentaArticulo (NumVenta, CodArt, CantArticulos)
    VALUES (@NumVenta, @CodArt, @CantArticulos);

    -- Verificar si la inserción fue exitosa
    IF @@ROWCOUNT = 0
    BEGIN
        RETURN -3; 
    END

    RETURN 2;
END
GO


CREATE PROCEDURE ListarVentaArt
    @NumVenta INT
AS
BEGIN
    SELECT * 
    FROM VentaArticulo
    WHERE NumVenta = @NumVenta
    ORDER BY CodArt;
END
GO




--EXEC  ListarVentaArt 43


-------------------------------------Listado de Estados de una Venta




Create PROCEDURE ListarEstadoVenta
    @NumVenta INT
AS
BEGIN
    SELECT *
    FROM EstadoGenerado
    WHERE NVenta = @NumVenta
    ORDER BY NVenta;
END
GO


--exec ListarEstadoVenta 20

--EXEC ListarEstadoVenta @NumVenta = 1;

CREATE PROCEDURE AsignarEstadoAVenta
    @NumVenta INT,
    @IdEstado INT
AS
BEGIN
    -- Verificar si la venta existe
    IF NOT EXISTS (SELECT 1 FROM Venta WHERE NumVenta = @NumVenta)
        RETURN -1;  -- La venta no existe

    -- Verificar si el estado existe
    IF NOT EXISTS (SELECT 1 FROM EstadoVenta WHERE IdEstado = @IdEstado)
        RETURN -2;  -- El estado no existe

    -- Verificar si el estado ya está asignado a la venta
    IF EXISTS (SELECT * FROM EstadoGenerado WHERE NVenta = @NumVenta AND T_Estado = @IdEstado)
        RETURN -3;  -- El estado ya está asignado

    -- Insertar el nuevo estado
    INSERT INTO EstadoGenerado (T_Estado, NVenta)
    VALUES (@IdEstado, @NumVenta);

    -- Verificar si la inserción tuvo errores
    IF @@ERROR <> 0  
        RETURN -4;  -- Error en la inserción

    RETURN 1;  -- Inserción exitosa
END
GO




---------------------ESTADOS 


CREATE PROCEDURE ListarEstados
AS
BEGIN
	SELECT * FROM EstadoVenta
	ORDER BY IdEstado
END 
GO
 
--exec ListarEstados

CREATE Procedure BuscarEstado
@IdEstado INT
AS
BEGIN

	SELECT * FROM EstadoVenta WHERE IdEstado = @IdEstado
END
GO	

--exec BuscarEstado 2

CREATE PROCEDURE ListadoHistoricoVenta
@NVenta INT
AS
BEGIN
	SELECT * FROM EstadoGenerado	
	WHERE NVenta = @NVenta
	ORDER BY T_Estado
END 
GO

--exec ListadoHistoricoVenta 30

select * from Venta


select * from EstadoGenerado
order by NVenta
--1 a 10 no tienen estado 