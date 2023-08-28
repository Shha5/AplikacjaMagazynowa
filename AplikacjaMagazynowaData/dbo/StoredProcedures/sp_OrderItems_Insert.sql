CREATE PROCEDURE [dbo].[sp_OrderItems_Insert]
	@OrderId INT,
	@ProductCode NVARCHAR(50),
	@Quantity INT

AS
BEGIN
	SET NOCOUNT ON
	DECLARE @OrderDate DATETIME2
	DECLARE @ProductId INT
	SET @ProductId = (SELECT [Product].[Id] FROM [Product] WHERE [Product].[ProductCode] = @ProductCode)
	SET @OrderDate = (SELECT [Order].[CreatedDate] FROM [Order] WHERE [Order].[Id] = @OrderId)

	INSERT INTO [OrderItems] ([OrderId], [ProductId], [Quantity], [ItemCompleted], [OrderDate], [LastModifiedDate])
	VALUES (@OrderId, @ProductId, @Quantity, 0, @OrderDate, @OrderDate)

	UPDATE [Product]
	SET [Product].[QuantityInStock] = ([Product].[QuantityInStock] - @Quantity) WHERE [Product].[Id] = @ProductId
END
