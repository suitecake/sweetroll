CREATE TABLE [dbo].[Pentakill]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
	[MemberId] INT NOT NULL,
    [ChampionId] INT NOT NULL,
    [Time] DATETIME2 NOT NULL,
    CONSTRAINT [PK_Pentakill] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Pentakill_Member] FOREIGN KEY ([MemberId]) REFERENCES Member(Id),
    CONSTRAINT [FK_Pentakill_Champion] FOREIGN KEY ([ChampionId]) REFERENCES Champion(Id)
)
