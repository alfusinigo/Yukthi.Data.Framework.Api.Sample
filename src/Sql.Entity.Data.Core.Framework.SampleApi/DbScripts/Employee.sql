drop table employee
GO
CREATE TABLE [dbo].[Employee] (
    [ID]         INT             NOT NULL IDENTITY(1,1),
    [FIRST_NAME] VARCHAR (50)    NOT NULL,
    [LAST_NAME]  VARCHAR (50)    NOT NULL,
    [ADDRESS]    VARchar (200) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
)

GO
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

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.CsvToTable') AND OBJECTPROPERTY(object_id, N'IsTableFunction')=1)
DROP FUNCTION dbo.CsvToTable
GO
CREATE FUNCTION dbo.CsvToTable 
(@CSV varchar(MAX))
RETURNS @valueTable table (value varchar(256), rownum int)
AS
BEGIN
	if @CSV <> ''
	BEGIN
		declare @seperator char(1)
		set @seperator = ','
		
		declare @sep_position int
		declare @arr_val varchar(max)
		declare @rowcount int
		set @rowcount = 1
		
		if RIGHT(@csv,1) != ','
			set @CSV = @CSV+','
			
		while PATINDEX('%,%',@csv) <> 0
		BEGIN
			select @sep_position = PATINDEX('%,%', @csv)
			select @arr_val = LEFT(@csv, @sep_position - 1)
			insert @valueTable values (ltrim(rtrim(@arr_val)), @rowcount)
			select @CSV=STUFF(@csv,1,@sep_position,'')
			set @rowcount = @rowcount + 1
		END
	END
	RETURN
END
GO
