CREATE TABLE [dbo].[AccountSummaries] (
    [Id]                  INT      IDENTITY (1, 1) NOT NULL,
    [CustomerId]          INT      NOT NULL,
    [TotalAccountBalance] MONEY    NOT NULL,
    [LastTransactionId]   INT      NOT NULL,
    [LastUpdated]         DATETIME2 NOT NULL,
    CONSTRAINT [PK_AccountSummaries] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_AccountSummaries_Customers] FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
);

