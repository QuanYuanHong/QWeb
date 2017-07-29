CREATE TABLE [dbo].[T_Stock_InHead] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [CreateTime] DATETIME      NULL,
    [ProviderId] INT           NULL,
    [Creater]    NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_In_ToProvider] FOREIGN KEY (ProviderId) REFERENCES [T_Base_Provider](Id)
);

