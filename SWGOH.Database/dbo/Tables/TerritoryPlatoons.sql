CREATE TABLE [dbo].[TerritoryPlatoons] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [PlatoonNumber]     INT              DEFAULT ((0)) NOT NULL,
    [PhaseTerritory_Id] UNIQUEIDENTIFIER NULL,
    [IsClosed]          BIT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.TerritoryPlatoons] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.TerritoryPlatoons_dbo.PhaseTerritories_PhaseTerritory_Id] FOREIGN KEY ([PhaseTerritory_Id]) REFERENCES [dbo].[PhaseTerritories] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_PhaseTerritory_Id]
    ON [dbo].[TerritoryPlatoons]([PhaseTerritory_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_IsClosed]
    ON [dbo].[TerritoryPlatoons]([IsClosed] ASC);

