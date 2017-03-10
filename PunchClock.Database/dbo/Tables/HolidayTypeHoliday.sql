CREATE TABLE [dbo].[HolidayTypeHoliday]
(
	Id int not null identity(1,1) constraint [PK_HolidayTypeHoliday_Id] Primary Key,
	[HolidayId] [INT] NOT NULL Constraint [FK_HolidayTypeHoliday_Holiday_HolidayId] Foreign Key ([HolidayId]) References Config.Holidays([HolidayId]) ,
	[TypeId] [INT] NOT NULL Constraint [FK_HolidayTypeHoliday_HolidayType_TypeId] Foreign Key ([TypeId]) References Config.HolidayType(HolidayTypeId)
)
