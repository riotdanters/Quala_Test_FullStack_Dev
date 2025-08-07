CREATE OR ALTER PROCEDURE afar_sp_EliminarProducto
    @CodigoProducto INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM afar_producto
    WHERE codigo_producto = @CodigoProducto;
END
GO