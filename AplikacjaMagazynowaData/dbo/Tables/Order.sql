CREATE TABLE [dbo].[Order]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[OrderNumber] NVARCHAR(50) NOT NULL,
	[OrderSignature] NVARCHAR(50) NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(),
	[LastModifiedDate] DATETIME2 NOT NULL DEFAULT getutcdate(),
	[IsComplete] BIT NOT NULL DEFAULT 0
)
