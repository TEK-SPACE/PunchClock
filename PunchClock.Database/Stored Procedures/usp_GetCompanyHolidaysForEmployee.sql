CREATE PROCEDURE [dbo].[usp_GetCompanyHolidaysForEmployee]
    (
      @companyId INT ,
      @EmployeeId INT
    )
AS
    BEGIN
        SELECT  H.HolidayName AS HolidayName ,
                CONVERT(DATE, CONVERT(VARCHAR(4), DATEPART(YYYY, GETDATE()))
                + '/' + CONVERT(VARCHAR(2), H.HolidayMonth) + '/'
                + CONVERT(VARCHAR(2), H.HolidayDate)) AS HolidayDate
        FROM    Config.Holidays AS H
                JOIN dbo.CompanyHoliday AS CH ON CH.HolidayId = H.HolidayId
                JOIN dbo.Company AS C ON C.CompanyId = CH.CompanyId
                JOIN dbo.CompanyEmployeeHolidayPaid AS CEHP ON CEHP.CompanyId = C.CompanyId
                JOIN dbo.Users AS U ON U.CompanyId = C.CompanyId
                JOIN Config.EmploymentType AS ET ON ET.EmploymentTypeId = U.[EmploymentType]
        WHERE   C.CompanyId = @companyId
                AND U.UserId = @EmployeeId
                AND CEHP.EmploymentTypeId = U.[EmploymentType]
                AND CEHP.IsHolidayPaid = 1
        ORDER BY CONVERT(DATE, CONVERT(VARCHAR(4), DATEPART(YYYY, GETDATE()))
                + '/' + CONVERT(VARCHAR(2), H.HolidayMonth) + '/'
                + CONVERT(VARCHAR(2), H.HolidayDate))
    END

