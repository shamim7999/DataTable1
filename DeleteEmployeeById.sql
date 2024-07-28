USE MyEmployeeDB;
GO

CREATE PROCEDURE deleteEmployeeById
    @id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE Employee 
	WHERE Id = @Id
END
GO
