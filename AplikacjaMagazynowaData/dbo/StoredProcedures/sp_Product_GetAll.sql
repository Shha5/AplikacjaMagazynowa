CREATE PROCEDURE [dbo].[sp_Product_GetAll]

AS
BEGIN
	SET NOCOUNT ON
	SELECT [Product].[Id], [Product].[ProductCode], [Product].[ProductName], [Product].[QuantityInStock], [Product].[CreatedDate], [Product].[LastModifiedDate]
	FROM [Product]
END