IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Clientes] (
    [Id] int NOT NULL IDENTITY,
    [CI] nvarchar(20) NOT NULL,
    [Nombres] nvarchar(120) NOT NULL,
    [Direccion] nvarchar(180) NOT NULL,
    [Telefono] nvarchar(30) NOT NULL,
    [FotoCasa1Url] nvarchar(max) NULL,
    [FotoCasa2Url] nvarchar(max) NULL,
    [FotoCasa3Url] nvarchar(max) NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id])
);

CREATE TABLE [LogsApi] (
    [Id] int NOT NULL IDENTITY,
    [Fecha] datetime2 NOT NULL,
    [TipoLog] nvarchar(20) NOT NULL,
    [MetodoHttp] nvarchar(10) NOT NULL,
    [UrlEndpoint] nvarchar(500) NOT NULL,
    [DireccionIp] nvarchar(max) NULL,
    [RequestBody] nvarchar(max) NULL,
    [ResponseBody] nvarchar(max) NULL,
    [Detalle] nvarchar(max) NULL,
    CONSTRAINT [PK_LogsApi] PRIMARY KEY ([Id])
);

CREATE TABLE [ArchivosCliente] (
    [Id] int NOT NULL IDENTITY,
    [ClienteId] int NOT NULL,
    [NombreArchivo] nvarchar(255) NOT NULL,
    [UrlArchivo] nvarchar(500) NOT NULL,
    [Extension] nvarchar(12) NOT NULL,
    [TamanoBytes] bigint NOT NULL,
    [FechaSubida] datetime2 NOT NULL,
    CONSTRAINT [PK_ArchivosCliente] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ArchivosCliente_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_ArchivosCliente_ClienteId] ON [ArchivosCliente] ([ClienteId]);

CREATE UNIQUE INDEX [IX_Clientes_CI] ON [Clientes] ([CI]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251108155001_InitialCreate', N'9.0.10');

COMMIT;
GO

