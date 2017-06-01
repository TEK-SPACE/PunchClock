INSERT INTO	[Config].Holidays
        ( HolidayId ,
          HolidayName ,
          [Description] ,
          HolidayMonth ,
          HolidayDate ,
          HolidayTypeId ,
          CountryId
        )
VALUES  ( 1, -- holidayId - int
          'New Year''s Day' , -- holidayName - varchar(200)
          null , -- description - varchar(500)
          1 , -- holidayMonth - int
          1 , -- holidayDate - int
          (SELECT THT.HolidayTypeId FROM [Config].HolidayType AS THT WHERE THT.TypeName ='National Holiday'), -- holidayTypeId - int
          (SELECT TC.[CountryId] FROM [Config].[Country] AS TC WHERE TC.[ThreeLetterIsoCode] = 'USA') -- countryId - int
        )

INSERT INTO	[Config].Holidays
        ( HolidayId ,
          HolidayName ,
          [Description] ,
          HolidayMonth ,
          HolidayDate ,
          HolidayTypeId ,
          CountryId
        )
VALUES  ( 2, -- holidayId - int
          'Martin Luther King Day' , -- holidayName - varchar(200)
          null , -- description - varchar(500)
          1 , -- holidayMonth - int
          20 , -- holidayDate - int
          (SELECT THT.HolidayTypeId FROM [Config].HolidayType AS THT WHERE THT.TypeName ='National Holiday'), -- holidayTypeId - int
          (SELECT TC.[CountryId] FROM [Config].[Country] AS TC WHERE TC.[ThreeLetterIsoCode] = 'USA') -- countryId - int
        )


INSERT INTO	[Config].Holidays
        ( HolidayId ,
          HolidayName ,
          [Description] ,
          HolidayMonth ,
          HolidayDate ,
          HolidayTypeId ,
          CountryId
        )
VALUES  ( 3, -- holidayId - int
          'Presidents'' Day (Washington''s Birthday)' , -- holidayName - varchar(200)
          null , -- description - varchar(500)
          2 , -- holidayMonth - int
          17 , -- holidayDate - int
          (SELECT THT.HolidayTypeId FROM [Config].HolidayType AS THT WHERE THT.TypeName ='National Holiday'), -- holidayTypeId - int
          (SELECT TC.[CountryId] FROM [Config].[Country] AS TC WHERE TC.[ThreeLetterIsoCode] = 'USA') -- countryId - int
        )


INSERT INTO	[Config].Holidays
        ( HolidayId ,
          HolidayName ,
          [Description] ,
          HolidayMonth ,
          HolidayDate ,
          HolidayTypeId ,
          CountryId
        )
VALUES  ( 4, -- holidayId - int
          'Memorial Day' , -- holidayName - varchar(200)
          null , -- description - varchar(500)
          5 , -- holidayMonth - int
          26 , -- holidayDate - int
          (SELECT THT.HolidayTypeId FROM [Config].HolidayType AS THT WHERE THT.TypeName ='National Holiday'), -- holidayTypeId - int
          (SELECT TC.[CountryId] FROM [Config].[Country] AS TC WHERE TC.[ThreeLetterIsoCode] = 'USA') -- countryId - int
        )


INSERT INTO	[Config].Holidays
        ( HolidayId ,
          HolidayName ,
          [Description] ,
          HolidayMonth ,
          HolidayDate ,
          HolidayTypeId ,
          CountryId
        )
VALUES  ( 5, -- holidayId - int
          'Independence Day' , -- holidayName - varchar(200)
          null , -- description - varchar(500)
          6 , -- holidayMonth - int
          4 , -- holidayDate - int
          (SELECT THT.HolidayTypeId FROM [Config].HolidayType AS THT WHERE THT.TypeName ='National Holiday'), -- holidayTypeId - int
          (SELECT TC.[CountryId] FROM [Config].[Country] AS TC WHERE TC.[ThreeLetterIsoCode] = 'USA') -- countryId - int
        )

