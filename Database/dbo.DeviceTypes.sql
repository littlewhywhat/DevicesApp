CREATE TABLE [dbo].[DeviceTypes]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [ParentId] INT NULL, 
    [IsMarker] BIT NULL, 
    [FullName] NVARCHAR(50) NULL, 
    [IVUK] NVARCHAR(50) NULL
)
