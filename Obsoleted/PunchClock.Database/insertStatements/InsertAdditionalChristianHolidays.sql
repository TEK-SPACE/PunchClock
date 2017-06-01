INSERT  INTO [Config].Holidays
        ( HolidayId ,
          HolidayName ,
          [Description] ,
          HolidayMonth ,
          HolidayDate ,
          HolidayTypeId ,
          CountryId
        )
VALUES  ( 19 , -- holidayId - int
          'Good Friday' , -- holidayName - varchar(200)
          NULL , -- description - varchar(500)
          4 , -- holidayMonth - int
          18 , -- holidayDate - int
          ( SELECT  THT.HolidayTypeId
            FROM    [Config].HolidayType AS THT
            WHERE   THT.TypeName = 'Christian Holiday'
          ) , -- holidayTypeId - int
          ( SELECT  TC.[CountryId]
            FROM    [Config].[Country] AS TC
            WHERE   TC.[ThreeLetterIsoCode] = 'USA'
          ) -- countryId - int
        )

INSERT  INTO [Config].Holidays
        ( HolidayId ,
          HolidayName ,
          [Description] ,
          HolidayMonth ,
          HolidayDate ,
          HolidayTypeId ,
          CountryId
        )
VALUES  ( 20 , -- holidayId - int
          'Easter Monday' , -- holidayName - varchar(200)
          NULL , -- description - varchar(500)
          4 , -- holidayMonth - int
          21 , -- holidayDate - int
          ( SELECT  THT.HolidayTypeId
            FROM    [Config].HolidayType AS THT
            WHERE   THT.TypeName = 'Christian Holiday'
          ) , -- holidayTypeId - int
          ( SELECT  TC.[CountryId]
            FROM    [Config].[Country] AS TC
            WHERE   TC.[ThreeLetterIsoCode] = 'USA'
          ) -- countryId - int
        )