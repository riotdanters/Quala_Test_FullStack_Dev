CREATE OR ALTER PROCEDURE afar_sp_CrearProducto
    @CodigoProducto INT,
    @Nombre NVARCHAR(250),
    @Descripcion NVARCHAR(250),
    @ReferenciaInterna NVARCHAR(100),
    @PrecioUnitario DECIMAL(18, 2),
    @Estado BIT,
    @UnidadMedida NVARCHAR(50),
    @FechaCreacion DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO afar_producto (
        codigo_producto,
        nombre,
        descripcion,
        referencia_interna,
        precio_unitario,
        estado,
        unidad_medida,
        fecha_creacion
    )
    VALUES (
        @CodigoProducto,
        @Nombre,
        @Descripcion,
        @ReferenciaInterna,
        @PrecioUnitario,
        @Estado,
        @UnidadMedida,
        @FechaCreacion
    );
END