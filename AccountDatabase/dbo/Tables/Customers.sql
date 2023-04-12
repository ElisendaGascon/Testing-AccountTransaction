CREATE TABLE [dbo].[Customers] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
	[CustomerGuid] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_Customers_CustomerGuid] DEFAULT newid(),
    [FullName] NVARCHAR (500) NOT NULL,
    [Zipcode]  VARCHAR (10)   NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

