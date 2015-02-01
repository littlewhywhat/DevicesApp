CREATE TRIGGER [StorageLimit]
	ON [dbo].[Logging]
	FOR INSERT
	AS
	BEGIN
		SELECT * FROM [dbo].Logging
		If @@ROWCOUNT > 5 
			BEGIN
				DELETE TOP(5) FROM [dbo].Logging 
			END
	END
