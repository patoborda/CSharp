
   --1. Obtener todos los productos vendidos --

  SELECT Producto.Descripciones , ProductoVendido.*
  FROM Producto
  INNER JOIN ProductoVendido
  ON Producto.Id = ProductoVendido.IdProducto

  --2.Obtener las ventas que registren dos o más productos vendidos del mismo tipo.. --

  SELECT ProductoVendido.IdVenta, ProductoVendido.Stock, Producto.Descripciones
  FROM ProductoVendido
  INNER JOIN  Producto
  ON IdProducto = Producto.Id
  WHERE ProductoVendido.Stock >= 2

  --3. Insertar la venta de 20 productos 'Remera manga larga' vendidos por el usuario 2 --

  INSERT INTO ProductoVendido (Stock, IdProducto, IdVenta)
  VALUES (20, 1, 7)

  INSERT INTO Venta (IdUsuario, Comentarios)
  VALUES (2, 'Remeras manga larga')

  -- Verificar --
  SELECT * FROM ProductoVendido
  INNER JOIN Producto
  ON ProductoVendido.Id = Producto.Id
