
-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[CharCountsForAssignments]
(		
	@Phase_Id uniqueidentifier
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON
	declare @Guild_Id uniqueidentifier = (Select tb.Guild_Id From TerritoryBattlePhases as tbp left join TerritoryBattles as tb on tb.Id = tbp.TerritoryBattle_Id where tbp.Id = @Phase_Id)--'0AF951A0-98E3-4060-BA9F-7AF4F4FA7B38'
	declare @RequiredStars int = (Select RequiredStars From TerritoryBattlePhases where Id = @Phase_Id)

declare @MyTempTable table 
	(CharacterId uniqueidentifier, 
	Name varchar(50),
	Available int)

insert into @MyTempTable select distinct mc.Character_Id	   	   
	   , c.DisplayName as Name
	   , count(mc.Stars) As Available
From MemberCharacters as mc
Left Join Members as m on m.Id = mc.Member_Id
left join Characters as c on c.Id = mc.Character_Id
Where m.Guild_Id = @Guild_Id
and mc.Stars >= @requiredStars
and c.Alignment = 1
group By mc.Character_Id, c.DisplayName, c.Alignment
order by Name

select * from @MyTempTable
where Available <= 5

END
