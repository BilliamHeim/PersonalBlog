USE PersonalBlog
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.Routines
	WHERE ROUTINE_NAME = 'DbReset')
		DROP PROCEDURE DbReset
GO

CREATE PROCEDURE DbReset
AS
BEGIN

	EXEC dbo.TableSetup;

	INSERT INTO Categories
	VALUES('Tech'),
	('Videogames'),
	('Movies'),
	('Music'),
	('Television'),
	('Politics'),
	('Education'),
	('Science'),
	('Memes'),
	('Cars'),
	('Sports'),
	('Travel'),
	('Religion'),
	('Fashion'),
	('Recreation'),
	('Home & Garden'),
	('Other')

	INSERT INTO Posts
	VALUES('Today''s Youth Don''t Know Good Cartoons', 'You see you little lit af whippersnappers, back in my day in 1996...', 1, '03-29-2018', 5,'Images/1/boredlookingkidswatchingnickelodeon.jpg'),
	('Self Driving Cars Wanna Kill Us All', 'Well, a pedistrian got killed by one of these fancy futuristic things. You done did it now Uber, y''all are fucking up...', 1, '03-30-2018', 1,'Images/2/uberlogo.jpg'),
	('See Here''s the Thing with Jews', 'I''m not antisemitic, but...', 0, '04-01-2018', 13,'Images/3/jewscontrolthemedia.jpg'),
	('March For Our Lives is a Testament to Our Society', 'Isn''t it odd how we''re having copious school shootings now out of all the times? They''ve spiked in occurences during a time when the political atmosphere is very unstable and divided and the Gun Debate topic is hotter than it''s ever been. The topic needed more fuel for the fire so...', 1, '04-20-2018', 6,'Images/4/ar15.jpg')

	INSERT INTO Tags
	VALUES('#MeToo'), --1
	('#OscarsSoWhite'), --2
	('#Neature'), --3
	('#Hashtag'), --4
	('#JustSayin'), --5
	('#CmonMan'), --6
	('#UberSux'), --7
	('#Woke'), --8
	('#Lit'), --9
	('#LitAF'), --10
	('#Dank'), --11
	('#BoujeeAF'), --12
	('#BackInMyDay'), --13
	('#Childhood'), --14
	('#MarchForOurLives'), --15
	('#ThanksObama'), --16
	('#GunsForEveryone') --17

	INSERT INTO PostTags
	VALUES(1, 5),
	(1, 6),
	(1, 13),
	(1, 14),
	(2, 6),
	(2, 7),
	(3, 5),
	(3, 13),
	(3, 16),
	(4, 5),
	(2, 8),
	(2, 16),
	(2, 17)

END
GO

EXEC dbo.DbReset
GO
