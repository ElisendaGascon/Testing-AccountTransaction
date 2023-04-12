PRINT 'Merging test customers'
SET IDENTITY_INSERT dbo.Customers ON
MERGE dbo.Customers AS T
USING
(
	SELECT * 
	FROM (
	VALUES 
	 (-1, 'f63a92b6-b4da-4b99-88d4-45a301fc0db0', 'Test One',   '000001')
	,(-2, 'd89135ac-4d7e-4a95-854b-55b0cc0b4c53', 'Test Two',   '000002')
	,(-3, '09f1d492-e184-4c92-95b2-50f2bda38dd3', 'Test Three', '000003')
	,(-4, '4cdff431-90dc-4c58-9970-4ab7f6e83e9c', 'Test Four',  '000004')
	,(-5, 'faf1428b-8426-48a7-8937-2ed2b66c205e', 'Test Five',  '000005')
		  ) AS vtable 
	([Id], [CustomerGuid], [FullName], [ZipCode])) AS S
ON T.Id = S.Id
WHEN NOT MATCHED THEN INSERT
  ([Id], [CustomerGuid], [FullName], [ZipCode])
  VALUES
  (S.Id, S.CustomerGuid, S.FullName, S.ZipCode)
WHEN MATCHED THEN UPDATE SET
  T.CustomerGuid = S.CustomerGuid,
  T.FullName = S.FullName,
  T.ZipCode = S.ZipCode;
SET IDENTITY_INSERT dbo.Customers OFF

PRINT 'Merging test accounts'
SET IDENTITY_INSERT dbo.Accounts ON
MERGE dbo.Accounts AS T
USING
(
	SELECT * 
	FROM (
	VALUES 
	 (-1, '0000000010', '1/1/2000', null,       '1/1/2000', -1, 0)
	,(-2, '0000000020', '1/1/2000', null,       '1/1/2000', -2, 0)
	,(-3, '0000000030', '1/1/2000', null,       '1/1/2000', -3, 0)
	,(-4, '0000000040', '1/1/2000', null,       '1/1/2000', -4, 0)
	,(-5, '0000000050', '1/1/2000', null,       '1/1/2000', -5, 1) --locked
	,(-6, '000000001A', '1/1/2000', null,       '1/1/2000', -1, 0)
	,(-7, '000000001B', '1/1/2000', '1/1/2000', '1/1/2000', -1, 0) --closed
	,(-8, '000000001C', '1/1/2000', null,       '1/1/2000', -1, 0)
		  ) AS vtable 
	([Id], [AccountNumber], [DateOpened], [DateClosed], [LastUpdated], [CustomerId], [Locked])) AS S
ON T.Id = S.Id
WHEN NOT MATCHED THEN INSERT
  ([Id], [AccountNumber], [DateOpened], [DateClosed], [LastUpdated], [CustomerId], [Locked])
  VALUES
  (S.Id, S.AccountNumber, S.DateOpened, S.DateClosed, S.LastUpdated, S.CustomerId, S.Locked)
WHEN MATCHED THEN UPDATE SET
  T.AccountNumber = S.AccountNumber,
  T.DateOpened = S.DateOpened,
  T.DateClosed = S.DateClosed,
  T.LastUpdated = S.LastUpdated,
  T.CustomerId = S.CustomerId,
  T.Locked = S.Locked;
SET IDENTITY_INSERT dbo.Accounts OFF

PRINT 'Removing test account summaries'
delete from AccountSummaries where CustomerId < 0
PRINT 'Removing test account transactions'
delete from Transactions where AccountId < 0
delete from audit.Transactions where AccountId < 0
