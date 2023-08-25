CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProductCode] NVARCHAR(50) NOT NULL,
	[ProductName] NVARCHAR(100) NOT NULL,
	[QuantityInStock] INT NOT NULL DEFAULT 1,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(),
	[LastModifiedDate] DATETIME2 NOT NULL DEFAULT getutcdate()
)
