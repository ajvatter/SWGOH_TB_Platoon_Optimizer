-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE sp_DiscordClosures
(
    -- Add the parameters for the stored procedure here
    @tbp_id UniqueIdentifier
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
   Select pt.PhaseLocation
	   , tp.PlatoonNumber 
from TerritoryPlatoons tp
left join PhaseTerritories as pt on pt.Id = tp.PhaseTerritory_Id
WHERE pt.TerritoryBattlePhase_Id = @tbp_id
And IsClosed = 1
order by PhaseLocation, tp.PlatoonNumber
END
