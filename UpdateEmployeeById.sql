USE MyEmployeeDB;
GO

CREATE PROCEDURE updateEmployeeById
    @id INT,
    @name NVARCHAR(MAX),
    @position NVARCHAR(MAX),
    @office NVARCHAR(MAX),
    @age INT,
    @salary INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Employee
	SET Name = @name, Position = @position, Office = @office, Age = @age, Salary = @salary
	WHERE Id = @Id
END
GO
