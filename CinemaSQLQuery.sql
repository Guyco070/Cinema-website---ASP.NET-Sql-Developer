USE model
GO

CREATE TABLE tblUsers (
	firstName nvarchar(50),
	lastName nvarchar(50),
	idNumber nvarchar(10),
	uPassword nvarchar(10),
	uType nvarchar(10),
	PRIMARY KEY(idNumber)
);



insert tblUsers values('Guy','Cohen','205579808','12345678','admin');

USE model
GO
select * from tblMovies;

insert tblUsers values('Yarin','Avraham','208401166','123456','admin');
insert tblUsers values('Yona','Hen','12345678','111111','user');


USE model
GO
drop table tblTickets;
drop table tblMovies;
drop table tblHalls;


USE model
GO

CREATE TABLE tblHalls  (
	hallId NVARCHAR(4) NOT NULL CHECK (hallId IN ('VIP','1','2','3')),
	seats int default 20 NOT NULL,
    PRIMARY KEY(hallId));

CREATE TABLE tblMovies  (
movieName NVARCHAR(100),
movieDate NVARCHAR(100),
movieTime NVARCHAR(100),
cost INT  NOT NULL,
hall NVARCHAR(4) NOT NULL CHECK (hall IN ('VIP','1','2','3')),
movieImg NVARCHAR(400) default 'https://nrb.org/files/3315/7367/4295/film-and-board-1000.jpg',
category NVARCHAR(100),
minAge INT default 18,
popularity int default 0,
sale INT default 0,
newCost float default 0,
duration int default 0,
durationDisplay NVARCHAR(15) default '',
--FOREIGN KEY (hall) REFERENCES tblHalls(hallId),
PRIMARY KEY(movieDate,movieTime,hall));

CREATE TABLE tblTickets  (
    idNumber NVARCHAR(10),
    movieName NVARCHAR(100),
    movieDate NVARCHAR(100),
	movieTime NVARCHAR(100),
    cost INT  NOT NULL,
	hall NVARCHAR(4) NOT NULL CHECK (hall IN ('VIP','1','2','3')),
	seat INT  NOT NULL,
	token int default 0,
    PRIMARY KEY(movieDate, movieTime,hall,seat),
	--FOREIGN KEY (hall) REFERENCES tblHalls(hallId),
    FOREIGN KEY (movieDate,movieTime,hall) REFERENCES tblMovies(movieDate,movieTime,hall));


insert tblHalls values('VIP',20);
insert tblHalls values('1',30);
insert tblHalls values('2',35);
insert tblHalls values('3',40);

insert tblMovies values('Spider-Man 3','16.7.2021','22:00','70','VIP','https://cosmicbook.news/images/spider-man-spiderverse-fan-art.jpg','Action, Adventure, Fantasy',16,0,0,0,120,'2h');
insert tblMovies values('Hagiga Basnoker','26.5.2021','20:00','30','3','https://upload.wikimedia.org/wikipedia/he/b/b3/Snuker_kraza.jpg','Comedy',18,0,0,0,120,'2h');
insert tblMovies values('Wonder Woman 2 (1984)','26.5.2021','21:00','30','1','https://i.pinimg.com/originals/ce/e9/c9/cee9c9b3f9764ec4efca30b3e0d7e356.jpg','Action, Adventure, Fantasy',16,0,10,0,120,'2h');
insert tblMovies values('Cinderella ','28.5.2021','16:00','40','VIP','https://miro.medium.com/max/1200/1*nE92UEnu-A2oJx_gxTQSVg.jpeg','Comedy, Family, Fantasy',14,0,0,0,120,'2h');
insert tblMovies values('Tom and Jerry ','25.1.2021','18:00','20','2','https://upload.wikimedia.org/wikipedia/he/thumb/f/fe/Tom_%26_Jerry_2021.jpg/220px-Tom_%26_Jerry_2021.jpg','Animation, Adventure, Comedy',10,0,0,0,120,'2h');
insert tblMovies values('Tom and Jerry ','26.1.2021','18:00','20','2','https://upload.wikimedia.org/wikipedia/he/thumb/f/fe/Tom_%26_Jerry_2021.jpg/220px-Tom_%26_Jerry_2021.jpg','Animation, Adventure, Comedy',10,0,0,0,120,'2h');
insert tblMovies values('Tom and Jerry ','26.1.2021','19:00','20','2','https://upload.wikimedia.org/wikipedia/he/thumb/f/fe/Tom_%26_Jerry_2021.jpg/220px-Tom_%26_Jerry_2021.jpg','Animation, Adventure, Comedy',10,0,0,0,120,'2h');
insert tblMovies values('Tom and Jerry ','23.1.2021','19:00','20','2','https://upload.wikimedia.org/wikipedia/he/thumb/f/fe/Tom_%26_Jerry_2021.jpg/220px-Tom_%26_Jerry_2021.jpg','Animation, Adventure, Comedy',10,0,0,0,120,'2h');
insert tblMovies values('Avatar','29.9.2021','22:00','70','VIP','https://www.joblo.com/assets/images/oldsite/posters/images/full/avatar-french-poster.jpg','Action',16,0,50,0,120,'2h');
insert tblMovies values('Enola Holmes','26.5.2021','21:00','45','3','https://upload.wikimedia.org/wikipedia/he/thumb/0/01/Enola_Holmes_2020.jpg/404px-Enola_Holmes_2020.jpg','Action',12,0,30,0,120,'2h');

insert tblTickets values('205579808','Spider-Man 3','16.7.2021','22:00','70','VIP',3,2);
insert tblTickets values('12345678','Hagiga Basnoker','26.5.2021','20:00','30','3',12,2);
insert tblTickets values('12345678','Wonder Woman 2 (1984)','26.5.2021','21:00','30','1',13,2);
insert tblTickets values('208401166','Cinderella ','28.5.2021','16:00','40','VIP',20,2);
insert tblTickets values('205579808','Tom and Jerry ','25.1.2021','18:00','20','2',12,2);
insert tblTickets values('12345678','Tom and Jerry ','26.1.2021','18:00','20','2',26,2);
insert tblTickets values('12345678','Tom and Jerry ','26.1.2021','18:00','20','2',27,2);
insert tblTickets values('12345678','Tom and Jerry ','26.1.2021','19:00','20','2',26,2);
insert tblTickets values('12345678','Tom and Jerry ','26.1.2021','19:00','20','2',27,2);

USE model
GO
select * from tblTickets;
select * from tblMovies;