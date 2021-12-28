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

    DECLARE @AppointmentStatus AS INTEGER = 1
    DECLARE @AppointmentDaysLate AS INTEGER = 0
    DECLARE @Today AS DATETIME = GETDATE()
    DECLARE @Comments AS NVARCHAR(60)
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

    IF (@Today > @NextAppointmentDate)
        BEGIN
            PRINT 'Client is late for an appointment, updating status to [Inactive]'
            SET @Result = 1
            SET @AppointmentStatus = (SELECT ClientStatusId FROM ClientStatuses WHERE ClientStatusName = 'Inactive')
            SET @AppointmentDaysLate = (SELECT DATEDIFF(DAY, @NextAppointmentDate, @Today))
            SET @Comments = (SELECT StatusCommentName FROM StatusComments WHERE StatusCommentId = 3) + CHAR(13) +
                            'Days Late: ' + @AppointmentDaysLate + CHAR(13) +
                            'Service Provider: ' + @ServiceProvider + ', ' + @ProviderRole

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

            -- update the clients table with latest appointment details
            UPDATE c
            SET c.ClientStatusId  = @AppointmentStatus,
                c.StatusCommentId = 3,
                c.DateEdit        = @Today
            FROM Clients c
                     LEFT JOIN Appointments app on c.ClientId = app.ClientId
            WHERE app.AppointmentId = @AppointmentId
        END
    ELSE
        BEGIN
            -- nothing to update
            PRINT 'Client is not late for their next appointment'
            SET @Result = 0
        END
    SELECT @Result
END
