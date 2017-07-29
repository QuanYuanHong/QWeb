CREATE TABLE [dbo].[T_Base_Provider]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NULL, 
    [Connector] NVARCHAR(50) NULL, 
    [Phone] NVARCHAR(20) NULL, 
    [Address] NVARCHAR(200) NULL, 
    [Email] NVARCHAR(50) NULL, 
    [Fax] NVARCHAR(20) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'供货商名字',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'T_Base_Provider',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'供货商联系人名字',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'T_Base_Provider',
    @level2type = N'COLUMN',
    @level2name = N'Connector'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'供货商电话',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'T_Base_Provider',
    @level2type = N'COLUMN',
    @level2name = N'Phone'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'供货商地址',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'T_Base_Provider',
    @level2type = N'COLUMN',
    @level2name = N'Address'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'供货商邮件',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'T_Base_Provider',
    @level2type = N'COLUMN',
    @level2name = N'Email'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'供货商传真',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'T_Base_Provider',
    @level2type = N'COLUMN',
    @level2name = N'Fax'