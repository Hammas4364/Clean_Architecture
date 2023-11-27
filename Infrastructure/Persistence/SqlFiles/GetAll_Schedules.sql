-- =============================================
-- Author: Quanika Developer
-- Create date: 13-10-2022   04:05 PM
-- Modified date: 13-10-2022 04:05 PM
-- Modified by : AF
-- Description:GET All SChedule With Scheudle Items Scroll list
--VB 4.5 VE--
-- =============================================
CREATE OR ALTER PROCEDURE  [dbo].[qn_Schedule_ScrollList] 

@searchValue varchar(255) = NULL,
@pageNumber int = 1,
@pageSize int = 50,
@organizationId bigint

AS
BEGIN

IF  @searchValue IS NOT NULL
BEGIN
		Select (Select s.Id, s.[Name], s.IsSubtraction,s.[Description] ,ScheduleItems.Id ,ScheduleItems.Summary , ScheduleItems.IsRecurrence
		FROM (
				SELECT Id,[Name],[IsSubtraction],[Description]
				From [Schedules] 
				WHERE OrganizationId = @organizationId or OrganizationId = -1
				) as s
		LEFT Join [ScheduleItems] as ScheduleItems
		On s.Id = ScheduleItems.ScheduleId
		WHERE ScheduleItems.[Summary] Like '%'+@searchValue+'%' or  s.Name Like '%'+@searchValue+'%'
		ORDER BY s.Name
				OFFSET ((@pageNumber - 1) * @pageSize) 
				ROWS FETCH NEXT @pageSize ROWS ONLY

		FOR JSON AUTO) as Result
END
ELSE
BEGIN
		Select (Select s.Id,s.[Name],s.[IsSubtraction],s.[Description], ScheduleItems.Id ,ScheduleItems.Summary , ScheduleItems.IsRecurrence
		FROM (
				SELECT Id,Name,[IsSubtraction],[Description]
				From [Schedules] 
				WHERE OrganizationId = @organizationId or OrganizationId = -1
				) as s
		LEFT Join [ScheduleItems] as ScheduleItems
		On s.Id = ScheduleItems.ScheduleId
		ORDER BY s.Name
				OFFSET ((@pageNumber - 1) * @pageSize) 
				ROWS FETCH NEXT @pageSize ROWS ONLY

		FOR JSON AUTO) as Result
END
END
