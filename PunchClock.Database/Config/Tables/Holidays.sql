CREATE TABLE [Config].Holidays
    (
      HolidayId INT NOT NULL
                    CONSTRAINT [PK_Holidays_HolidayId] PRIMARY KEY ,
      HolidayName VARCHAR(200) NOT NULL ,
      [Description] VARCHAR(500) NULL ,
      HolidayMonth INT NOT NULL ,
      HolidayDate INT NOT NULL ,
      HolidayTypeId INT
        NOT NULL
        CONSTRAINT [FK_Holidays_HolidayTypeId]
        FOREIGN KEY ( HolidayTypeId ) REFERENCES [Config].HolidayType ( HolidayTypeId ) ,
      CountryId INT
        NOT NULL
        CONSTRAINT [FK_Holidays_CountryId]
        FOREIGN KEY ( CountryId ) REFERENCES [Config].[Country] ( [CountryId] )
    )