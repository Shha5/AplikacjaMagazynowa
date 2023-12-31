﻿CREATE TABLE [dbo].[OrderItems]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[OrderId] INT NOT NULL,
	[ProductId] INT NOT NULL, 
	[Quantity] INT NOT NULL DEFAULT 1,
	[ItemCompleted] BIT NOT NULL DEFAULT 0,
	[OrderDate] DATETIME2 NOT NULL,
	[LastModifiedDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    CONSTRAINT [FK_OrderItems_Order] FOREIGN KEY ([OrderId]) REFERENCES [Order]([Id]), 
    CONSTRAINT [FK_OrderItems_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id])
)
