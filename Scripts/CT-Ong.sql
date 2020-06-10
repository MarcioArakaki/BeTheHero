CREATE TABLE Ong (
    id int identity PRIMARY KEY,
    name varchar(100) not null,
    email varchar(100) not  null,
	contact varchar(100) not null,
	city varchar(100) not null,
	uf varchar(2) not  null
);
