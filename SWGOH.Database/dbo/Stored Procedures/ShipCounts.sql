
-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[ShipCounts]
(
	@Guild_Id uniqueidentifier
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON


Select distinct ms.Ship_Id
	   , (case s.Alignment when 1 then 'Lightside' else 'Darkside' end) as Alignment
	   , s.DisplayName as Name
	   , count(case ms.Stars when 1 then 1 else null end) as OneStarCount
	   , count(case ms.Stars when 2 then 1 else null end) as TwoStarCount
	   , count(case ms.Stars when 3 then 1 else null end) as ThreeStarCount
	   , count(case ms.Stars when 4 then 1 else null end) as FourStarCount
	   , count(case ms.Stars when 5 then 1 else null end) as FiveStarCount
	   , count(case ms.Stars when 6 then 1 else null end) as SixStarCount
	   , count(case ms.Stars when 7 then 1 else null end) as SevenStarCount
From MemberShips as ms
Left Join Members as m on m.Id = ms.Member_Id
left join Ships as s on s.Id = ms.Ship_Id
Where m.Guild_Id = @Guild_Id
group By ms.Ship_Id, s.DisplayName, s.Alignment
END
