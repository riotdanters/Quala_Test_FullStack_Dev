CREATE OR ALTER PROCEDURE afar_sp_ActualizarProducto
    @CodigoProducto INT,
    @Nombre NVARCHAR(250),
    @Descripcion NVARCHAR(250),
    @ReferenciaInterna NVARCHAR(100),
    @PrecioUnitario DECIMAL(18, 2),
    @Estado BIT,
    @UnidadMedida NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE afar_producto
    SET
        nombre = @Nombre,
        descripcion = @Descripcion,
        referencia_interna = @ReferenciaInterna,
        precio_unitario = @PrecioUnitario,
        estado = @Estado,
        unidad_medida = @UnidadMedida
    WHERE codigo_producto = @CodigoProducto;
END
GO