CREATE PROCEDURE [dbo].[sp_Product_Insert]
	@ProductCode NVARCHAR(50),
	@ProductName NVARCHAR(100), 
	@QuantityInStock INT

AS
IF NOT EXISTS (SELECT 1 FROM [dbo].[Product] WHERE ProductCode = @ProductCode)
BEGIN
	INSERT INTO [dbo].[Product]([ProductCode], [ProductName], [QuantityInStock])
	VALUES (@ProductCode, @ProductName, @QuantityInStock)
END