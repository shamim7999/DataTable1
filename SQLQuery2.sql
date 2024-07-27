
spGetEmployees '', '', '', '',null, null, null,  'Id', 'desc', 0, 30

EXEC countTotalEmployee; 

SELECT COUNT(*) AS totalEmployees FROM tblEmployee

declare @col varchar(50) = 'Name';
Declare @sql varchar(max) = 'select * from tblEmployee
Order by ' + 
CASE
	WHEN @col = 'Name' Then @col
	WHEN @col = 'Salary' Then @col
END + ' ASC'

print(@sql)

exec(@sql)
                                      