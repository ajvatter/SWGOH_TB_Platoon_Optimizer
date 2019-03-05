CREATE TABLE [dbo].[TerritoryBattlePhases] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [RequiredStars]      INT              NOT NULL,
    [HasSecondTerritory] BIT              NOT NULL,
    [Phase]              INT              DEFAULT ((0)) NOT NULL,
    [TerritoryBattle_Id] UNIQUEIDENTIFIER NULL,
    [HasThirdTerritory]  BIT              DEFAULT ((0)) NOT NULL,
    [RefreshReport]      BIT              DEFAULT ((0)) NOT NULL,
    [Alignment] INT NULL, 
    CONSTRAINT [PK_dbo.TerritoryBattlePhases] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.TerritoryBattlePhases_dbo.TerritoryBattles_TerritoryBattle_Id] FOREIGN KEY ([TerritoryBattle_Id]) REFERENCES [dbo].[TerritoryBattles] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_TerritoryBattle_Id]
    ON [dbo].[TerritoryBattlePhases]([TerritoryBattle_Id] ASC);

