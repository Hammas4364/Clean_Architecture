-- =============================================
-- Author: Quanika Developer
-- Create date: 13-10-2022   04:05 PM
-- Modified date: 13-10-2022 04:05 PM
-- Modified by : ISM
-- Description:GET All Controllers Door Scroll list
--VB 4.5 VE--
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[qn_GetAll_Controller] 

@searchValue varchar(255) = NULL,
@pageNumber int = 1,
@pageSize int = 50,
@organizationId bigint

AS
BEGIN

IF  @searchValue IS NOT NULL
BEGIN
		Select (Select c.Id,c.Name,c.Status, Doors.Id ,Doors.Name , Doors.DoorType
		FROM (
				SELECT Controllers.Id,Controllers.Name,Controllers.Status 
				From [Controllers] 
				WHERE Controllers.OrganizationId = @organizationId
				) as c  
		LEFT Join [Doors]
		On c.Id = Doors.ControllerId
		WHERE Doors.[Name] Like '%'+@searchValue+'%' or  c.Name Like '%'+@searchValue+'%'
		ORDER BY c.Name
				OFFSET ((@pageNumber - 1) * @pageSize) 
				ROWS FETCH NEXT @pageSize ROWS ONLY

		FOR JSON AUTO) as Result
END
ELSE
BEGIN
		Select (Select c.Id,c.Name,c.Status,  Doors.Id ,Doors.Name , Doors.DoorType
		FROM (
				SELECT Controllers.Id,Controllers.Name,Controllers.Status 
				From [Controllers] 
				WHERE Controllers.OrganizationId = @organizationId
				) as c  
		LEFT Join [Doors]
		On c.Id = Doors.ControllerId
		ORDER BY c.Name
				OFFSET ((@pageNumber - 1) * @pageSize) 
				ROWS FETCH NEXT @pageSize ROWS ONLY

		FOR JSON AUTO) as Result
END
END