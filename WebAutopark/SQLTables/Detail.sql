CREATE TABLE [dbo].[Details] (
    [DetailId] INT          IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Detail] PRIMARY KEY CLUSTERED ([DetailId] ASC)
);