CREATE TABLE [dbo].[Friend]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [Name] NVARCHAR(64) NOT NULL, 
    CONSTRAINT [PK_Friend] PRIMARY KEY ([Id]),
    CONSTRAINT [UC_Friend_Name] UNIQUE(Name)
)
