/****** Script for SelectTopNRows command from SSMS  ******/
--SELECT *  FROM [dbo].[AspNetUsers]  where email  = 'jclockwork13@hotmail.com'
--  select *from guilds where id = 'AD451DEB-4B16-49BB-8658-E3D1435CC7BB'


--Delete MemberCharacters from MemberCharacters as mc left join members as m on m.id = mc.Member_Id where m.Guild_Id = 'AD451DEB-4B16-49BB-8658-E3D1435CC7BB'
--Delete MemberShips from MemberShips as ms left join members as m on m.id = ms.Member_Id where m.Guild_Id = 'AD451DEB-4B16-49BB-8658-E3D1435CC7BB'
--Delete members from Members where Guild_Id = 'AD451DEB-4B16-49BB-8658-E3D1435CC7BB'

Update AspNetUsers Set Guild_Id = '0AF951A0-98E3-4060-BA9F-7AF4F4FA7B38' Where email = 'ajvatter@gmail.com'