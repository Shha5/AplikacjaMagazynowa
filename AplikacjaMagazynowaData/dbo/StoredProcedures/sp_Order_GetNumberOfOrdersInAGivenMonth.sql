CREATE PROCEDURE [dbo].[sp_Order_GetNumberOfOrdersInAGivenMonth]
	@StartDate DATETIME2,
	@EndDate DATETIME2
AS
BEGIN
	SELECT COUNT([Order].[Id]) FROM [Order] WHERE [Order].[CreatedDate] BETWEEN @StartDate AND @EndDate 
END
