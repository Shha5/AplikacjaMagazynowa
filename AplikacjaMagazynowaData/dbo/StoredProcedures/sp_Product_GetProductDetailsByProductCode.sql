CREATE PROCEDURE [dbo].[sp_Product_GetProductDetailsByProductCode]
	@ProductCode NVARCHAR(50)

AS 
BEGIN
SET NOCOUNT ON
	SELECT [Product].[Id], [Product].[QuantityInStock]
	FROM [Product]
	WHERE [Product].[ProductCode] = @ProductCode
END

