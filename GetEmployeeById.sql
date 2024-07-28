USE MyEmployeeDB;
GO

CREATE PROCEDURE getEmployeeById
    @id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Employee
	WHERE Id = @id
END
GO
