-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [sp_PlatoonCharacters]
(
    @phaseGuid uniqueidentifier = null
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

declare @requiredStars int = (select RequiredStars from TerritoryBattlePhases where id = @phaseGuid);

Select Count(pc.Character_Id) as [NeedCount]
         , (Select Count(*) 
		       from MemberCharacters 
			   left join Members as m on m.Id = MemberCharacters.Member_Id 
			   where Character_Id = pc.Character_Id 
			   and m.Guild_Id = tb.Guild_Id
			   and MemberCharacters.Stars >= @requiredStars
			) as HaveCount 
		 , c.DisplayName
		 , c.Id
		 , tb.Guild_Id
  From Characters as c
  left join PlatoonCharacters as pc on pc.Character_Id = c.Id
  left Join TerritoryPlatoons as tp on tp.Id = pc.TerritoryPlatoon_Id
  left join PhaseTerritories as pt on pt.Id = tp.PhaseTerritory_Id
  Left join TerritoryBattlePhases as tbp on tbp.Id = pt.TerritoryBattlePhase_Id
  left join TerritoryBattles as tb on tb.Id = tbp.TerritoryBattle_Id
   Where tbp.id = @phaseGuid
   and tp.IsClosed = 0
   Group By c.DisplayName, pc.Character_Id, tb.Guild_Id, c.Id
   order by [NeedCount] desc
END
