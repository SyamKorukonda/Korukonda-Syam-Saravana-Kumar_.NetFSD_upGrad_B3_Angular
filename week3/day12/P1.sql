CREATE DATABASE EventDb ;
use EventDb ;

create table UserInfo(
EmailId varchar(50) primary key,
UserName varchar(50) not null check(len(UserName) between 1 and 50),
role varchar(20) not null check(role in('Admin','Participant')),
password varchar(20) not null check(len(password) between 6 and 20));

create table EventDetails(
    EventId INT PRIMARY KEY,
    EventName VARCHAR(50) NOT NULL CHECK (LEN(EventName) BETWEEN 1 AND 50),
    EventCategory VARCHAR(50) NOT NULL CHECK (LEN(EventCategory) BETWEEN 1 AND 50),
	EventDate DATETIME NOT NULL,
	Description VARCHAR(100) NULL,
	Status VARCHAR(10) CHECK(Status in('Active','In-Active')));

create table SpeakersDetails(
SpeakerId INT PRIMARY KEY,
SpeakerName VARCHAR(50) NOT NULL CHECK (LEN(SpeakerName) BETWEEN 1 AND 50));

create table SessionInfo(
SessionId INT PRIMARY KEY,
EventId INT NOT NULL,
SessionTitle VARCHAR(50) NOT NULL CHECK (LEN(SessionTitle) BETWEEN 1 AND 50),
SpeakerId INT NOT NULL,
Description varchar(100) NULL,
SessionStart DATETIME NOT NULL,
SessionEnd datetime not null,
SessionUrl varchar(50),
foreign key(EventId) references EventDetails(EventId),
FOREIGN KEY (SpeakerId) REFERENCES SpeakersDetails(SpeakerId));

CREATE TABLE ParticipantEventDetails (
    Id INT PRIMARY KEY,
    ParticipantEmailId VARCHAR(50) NOT NULL,
    EventId INT NOT NULL,
    SessionId INT NOT NULL,
    IsAttended BIT NOT NULL,
    FOREIGN KEY (ParticipantEmailId) REFERENCES UserInfo(EmailId),
	FOREIGN KEY (EventId) REFERENCES EventDetails(EventId),
	FOREIGN KEY (SessionId) REFERENCES SessionInfo(SessionId));

INSERT INTO UserInfo VALUES
('admin@gmail.com','AdminUser','Admin','admin123'),
('user1@gmail.com','Ravi','Participant','user123');

INSERT INTO EventDetails VALUES
(1,'AI Conference','Technology','2026-04-10','AI & ML Event','Active');

INSERT INTO SpeakersDetails VALUES
(1,'Dr. Sharma');

INSERT INTO SessionInfo VALUES
(1,1,'Deep Learning Basics',1,'Intro Session',
 '2026-04-10 10:00:00',
 '2026-04-10 11:00:00',
 'https://meetlink.com');

INSERT INTO ParticipantEventDetails VALUES
(1,'user1@gmail.com',1,1,1);

select * from UserInfo;

select * from EventDetails;

select * from SpeakersDetails;

select * from SessionInfo;

select * from ParticipantEventDetails;
