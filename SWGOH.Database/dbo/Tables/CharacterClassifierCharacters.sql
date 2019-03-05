CREATE TABLE [dbo].[CharacterClassifierCharacters] (
    [CharacterClassifier_Id] UNIQUEIDENTIFIER NOT NULL,
    [Character_Id]           UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.CharacterClassifierCharacters] PRIMARY KEY CLUSTERED ([CharacterClassifier_Id] ASC, [Character_Id] ASC),
    CONSTRAINT [FK_dbo.CharacterClassifierCharacters_dbo.CharacterClassifiers_CharacterClassifier_Id] FOREIGN KEY ([CharacterClassifier_Id]) REFERENCES [dbo].[CharacterClassifiers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.CharacterClassifierCharacters_dbo.Characters_Character_Id] FOREIGN KEY ([Character_Id]) REFERENCES [dbo].[Characters] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_CharacterClassifier_Id]
    ON [dbo].[CharacterClassifierCharacters]([CharacterClassifier_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Character_Id]
    ON [dbo].[CharacterClassifierCharacters]([Character_Id] ASC);

