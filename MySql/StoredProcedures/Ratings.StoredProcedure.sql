-- ----------------------------------------------------------------------------
-- Routine SPInsertRating
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPInsertRating`(
UserId  int,
ServiceRequestId int,
RatingValue double,
Comment varchar(500))
BEGIN
 INSERT INTO Ratings (UserId,ServiceRequestId,RatingValue,Comment) VALUES (UserId,ServiceRequestId,RatingValue,Comment);

 call SPUpdateAverageRating(UserId);
END$$
DELIMITER $$

-- -------------------------------------------------------------------------------------------------------------
-- Routine SPUpdateAverageRating - THIS SP IS CALLED BY SPInsertRating JUST AFTER INSERTING A NEW RATING RECORD
-- -------------------------------------------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPUpdateAverageRating`(
UserId  int)
BEGIN
 DECLARE average_rating double default 0.0; 

 SELECT 
	AVG(RatingValue) INTO average_rating
 FROM Ratings
 WHERE Ratings.UserId = UserId;

 UPDATE AspNetUsers SET
	AspNetUsers.AverageRating = average_rating
 WHERE AspNetUsers.Id = UserId;
END$$
DELIMITER $$