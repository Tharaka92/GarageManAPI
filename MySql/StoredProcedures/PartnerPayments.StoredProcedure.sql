-- ----------------------------------------------------------------------------
-- Routine SPRetrievePartnerPayment
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE DEFINER=`breakdown_user`@`%` PROCEDURE `SPRetrievePartnerPayment`(
PartnerId  int)
BEGIN
 SELECT 
	PartnerPayments.PartnerPaymentId,
	PartnerPayments.PartnerId,
	PartnerPayments.CashCount,
	PartnerPayments.CardCount,
	PartnerPayments.FromDate,
	PartnerPayments.ToDate,
	PartnerPayments.CreatedDate,
	PartnerPayments.TotalCashAmount,
	PartnerPayments.TotalCardAmount,
	PartnerPayments.AppFee,
	PartnerPayments.AppFeePaidAmount,
	PartnerPayments.AppFeeRemainingAmount,
	PartnerPayments.HasPaid,
	PartnerPayments.HasReceived
 FROM PartnerPayments
 WHERE 
	PartnerPayments.PartnerId = PartnerId AND
    PartnerPayments.HasReceived = 0
 ORDER BY CreatedDate DESC
 LIMIT 1;
END$$
DELIMITER $$

-- ----------------------------------------------------------------------------
-- Routine SPInsertPartnerPayment
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPInsertPartnerPayment`(
PartnerId int,
CashCount int,
CardCount int,
FromDate datetime,
ToDate datetime,
CreatedDate datetime,
TotalCashAmount decimal,
TotalCardAmount decimal,
AppFee decimal,
AppFeePaidAmount decimal,
AppFeeRemainingAmount decimal,
HasPaid tinyint,
HasReceived tinyint)
BEGIN
	INSERT INTO PartnerPayments
		(PartnerId, CashCount, CardCount, FromDate, ToDate, CreatedDate, TotalCashAmount, TotalCardAmount, AppFee, AppFeePaidAmount, AppFeeRemainingAmount, HasPaid, HasReceived)
	VALUES
		(PartnerId, CashCount, CardCount, FromDate, ToDate, CreatedDate, TotalCashAmount, TotalCardAmount, AppFee, AppFeePaidAmount, AppFeeRemainingAmount, HasPaid, HasReceived);
END$$
DELIMITER $$
