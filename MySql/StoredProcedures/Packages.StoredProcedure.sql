-- ----------------------------------------------------------------------------
-- Routine SPRetrievePackage
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPRetrievePackage`(
PackageId  int)
BEGIN
 SELECT 
	Packages.PackageId,
	Packages.Name,
	Packages.Description,
	Packages.Price,
	Packages.IsDeleted
 FROM Packages
 WHERE 
	(1 = CASE WHEN PackageId IS NULL THEN 1 WHEN Packages.PackageId = PackageId THEN 1 ELSE 0 END) AND
    Packages.IsDeleted = 0;
END$$
DELIMITER $$

-- ----------------------------------------------------------------------------
-- Routine SPInsertPackage
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPInsertPackage`(
Name varchar(100),
Description varchar(1000),
Price decimal)
BEGIN
 INSERT INTO Packages (Name,Description,Price) VALUES (Name,Description,Price);
END$$
DELIMITER $$

-- ----------------------------------------------------------------------------
-- Routine SPUpdatePackage
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPUpdatePackage`(
PackageId  int,
Name varchar(100),
Description varchar(1000),
Price decimal)
BEGIN
 UPDATE Packages SET 
	Packages.Name = Name,
	Packages.Description = Description,
	Packages.Price = Price
 WHERE Packages.PackageId = PackageId; 
END$$
DELIMITER $$

-- ----------------------------------------------------------------------------
-- Routine SPDeletePackage
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPDeletePackage`(
PackageId int)
BEGIN
 UPDATE Packages SET 
	Packages.IsDeleted = 1
 WHERE Packages.PackageId = PackageId;
END$$
DELIMITER $$