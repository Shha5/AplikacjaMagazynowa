CREATE PROCEDURE [dbo].[sp_Order_GetOrderDetailsByOrderNumber]
	@OrderNumber NVARCHAR(50)

AS
BEGIN
	SELECT [Order].[Id], [Order].[OrderNumber], [Order].[OrderSignature], [Order].[CreatedDate], [Order].[LastModifiedDate], [Order].[IsComplete]
	FROM [Order] 
	WHERE [Order].[OrderNumber] = @OrderNumber		
END