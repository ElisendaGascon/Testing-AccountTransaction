CREATE TABLE [dbo].[Transactions] (
    [Id]              INT      IDENTITY (1, 1) NOT NULL,
    [Amount]          MONEY    NOT NULL,
    [TransactionDate] DATETIME2 NOT NULL,
    [Cleared]         BIT      NULL,
    [DateAdded]       DATETIME2 NOT NULL,
    [AccountId]       INT      NOT NULL,
    [TransactionTypeId] INT NOT NULL, 
    [LastUpdated] DATETIME2 NOT NULL CONSTRAINT [DF_Transactions_LastUpdated] DEFAULT getutcdate(), 
    CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Transactions_TransactionTypes] FOREIGN KEY (TransactionTypeId) REFERENCES TransactionTypes(Id), 
    CONSTRAINT [FK_Transactions_Accounts] FOREIGN KEY (AccountId) REFERENCES Accounts(Id)
);


GO
/*
CREATE TRIGGER dbo.tr_Transactions_Insert 
   ON  dbo.Transactions 
   FOR INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [audit].[Transactions]
           ([TransactionId]
           ,[Amount]
           ,[TransactionDate]
           ,[Cleared]
           ,[DateAdded]
           ,[AccountId]
           ,[AuditAction])
     SELECT
           Id
           ,Amount
           ,TransactionDate
           ,Cleared
           ,DateAdded
           ,AccountId
           ,'I'
	FROM INSERTED		


END

GO */
CREATE TRIGGER dbo.tr_Transactions_Update
   ON  dbo.Transactions 
   FOR UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [audit].[Transactions]
           ([TransactionId]
           ,[Amount]
           ,[TransactionDate]
           ,[Cleared]
           ,[DateAdded]
           ,[AccountId]
           ,[AuditAction]
           ,[AuditCreated])
     SELECT
           Id
           ,Amount
           ,TransactionDate
           ,Cleared
           ,DateAdded
           ,AccountId
           ,'U'
           ,GETUTCDATE()
	FROM UPDATED		


END

GO
CREATE TRIGGER dbo.tr_Transactions_Delete
   ON  dbo.Transactions 
   FOR DELETE
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [audit].[Transactions]
           ([TransactionId]
           ,[Amount]
           ,[TransactionDate]
           ,[Cleared]
           ,[DateAdded]
           ,[AccountId]
           ,[AuditAction]
           ,[AuditCreated])
     SELECT
           Id
           ,Amount
           ,TransactionDate
           ,Cleared
           ,DateAdded
           ,AccountId
           ,'D'
           ,GETUTCDATE()
	FROM DELETED		


END
