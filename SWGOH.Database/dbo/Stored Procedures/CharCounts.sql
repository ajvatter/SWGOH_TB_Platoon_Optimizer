-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[CharCounts]
(
	@Guild_Id uniqueidentifier
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON


Select distinct mc.Character_Id	   
	   , (case c.Alignment when 1 then 'Lightside' else 'Darkside' end) as Alignment
	   , c.DisplayName as Name
	   , count(case mc.Stars when 1 then 1 else null end) as OneStarCount
	   , count(case mc.Stars when 2 then 1 else null end) as TwoStarCount
	   , count(case mc.Stars when 3 then 1 else null end) as ThreeStarCount
	   , count(case mc.Stars when 4 then 1 else null end) as FourStarCount
	   , count(case mc.Stars when 5 then 1 else null end) as FiveStarCount
	   , count(case mc.Stars when 6 then 1 else null end) as SixStarCount
	   , count(case mc.Stars when 7 then 1 else null end) as SevenStarCount
From MemberCharacters as mc
Left Join Members as m on m.Id = mc.Member_Id
left join Characters as c on c.Id = mc.Character_Id
Where m.Guild_Id = @Guild_Id
group By mc.Character_Id, c.DisplayName, c.Alignment
END
