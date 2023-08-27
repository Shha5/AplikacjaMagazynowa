CREATE PROCEDURE [dbo].[sp_OrderItems_Insert]
	@OrderId INT,
	@ProductCode NVARCHAR(50),
	@Quantity INT

AS
	BEGIN
		SET NOCOUNT ON

		DECLARE @ProductId INT
		SET @ProductId = (SELECT [Product].[Id] FROM [Product] WHERE [Product].[ProductCode] = @ProductCode)

		DECLARE @OrderDate DATETIME2
		SET @OrderDate = (SELECT [Order].[CreatedDate] FROM [Order] WHERE [Order].[Id] = @OrderId)

		INSERT INTO [OrderItems] ([OrderId], [ProductId], [Quantity], [ItemCompleted], [OrderDate], [LastModifiedDate])
		VALUES (@OrderId, (SELECT [Product].[Id] FROM [Product] WHERE [Product].[ProductCode] = @ProductCode), @Quantity, 0, @OrderDate, @OrderDate)

		UPDATE [Product]
		SET [Product].[QuantityInStock] = ([Product].[QuantityInStock] - @Quantity) WHERE [Product].[Id] = @ProductId
	END
