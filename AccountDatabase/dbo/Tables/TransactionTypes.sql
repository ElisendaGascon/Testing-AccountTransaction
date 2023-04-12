CREATE TABLE [dbo].[TransactionTypes]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [Type] CHAR NOT NULL, 
    [DisplayName] VARCHAR(50) NOT NULL
)
