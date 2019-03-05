CREATE TABLE [dbo].[PhaseTerritories] (
    [Id]                      UNIQUEIDENTIFIER NOT NULL,
    [TotalPointsEarned]       INT              NULL,
    [PhaseLocation]           NVARCHAR (MAX)   NULL,
    [TerritoryBattlePhase_Id] UNIQUEIDENTIFIER NULL,
    [PointsEarned]            INT              NULL,
    CONSTRAINT [PK_dbo.PhaseTerritories] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.PhaseTerritories_dbo.TerritoryBattlePhases_TerritoryBattlePhase_Id] FOREIGN KEY ([TerritoryBattlePhase_Id]) REFERENCES [dbo].[TerritoryBattlePhases] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_TerritoryBattlePhase_Id]
    ON [dbo].[PhaseTerritories]([TerritoryBattlePhase_Id] ASC);

