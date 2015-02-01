CREATE TABLE [dbo].[Logging]
(
	[ItemId] INT, 
    [Timestamp] TIMESTAMP NULL, 
    [TableName] VARCHAR(50) NULL, 
    [ActionType] VARCHAR(50) NULL
)
