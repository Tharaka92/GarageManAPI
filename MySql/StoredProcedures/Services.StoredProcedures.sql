-- ----------------------------------------------------------------------------
-- Routine SPRetrieveService
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPRetrieveService`(
ServiceId  int)
BEGIN
 SELECT 
	Services.ServiceId,
	Services.ServiceName,
	Services.IsDeleted
 FROM Services
 WHERE 
	(1 = CASE WHEN ServiceId IS NULL THEN 1 WHEN Services.ServiceId = ServiceId THEN 1 ELSE 0 END) AND
    Services.IsDeleted = 0;
END$$
DELIMITER $$

-- ----------------------------------------------------------------------------
-- Routine SPInsertService
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPInsertService`(
ServiceName varchar(100))
BEGIN
 INSERT INTO Services (ServiceName) VALUES (ServiceName);
END$$
DELIMITER $$

-- ----------------------------------------------------------------------------
-- Routine SPUpdateService
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPUpdateService`(
ServiceId  int,
ServiceName varchar(100))
BEGIN
 UPDATE Services SET 
	Services.ServiceName = ServiceName
 WHERE Services.ServiceId = ServiceId; 
END$$
DELIMITER $$

-- ----------------------------------------------------------------------------
-- Routine SPDeleteService
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPDeleteService`(
ServiceId int)
BEGIN
 UPDATE Services SET 
	Services.IsDeleted = 1
 WHERE Services.ServiceId = ServiceId;
END$$
DELIMITER $$