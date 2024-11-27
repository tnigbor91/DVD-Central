BEGIN
	INSERT INTO tblCustomer (Id, FirstName, LastName, UserId, Address, City, State, ZIP, Phone)
	VALUES
	(1, 'Frodo', 'Baggins', 1, '451 Shire St', 'Oshkosh', 'WI', '54897', '9201547788' ),
	(2, 'Aragorn', 'Son of Arathorn', 3, '88 King Rd', 'Appleton', 'WI', '547821', '9209877414' ),
	(3, 'Legolas', 'Greenleaf', 2, '7 Archer Ct', 'Waupaca', 'WI', '447852','9201247581' )
END