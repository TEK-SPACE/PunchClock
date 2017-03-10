CREATE TABLE [Config].[AppConfig]
(
	[AppConfigId] INT NOT NULL CONSTRAINT[PK_AppConfig_AppConfigId] PRIMARY KEY IDENTITY(1,1),
	[Name] VARCHAR(150) NOT NULL,
	[Description] VARCHAR(1000) NULL,
	[ValueString] VARCHAR(max) NULL,
	[ValueBit] BIT NULL,
	[ValueFloat] FLOAT NULL,
	[ValueInt] INT NULL,
	[IsActive] BIT NOT NULL CONSTRAINT [DF_AppConfig_IsActive] DEFAULT 1, 
    [IsDeleted] BIT NOT NULL CONSTRAINT [DF_AppConfig_IsDeleted] DEFAULT 0, 
)
