CREATE TABLE Member(
   allyCode         INTEGER  NOT NULL PRIMARY KEY 
  ,name             VARCHAR(11) NOT NULL
  ,level            INTEGER  NOT NULL
  ,guildMemberLevel INTEGER  NOT NULL
  ,guildId       VARCHAR(11) NOT NULL
);