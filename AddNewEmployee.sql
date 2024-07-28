USE MyEmployeeDB;
GO

CREATE PROCEDURE addNewEmployee
    --@id INT,
    @name NVARCHAR(MAX),
    @position NVARCHAR(MAX),
    @office NVARCHAR(MAX),
    @age INT,
    @salary INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Employee ([Name], Position, Office, Age, Salary)
    VALUES (@name, @position, @office, @age, @salary);
END
GO
