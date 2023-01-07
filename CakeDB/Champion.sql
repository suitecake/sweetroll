CREATE TABLE [dbo].[Champion]
(
	[Id] INT NOT NULL, 
    [InternalName] NVARCHAR(128) NOT NULL, 
    [DisplayName] NVARCHAR(128) NOT NULL,
    CONSTRAINT [PK_Champion] PRIMARY KEY ([Id]),
    CONSTRAINT [UC_Champion_InternalName] UNIQUE(InternalName)
)
