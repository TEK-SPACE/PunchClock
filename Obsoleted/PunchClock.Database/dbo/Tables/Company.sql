CREATE TABLE [dbo].[Company]
(
	[CompanyId] INT NOT NULL CONSTRAINT [PK_Companies_CompanyId] PRIMARY KEY IDENTITY(1,1), 
	[GlobalId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_Company_GlobalId] DEFAULT NEWID(),
    [Name] VARCHAR(250) NOT NULL, 
    [Summary] VARCHAR(250) NULL, 
    [LogoUrl] VARCHAR(250) NULL,
	[LogoBinary] VARBINARY(max) NULL,
	[DeltaPunchTime] INT NOT NULL CONSTRAINT [df_tCompanies_deltaPunchTime] DEFAULT 5,
	[RegisterCode] VARCHAR(10) NOT NULL,
	[CreatedBy] INT NULL CONSTRAINT[FK_Company_CreatedBy] FOREIGN KEY([CreatedBy]) REFERENCES dbo.[Users]([UserId]),
	[IsActive] BIT NOT NULL CONSTRAINT [DF_Company_IsActive] DEFAULT 1, 
    [IsDeleted] BIT NOT NULL CONSTRAINT [DF_Company_IsDeleted] DEFAULT 0
	--[dateCreated_utc] DATETIMEOFFSET NOT NULL CONSTRAINT [df_tCompanies_dateCreated_utc] DEFAULT GETUTCDATE(), 
 --   [lastUpdated_utc] DATETIMEOFFSET NOT NULL CONSTRAINT [df_tCompanies_lastUpdated_utc] DEFAULT GETUTCDATE(), 
 --   [lastActivityDate_utc] DATETIMEOFFSET NOT NULL CONSTRAINT [df_tCompanies_lastActivityDate_utc] DEFAULT GETUTCDATE(), 
 --   [companyRegistered_ip] VARCHAR(50) NULL, 
	--[registered_MAC_address] VARCHAR(50),
	--[lastActive_MAC_address] VARCHAR(50),
 --   [lastActivity_ip] VARCHAR(50) NULL
)