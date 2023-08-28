CREATE PROCEDURE [dbo].[sp_OrderItems_GetOrderItemByOrderNumberAndProductCode]
	@OrderNumber NVARCHAR(50),
	@ProductCode NVARCHAR(50)
AS
BEGIN
	DECLARE @ProductId INT
	DECLARE @OrderId INT 
	SET @ProductId = (SELECT [Product].[Id] FROM [Product] WHERE [Product].[ProductCode] = @ProductCode)
	SET @OrderId = (SELECT [Order].[Id] FROM [Order] WHERE [Order].[OrderNumber] = @OrderNumber)
	
	SELECT [OrderItems].[Id], [OrderItems].[ProductId], [OrderItems].[OrderId], [OrderItems].[Quantity], [OrderItems].[ItemCompleted]
	FROM [OrderItems]
	WHERE [OrderItems].[ProductId] = @ProductId AND [OrderItems].[OrderId] = @OrderId
END
