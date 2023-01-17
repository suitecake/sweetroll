CREATE TABLE [dbo].[SoloRecord]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [Name] NVARCHAR(256) NOT NULL,
    [CreateTime] DATETIME2 NOT NULL,
    [MemberId] INT NULL, 
    [Value] DECIMAL(15, 5) NULL, 
    [RecordTime] DATETIME2 NULL, 
    [ChampionId] INT NULL,
    CONSTRAINT [PK_SoloRecord] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SoloRecord_Member] FOREIGN KEY (MemberId) REFERENCES Member(Id),
    CONSTRAINT [FK_SoloRecord_Champion] FOREIGN KEY (ChampionId) REFERENCES Champion(Id),
    CONSTRAINT [UC_SoloRecord_Name] UNIQUE(Name)
)
