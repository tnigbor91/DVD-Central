BEGIN 
	INSERT INTO tblOrder (Id, CustomerId, OrderDate, UserId, ShipDate)
	VALUES
	(1, 1, '2023-10-15', 1, '2023-10-20'),
	(2, 3, '2023-01-20', 3, '2023-01-21'),
	(3, 1, '2023-12-05', 2, '2023-12-07')
END