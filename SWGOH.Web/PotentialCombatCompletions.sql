declare @guildId uniqueidentifier = '0AF951A0-98E3-4060-BA9F-7AF4F4FA7B38';

Select Member_Id,
(Select count(Stars) from MemberCharacters as mc2 left join Characters as c on c.Id = mc2.Character_Id where Stars >= 2 and Member_Id = mc.Member_Id and c.Alignment = 1) as TwoStar,
(Select count(Stars) from MemberCharacters as mc2 left join Characters as c on c.Id = mc2.Character_Id where Stars >= 3 and Member_Id = mc.Member_Id and c.Alignment = 1) as ThreeStar,
(Select count(Stars) from MemberCharacters as mc2 left join Characters as c on c.Id = mc2.Character_Id where Stars >= 4 and Member_Id = mc.Member_Id and c.Alignment = 1) as FourStar,
(Select count(Stars) from MemberCharacters as mc2 left join Characters as c on c.Id = mc2.Character_Id where Stars >= 5 and Member_Id = mc.Member_Id and c.Alignment = 1) as FiveStar,
(Select count(Stars) from MemberCharacters as mc2 left join Characters as c on c.Id = mc2.Character_Id where Stars >= 6 and Member_Id = mc.Member_Id and c.Alignment = 1) as SixStar,
(Select count(Stars) from MemberCharacters as mc2 left join Characters as c on c.Id = mc2.Character_Id where Stars = 7 and Member_Id = mc.Member_Id and c.Alignment = 1) as SevenStar,
(Select Stars from MemberCharacters where Character_Id = '251567FE-0A0B-4FF8-AF67-069AE5B88103' and Member_Id = mc.Member_Id) as HRScout,
(Select Stars from MemberCharacters where Character_Id = '4F79CC86-31A4-438D-8F03-0C61A959FD30' and Member_Id = mc.Member_Id) as HRSoldier,
(Select Stars from MemberCharacters where Character_Id = '6A52F9B7-28D8-438D-96C8-9B166E48DECB' and Member_Id = mc.Member_Id) as CHS,
(Select Stars from MemberCharacters where Character_Id = 'D683B438-D04F-4462-A23A-A5E9D0160767' and Member_Id = mc.Member_Id) as ROLO,
(Select Stars from MemberCharacters where Character_Id = 'B07CD075-6657-489A-874A-EBCF19093D62' and Member_Id = mc.Member_Id) as CLS,
(Select count(stars) from MemberCharacters as mc2 left join CharacterClassifierCharacters as ccc on ccc.Character_Id = mc2.Character_Id where ccc.CharacterClassifier_Id = 'FFB1D4C6-C567-4CF1-B01D-7D46CAC36A32' and Member_Id = mc.Member_Id and mc2.Stars >= 3) as RogueOneCountAtThreeStars,
(Select count(stars) from MemberCharacters as mc2 left join CharacterClassifierCharacters as ccc on ccc.Character_Id = mc2.Character_Id where ccc.CharacterClassifier_Id = 'FFB1D4C6-C567-4CF1-B01D-7D46CAC36A32' and Member_Id = mc.Member_Id and mc2.Stars >= 7) as RogueOneCountAtSevenStars,
(Select count(stars) from MemberCharacters as mc2 left join CharacterClassifierCharacters as ccc on ccc.Character_Id = mc2.Character_Id where ccc.CharacterClassifier_Id = 'FC50C264-95B8-423E-86C7-4EBE6F32E25B' and Member_Id = mc.Member_Id and mc2.Stars >= 2) as PhoenixCountAtTwoStars,
(Select count(stars) from MemberCharacters as mc2 left join CharacterClassifierCharacters as ccc on ccc.Character_Id = mc2.Character_Id where ccc.CharacterClassifier_Id = 'FC50C264-95B8-423E-86C7-4EBE6F32E25B' and Member_Id = mc.Member_Id and mc2.Stars >= 6) as PhoenixCountAtSixStars--,
--(Select count(stars) from MemberCharacters as mc2 left join CharacterClassifierCharacters as ccc on ccc.Character_Id = mc2.Character_Id where ccc.CharacterClassifier_Id = '5196A86D-573D-4A07-9761-E4ACBA9AAD97' and Member_Id = mc.Member_Id and mc2.Stars >= 3) as RebelCountAtThreeStars,
--(Select count(stars) from MemberCharacters as mc2 left join CharacterClassifierCharacters as ccc on ccc.Character_Id = mc2.Character_Id where ccc.CharacterClassifier_Id = '5196A86D-573D-4A07-9761-E4ACBA9AAD97' and Member_Id = mc.Member_Id and mc2.Stars >= 4) as RebelCountAtFourStars,
--(Select count(stars) from MemberCharacters as mc2 left join CharacterClassifierCharacters as ccc on ccc.Character_Id = mc2.Character_Id where ccc.CharacterClassifier_Id = '5196A86D-573D-4A07-9761-E4ACBA9AAD97' and Member_Id = mc.Member_Id and mc2.Stars >= 5) as RebelCountAtFiveStars,
--(Select count(stars) from MemberCharacters as mc2 left join CharacterClassifierCharacters as ccc on ccc.Character_Id = mc2.Character_Id where ccc.CharacterClassifier_Id = '5196A86D-573D-4A07-9761-E4ACBA9AAD97' and Member_Id = mc.Member_Id and mc2.Stars >= 6) as RebelCountAtSixStars,
--(Select count(stars) from MemberCharacters as mc2 left join CharacterClassifierCharacters as ccc on ccc.Character_Id = mc2.Character_Id where ccc.CharacterClassifier_Id = '5196A86D-573D-4A07-9761-E4ACBA9AAD97' and Member_Id = mc.Member_Id and mc2.Stars >= 7) as RebelCountAtSevenStars
Into #MyTempTable 
from MemberCharacters as mc
left join Members as m on m.Id = mc.Member_Id
--left join Characters as c on c.Id = mc. Character_Id
Where m.Guild_Id = @guildId
Group By Member_Id

