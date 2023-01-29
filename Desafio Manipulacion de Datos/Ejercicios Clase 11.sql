/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[Nombre]
      ,[Apellido]
      ,[NombreUsuario]
      ,[Contraseña]
      ,[Mail]
  FROM [SistemaGestion].[dbo].[Usuario]

  -- EJERCICIOS CLASE 11 --
-- 1. Modificar Usuario --
UPDATE dbo.Usuario
SET
Contraseña = 'contraCambiada'
WHERE Id = 2

-- 2. Eliminar usuario --
-- Elimino los 'hijos' para luego eliminar el usuario --

DELETE FROM dbo.ProductoVendido
WHERE IdVenta = 1

--DELETE FROM dbo.Producto --
-- WHERE IdUsuario = 1 --

DELETE FROM dbo.Venta
WHERE IdUsuario = 1

DELETE FROM dbo.Usuario
WHERE Id = 1

-- 3. Modificar un producto --
UPDATE dbo.Producto
SET 
Stock = 3
WHERE Descripciones = 'Pantalon Jean'

-- 4. Eliminar producto campera --

DELETE FROM Producto
WHERE Descripciones = 'Campera'

--5. Obtener los nombres de usuarios que cargaron los productos --

SELECT Usuario.NombreUsuario , Producto.Descripciones
FROM Usuario
INNER JOIN Producto
ON Usuario.Id = Producto.IdUsuario