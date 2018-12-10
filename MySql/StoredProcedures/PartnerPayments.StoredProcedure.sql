-- ----------------------------------------------------------------------------
-- Routine SPRetrievePartnerPayment
-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SPRetrievePartnerPayment`(
PartnerId  int,
FromDate datetime,
ToDate datetime)
BEGIN
 SELECT 
	PartnerPayments.PartnerPaymentId,
	PartnerPayments.PartnerId,
	PartnerPayments.CashCount,
	PartnerPayments.CardCount,
	PartnerPayments.From,
	PartnerPayments.To,
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
 	PartnerPayments.From = FromDate AND
	PartnerPayments.To = ToDate AND
	PartnerPayments.PartnerId = PartnerId;
END$$
DELIMITER $$

-- ----------------------------------------------------------------------------
-- Routine SPInsertPartnerPayment
-- ----------------------------------------------------------------------------

DELIMITER $
$
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
