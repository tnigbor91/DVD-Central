BEGIN 
	INSERT INTO tblOrderItem (Id, OrderId, Quantity, MovieId, Cost)
	VALUES
	(1, 3, 5, 1, 19.99),
	(2, 2, 6, 2, 30.00),
	(3, 1, 1, 3, 12.99)
END