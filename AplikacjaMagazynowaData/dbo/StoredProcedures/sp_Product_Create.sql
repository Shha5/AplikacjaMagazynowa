CREATE PROCEDURE [dbo].[sp_Product_Create]
	@ProductCode NVARCHAR(50),
	@ProductName NVARCHAR(100), 
	@QuantityInStock INT = 1
AS
	IF NOT EXISTS (SELECT 1 FROM [dbo].[Product] WHERE ProductCode = @ProductCode)
	BEGIN
		SET NOCOUNT ON
		INSERT INTO [dbo].[Product]([ProductCode], [ProductName], [QuantityInStock])
		VALUES (@ProductCode, @ProductName, @QuantityInStock)
	END
