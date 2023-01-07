CREATE TABLE [dbo].[TeamRecordMember]
(
	[TeamRecordId] INT NOT NULL, 
    [MemberId] INT NOT NULL,
	[ChampionId] INT NOT NULL, 
    CONSTRAINT [PK_TeamRecordMember] PRIMARY KEY ([TeamRecordId], [MemberId]),
    CONSTRAINT [FK_TeamRecordMember_TeamRecord] FOREIGN KEY ([TeamRecordId]) REFERENCES TeamRecord(Id),
	CONSTRAINT [FK_TeamRecordMember_Member] FOREIGN KEY ([MemberId]) REFERENCES Member(Id),
	CONSTRAINT [FK_TeamRecordMember_Champion] FOREIGN KEY ([ChampionId]) REFERENCES Champion(Id)
)
