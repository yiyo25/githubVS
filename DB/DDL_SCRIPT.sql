EXEC ups_GetAll'AERO%'

GO


CREATE PROCEDURE ups_GetAll
(
	@filterByName nvarchar(100)
)
as
begin
	SELECT * FROM Artist
	WHERE Name like @filterByName
end
