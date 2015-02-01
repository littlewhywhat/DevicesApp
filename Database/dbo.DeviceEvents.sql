CREATE TABLE [dbo].[DeviceEvents]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [DeviceId] INT NULL, 
    [Type] NVARCHAR(50) NULL, 
    [Date] DATETIME NULL
)
