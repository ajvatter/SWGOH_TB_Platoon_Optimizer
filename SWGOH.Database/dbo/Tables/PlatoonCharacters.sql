CREATE TABLE [dbo].[PlatoonCharacters] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [Character_Id]        UNIQUEIDENTIFIER NULL,
    [TerritoryPlatoon_Id] UNIQUEIDENTIFIER NULL,
    [PlatoonPosition]     INT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.PlatoonCharacters] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.PlatoonCharacters_dbo.Characters_Character_Id] FOREIGN KEY ([Character_Id]) REFERENCES [dbo].[Characters] ([Id]),
    CONSTRAINT [FK_dbo.PlatoonCharacters_dbo.TerritoryPlatoons_TerritoryPlatoon_Id] FOREIGN KEY ([TerritoryPlatoon_Id]) REFERENCES [dbo].[TerritoryPlatoons] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Character_Id]
    ON [dbo].[PlatoonCharacters]([Character_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TerritoryPlatoon_Id]
    ON [dbo].[PlatoonCharacters]([TerritoryPlatoon_Id] ASC);

