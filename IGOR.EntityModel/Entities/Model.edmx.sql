
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/20/2019 19:39:35
-- Generated from EDMX file: C:\Users\Arsanukaev\Projects\IGOR\IGOR.EntityModel\Entities\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Per2com];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ManufacturerComponent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Components] DROP CONSTRAINT [FK_ManufacturerComponent];
GO
IF OBJECT_ID(N'[dbo].[FK_ComponentOrderItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderItems] DROP CONSTRAINT [FK_ComponentOrderItem];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderOrderItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderItems] DROP CONSTRAINT [FK_OrderOrderItem];
GO
IF OBJECT_ID(N'[dbo].[FK_MemoryTypeRam]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Components_Ram] DROP CONSTRAINT [FK_MemoryTypeRam];
GO
IF OBJECT_ID(N'[dbo].[FK_MemoryTypeMotherboard]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Components_Motherboard] DROP CONSTRAINT [FK_MemoryTypeMotherboard];
GO
IF OBJECT_ID(N'[dbo].[FK_Ram_inherits_Component]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Components_Ram] DROP CONSTRAINT [FK_Ram_inherits_Component];
GO
IF OBJECT_ID(N'[dbo].[FK_Motherboard_inherits_Component]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Components_Motherboard] DROP CONSTRAINT [FK_Motherboard_inherits_Component];
GO
IF OBJECT_ID(N'[dbo].[FK_Cpu_inherits_Component]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Components_Cpu] DROP CONSTRAINT [FK_Cpu_inherits_Component];
GO
IF OBJECT_ID(N'[dbo].[FK_GraphicsCard_inherits_Component]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Components_GraphicsCard] DROP CONSTRAINT [FK_GraphicsCard_inherits_Component];
GO
IF OBJECT_ID(N'[dbo].[FK_Drive_inherits_Component]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Components_Drive] DROP CONSTRAINT [FK_Drive_inherits_Component];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Manufacturers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Manufacturers];
GO
IF OBJECT_ID(N'[dbo].[Components]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Components];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[OrderItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderItems];
GO
IF OBJECT_ID(N'[dbo].[MemoryTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MemoryTypes];
GO
IF OBJECT_ID(N'[dbo].[Components_Ram]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Components_Ram];
GO
IF OBJECT_ID(N'[dbo].[Components_Motherboard]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Components_Motherboard];
GO
IF OBJECT_ID(N'[dbo].[Components_Cpu]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Components_Cpu];
GO
IF OBJECT_ID(N'[dbo].[Components_GraphicsCard]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Components_GraphicsCard];
GO
IF OBJECT_ID(N'[dbo].[Components_Drive]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Components_Drive];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Manufacturers'
CREATE TABLE [dbo].[Manufacturers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Country] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Components'
CREATE TABLE [dbo].[Components] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Count] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] real  NOT NULL,
    [ManufacturerId] int  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Customer] nvarchar(max)  NOT NULL,
    [OrderMoment] datetime  NOT NULL,
    [State] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrderItems'
CREATE TABLE [dbo].[OrderItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Count] int  NOT NULL,
    [ComponentId] int  NOT NULL,
    [OrderId] int  NOT NULL
);
GO

-- Creating table 'MemoryTypes'
CREATE TABLE [dbo].[MemoryTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Components_Ram'
CREATE TABLE [dbo].[Components_Ram] (
    [Capacity] real  NOT NULL,
    [Frequency] real  NOT NULL,
    [MemoryTypeId] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Components_Motherboard'
CREATE TABLE [dbo].[Components_Motherboard] (
    [Chipset] nvarchar(max)  NOT NULL,
    [Socket] nvarchar(max)  NOT NULL,
    [MemoryTypeId] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Components_Cpu'
CREATE TABLE [dbo].[Components_Cpu] (
    [Frequency] real  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Components_GraphicsCard'
CREATE TABLE [dbo].[Components_GraphicsCard] (
    [Capacity] real  NOT NULL,
    [GraphicalMemoryType] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Components_Drive'
CREATE TABLE [dbo].[Components_Drive] (
    [Capacity] real  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Manufacturers'
ALTER TABLE [dbo].[Manufacturers]
ADD CONSTRAINT [PK_Manufacturers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Components'
ALTER TABLE [dbo].[Components]
ADD CONSTRAINT [PK_Components]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderItems'
ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [PK_OrderItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MemoryTypes'
ALTER TABLE [dbo].[MemoryTypes]
ADD CONSTRAINT [PK_MemoryTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Components_Ram'
ALTER TABLE [dbo].[Components_Ram]
ADD CONSTRAINT [PK_Components_Ram]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Components_Motherboard'
ALTER TABLE [dbo].[Components_Motherboard]
ADD CONSTRAINT [PK_Components_Motherboard]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Components_Cpu'
ALTER TABLE [dbo].[Components_Cpu]
ADD CONSTRAINT [PK_Components_Cpu]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Components_GraphicsCard'
ALTER TABLE [dbo].[Components_GraphicsCard]
ADD CONSTRAINT [PK_Components_GraphicsCard]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Components_Drive'
ALTER TABLE [dbo].[Components_Drive]
ADD CONSTRAINT [PK_Components_Drive]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ManufacturerId] in table 'Components'
ALTER TABLE [dbo].[Components]
ADD CONSTRAINT [FK_ManufacturerComponent]
    FOREIGN KEY ([ManufacturerId])
    REFERENCES [dbo].[Manufacturers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ManufacturerComponent'
CREATE INDEX [IX_FK_ManufacturerComponent]
ON [dbo].[Components]
    ([ManufacturerId]);
GO

-- Creating foreign key on [ComponentId] in table 'OrderItems'
ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [FK_ComponentOrderItem]
    FOREIGN KEY ([ComponentId])
    REFERENCES [dbo].[Components]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ComponentOrderItem'
CREATE INDEX [IX_FK_ComponentOrderItem]
ON [dbo].[OrderItems]
    ([ComponentId]);
GO

-- Creating foreign key on [OrderId] in table 'OrderItems'
ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [FK_OrderOrderItem]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[Orders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderOrderItem'
CREATE INDEX [IX_FK_OrderOrderItem]
ON [dbo].[OrderItems]
    ([OrderId]);
GO

-- Creating foreign key on [MemoryTypeId] in table 'Components_Ram'
ALTER TABLE [dbo].[Components_Ram]
ADD CONSTRAINT [FK_MemoryTypeRam]
    FOREIGN KEY ([MemoryTypeId])
    REFERENCES [dbo].[MemoryTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MemoryTypeRam'
CREATE INDEX [IX_FK_MemoryTypeRam]
ON [dbo].[Components_Ram]
    ([MemoryTypeId]);
GO

-- Creating foreign key on [MemoryTypeId] in table 'Components_Motherboard'
ALTER TABLE [dbo].[Components_Motherboard]
ADD CONSTRAINT [FK_MemoryTypeMotherboard]
    FOREIGN KEY ([MemoryTypeId])
    REFERENCES [dbo].[MemoryTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MemoryTypeMotherboard'
CREATE INDEX [IX_FK_MemoryTypeMotherboard]
ON [dbo].[Components_Motherboard]
    ([MemoryTypeId]);
GO

-- Creating foreign key on [Id] in table 'Components_Ram'
ALTER TABLE [dbo].[Components_Ram]
ADD CONSTRAINT [FK_Ram_inherits_Component]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Components]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Components_Motherboard'
ALTER TABLE [dbo].[Components_Motherboard]
ADD CONSTRAINT [FK_Motherboard_inherits_Component]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Components]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Components_Cpu'
ALTER TABLE [dbo].[Components_Cpu]
ADD CONSTRAINT [FK_Cpu_inherits_Component]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Components]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Components_GraphicsCard'
ALTER TABLE [dbo].[Components_GraphicsCard]
ADD CONSTRAINT [FK_GraphicsCard_inherits_Component]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Components]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Components_Drive'
ALTER TABLE [dbo].[Components_Drive]
ADD CONSTRAINT [FK_Drive_inherits_Component]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Components]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------