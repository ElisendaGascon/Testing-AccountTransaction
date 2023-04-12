CREATE TABLE [dbo].[Accounts] (
    [Id]            INT          IDENTITY (1, 1) NOT NULL,
    [AccountNumber] VARCHAR (10) NOT NULL,
    [DateOpened]    DATETIME2     NULL,
    [DateClosed]    DATETIME2     NULL,
    [LastUpdated]   DATETIME2     NOT NULL,
    [CustomerId]    INT          NOT NULL,
    [Locked]        BIT          CONSTRAINT [DF_Accounts_Locked] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Accounts_Customers] FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
);

