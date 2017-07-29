CREATE TABLE [dbo].[T_Base_Customer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NULL, 
    [Phone] NVARCHAR(20) NULL, 
    [Address] NVARCHAR(200) NULL
)
