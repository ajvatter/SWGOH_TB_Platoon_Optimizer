CREATE TABLE [dbo].[GuildEventSchedules] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [StartDate]     DATETIME         NOT NULL,
    [GuildEvent_Id] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.GuildEventSchedules] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.GuildEventSchedules_dbo.GuildEvents_GuildEvent_Id] FOREIGN KEY ([GuildEvent_Id]) REFERENCES [dbo].[GuildEvents] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_GuildEvent_Id]
    ON [dbo].[GuildEventSchedules]([GuildEvent_Id] ASC);

