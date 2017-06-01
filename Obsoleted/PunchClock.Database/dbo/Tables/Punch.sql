CREATE TABLE Punch
    (
      PunchId INT NOT NULL
                  IDENTITY(1, 1)
                  CONSTRAINT [PK_Punch_Id] PRIMARY KEY ,
     PunchDate DATETIME2 NOT NULL
        CONSTRAINT [DF_Punch_PunchDate] DEFAULT GETUTCDATE() ,
      PunchIn TIME NOT NULL
                   CONSTRAINT [df_tPunch_In] DEFAULT CONVERT(TIME, GETDATE()) ,
      PunchOut TIME
        NULL
        CONSTRAINT [DF_Punch_PunchOut] DEFAULT CONVERT(TIME, GETDATE()) ,
      UserId INT
        NOT NULL
        CONSTRAINT [FK_Punch_UserId]
        FOREIGN KEY REFERENCES dbo.[Users] ( [UserId] ) ,
      IsManagerAccepted BIT NOT NULL
                            CONSTRAINT [DF_Punch_IsManagerAccepted] DEFAULT 0 ,
      RequestForApproval BIT
        NOT NULL
        CONSTRAINT [DF_Punch_RequestForApproval] DEFAULT 0,
		Comments VARCHAR(500) NULL
    )