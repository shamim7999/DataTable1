USE MyEmployeeDB;
GO

IF OBJECT_ID('dbo.spGetEmployees', 'P') IS NOT NULL
    DROP PROCEDURE dbo.spGetEmployees;
GO