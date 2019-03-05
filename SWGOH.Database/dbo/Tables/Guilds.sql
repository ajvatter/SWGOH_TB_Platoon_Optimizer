CREATE TABLE [dbo].[Guilds] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [Name]           NVARCHAR (255)   NULL,
    [UrlExt]         NVARCHAR (255)   NULL,
    [LastScrape]     DATETIME         DEFAULT ('1900-01-01T00:00:00.000') NOT NULL,
    [ShipPower]      INT              DEFAULT ((0)) NOT NULL,
    [CharacterPower] INT              DEFAULT ((0)) NOT NULL,
    [ServerId]       BIGINT           NULL,
    CONSTRAINT [PK_dbo.Guilds] PRIMARY KEY CLUSTERED ([Id] ASC)
);

