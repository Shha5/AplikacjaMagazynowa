CREATE PROCEDURE [dbo].[sp_Order_Delete]
	@OrderId INT 
AS
BEGIN
	DELETE FROM [Order] WHERE [Order].[Id] = @OrderId
	DELETE FROM [OrderItems] WHERE [OrderItems].[OrderId] = @OrderId
END
