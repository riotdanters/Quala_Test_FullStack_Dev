CREATE OR ALTER PROCEDURE afar_sp_ObtenerProductoPorCodigo
    @CodigoProducto INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        codigo_producto AS CodigoProducto,
        nombre AS Nombre,
        descripcion AS Descripcion,
        referencia_interna AS ReferenciaInterna,
        precio_unitario AS PrecioUnitario,
        estado AS Estado,
        unidad_medida AS UnidadMedida,
        fecha_creacion AS FechaCreacion
    FROM afar_producto
    WHERE codigo_producto = @CodigoProducto;
END
GO