--select * from #MyTempTable

Select
  case when (PhoenixCountAtTwoStars < 1) THEN 'No' else 'Yes' END AS Phase1Special,
  case when (RogueOneCountAtThreeStars < 1) THEN 'No' else 'Yes' END AS Phase2Special,
  case when (HRSoldier <= 3) Then 'No' else 'Yes' END AS Phase2Hoth,
  case when (HRScout <= 4) Then 'No' else 'Yes' END AS Phase3Hoth,
  case when (HRSoldier >= 5 and CHS >= 5) Then 'Yes' else 'No' END AS Phase3Special,
  case when (HRSoldier <= 5) Then 'No' else 'Yes' END AS Phase4Hoth,
  case when (Rolo >= 5 and CHS >= 5) Then 'Yes' else 'No' END AS Phase4Special,
  case when (HRScout <= 6) Then 'No' else 'Yes' END AS Phase5Hoth,
  case when (CLS <= 6) Then 'No' else 'Yes' END AS Phase5Special,
  case when (PhoenixCountAtSixStars < 1) THEN 'No' else 'Yes' END AS Phase5Phoenix,
  case when (RogueOneCountAtSevenStars < 1) THEN 'No' else 'Yes' END AS Phase6RogueOne,
  case when (ROLO >= 7 and CHS >= 7) Then 'Yes' else 'No' END AS Phase6Special
into #MyTempTable2
From #MyTempTable

select Count(case Phase1Special when 'Yes' then 1 else null end) as 'Phase 1 Special',
	   Count(case Phase2Hoth when 'Yes' then 1 else null end) as 'Phase 2 Hoth Soldier',
	   Count(case Phase2Special when 'Yes' then 1 else null end) as 'Phase 2 Special',
	   Count(case Phase3Hoth when 'Yes' then 1 else null end) as 'Phase 3 Hoth Scout',
	   Count(case Phase3Special when 'Yes' then 1 else null end) as 'Phase 3 Special',
	   Count(case Phase4Hoth when 'Yes' then 1 else null end) as 'Phase 4 Hoth Soldier',
	   Count(case Phase4Special when 'Yes' then 1 else null end) as 'Phase 4 Special',
	   Count(case Phase5Hoth when 'Yes' then 1 else null end) as 'Phase 5 Hoth Scout',
	   Count(case Phase5Phoenix when 'Yes' then 1 else null end) as 'Phase 5 Phoenix',
	   Count(case Phase5Special when 'Yes' then 1 else null end) as 'Phase 5 Special',
	   Count(case Phase6RogueOne when 'Yes' then 1 else null end) as 'Phase 6 Rogue One',
	   Count(case Phase6Special when 'Yes' then 1 else null end) as 'Phase 6 Special'
From #MyTempTable2

drop table #MyTempTable
drop table #MyTempTable2