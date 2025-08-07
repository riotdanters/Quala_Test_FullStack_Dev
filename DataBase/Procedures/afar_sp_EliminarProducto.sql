CREATE OR ALTER PROCEDURE afar_sp_EliminarProducto
    @CodigoProducto INT
AS
BEGIN
    DELETE FROM afar_producto
    WHERE codigo_producto = @CodigoProducto;
END
GO