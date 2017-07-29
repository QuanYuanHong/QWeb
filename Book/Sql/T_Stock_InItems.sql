CREATE TABLE [dbo].[T_Stock_InItems] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [HeadId]   INT          NOT NULL,
    [BookId]   INT          NOT NULL,
    [Amount]   INT          NULL,
    [Discount] DECIMAL (18) NULL,
    CONSTRAINT [PK_T_Stock_InItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InItems_ToHead] FOREIGN KEY ([HeadId]) REFERENCES [dbo].[T_Stock_InHead] ([Id]),
    CONSTRAINT [FK_InItems_ToBook] FOREIGN KEY ([BookId]) REFERENCES [dbo].[T_Base_Book] ([Id])
);

