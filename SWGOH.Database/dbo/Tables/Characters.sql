CREATE TABLE [dbo].[Characters] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (255)   NULL,
    [UrlExt]      NVARCHAR (255)   NULL,
    [DisplayName] NVARCHAR (255)   NULL,
    [Alignment]   INT              DEFAULT ((0)) NOT NULL,
    [BaseId]      NVARCHAR (MAX)   NULL,
    [MaxPower]    INT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.Characters] PRIMARY KEY CLUSTERED ([Id] ASC)
);

