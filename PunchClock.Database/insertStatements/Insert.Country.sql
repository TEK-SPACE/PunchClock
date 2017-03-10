/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
IF NOT EXISTS ( SELECT  Name
                FROM    Config.Country AS C
                WHERE   C.Name = 'United States' )
    BEGIN	
        INSERT  INTO Config.Country
                ( CountryGuid ,
                  Name ,
                  TwoLetterIsoCode ,
                  ThreeLetterIsoCode ,
                  NumericIsoCode ,
                  Published ,
                  DisplayOrder ,
                  ExtensionData ,
                  CreatedOn_utc
                )
        VALUES  ( NEWID() , -- CountryGuid - uniqueidentifier
                  N'United States' , -- Name - nvarchar(200)
                  N'US' , -- TwoLetterIsoCode - nvarchar(2)
                  N'USA' , -- ThreeLetterIsoCode - nvarchar(3)
                  N'000' , -- NumericIsoCode - nvarchar(3)
                  1 , -- Published - tinyint
                  1 , -- DisplayOrder - int
                  NULL , -- ExtensionData - ntext
                  GETUTCDATE()  -- CreatedOn_utc - datetime2
                )

    END