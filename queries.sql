BEGIN TRANSACTION;  
	DROP TABLE [__EFMigrationsHistory];
	DROP TABLE [JobApplication];
	DROP TABLE [User];
	DROP TABLE [Student];
	DROP TABLE [Customer];
	DROP TABLE [JobOffer];
	DROP TABLE [Group];
	DROP TABLE [Address];
COMMIT; 

SELECT a.TABLE_CATALOG as 'Database', a.TABLE_SCHEMA as 'Schema', a.TABLE_NAME as 'Table Name', a.TABLE_TYPE as 'Table Type' 
FROM INFORMATION_SCHEMA.TABLES a;

SELECT * FROM [JobApplication];
SELECT * FROM [User];
SELECT * FROM [Student];
SELECT * FROM [StudentbyDB.Customer];
SELECT * FROM [JobOffer];
SELECT * FROM [Group];
SELECT * FROM [Address];

INSTERT INTO