CREATE TABLE [audit].[Transactions] (
    [Id]              INT      IDENTITY (1, 1) NOT NULL,
    [TransactionId]   INT      NOT NULL,
    [Amount]          MONEY    NOT NULL,
    [TransactionDate] DATETIME NOT NULL,
    [Cleared]         BIT      NULL,
    [DateAdded]       DATETIME NOT NULL,
    [AccountId]       INT      NOT NULL,
    [AuditAction]     CHAR (1) NOT NULL,
    [AuditCreated]    DATETIME NOT NULL,
    CONSTRAINT [PK_Audit_Transactions] PRIMARY KEY CLUSTERED ([Id] ASC)
);

