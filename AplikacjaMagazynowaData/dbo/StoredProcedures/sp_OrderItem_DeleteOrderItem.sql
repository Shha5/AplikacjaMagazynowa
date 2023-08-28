CREATE PROCEDURE [dbo].[sp_OrderItem_DeleteOrderItem]
	@OrderId INT,
	@ProductId INT,
	@Id INT,
	@Quantity INT
AS
BEGIN
	
	UPDATE [Product]
	SET [Product].[QuantityInStock] = ([Product].[QuantityInStock] + @Quantity)
	WHERE [Product].[Id] = @ProductId

	UPDATE [Order]
	SET [Order].[LastModifiedDate] = GETUTCDATE()
	WHERE [Order].[Id] = @OrderId

	DELETE FROM [OrderItems] WHERE [OrderItems].[Id] = @Id

END
