/****** Script for SelectTopNRows command from SSMS  ******/
SELECT [Level]
      ,[Gear]
      ,[Stars]
      ,[Power]
      ,[Character_Id]
      ,[Member_Id]
  FROM [dbo].[MemberCharacters] as mc
  Left Join Members as m on m.Id = mc.Member_Id
  left join characters as c on c.id = mc.Id
  Where m.Guild_Id = '0AF951A0-98E3-4060-BA9F-7AF4F4FA7B38'
  and power = 0