INSERT INTO	[Config].Holidays
        ( HolidayId ,
          HolidayName ,
          [Description] ,
          HolidayMonth ,
          HolidayDate ,
          HolidayTypeId ,
          CountryId
        )
VALUES  ( 6, -- holidayId - int
          'Labor Day' , -- holidayName - varchar(200)
          null , -- description - varchar(500)
          8 , -- holidayMonth - int
          1 , -- holidayDate - int
          (SELECT THT.HolidayTypeId FROM [Config].HolidayType AS THT WHERE THT.TypeName ='National Holiday'), -- holidayTypeId - int
          (SELECT TC.[CountryId] FROM [Config].[Country] AS TC WHERE TC.[ThreeLetterIsoCode] = 'USA') -- countryId - int
        )

INSERT INTO	[Config].Holidays
        ( HolidayId ,
          HolidayName ,
          [Description] ,
          HolidayMonth ,
          HolidayDate ,
          HolidayTypeId ,
          CountryId
        )
VALUES  ( 7, -- holidayId - int
          'Columbus Day' , -- holidayName - varchar(200)
          null , -- description - varchar(500)
          10 , -- holidayMonth - int
          13 , -- holidayDate - int
          (SELECT THT.HolidayTypeId FROM [Config].HolidayType AS THT WHERE THT.TypeName ='National Holiday'), -- holidayTypeId - int
          (SELECT TC.[CountryId] FROM [Config].[Country] AS TC WHERE TC.[ThreeLetterIsoCode] = 'USA') -- countryId - int
        )

INSERT INTO	[Config].Holidays
        ( HolidayId ,
          HolidayName ,
          [Description] ,
          HolidayMonth ,
          HolidayDate ,
          HolidayTypeId ,
          CountryId
        )
VALUES  ( 8, -- holidayId - int
          'Veterans Day' , -- holidayName - varchar(200)
          null , -- description - varchar(500)
          11 , -- holidayMonth - int
          11 , -- holidayDate - int
          (SELECT THT.HolidayTypeId FROM [Config].HolidayType AS THT WHERE THT.TypeName ='National Holiday'), -- holidayTypeId - int
          (SELECT TC.[CountryId] FROM [Config].[Country] AS TC WHERE TC.[ThreeLetterIsoCode] = 'USA') -- countryId - int
        )


INSERT INTO	[Config].Holidays
        ( HolidayId ,
          HolidayName ,
          [Description] ,
          HolidayMonth ,
          HolidayDate ,
          HolidayTypeId ,
          CountryId
        )
VALUES  ( 9, -- holidayId - int
          'Thanksgiving Day' , -- holidayName - varchar(200)
          null , -- description - varchar(500)
          11 , -- holidayMonth - int
          27 , -- holidayDate - int
          (SELECT THT.HolidayTypeId FROM [Config].HolidayType AS THT WHERE THT.TypeName ='National Holiday'), -- holidayTypeId - int
          (SELECT TC.[CountryId] FROM [Config].[Country] AS TC WHERE TC.[ThreeLetterIsoCode] = 'USA') -- countryId - int
        )


INSERT INTO	[Config].Holidays
        ( HolidayId ,
          HolidayName ,
          [Description] ,
          HolidayMonth ,
          HolidayDate ,
          HolidayTypeId ,
          CountryId
        )
VALUES  ( 10, -- holidayId - int
          'Christmas Day' , -- holidayName - varchar(200)
          null , -- description - varchar(500)
          12 , -- holidayMonth - int
          25 , -- holidayDate - int
          (SELECT THT.HolidayTypeId FROM [Config].HolidayType AS THT WHERE THT.TypeName ='National Holiday'), -- holidayTypeId - int
          (SELECT TC.[CountryId] FROM [Config].[Country] AS TC WHERE TC.[ThreeLetterIsoCode] = 'USA') -- countryId - int
        )