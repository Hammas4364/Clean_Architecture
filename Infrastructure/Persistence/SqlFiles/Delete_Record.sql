-- Create date: 05/01/2021 4:16 PM
-- Modified date: 25-01-2021 12:02 PM
-- Modified by : AbdulBasit
-- Description: Check Id Exits
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[g_Delete_ByCondition] 
@tableName varchar(250),
@condition varchar(255),
@validateBeforeExecution bit = 1,
@id varchar(max) = NULL out,
@return_Message VARCHAR(MAX) = NULL OUT
AS
SET NOCOUNT ON;


BEGIN
IF @validateBeforeExecution = 1
BEGIN
	DECLARE @selectQuery  NVARCHAR(255) =  N'SELECT @id = [Id] FROM [dbo].['+@tableName+'] Where '+@condition
	EXEC Sp_executesql @selectQuery,N'@id varchar(max) out', @id out

	IF @id IS NULL
		SET @return_Message = @tableName+'.NotFound'	
END	
IF @return_Message IS NULL
BEGIN
	DECLARE @deleteQuery  NVARCHAR(255) =  N'DELETE FROM [dbo].['+@tableName+']  Where '+@condition
	EXEC Sp_executesql @deleteQuery
	SET @return_Message = 'OK'
END
END