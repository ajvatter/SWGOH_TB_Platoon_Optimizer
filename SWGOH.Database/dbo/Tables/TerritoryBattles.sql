CREATE TABLE [dbo].[TerritoryBattles] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [Guild_Id]   UNIQUEIDENTIFIER NOT NULL,
    [StartDate]  DATETIME         NOT NULL,
    [TotalStars] INT              NULL,
    [IsActive]   BIT              NOT NULL,
    CONSTRAINT [PK_dbo.TerritoryBattles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.TerritoryBattles_dbo.Guilds_Guild_Id] FOREIGN KEY ([Guild_Id]) REFERENCES [dbo].[Guilds] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Guild_Id]
    ON [dbo].[TerritoryBattles]([Guild_Id] ASC);

