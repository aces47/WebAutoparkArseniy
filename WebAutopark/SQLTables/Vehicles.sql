CREATE TABLE [dbo].[Vehicles] (
    [VehicleId]         INT           IDENTITY (1, 1) NOT NULL,
    [VehicleTypeId]     INT           NOT NULL,
    [ModelName]         NVARCHAR (50) NOT NULL,
    [StateNumber]       NVARCHAR (10) NOT NULL,
    [ManufactureYear]   INT           NOT NULL,
    [Mileage]           FLOAT (53)    NOT NULL,
    [Weight]            FLOAT (53)    NOT NULL,
    [EngineType]        NVARCHAR (50) NOT NULL,
    [EngineCapacity]    FLOAT (53)    NOT NULL,
    [EngineConsumption] FLOAT (53)    NULL,
    [TankCapacity]      FLOAT (53)    NOT NULL,
    [Color]             INT           NOT NULL,
    CONSTRAINT [PK_Vehicles] PRIMARY KEY CLUSTERED ([VehicleId] ASC),
    CONSTRAINT [FK_Vehicles_VehicleTypes] FOREIGN KEY ([VehicleTypeId]) REFERENCES [dbo].[VehicleTypes] ([VehicleTypeId])
);