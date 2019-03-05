CREATE TABLE [dbo].[MemberCharacters] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Level]        INT              NOT NULL,
    [Gear]         INT              NOT NULL,
    [Stars]        INT              NULL,
    [Power]        INT              NOT NULL,
    [Character_Id] UNIQUEIDENTIFIER NOT NULL,
    [Member_Id]    UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.MemberCharacters] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.MemberCharacters_dbo.Characters_Character_Id] FOREIGN KEY ([Character_Id]) REFERENCES [dbo].[Characters] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.MemberCharacters_dbo.Members_Member_Id] FOREIGN KEY ([Member_Id]) REFERENCES [dbo].[Members] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Character_Id]
    ON [dbo].[MemberCharacters]([Character_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Member_Id]
    ON [dbo].[MemberCharacters]([Member_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Stars]
    ON [dbo].[MemberCharacters]([Stars] ASC);


GO
CREATE NONCLUSTERED INDEX [nci_wi_MemberCharacters_462397A472056AEE770CCA8F171C4512]
    ON [dbo].[MemberCharacters]([Member_Id] ASC, [Character_Id] ASC, [Stars] ASC)
    INCLUDE([Gear], [Level], [Power]);

