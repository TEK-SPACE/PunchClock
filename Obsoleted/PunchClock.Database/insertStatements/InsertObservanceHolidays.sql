INSERT  INTO [Config].Holidays
        ( HolidayId ,
          HolidayName ,
          [Description] ,
          HolidayMonth ,
          HolidayDate ,
          HolidayTypeId ,
          countryId
        )
VALUES  ( 11 , -- holidayId - int
          'Valentine''s DAY' , -- holidayName - varchar(200)
          NULL , -- description - varchar(500)
          2 , -- holidayMonth - int
          14 , -- holidayDate - int
          ( SELECT  THT.HolidayTypeId
            FROM    [Config].HolidayType AS THT
            WHERE   THT.TypeName = 'Observance'
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
          countryId
        )
VALUES  ( 12 , -- holidayId - int
          'Thomas Jefferson''s Birthday' , -- holidayName - varchar(200)
          NULL , -- description - varchar(500)
          4 , -- holidayMonth - int
          13 , -- holidayDate - int
          ( SELECT  THT.HolidayTypeId
            FROM    [Config].HolidayType AS THT
            WHERE   THT.TypeName = 'Observance'
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
          countryId
        )
VALUES  ( 13 , -- holidayId - int
          'Easter Sunday' , -- holidayName - varchar(200)
          NULL , -- description - varchar(500)
          4 , -- holidayMonth - int
          20 , -- holidayDate - int
          ( SELECT  THT.HolidayTypeId
            FROM    [Config].HolidayType AS THT
            WHERE   THT.TypeName = 'Observance'
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
          countryId
        )
VALUES  ( 14 , -- holidayId - int
          'Mothers'' Day' , -- holidayName - varchar(200)
          NULL , -- description - varchar(500)
          5 , -- holidayMonth - int
          11 , -- holidayDate - int
          ( SELECT  THT.HolidayTypeId
            FROM    [Config].HolidayType AS THT
            WHERE   THT.TypeName = 'Observance'
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
          countryId
        )
VALUES  ( 15 , -- holidayId - int
          'Fathers'' Day' , -- holidayName - varchar(200)
          NULL , -- description - varchar(500)
          6 , -- holidayMonth - int
          15 , -- holidayDate - int
          ( SELECT  THT.HolidayTypeId
            FROM    [Config].HolidayType AS THT
            WHERE   THT.TypeName = 'Observance'
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
          countryId
        )
VALUES  ( 16 , -- holidayId - int
          'Halloween' , -- holidayName - varchar(200)
          NULL , -- description - varchar(500)
          10 , -- holidayMonth - int
          31 , -- holidayDate - int
          ( SELECT  THT.HolidayTypeId
            FROM    [Config].HolidayType AS THT
            WHERE   THT.TypeName = 'Observance'
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
          countryId
        )
VALUES  ( 17 , -- holidayId - int
          'Christmas Eve' , -- holidayName - varchar(200)
          NULL , -- description - varchar(500)
          12 , -- holidayMonth - int
          24 , -- holidayDate - int
          ( SELECT  THT.HolidayTypeId
            FROM    [Config].HolidayType AS THT
            WHERE   THT.TypeName = 'Observance'
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
          countryId
        )
VALUES  ( 18 , -- holidayId - int
          'New Year''s Eve' , -- holidayName - varchar(200)
          NULL , -- description - varchar(500)
          12 , -- holidayMonth - int
          31 , -- holidayDate - int
          ( SELECT  THT.HolidayTypeId
            FROM    [Config].HolidayType AS THT
            WHERE   THT.TypeName = 'Observance'
          ) , -- holidayTypeId - int
          ( SELECT  TC.[CountryId]
            FROM    [Config].[Country] AS TC
            WHERE   TC.[ThreeLetterIsoCode] = 'USA'
          ) -- countryId - int
        )