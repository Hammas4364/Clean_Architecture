-- =============================================
-- Author: Quanika Developer
-- Create date: 13-10-2022   04:05 PM
-- Modified date: 13-10-2022 04:05 PM
-- Modified by : ISM
-- Description:GET All Controllers Door Scroll list
--VB 4.5 VE--
-- =============================================
CREATE PROCEDURE SP_GetAll_Organinzation 

@searchValue varchar(255) = NULL,
@pageNumber int = 1,
@pageSize int = 50

AS
BEGIN

IF  @searchValue IS NOT NULL
BEGIN	
		SELECT 
		(
			SELECT Id,OrgName,OrgDetail, Active, Token, CreatedDate, LastModifiedDate
			From Orgainzations 
			WHERE OrgName Like '%'+@searchValue+'%' or  OrgName Like '%'+@searchValue+'%'
			ORDER BY OrgName
			OFFSET ((@pageNumber - 1) * @pageSize) 
			ROWS FETCH NEXT @pageSize ROWS ONLY
			FOR JSON AUTO
		) as Result
END
ELSE
BEGIN
		SELECT 
		(
			SELECT Id,OrgName,OrgDetail, Active, Token, CreatedDate, LastModifiedDate
			From Orgainzations 			
			ORDER BY OrgName
			OFFSET ((@pageNumber - 1) * @pageSize) 
			ROWS FETCH NEXT @pageSize ROWS ONLY
			FOR JSON AUTO
		) as Result
END
END