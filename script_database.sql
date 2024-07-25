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
GO

CREATE TABLE [Products] (
    [Id] uniqueidentifier NOT NULL,
    [Descricao] nvarchar(250) NOT NULL,
    [Marca] nvarchar(250) NOT NULL,
    [UnidadeMedida] int NOT NULL,
    [ValorDeCompra] float NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);
GO

CREATE UNIQUE INDEX [IX_Products_Descricao] ON [Products] ([Descricao]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240724051428_InitialMigration', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Products] ADD [ProviderId] uniqueidentifier NULL;
GO

CREATE TABLE [Providers] (
    [Id] uniqueidentifier NOT NULL,
    [CNPJ] nvarchar(60) NOT NULL,
    [Nome] nvarchar(250) NOT NULL,
    [Endereco] nvarchar(250) NOT NULL,
    [Telefone] nvarchar(250) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    CONSTRAINT [PK_Providers] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_Products_ProviderId] ON [Products] ([ProviderId]);
GO

CREATE UNIQUE INDEX [IX_Providers_CNPJ] ON [Providers] ([CNPJ]);
GO

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_Providers_ProviderId] FOREIGN KEY ([ProviderId]) REFERENCES [Providers] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240724151141_ProvidersEntity', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_Providers_ProviderId];
GO

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_Providers_ProviderId] FOREIGN KEY ([ProviderId]) REFERENCES [Providers] ([Id]) ON DELETE SET NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240725032019_SetNullProviderId', N'8.0.7');
GO

COMMIT;
GO

