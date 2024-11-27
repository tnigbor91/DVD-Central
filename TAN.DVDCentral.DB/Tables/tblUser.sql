CREATE TABLE [dbo].[tblUser]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[UserId] VARCHAR(25) NOT NULL, 
	[FirstName] Varchar(MAX) NOT NULL, 
    [LastName] VARCHAR(MAX) NOT NULL, 
    [Password] VARCHAR(28) NOT NULL,
)
