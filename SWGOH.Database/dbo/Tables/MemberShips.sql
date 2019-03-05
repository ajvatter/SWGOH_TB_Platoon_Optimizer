CREATE TABLE [dbo].[MemberShips] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Level]     INT              NOT NULL,
    [Stars]     INT              NULL,
    [Power]     NVARCHAR (MAX)   NULL,
    [Ship_Id]   UNIQUEIDENTIFIER NOT NULL,
    [Member_Id] UNIQUEIDENTIFIER NOT NULL,
    [ShipPower] INT NULL, 
    CONSTRAINT [PK_dbo.MemberShips] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.MemberShips_dbo.Members_Member_Id] FOREIGN KEY ([Member_Id]) REFERENCES [dbo].[Members] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.MemberShips_dbo.Ships_Ship_Id] FOREIGN KEY ([Ship_Id]) REFERENCES [dbo].[Ships] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Ship_Id]
    ON [dbo].[MemberShips]([Ship_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Member_Id]
    ON [dbo].[MemberShips]([Member_Id] ASC);

