CREATE TABLE [dbo].[Ships] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (255)   NULL,
    [DisplayName] NVARCHAR (255)   NULL,
    [UrlExt]      NVARCHAR (255)   NULL,
    [Alignment]   INT              NOT NULL,
    [BaseId]      NVARCHAR (MAX)   NULL,
    [MaxPower]    INT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.Ships] PRIMARY KEY CLUSTERED ([Id] ASC)
);

