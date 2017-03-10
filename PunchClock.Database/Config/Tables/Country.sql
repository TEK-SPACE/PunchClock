CREATE TABLE [Config].[Country]
    (
      [CountryId] [int] IDENTITY(1, 1)
                        NOT NULL
                        CONSTRAINT [PK_Country_CountryId] PRIMARY KEY ,
      [CountryGuid] [uniqueidentifier] NOT NULL ,
      [Name] [nvarchar](200) NOT NULL ,
      [TwoLetterIsoCode] [nvarchar](2) NULL ,
      [ThreeLetterIsoCode] [nvarchar](3) NULL ,
      [NumericIsoCode] [nvarchar](3) NULL ,
      [Published] [tinyint] NOT NULL ,
      [DisplayOrder] [int] NOT NULL ,
      [ExtensionData] [ntext] NULL ,
      [CreatedOn_utc] [datetime2] NOT NULL CONSTRAINT [DF_Country_CreatedOn_utc] DEFAULT GETUTCDATE(),
    )