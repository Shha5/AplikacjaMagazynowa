CREATE PROCEDURE [dbo].[sp_OrderItems_MarkComplete]
	@OrderNumber NVARCHAR(50),
	@ProductCode NVARCHAR(50)
AS
BEGIN
	DECLARE @ProductId INT
	DECLARE @OrderId INT
	SET @ProductId = (SELECT [Product].[Id] FROM [Product] WHERE [Product].[ProductCode] = @ProductCode)
	SET @OrderId = (SELECT [Order].[Id] FROM [Order] WHERE [Order].[OrderNumber] = @OrderNumber)

	UPDATE [OrderItems]
	SET [OrderItems].[ItemCompleted] = 1, [OrderItems].[LastModifiedDate] = GETUTCDATE()
	WHERE [ProductId] = @ProductId AND [OrderId] = @OrderId

	UPDATE [Order]
	SET [Order].[LastModifiedDate] = GETUTCDATE()
	WHERE [Order].[Id] = @OrderId
END
