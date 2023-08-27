CREATE PROCEDURE [dbo].[sp_Order_Insert]
	@OrderSignature NVARCHAR(50),
	@OrderNumber NVARCHAR(50)

AS
	BEGIN
		SET NOCOUNT ON
		INSERT INTO [Order] ([OrderSignature], [OrderNumber], [CreatedDate], [LastModifiedDate])
		VALUES (@OrderSignature, @OrderNumber, GETUTCDATE(), GETUTCDATE())
	END