CREATE TABLE [dbo].[Devices]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [ParentId] INT NULL, 
    [CompanyId] INT NULL, 
    [TypeId] INT NULL
)
