CREATE TABLE [dbo].[Users]
    (
      [UserId] INT NOT NULL
                   CONSTRAINT [PK_Users_UserId] PRIMARY KEY
                   IDENTITY(1, 1) ,
      [GlobalId] UNIQUEIDENTIFIER
        NOT NULL
        CONSTRAINT [DF_Users_GlobalId] DEFAULT NEWID() ,
      UserTypeId INT
        NOT NULL
        CONSTRAINT [FK_Users_UserTypeId]
        FOREIGN KEY ( [UserTypeId] ) REFERENCES [config].[UserTypes] ( [userTypeId] ) 
		Constraint [DF_Users_UserId] DEFAULT  1 ,
      [EmploymentType] INT
        NOT NULL
        CONSTRAINT [FK_Users_EmploymentType_EmploymentTypeId]
        FOREIGN KEY ( [EmploymentType] ) REFERENCES [Config].[EmploymentType] ( [employmentTypeId] ) ,
      [CompanyId] INT
        NOT NULL
        CONSTRAINT [FK_Users_CompanyId]
        FOREIGN KEY ( [CompanyId] ) REFERENCES dbo.[Company] ( [companyId] ) ,
      [FirstName] VARCHAR(50) NOT NULL ,
      [LastName] VARCHAR(50) NOT NULL ,
      [Email] VARCHAR(80) NOT NULL ,
      [Telephone] VARCHAR(50) NULL ,
      [MiddleName] VARCHAR(50) NULL ,
      [UserName] VARCHAR(50) NOT NULL ,
      [PasswordHash] VARCHAR(1024) NULL ,
      [PasswordSalt] VARCHAR(1024) NULL ,
      [PasswordLastChanged] DATETIME2 NULL ,
      [PasswordDisabled] BIT NOT NULL
                             CONSTRAINT [DF_Users_PasswordDisabled] DEFAULT 0 ,
      [RegisteredTimeZone] VARCHAR(500)
        NOT NULL
        CONSTRAINT [DF_Users_RegisteredTimeZone]
        DEFAULT ( 'Eastern Standard Time' ) ,
      [IsActive] BIT NOT NULL
                     CONSTRAINT [DF_Users_IsActive] DEFAULT 1 ,
      [IsDeleted] BIT NOT NULL
                      CONSTRAINT [DF_Users_IsDeleted] DEFAULT 0 ,
      [IsAdmin] INT NULL ,
      [DateCreated_utc] DATETIMEOFFSET
        NOT NULL
        CONSTRAINT [DF_Users_DateCreated_utc] DEFAULT GETUTCDATE() ,
      [LastUpdated_utc] DATETIMEOFFSET
        NOT NULL
        CONSTRAINT [DF_Users_LastUpdated_utc] DEFAULT GETUTCDATE() ,
      [LastActivityDate_utc] DATETIMEOFFSET
        NOT NULL
        CONSTRAINT [DF_Users_LastActivityDate_utc] DEFAULT GETUTCDATE() ,
      [UserRegistered_IP] VARCHAR(50) NULL ,
      [Registered_MAC_Address] VARCHAR(50) ,
      [LastActive_MAC_Address] VARCHAR(50) ,
      [LastActivity_IP] VARCHAR(50) NULL
    )
