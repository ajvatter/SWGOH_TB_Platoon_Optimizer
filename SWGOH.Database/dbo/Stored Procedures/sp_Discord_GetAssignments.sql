-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[sp_Discord_GetAssignments]
(
    -- Add the parameters for the stored procedure here
    @memberId Uniqueidentifier
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    SELECT ptc.PhaseLocation + ' ' + CAST(tpc.PlatoonNumber as VARCHAR) + ' - ' + c.DisplayName as CharAssignment
	  ,pts.PhaseLocation + ' ' + CAST(tps.PlatoonNumber as VARCHAR) + ' - ' + S.DisplayName as ShipAssignment	  
  FROM [dbo].[PhaseReports] as pr
  Left Join MemberCharacters as mc on mc.Id = pr.MemberCharacter_Id
  Left Join PlatoonCharacters as pc on pc.Id = pr.PlatoonCharacter_Id
  Left Join MemberShips as ms on ms.Id = pr.MemberShip_Id
  Left Join PlatoonShips as ps on ps.Id = pr.PlatoonShip_Id
  Left Join TerritoryPlatoons as tpc on tpc.Id = pc.TerritoryPlatoon_Id
  Left Join PhaseTerritories as ptc on ptc.Id = tpc.PhaseTerritory_Id
  LEFT Join TerritoryPlatoons as tps on tps.Id = ps.TerritoryPlatoon_Id
  LEFT Join PhaseTerritories as pts on pts.Id = tps.PhaseTerritory_Id
  left join Characters as c on c.Id = pc.Character_Id
  left join Ships as s on s.Id = ps.Ship_Id

  Where  mc.Member_Id = @memberId
  OR ms.Member_Id = @memberId
  Order By ptc.PhaseLocation, tpc.PlatoonNumber, tps.PlatoonNumber
END
