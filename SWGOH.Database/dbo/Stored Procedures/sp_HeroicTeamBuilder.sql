-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[sp_HeroicTeamBuilder]
(
    -- Add the parameters for the stored procedure here
	@characterOneId uniqueidentifier,
	@characterTwoId uniqueidentifier,
	@characterThreeId uniqueidentifier,
	@characterFourId uniqueidentifier,
	@characterFiveId uniqueidentifier,
	@guildId uniqueidentifier
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
    
Select DisplayName,
	   ISNULL(a.Stars, 0) Stars,
	   ISNULL(a.Gear, 0) Gear,
	   ISNULL(a.Level, 0) Level,
	   ISNULL(a.Power, 0) Power,
	   ISNULL(b.Stars, 0) Stars1,
	   ISNULL(b.Gear, 0) Gear1,
	   ISNULL(b.Level, 0) Level1,
	   ISNULL(b.Power, 0) Power1,
	   ISNULL(c.Stars, 0) Stars2,
	   ISNULL(c.Gear, 0) Gear2,
	   ISNULL(c.Level, 0) Level2,
	   ISNULL(c.Power, 0) Power2,
	   ISNULL(d.Stars, 0) Stars3,
	   ISNULL(d.Gear, 0) Gear3,
	   ISNULL(d.Level, 0) Level3,
	   ISNULL(d.Power, 0) Power3,
	   ISNULL(e.Stars, 0) Stars4,
	   ISNULL(e.Gear, 0) Gear4,
	   ISNULL(e.Level, 0) Level4,
	   ISNULL(e.Power, 0) Power4
From Members as m
left join (SElect Stars, Gear, Level, Power, Member_Id From MemberCharacters where Character_Id = @characterOneId) as a on a.Member_Id = m.Id
left join (SElect Stars, Gear, Level, Power, Member_Id From MemberCharacters where Character_Id = @characterTwoId) as b on b.Member_Id = m.Id
left join (SElect Stars, Gear, Level, Power, Member_Id From MemberCharacters where Character_Id = @characterThreeId) as c on c.Member_Id = m.Id
left join (SElect Stars, Gear, Level, Power, Member_Id From MemberCharacters where Character_Id = @characterFourId) as d on d.Member_Id = m.Id
left join (SElect Stars, Gear, Level, Power, Member_Id From MemberCharacters where Character_Id = @characterFiveId) as e on e.Member_Id = m.Id
Where Guild_Id = @guildId
END
