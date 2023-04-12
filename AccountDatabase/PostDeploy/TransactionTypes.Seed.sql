PRINT 'Merging data in table dbo.TransactionTypes'
SET IDENTITY_INSERT dbo.TransactionTypes ON
MERGE dbo.TransactionTypes AS T
USING
(
	SELECT * 
	FROM (
	VALUES 
	 (1, 'D', 'Deposit')
	,(2, 'W', 'Withdrawl')) AS vtable ([Id], [Type], [DisplayName])) AS S
ON T.Id = S.Id
WHEN NOT MATCHED THEN INSERT
  ([Id], [Type], [DisplayName])
  VALUES
  (S.Id, S.Type, S.DisplayName)
WHEN MATCHED THEN UPDATE SET
  T.Type = S.Type,
  T.DisplayName = S.DisplayName;

SET IDENTITY_INSERT dbo.TransactionTypes OFF

