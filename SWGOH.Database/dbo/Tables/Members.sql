CREATE TABLE [dbo].[Members] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [Name]           NVARCHAR (255)   NULL,
    [UrlExt]         NVARCHAR (255)   NULL,
    [Guild_Id]       UNIQUEIDENTIFIER NOT NULL,
    [DisplayName]    NVARCHAR (255)   NULL,
    [ShipPower]      INT              DEFAULT ((0)) NOT NULL,
    [CharacterPower] INT              DEFAULT ((0)) NOT NULL,
    [DiscordId]      BIGINT           NULL,
    CONSTRAINT [PK_dbo.Members] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Members_dbo.Guilds_Guild_Id] FOREIGN KEY ([Guild_Id]) REFERENCES [dbo].[Guilds] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Guild_Id]
    ON [dbo].[Members]([Guild_Id] ASC);

