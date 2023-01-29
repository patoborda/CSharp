/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[Descripciones]
      ,[Costo]
      ,[PrecioVenta]
      ,[Stock]
      ,[IdUsuario]
  FROM [SistemaGestion].[dbo].[Producto]


  -- 1. Primera consulta --
  -- Deberás realizar una consulta que traiga los datos del usuario que tiene como nombre de usuario eperez--
  SELECT * 
  FROM dbo.Usuario 
  WHERE NombreUsuario = 'eperez'

  -- 2. Consulta con condiciones --
  -- Hay coincidencia --
  SELECT *
  FROM dbo.Usuario
  WHERE NombreUsuario = 'eperez' AND Contraseña = 'SoyErnestoPerez'

  -- No hay coincidencia --
  SELECT *
  FROM dbo.Usuario
  WHERE NombreUsuario = 'ernestoperez' AND Contraseña = 'SoyErnestoPerez'


  -- 3. Consulta a Producto --
  SELECT *
  FROM dbo.Producto
  WHERE Producto.IdUsuario = '3'

  -- 4. Nuevo Usuario --
  INSERT INTO Usuario(Nombre, Apellido, NombreUsuario, Mail, Contraseña)
  VALUES ('Patricio', 'Borda', 'Pborda', 'patoeda@hotmail.com', '123456');

  -- 5. Nuevo producto --
  INSERT INTO Producto (Descripciones, Costo, PrecioVenta, Stock, IdUsuario)
  VALUES ('Zapatillas Nike', 24000, 27000, 10, 3)
