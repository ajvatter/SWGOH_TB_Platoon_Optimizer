
-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[ShipCountsForAssignments]
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
	(ShipId uniqueidentifier, 
	Name varchar(50),
	Available int)

insert into @MyTempTable select distinct ms.Ship_Id	   	   
	   , s.DisplayName as Name
	   , count(ms.Stars) As Available
From MemberShips as ms
Left Join Members as m on m.Id = ms.Member_Id
left join Ships as s on s.Id = ms.Ship_Id
Where m.Guild_Id = @Guild_Id
and ms.Stars >= @requiredStars
group By ms.Ship_Id, s.DisplayName, s.Alignment
order by Name

select * from @MyTempTable
where Available <= 5

END
