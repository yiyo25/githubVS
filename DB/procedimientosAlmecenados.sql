ALTER PROCEDURE usp_InsertArtist
@pName NVARCHAR(120)
AS
BEGIN
	
	/*DECLARE @nroReg INT
	
	SELECT @nroReg = COUNT(1) FROM Artist WHERE Name = @pName
	*/

	IF NOT EXISTS(SELECT ArtistId FROM Artist WHERE Name = @pName)
	BEGIN

		INSERT INTO Artist(Name)
		VALUES (@pName)

		SELECT SCOPE_IDENTITY()
	END
	ELSE
	BEGIN

		SELECT 0
	END


END


go

CREATE PROCEDURE usp_UpdateArtist
@pName NVARCHAR(120),
@pId INT
as

BEGIN
	IF NOT EXISTS(SELECT ArtistId FROM Artist WHERE Name = @pName)
	begin
		UPDATE Artist SET Name = @pName
		WHERE ArtistId = @pId
	END
END

go

CREATE PROCEDURE usp_DeleteArtist
@pId INT
as

BEGIN

	DELETE FROM Artist where ArtistId = @pId

end 

go