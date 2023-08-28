CREATE PROCEDURE [dbo].[sp_Order_GetOrderIdByOrderNumber]
	@OrderNumber NVARCHAR(50)

AS
BEGIN
	SET NOCOUNT ON
	SELECT [Order].[Id]
	FROM [Order]
	WHERE [Order].[OrderNumber] = @OrderNumber
END