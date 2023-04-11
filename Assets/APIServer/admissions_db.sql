DROP DATABASE IF EXISTS admissions;

CREATE DATABASE admissions;

USE admissions;

CREATE TABLE College (
	cName VARCHAR(50) NOT NULL,
    city VARCHAR(30) NOT NULL,
    enrollment INT,
    CONSTRAINT `pk_college` PRIMARY KEY(cName)
);

CREATE TABLE Student (
	sID INT UNIQUE NOT NULL,
	sName VARCHAR(30) NOT NULL,
    GPA FIXED(3,2),
    CONSTRAINT `pk_student` PRIMARY KEY(sId)
);

CREATE TABLE Apply (
	sID INT NOT NULL,
    cName VARCHAR(50) NOT NULL,
    major VARCHAR(30),
    decision CHAR(1),
    CONSTRAINT `pk_apply` PRIMARY KEY(sID, cName, major),
    CONSTRAINT `fk_apply_sId` FOREIGN KEY (sID) REFERENCES Student(sID),
    CONSTRAINT `fk_apply_cName` FOREIGN KEY (cName) REFERENCES College(cName)
);

INSERT INTO College VALUES
ROW("De La Salle University", "Manila", 16000),
ROW("University of the Philippines Diliman", "Quezon", 24000),
ROW("Ateneo de Manila", "Manila", 15200),
ROW("Mindanao State University", "Marawi", 45000);

INSERT INTO Student VALUES
ROW(123, "Amy", 3.9),
ROW(234, "Bob", 3.6),
ROW(345, "Craig", 3.5),
ROW(456, "Doris", 3.9),
ROW(567, "Edward", 2.9),
ROW(678, "Fay", 3.8),
ROW(789, "Gary", 3.4),
ROW(987, "Helen", 3.7),
ROW(876, "Irene", 3.9),
ROW(765, "Jay", 2.9),
ROW(654, "Amy", 3.9),
ROW(543, "Craig", 3.4);
 
INSERT INTO Apply VALUES
ROW(123, "De La Salle University", "CS", "Y"),
ROW(123, "De La Salle University", "EE", "N"),
ROW(123, "Ateneo de Manila", "CS", "Y"),
ROW(123, "University of the Philippines Diliman", "EE", "Y"),	
ROW(234, "Ateneo De Manila", "Biology", "N"),
ROW(345, "Mindanao State University", "BioEng", "Y"),
ROW(345, "University of the Philippines Diliman", "BioEng", "N"),
ROW(345, "University of the Philippines Diliman", "CS", "Y"),
ROW(345, "University of the Philippines Diliman", "EE", "N"),
ROW(678, "De La Salle University", "History", "Y"),
ROW(987, "De La Salle University", "CS", "Y"),
ROW(987, "Ateneo de Manila", "CS", "Y"),
ROW(876, "De La Salle University", "CS", "N"),
ROW(876, "Mindanao State University", "Biology", "Y"),
ROW(876, "Mindanao State University", "MarineBio", "N"),
ROW(765, "De La Salle University", "History", "Y"),
ROW(765, "University of the Philippines Diliman", "History", "N"),
ROW(765, "University of the Philippines Diliman", "Psych", "Y"),
ROW(543, "Mindanao State University", "CS", "N");
