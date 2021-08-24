CREATE TABLE [dbo].[Orders] (
    [OrderId]   INT IDENTITY (1, 1) NOT NULL,
    [VehicleId] INT NOT NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([OrderId] ASC),
    CONSTRAINT [FK_Orders_Vehicles] FOREIGN KEY ([VehicleId]) REFERENCES [dbo].[Vehicles] ([VehicleId])
);