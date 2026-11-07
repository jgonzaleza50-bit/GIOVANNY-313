-- Script de base de datos - Tarea 9
-- Ejecutar contra (localdb)\MSSQLLocalDB

IF DB_ID('GastosDB') IS NULL
BEGIN
    CREATE DATABASE GastosDB;
END
GO

USE GastosDB;
GO

IF OBJECT_ID('dbo.Gastos', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Gastos (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Descripcion NVARCHAR(100) NOT NULL,
        Monto DECIMAL(10,2) NOT NULL,
        Categoria NVARCHAR(50) NOT NULL
    );

    INSERT INTO dbo.Gastos (Descripcion, Monto, Categoria) VALUES
        ('Almuerzo', 45.50, 'Comida'),
        ('Gasolina', 250.00, 'Transporte'),
        ('Cine', 75.00, 'Entretenimiento');
END
GO

SELECT * FROM dbo.Gastos;
GO
GO