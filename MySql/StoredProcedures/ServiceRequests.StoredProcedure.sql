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
-- Routine SPUpdateServiceRequestStatus
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPUpdateServiceRequestStatus`(
ServiceRequestId  int,
ServiceRequestStatus varchar(100))
BEGIN
 UPDATE ServiceRequests SET 
	ServiceRequests.ServiceRequestStatus = ServiceRequestStatus
 WHERE ServiceRequests.ServiceRequestId = ServiceRequestId; 
END$$
DELIMITER $$

-- ----------------------------------------------------------------------------
-- Routine SPCompleteServiceRequest
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPCompleteServiceRequest`(
ServiceRequestId  int,
ServiceRequestStatus varchar(100),
StartDate datetime,
EndDate datetime)
BEGIN
 UPDATE ServiceRequests SET 
	ServiceRequests.ServiceRequestStatus = ServiceRequestStatus,
	ServiceRequests.StartDate = StartDate,
	ServiceRequests.EndDate = EndDate,
	ServiceRequests.IsCompleted = 1
 WHERE ServiceRequests.ServiceRequestId = ServiceRequestId; 
END$$
DELIMITER $$

-- ----------------------------------------------------------------------------
-- Routine SPUpdatePaymentDetails
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPUpdatePaymentDetails`(
ServiceRequestId  int,
TotalAmount decimal,
PackagePrice decimal,
PartnerAmount decimal,
PaymentStatus varchar(50),
PaymentType varchar(20))
BEGIN
 UPDATE ServiceRequests SET 
	ServiceRequests.TotalAmount = TotalAmount,
	ServiceRequests.PackagePrice = PackagePrice,
	ServiceRequests.PartnerAmount = PartnerAmount,
	ServiceRequests.PaymentStatus = PaymentStatus,
	ServiceRequests.PaymentType = PaymentType
 WHERE ServiceRequests.ServiceRequestId = ServiceRequestId; 
END$$
DELIMITER $$

-- ----------------------------------------------------------------------------
-- Routine SPRetrievePaymentAmount
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE DEFINER=`breakdown_user`@`%` PROCEDURE `SPRetrievePaymentAmount`(
IN PartnerId  int,
IN FromDate datetime,
IN ToDate datetime,
OUT AppFee decimal(15,2),
OUT TotalCashAmount decimal(15,2),
OUT TotalCardAmount decimal(15,2),
OUT CashCount int,
OUT CardCount int)
BEGIN
 SELECT 
	ROUND(SUM(ServiceRequests.PackagePrice), 2) INTO AppFee
 FROM ServiceRequests
 WHERE 
	ServiceRequests.PartnerId = PartnerId AND
    ServiceRequests.SubmittedDate >= FromDate AND
    ServiceRequests.SubmittedDate <= ToDate AND
    ServiceRequests.IsCompleted = 1;
    
 SELECT 
	ROUND(SUM(ServiceRequests.PartnerAmount), 2) INTO TotalCashAmount
 FROM ServiceRequests
 WHERE 
	ServiceRequests.PartnerId = PartnerId AND
    ServiceRequests.SubmittedDate >= FromDate AND
    ServiceRequests.SubmittedDate <= ToDate AND
    ServiceRequests.PaymentType = 'cash' AND
    ServiceRequests.IsCompleted = 1;
    
 SELECT 
	ROUND(SUM(ServiceRequests.PartnerAmount), 2) INTO TotalCardAmount
 FROM ServiceRequests
 WHERE 
	ServiceRequests.PartnerId = PartnerId AND
    ServiceRequests.SubmittedDate >= FromDate AND
    ServiceRequests.SubmittedDate <= ToDate AND
    ServiceRequests.PaymentType = 'card' AND
    ServiceRequests.IsCompleted = 1;
    
 SELECT 
	COUNT(ServiceRequests.ServiceRequestId) INTO CashCount
 FROM ServiceRequests
 WHERE 
	ServiceRequests.PartnerId = PartnerId AND
    ServiceRequests.SubmittedDate >= FromDate AND
    ServiceRequests.SubmittedDate <= ToDate AND
    ServiceRequests.PaymentType = 'cash' AND
    ServiceRequests.IsCompleted = 1;
    
 SELECT 
	COUNT(ServiceRequests.ServiceRequestId) INTO CardCount
 FROM ServiceRequests
 WHERE 
	ServiceRequests.PartnerId = PartnerId AND
    ServiceRequests.SubmittedDate >= FromDate AND
    ServiceRequests.SubmittedDate <= ToDate AND
    ServiceRequests.PaymentType = 'card' AND
    ServiceRequests.IsCompleted = 1;
    
 SELECT AppFee, CashCount, CardCount, TotalCashAmount, TotalCardAmount;
END$$
DELIMITER $$