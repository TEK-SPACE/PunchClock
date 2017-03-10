CREATE TABLE [Config].HolidayType
(
	HolidayTypeId INT NOT NULL CONSTRAINT [PK_HolidayType_HolidayTypeId] PRIMARY KEY ,
	TypeName VARCHAR(250) NOT NULL,
	[Description] VARCHAR(500) NULL,
	DateEntered_utc DATETIME2 NOT NULL CONSTRAINT [DF_HolidayType_DateEntered_utc] DEFAULT GETUTCDATE()
)