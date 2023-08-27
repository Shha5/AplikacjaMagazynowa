CREATE PROCEDURE [dbo].[sp_Shipment_Product_AddProductShipment]
	@ProductId INT = NULL,
	@ProductCode NVARCHAR(50),
	@Quantity INT = 1
AS
IF EXISTS (SELECT 1 FROM [Product] WHERE @ProductCode = [Product].[ProductCode])
BEGIN 
	SET @ProductId = (SELECT [Product].[Id] FROM [Product] WHERE [Product].[ProductCode] = @ProductCode)
	
	UPDATE [Product]
	SET [QuantityInStock] = [QuantityInStock] + @Quantity, [LastModifiedDate] = GETUTCDATE()
	WHERE [Product].[ProductCode] = @ProductCode
	
	INSERT INTO [Shipment] ([ProductId], [Quantity])
	VALUES (@ProductId, @Quantity)
END