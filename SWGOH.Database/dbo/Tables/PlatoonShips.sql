CREATE TABLE [dbo].[PlatoonShips] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [PlatoonPosition]     INT              NOT NULL,
    [Ship_Id]             UNIQUEIDENTIFIER NULL,
    [TerritoryPlatoon_Id] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.PlatoonShips] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.PlatoonShips_dbo.Ships_Ship_Id] FOREIGN KEY ([Ship_Id]) REFERENCES [dbo].[Ships] ([Id]),
    CONSTRAINT [FK_dbo.PlatoonShips_dbo.TerritoryPlatoons_TerritoryPlatoon_Id] FOREIGN KEY ([TerritoryPlatoon_Id]) REFERENCES [dbo].[TerritoryPlatoons] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Ship_Id]
    ON [dbo].[PlatoonShips]([Ship_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TerritoryPlatoon_Id]
    ON [dbo].[PlatoonShips]([TerritoryPlatoon_Id] ASC);

