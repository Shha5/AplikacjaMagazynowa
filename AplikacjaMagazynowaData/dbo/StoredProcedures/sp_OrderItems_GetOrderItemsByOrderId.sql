CREATE PROCEDURE [dbo].[sp_OrderItems_GetOrderItemsByOrderId]
	@OrderId INT
	
AS
BEGIN 
	SELECT [OrderItems].[Id], [OrderItems].[ProductId], [OrderItems].[Quantity], [OrderItems].[ItemCompleted], [OrderItems].[LastModifiedDate]
	FROM [OrderItems]
	WHERE [OrderItems].[OrderId] = @OrderId
END
