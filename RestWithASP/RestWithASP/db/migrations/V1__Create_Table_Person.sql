CREATE TABLE person(
	id bigint IDENTITY(1,1) NOT NULL,
	address nvarchar(100) NOT NULL,
	first_name nvarchar(80) NOT NULL,
	gender nvarchar(6) NOT NULL,
	last_name nvarchar(80) NOT NULL
	);