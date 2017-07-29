CREATE TABLE [dbo].[T_Base_User] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Username] NVARCHAR (50) NULL,
    [Password] NVARCHAR (50) NULL,
    [Phone] NVARCHAR(20) NULL, 
    [Address] NVARCHAR(200) NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

