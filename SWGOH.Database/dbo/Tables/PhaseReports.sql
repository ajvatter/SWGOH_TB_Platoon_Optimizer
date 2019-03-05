CREATE TABLE [dbo].[PhaseReports] (
    [Id]                      UNIQUEIDENTIFIER NOT NULL,
    [TerritoryBattlePhase_Id] UNIQUEIDENTIFIER NOT NULL,
    [MemberCharacter_Id]      UNIQUEIDENTIFIER NULL,
    [MemberShip_Id]           UNIQUEIDENTIFIER NULL,
    [PlatoonCharacter_Id]     UNIQUEIDENTIFIER NULL,
    [PlatoonShip_Id]          UNIQUEIDENTIFIER NULL,
    [GuildId]                 UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_dbo.PhaseReports] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.PhaseReports_dbo.Guilds_GuildId] FOREIGN KEY ([GuildId]) REFERENCES [dbo].[Guilds] ([Id]),
    CONSTRAINT [FK_dbo.PhaseReports_dbo.MemberCharacters_MemberCharacter_Id] FOREIGN KEY ([MemberCharacter_Id]) REFERENCES [dbo].[MemberCharacters] ([Id]),
    CONSTRAINT [FK_dbo.PhaseReports_dbo.MemberShips_MemberShip_Id] FOREIGN KEY ([MemberShip_Id]) REFERENCES [dbo].[MemberShips] ([Id]),
    CONSTRAINT [FK_dbo.PhaseReports_dbo.PlatoonCharacters_PlatoonCharacter_Id] FOREIGN KEY ([PlatoonCharacter_Id]) REFERENCES [dbo].[PlatoonCharacters] ([Id]),
    CONSTRAINT [FK_dbo.PhaseReports_dbo.PlatoonShips_PlatoonShip_Id] FOREIGN KEY ([PlatoonShip_Id]) REFERENCES [dbo].[PlatoonShips] ([Id]),
    CONSTRAINT [FK_dbo.PhaseReports_dbo.TerritoryBattlePhases_TerritoryBattlePhase_Id] FOREIGN KEY ([TerritoryBattlePhase_Id]) REFERENCES [dbo].[TerritoryBattlePhases] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_TerritoryBattlePhase_Id]
    ON [dbo].[PhaseReports]([TerritoryBattlePhase_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MemberCharacter_Id]
    ON [dbo].[PhaseReports]([MemberCharacter_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PlatoonCharacter_Id]
    ON [dbo].[PhaseReports]([PlatoonCharacter_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PlatoonShip_Id]
    ON [dbo].[PhaseReports]([PlatoonShip_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MemberShip_Id]
    ON [dbo].[PhaseReports]([MemberShip_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_GuildId]
    ON [dbo].[PhaseReports]([GuildId] ASC);

