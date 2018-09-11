-- -- ----------------------------------------------------------------------------
-- -- Routine SPRetrieveVehicleType
-- -- ----------------------------------------------------------------------------

-- DELIMITER $$
-- CREATE PROCEDURE `SPRetrieveVehicleType`(
-- VehicleTypeId  int)
-- BEGIN
--  SELECT 
-- 	VehicleTypes.VehicleTypeId,
-- 	VehicleTypes.Name,
-- 	VehicleTypes.Description,
-- 	VehicleTypes.IsDeleted
--  FROM VehicleTypes
--  WHERE 
-- 	(1 = CASE WHEN VehicleTypeId IS NULL THEN 1 WHEN VehicleTypes.VehicleTypeId = VehicleTypeId THEN 1 ELSE 0 END) AND
--     VehicleTypes.IsDeleted = 0;
-- END$$
-- DELIMITER $$

-- ----------------------------------------------------------------------------
-- Routine SPInsertServiceRequest
-- ----------------------------------------------------------------------------

DELIMITER $
$
CREATE PROCEDURE `SPInsertServiceRequest`(
CustomerId int,
PartnerId int,
ServiceId int,
VehicleTypeId int,
PackageId int,
SubmittedDate datetime,
ServiceRequestStatus varchar(100),
PaymentStatus varchar(100))
BEGIN
	INSERT INTO ServiceRequests
		(CustomerId, PartnerId, ServiceId, VehicleTypeId, PackageId, SubmittedDate, ServiceRequestStatus, PaymentStatus)
	VALUES
		(CustomerId, PartnerId, ServiceId, VehicleTypeId, PackageId, SubmittedDate, ServiceRequestStatus, PaymentStatus);
END$$
DELIMITER $$

-- ----------------------------------------------------------------------------
-- Routine SPUpdateVehicleType
-- ----------------------------------------------------------------------------

-- DELIMITER $$
-- CREATE PROCEDURE `SPUpdateVehicleType`(
-- VehicleTypeId  int,
-- Name varchar(100),
-- Description varchar(300))
-- BEGIN
--  UPDATE VehicleTypes SET 
-- 	VehicleTypes.Name = Name,
-- 	VehicleTypes.Description = Description
--  WHERE VehicleTypes.VehicleTypeId = VehicleTypeId; 
-- END$$
-- DELIMITER $$

-- ----------------------------------------------------------------------------
-- Routine SPDeleteVehicleType
-- ----------------------------------------------------------------------------

-- DELIMITER $$
-- CREATE PROCEDURE `SPDeleteVehicleType`(
-- VehicleTypeId int)
-- BEGIN
--  UPDATE VehicleTypes SET 
-- 	VehicleTypes.IsDeleted = 1
--  WHERE VehicleTypes.VehicleTypeId = VehicleTypeId;
-- END$$
-- DELIMITER $$