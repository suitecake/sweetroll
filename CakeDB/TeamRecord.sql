CREATE TABLE [dbo].[TeamRecord]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [Name] NVARCHAR(1024) NOT NULL, 
    [CreateTime] DATETIME2 NOT NULL, 
    [Value] DECIMAL(15, 5) NOT NULL, 
    [RecordTime] DATETIME2 NOT NULL, 
    CONSTRAINT [PK_TeamRecord] PRIMARY KEY ([Id]),
    CONSTRAINT [UC_TeamRecord_Name] UNIQUE(Name)
)
