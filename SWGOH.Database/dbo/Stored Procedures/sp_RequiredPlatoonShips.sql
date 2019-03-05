
-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[sp_RequiredPlatoonShips]
(
    @phaseGuid uniqueidentifier = null
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

  --Select Count(pc.Character_Id) as [NeedCount]
		-- , c.Id
  --From Characters as c
  --left join PlatoonCharacters as pc on pc.Character_Id = c.Id
  --left Join TerritoryPlatoons as tp on tp.Id = pc.TerritoryPlatoon_Id
  --left join PhaseTerritories as pt on pt.Id = tp.PhaseTerritory_Id
  -- Where pt.TerritoryBattlePhase_Id = @phaseGuid
  -- and tp.IsClosed = 0
  -- Group By c.Id

  Select ps.Id as PlatoonShip_Id
		   , ps.Ship_Id
		   , @phaseGuid as TerritoryBattlePhase_Id
  From PlatoonShips as ps
  left Join TerritoryPlatoons as tp on tp.Id = ps.TerritoryPlatoon_Id
  Left join PhaseTerritories as pt on pt.Id = tp.PhaseTerritory_Id
  Where pt.TerritoryBattlePhase_Id = @phaseGuid
  and tp.IsClosed = 0
  order by ps.Ship_Id

END
