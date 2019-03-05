CREATE TABLE [dbo].[GuildEvents] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Name]      NVARCHAR (255)   NULL,
    [Type]      NVARCHAR (255)   NULL,
    [Alignment] INT              NOT NULL,
    CONSTRAINT [PK_dbo.GuildEvents] PRIMARY KEY CLUSTERED ([Id] ASC)
);

