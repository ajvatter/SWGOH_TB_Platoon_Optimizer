CREATE TABLE [dbo].[CharacterClassifiers] (
    [Id]   UNIQUEIDENTIFIER CONSTRAINT [DF_CharacterClassifiers_Id] DEFAULT (newid()) NOT NULL,
    [Name] NVARCHAR (255)   NULL,
    CONSTRAINT [PK_dbo.CharacterClassifiers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

