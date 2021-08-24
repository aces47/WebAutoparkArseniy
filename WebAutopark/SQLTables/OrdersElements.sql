CREATE TABLE [dbo].[OrdersElements] (
    [OrderElementId] INT IDENTITY (1, 1) NOT NULL,
    [OrderId]        INT NOT NULL,
    [DetailId]       INT NOT NULL,
    [DetailCount]    INT NOT NULL,
    CONSTRAINT [PK_OrderElement] PRIMARY KEY CLUSTERED ([OrderElementId] ASC),
    CONSTRAINT [FK_OrderElement_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([OrderId]),
    CONSTRAINT [FK_OrderElement_Detail] FOREIGN KEY ([DetailId]) REFERENCES [dbo].[Details] ([DetailId])
    );