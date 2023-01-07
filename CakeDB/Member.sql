CREATE TABLE [dbo].[Member]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [FriendId] INT NOT NULL, 
    [SummonerName] NVARCHAR(256) NOT NULL, 
    [PUUID] NVARCHAR(128) NOT NULL, 
    [LastProcessedGameTime] BIGINT NOT NULL, 
    CONSTRAINT [PK_Member] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Member_Friend] FOREIGN KEY ([FriendId]) REFERENCES Friend(Id),
    CONSTRAINT [UC_Member_SummonerName] UNIQUE(SummonerName),
    CONSTRAINT [UC_Member_PUUID] UNIQUE(PUUID)
)
