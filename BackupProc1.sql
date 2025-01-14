﻿USE MyEmployeeDB;
GO

CREATE PROCEDURE spGetEmployees
    @searchValue NVARCHAR(MAX) = NULL,
    @name NVARCHAR(MAX) = NULL,
    @position NVARCHAR(MAX) = NULL,
    @office NVARCHAR(MAX) = NULL,
    @id INT = NULL,
    @age INT = NULL,
    @salary INT = NULL,
    @sortColumnName NVARCHAR(MAX) = 'Id',
    @sortDirection NVARCHAR(MAX) = 'ASC',
    @start INT = 0,
    @length INT = 10
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @OrderBy NVARCHAR(MAX);

    -- Constructing the dynamic ORDER BY clause
    SET @OrderBy = 
        CASE 
            WHEN @sortColumnName = 'Id' THEN 'Id'
            WHEN @sortColumnName = 'Name' THEN 'Name'
            WHEN @sortColumnName = 'Position' THEN 'Position'
            WHEN @sortColumnName = 'Office' THEN 'Office'
            WHEN @sortColumnName = 'Age' THEN 'Age'
            WHEN @sortColumnName = 'Salary' THEN 'Salary'
        END + 
        CASE @sortDirection
            WHEN 'ASC' THEN ' ASC'
            WHEN 'DESC' THEN ' DESC'
        END;

    -- Dynamic SQL query
    DECLARE @SQLQuery NVARCHAR(MAX);
    SET @SQLQuery = '
        WITH FilteredEmployees AS (
            SELECT *,
                   COUNT(*) OVER() AS TotalCount
            FROM tblEmployee
            WHERE 
                (@searchValue IS NULL OR 
                 Id LIKE ''%'' + @searchValue + ''%'' OR
                 Name LIKE ''%'' + @searchValue + ''%'' OR
                 Position LIKE ''%'' + @searchValue + ''%'' OR
                 Office LIKE ''%'' + @searchValue + ''%'' OR
                 Age LIKE ''%'' + @searchValue + ''%'' OR
                 Salary LIKE ''%'' + @searchValue + ''%'')
                AND (@name IS NULL OR Name LIKE ''%'' + @name + ''%'')
                AND (@position IS NULL OR Position LIKE ''%'' + @position + ''%'')
                AND (@office IS NULL OR Office LIKE ''%'' + @office + ''%'')
                AND (@id IS NULL OR Id = @id)
                AND (@age IS NULL OR Age = @age)
                AND (@salary IS NULL OR Salary = @salary)
        )
        SELECT * 
        FROM FilteredEmployees
        ORDER BY ' + @OrderBy + '
        OFFSET @start ROWS
        FETCH NEXT @length ROWS ONLY;';

    -- Executing the dynamic SQL query
    EXEC sp_executesql @SQLQuery,
        N'@searchValue NVARCHAR(MAX), @name NVARCHAR(MAX), @position NVARCHAR(MAX), @office NVARCHAR(MAX),
          @id INT, @age INT, @salary INT, @start INT, @length INT',
        @searchValue, @name, @position, @office, @id, @age, @salary, @start, @length;
END
GO
