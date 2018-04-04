USE PersonalBlog
GO

CREATE PROCEDURE DeletePost(@PostId INT)
AS
DELETE FROM PostTags
WHERE PostId=@PostId
DELETE FROM Posts
WHERE PostId=@PostId
GO