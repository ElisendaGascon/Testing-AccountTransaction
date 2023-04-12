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


:r .\TransactionTypes.Seed.sql


if('$(IncludeTestAccounts)' = 'true')
begin
	RaisError ('Seeding test accounts', 0, 0) with log
	:r .\TestAccounts.Seed.sql
	RaisError ('Finished test accounts', 0, 0) with log
end

