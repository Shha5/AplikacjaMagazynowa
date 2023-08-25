CREATE TABLE [dbo].[Order]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[OrderNumber] NVARCHAR(50) NOT NULL,
	[OrderSignature] NVARCHAR(50) NOT NULL,
	[AddedDate] DATETIME2 NOT NULL DEFAULT getutcdate(),
	[LastModifiedDate] DATETIME2 NOT NULL DEFAULT getutcdate()
)
