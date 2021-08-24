CREATE TABLE [dbo].[VehicleTypes] (
    [VehicleTypeId]  INT          IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (50) NOT NULL,
    [TaxCoefficient] FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_VehicleType] PRIMARY KEY CLUSTERED ([VehicleTypeId] ASC)
);