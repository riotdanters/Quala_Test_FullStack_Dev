IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='afar_producto' and xtype='U')
BEGIN
    CREATE TABLE afar_producto (
        codigo_producto INT PRIMARY KEY NOT NULL,
        nombre NVARCHAR(250) NOT NULL,
        descripcion NVARCHAR(250) NOT NULL,
        referencia_interna NVARCHAR(100) NOT NULL,
        precio_unitario DECIMAL(18, 2) NOT NULL,
        estado BIT NOT NULL,
        unidad_medida NVARCHAR(50) NOT NULL,
        fecha_creacion DATETIME NOT NULL CONSTRAINT DF_afar_producto_fecha_creacion DEFAULT GETDATE(),
        CONSTRAINT CK_afar_producto_precio_positivo CHECK (precio_unitario > 0)
    );
END
GO