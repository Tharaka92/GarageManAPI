-- ----------------------------------------------------------------------------
-- Routine SPRetrieveLatestServiceRequest
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPRetrieveLatestServiceRequestId`(
PartnerId  int,
CustomerId int,
ServiceRequestStatus varchar(100))
BEGIN
 SELECT 
	ServiceRequests.ServiceRequestId
 FROM ServiceRequests
 WHERE 
 	ServiceRequests.ServiceRequestStatus = ServiceRequestStatus AND
	ServiceRequests.PartnerId = PartnerId AND
	ServiceRequests.CustomerId = CustomerId AND
    ServiceRequests.IsCompleted = 0
 ORDER BY SubmittedDate DESC 
 LIMIT 1;
END$$
DELIMITER $$

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
-- Routine SPCancelServiceRequest
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPCancelServiceRequest`(
ServiceRequestId  int,
ServiceRequestStatus varchar(100))
BEGIN
 UPDATE ServiceRequests SET 
	ServiceRequests.ServiceRequestStatus = ServiceRequestStatus
 WHERE ServiceRequests.ServiceRequestId = ServiceRequestId; 
END$$
DELIMITER $$

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