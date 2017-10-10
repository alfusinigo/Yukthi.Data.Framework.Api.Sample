IF OBJECT_ID('dbo.Employee', 'U') IS NOT NULL 
DROP TABLE dbo.Employee
GO
CREATE TABLE [dbo].[Employee] (
    [ID]         INT             NOT NULL IDENTITY(1,1),
    [FIRST_NAME] VARCHAR (50)    NOT NULL,
    [LAST_NAME]  VARCHAR (50)    NOT NULL,
    [ADDRESS]    VARchar (200) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
)

GO
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.InsertEmployee'))
DROP PROCEDURE [dbo].[InsertEmployee]
GO
CREATE PROCEDURE [dbo].[InsertEmployee]
	@FIRST_NAME varchar(50),
	@LAST_NAME varchar(50),
	@ADDRESS varchar(200) = null
AS
	INSERT INTO dbo.Employee(FIRST_NAME,LAST_NAME,ADDRESS) VALUES (@FIRST_NAME,@LAST_NAME,@ADDRESS)
	SELECT SCOPE_IDENTITY()
GO
