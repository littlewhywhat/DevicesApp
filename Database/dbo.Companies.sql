CREATE TABLE [dbo].[Companies]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [ParentId] INT NULL, 
    [Info] NVARCHAR(50) NULL
)
