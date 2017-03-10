CREATE PROCEDURE [dbo].[usp_GetCompanyHolidays] ( @companyId INT )
AS
    BEGIN
        SELECT  H.HolidayName ,
                CONVERT(DATE, CONVERT(VARCHAR(4), DATEPART(YYYY, GETDATE()))
                + '/' + CONVERT(VARCHAR(2), H.HolidayMonth) + '/'
                + CONVERT(VARCHAR(2), H.HolidayDate)) AS HolidayDate
        FROM    dbo.Holidays AS H
                JOIN dbo.CompanyHoliday AS CH ON CH.HolidayId = H.HolidayId
                JOIN dbo.Company AS C ON C.CompanyId = CH.CompanyId
        WHERE   C.CompanyId = @companyId
    END


