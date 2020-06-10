CREATE TABLE Incident (
    id int identity,
	title varchar(255)  not null,
    description varchar(255) not null,
    value decimal not  null,
	ongId int not null FOREIGN KEY REFERENCES Ong(id),
);
