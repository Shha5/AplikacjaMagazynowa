CREATE PROCEDURE [dbo].[sp_Order_GetOrderByOrderNumber]
	@OrderNumber NVARCHAR(50)

AS
	BEGIN
		SELECT [Order].[Id], [Order].[OrderNumber], [Order].[OrderSignature], [Order].[CreatedDate], [Order].[LastModifiedDate], [Order].[IsComplete],
			   [OrderItems].[ProductId], [OrderItems].[Quantity], [OrderItems].[ItemCompleted]
		FROM [Order]
		JOIN [OrderItems] ON [Order].[Id] = [OrderItems].[OrderId]
		WHERE [Order].[OrderNumber] = @OrderNumber	
END