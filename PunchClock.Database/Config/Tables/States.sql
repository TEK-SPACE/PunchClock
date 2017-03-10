CREATE TABLE [Config].[States]
    (
      [StateId] [int] IDENTITY(1, 1)
                      NOT NULL
                      CONSTRAINT [PK_States_StateId] PRIMARY KEY ,
      [StateGUID] [uniqueidentifier] NOT NULL ,
      [Name] [nvarchar](200) NOT NULL ,
      [CountryId] [int] NULL ,
      [Abbreviation] [nvarchar](5) NOT NULL ,
      [Published] [tinyint] NOT NULL ,
      [DisplayOrder] [int] NOT NULL ,
      [ExtensionData] [ntext] NULL ,
      [CreatedOn_utc] [datetime2]
        NOT NULL
        CONSTRAINT [DF_States_CreatedOn_utc] DEFAULT GETUTCDATE()
    )

