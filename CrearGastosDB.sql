USE GastosDB;
GO
DELETE FROM Gastos;
DBCC CHECKIDENT ('Gastos', RESEED, 0);
GO
INSERT INTO Gastos (Descripcion, Monto, Categoria) VALUES (N'Almuerzo', 45.50, N'Comida');
INSERT INTO Gastos (Descripcion, Monto, Categoria) VALUES (N'Gasolina', 250.00, N'Transporte');
INSERT INTO Gastos (Descripcion, Monto, Categoria) VALUES (N'Cine', 75.00, N'Entretenimiento');
GO
