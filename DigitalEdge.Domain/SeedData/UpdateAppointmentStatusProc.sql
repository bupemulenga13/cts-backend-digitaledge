USE [CTSMigrationDB]
GO
/****** Object:  StoredProcedure [dbo].[UpdateAppointmentStatus]    Script Date: 27/12/2021 17:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		    Gift Muyembe JR
-- Create date:     23.12.2021
-- Description:	    Updates the appointment status and LTFU depending on the next appointment date,
--                  including all reference data relative to client appointments
-- Called by :      CTS Appointments Manager
-- Parameters(s):   @AppointmentId - primary identifier of the appointment
--                  @ProviderId - Service provider at the facility
-- =============================================
-- DROP PROCEDURE IF EXISTS [dbo].[updateAppointmentStatus];
CREATE OR ALTER PROCEDURE [dbo].[UpdateAppointmentStatus](@AppointmentId AS INT, @ProviderId AS INT, @Result BIT OUTPUT)
AS
BEGIN

    DECLARE @AppointmentDaysLate AS INT = 0
    DECLARE @Today AS DATETIME = GETDATE()
    DECLARE @Comments AS NVARCHAR(60)
    DECLARE @AppointmentStatus AS INT = (SELECT AppointmentId
                                         FROM Appointments
                                         WHERE AppointmentId = @AppointmentId)
    DECLARE @InteractionDate AS DATETIME = (SELECT InteractionDate
                                            FROM Appointments
                                            WHERE AppointmentId = @AppointmentId)
    DECLARE @NextAppointmentDate AS DATETIME = (SELECT AppointmentDate
                                                FROM Appointments
                                                WHERE AppointmentId = @AppointmentId)
    DECLARE @ServiceProvider AS NVARCHAR(50) = (SELECT ISNULL(u.FirstName, 'FN_UNKNOWN') + '' +
                                                       ISNULL(u.LastName, 'LN_UNKNOWN')
                                                FROM Users u
                                                WHERE u.Id = @ProviderId)
    DECLARE @ProviderRole AS NVARCHAR(50) = (SELECT ISNULL(ur.RoleName, 'UNKNOWN_ROLE')
                                             FROM UserRoles ur
                                                      JOIN Users u ON ur.RoleId = u.RoleId
                                             WHERE ur.RoleId = @ProviderId)
    -- missed appointment
    IF (@Today > @NextAppointmentDate)
        BEGIN
            PRINT 'Client is late for an appointment, updating status to [Inactive]'
            SET @Result = 1
            SET @AppointmentStatus = -1
            SET @AppointmentDaysLate = (SELECT DATEDIFF(DAY, @NextAppointmentDate, @Today))
            SET @Comments = 'Missed appointment by ' + @AppointmentDaysLate + 'days.'

            -- populate all appointments reference data
            -- update the appointments table
            UPDATE app
            SET app.AppointmentStatus = @AppointmentStatus,
                app.DateEdited        = @Today,
                app.DaysLate          = @AppointmentDaysLate,
                app.Comment           = @Comments,
                app.EditedBy          = @ProviderId
            FROM [dbo].[Appointments] app
            WHERE app.AppointmentId = @AppointmentId

        END
    ELSE
        -- attended appointments
        IF (@AppointmentStatus = 0 AND @InteractionDate IS NOT NULL)
            BEGIN
                PRINT 'Client is not late for their next appointment'
                SET @Result = 0

                UPDATE app
                SET app.AppointmentStatus = 1,
                    app.DateEdited        = @Today,
                    app.EditedBy = @ProviderId
                FROM [dbo].[Appointments] app
                WHERE app.AppointmentId = @AppointmentId
            END
    RETURN @Result
END
