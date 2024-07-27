USE MyEmployeeDB;
GO

CREATE PROCEDURE deleteEmployeeById
    @id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE tblEmployee 
	WHERE Id = @Id
END
GO
