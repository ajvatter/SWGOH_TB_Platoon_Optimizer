declare @phaseGuid uniqueidentifier = 'a30c16d4-5dc2-4120-8931-badaddd8381b'
declare @requiredStars int = 6--(select RequiredStars from TerritoryBattlePhases where id = @phaseGuid);

CREATE TABLE #MyTempTable
(
NeedCount int,
HaveCount int,
DisplayName nvarchar(100),
CharId uniqueIdentifier,
GuildId uniqueIdentifier
)
insert into #MyTempTable
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
   select DisplayName, NeedCount, HaveCount
		  , (select top (NeedCount) m.DisplayName + ', ' 
		     From Members as m 
			 Left Join MemberCharacters as mc on mc.Member_Id = m.Id 
			 where Character_Id = CharId 
			 and m.Guild_Id = GuildId
			 and mc.Stars >= @requiredStars
			 order by stars 
			 For XML PATH ('')) AS AssignedMembers
   
   From #MyTempTable-- where NeedCount > HaveCount
   Order By NeedCount desc



   drop Table #MyTempTable
