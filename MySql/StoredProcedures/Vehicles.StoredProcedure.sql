-- ----------------------------------------------------------------------------
-- Routine SPRetrieveVehicle
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPRetrieveVehicle`(
VehicleId  int,
UserId int)
BEGIN
 SELECT 
	Vehicles.VehicleId,
	Vehicles.UserId,
	Vehicles.LicensePlate,
	Vehicles.VehicleType,
	Vehicles.Make,
	Vehicles.Model,
	Vehicles.Color,
	Vehicles.YOM,
	Vehicles.IsDeleted
 FROM Vehicles
 WHERE 
	(1 = CASE WHEN VehicleId IS NULL THEN 1 WHEN Vehicles.VehicleId = VehicleId THEN 1 ELSE 0 END) AND
	(1 = CASE WHEN UserId IS NULL THEN 1 WHEN Vehicles.UserId = UserId THEN 1 ELSE 0 END) AND
    Vehicles.IsDeleted = 0;
END$$
DELIMITER $$

-- ----------------------------------------------------------------------------
-- Routine SPInsertVehicle
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPInsertVehicle`(
UserId varchar(127),
LicensePlate varchar(50),
VehicleType varchar(10),
Make varchar(20),
Model varchar(20),
Color varchar(10),
YOM varchar(5))
BEGIN
 INSERT INTO Vehicles (UserId,LicensePlate,VehicleType,Make,Model,Color,YOM) VALUES (UserId,LicensePlate,VehicleType,Make,Model,Color,YOM);
END$$
DELIMITER $$

-- ----------------------------------------------------------------------------
-- Routine SPUpdateVehicle
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPUpdateVehicle`(
VehicleId  int,
LicensePlate varchar(50),
VehicleType varchar(10),
Make varchar(20),
Model varchar(20),
Color varchar(10),
YOM varchar(5))
BEGIN
 UPDATE Vehicles SET 
	Vehicles.LicensePlate = LicensePlate,
	Vehicles.VehicleType = VehicleType,
	Vehicles.Make = Make,
	Vehicles.Model = Model,
	Vehicles.Color = Color,
	Vehicles.YOM = YOM
 WHERE Vehicles.VehicleId = VehicleId; 
END$$
DELIMITER $$

-- ----------------------------------------------------------------------------
-- Routine SPDeleteVehicle
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPDeleteVehicle`(
VehicleId int)
BEGIN
 UPDATE Vehicles SET 
	Vehicles.IsDeleted = 1
 WHERE Vehicles.VehicleId = VehicleId;
END$$
DELIMITER $$