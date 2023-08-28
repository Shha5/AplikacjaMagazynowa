CREATE PROCEDURE [dbo].[sp_Product_GetProductDetailsById]
	@ProductId INT

AS
BEGIN
	SELECT [Product].[ProductCode], [Product].[ProductName], [Product].[QuantityInStock]
	FROM [Product]
	WHERE [Product].[Id] = @ProductId
END
