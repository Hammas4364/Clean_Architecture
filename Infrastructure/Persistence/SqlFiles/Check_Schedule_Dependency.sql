CREATE PROCEDURE [dbo].[sp_Check_Schedule_Dependency] @Id bigint
AS
BEGIN
  DECLARE @SId int
If EXISTS (SELECT TOP 1 * FROM Schedules WHERE id =@Id)
BEGIN
		If EXISTS (SELECT TOP 1 * FROM DoorAdvanceConfigurations WHERE DuringScheduleId =@Id)
			BEGIN
				SET @SId = 1;
			END 
		ELSE    
			BEGIN
				SET @SId = 0;
			END
			If EXISTS (SELECT TOP 1 * FROM AccessLevels WHERE DuringScheduleId =@Id)
			BEGIN
				SET @SId = 1;
			END 
		ELSE    
			BEGIN
				SET @SId = 0;
			END
			If EXISTS (SELECT TOP 1 * FROM Rexes WHERE RexDuringScheduleId =@Id or RexExceptScheduleId = @Id)
			BEGIN
				SET @SId = 1;
			END 
		ELSE    
			BEGIN
				SET @SId = 0;
			END
			If EXISTS (SELECT TOP 1 * FROM ReaderIdentificationTypes WHERE DuringScheduleId =@Id)
			BEGIN
				SET @SId = 1;
			END 
		ELSE    
			BEGIN
				SET @SId = 0;
			END
			If EXISTS (SELECT TOP 1 * FROM DoorAdvanceConfigurations WHERE DuringScheduleId =@Id)
			BEGIN
				SET @SId = 1;
			END 
		ELSE    
			BEGIN
				SET @SId = 0;
			END
			If EXISTS (SELECT TOP 1 * FROM AccessLevelDoors WHERE DuringScheduleId =@Id)
			BEGIN
				SET @SId = 1;
			END 
		ELSE    
			BEGIN
				SET @SId = 0;
			END
END 
ELSE    
BEGIN
SET @SId = 0;
END

 SELECT @SId

END
