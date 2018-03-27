USE PersonalBlog
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.Routines
	WHERE ROUTINE_NAME = 'DbReset')
		DROP PROCEDURE DbReset
GO

CREATE PROCEDURE DbReset
AS
BEGIN
	DELETE FROM AspNetUsers WHERE Id = '00000000-0000-0000-0000-000000000000';

	INSERT INTO AspNetUsers(Id, [Role], EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName)
	VALUES('00000000-0000-0000-0000-000000000000', 'Admin', 0, 0, 0, 0, 0, 'admin')

END