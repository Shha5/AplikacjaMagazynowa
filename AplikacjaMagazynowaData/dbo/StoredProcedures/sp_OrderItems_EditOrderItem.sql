CREATE PROCEDURE [dbo].[sp_OrderItems_EditOrderItem]
	@Id INT,
	@ProductId INT,
	@OrderId INT,
	@NewQuantity INT,
	@QuantityDifference INT
AS
BEGIN
	UPDATE [OrderItems]
	SET [Quantity] = @NewQuantity, [LastModifiedDate] = GETUTCDATE()
	WHERE [OrderItems].[Id] = @Id

	UPDATE [Product]
	SET [Product].[QuantityInStock] = ([Product].[QuantityInStock] + @QuantityDifference)
	WHERE [Product].[Id] = @ProductId

	UPDATE [Order]
	SET [Order].[LastModifiedDate] = GETUTCDATE()
	WHERE [Order].[Id] = @OrderId
END
