CREATE TABLE [dbo].[PaidHolidays]
    (
      [PaidHolidayId] [INT] NOT NULL ,
      [HolidayTypeName] [VARCHAR](150) NULL ,
      [Description] [VARCHAR](500) NULL ,
      CONSTRAINT [PK_PaidHolidays_PaidHolidayId] PRIMARY KEY CLUSTERED
        ( [PaidHolidayId] ASC )
        WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
               IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
               ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
    )
ON  [PRIMARY]
