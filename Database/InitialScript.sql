-- Make sure not using other db's
USE master
GO

ALTER DATABASE NannyDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

-- Drop the existing db if it already exists
DROP DATABASE IF EXISTS NannyDB

-- Create new database
CREATE DATABASE NannyDB
GO

USE NannyDB
GO

CREATE TABLE Address (
	AddressID INT IDENTITY PRIMARY KEY,
	Address1 VARCHAR(200) NOT NULL,
	Address2 VARCHAR(200),
	Address3 VARCHAR(200),
	Address4 VARCHAR(200),
	Locality VARCHAR(200) NOT NULL,
	Region VARCHAR(200) NOT NULL,
	PostalCode VARCHAR(10),
	County VARCHAR(60),
	Country VARCHAR(60) NOT NULL,
)

CREATE TABLE Role (
	RoleID INT IDENTITY PRIMARY KEY,
	RoleName VARCHAR(10)
)

CREATE TABLE ApplicationUser (
	UserID INT IDENTITY PRIMARY KEY,
	FirstName VARCHAR(80) NOT NULL,
	LastName VARCHAR(80) NOT NULL,
	UserName VARCHAR(80) NOT NULL,
	EmailAddress VARCHAR(120) NOT NULL,
	Password NVARCHAR (250) NOT NULL,
	PhoneNumber VARCHAR(20) NOT NULL,
	Salt NVARCHAR(20) NOT NULL,
	RoleID INT NOT NULL,
	IsVerified BIT NOT NULL
	CONSTRAINT fk_Role_to_User FOREIGN KEY (RoleID) REFERENCES Role(RoleID)
)

CREATE TABLE UserAddress (
	UserID INT NOT NULL,
	AddressID INT NOT NULL,
	CONSTRAINT pk_UserAddress PRIMARY KEY (UserID, AddressID),
	CONSTRAINT fk_User_to_Address FOREIGN KEY (UserID) REFERENCES ApplicationUser(UserID),
	CONSTRAINT fk_Address_to_User FOREIGN KEY (AddressID) REFERENCES Address(AddressID)
)

CREATE TABLE Gender (
	GenderID INT IDENTITY PRIMARY KEY NOT NULL,
	GenderName VARCHAR(10),
	GenderCode VARCHAR(1)
)

CREATE TABLE Child (
	ChildID INT IDENTITY PRIMARY KEY,
	FirstName VARCHAR(80) NOT NULL,
	LastName VARCHAR(80) NOT NULL,
	GenderID INT NOT NULL,
	DateOfBirth DATETIMEOFFSET(7),
	RatePerHour DECIMAL(9,4) NOT NULL,
	NeedsDiapers BIT NOT NULL,
	Active BIT NOT NULL,
	ImageURL VARCHAR(100),
	CONSTRAINT fk_Child_Gender FOREIGN KEY (GenderID) REFERENCES Gender(GenderID)
)

CREATE TABLE ChildUser (
	ChildID INT NOT NULL,
	UserID INT NOT NULL,
	CONSTRAINT pk_Child_User PRIMARY KEY (ChildID, UserID),
	CONSTRAINT fk_Child_to_User FOREIGN KEY (ChildID) REFERENCES Child(ChildID),
	CONSTRAINT fk_User_to_Child FOREIGN KEY (UserID) REFERENCES ApplicationUser(UserID)
)

CREATE TABLE Session (
	SessionID INT IDENTITY PRIMARY KEY,
	ChildID INT NOT NULL,
	DropOff DATETIMEOFFSET(7) NOT NULL,
	PickUp DATETIMEOFFSET(7) NOT NULL,
	Notes TEXT
	CONSTRAINT fk_session_child FOREIGN KEY (ChildID) REFERENCES Child(ChildID)
)

CREATE TABLE SessionUser (
	SessionID INT NOT NULL,
	UserID INT NOT NULL
	CONSTRAINT pk_Session_User PRIMARY KEY (SessionID, UserID),
	CONSTRAINT fk_Session_to_User FOREIGN KEY (SessionID) REFERENCES session(SessionID),
	CONSTRAINT fk_User_to_Session FOREIGN KEY (UserID) REFERENCES ApplicationUser(UserID)
)

CREATE TABLE Diaper (
	DiaperID INT IDENTITY PRIMARY KEY,
	SessionID INT NOT NULL,
	ChangeTime DATETIMEOFFSET(7) NOT NULL,
	Notes TEXT
	CONSTRAINT fk_diaper_session FOREIGN KEY (SessionID) REFERENCES Session(SessionID)
)

CREATE TABLE Meal (
	MealID INT IDENTITY PRIMARY KEY,
	SessionID INT NOT NULL,
	MealTime DATETIMEOFFSET(7) NOT NULL,
	MealType VARCHAR(20),
	Notes TEXT
	CONSTRAINT fk_meal_session FOREIGN KEY (SessionID) REFERENCES Session(SessionID)
)

CREATE TABLE Nap (
	NapID INT IDENTITY PRIMARY KEY,
	SessionID INT NOT NULL,
	StartTime DATETIME,
	EndTime DATETIME,
	Notes TEXT
	CONSTRAINT fk_nap_session FOREIGN KEY (SessionID) REFERENCES Session(SessionID)
)


-- Creates the table for allergy types
CREATE TABLE AllergyType (
	AllergyTypeID INT IDENTITY PRIMARY KEY,
	AllergyTypeName VARCHAR(80) NOT NULL
)

-- Creates the table for an allergy
CREATE TABLE Allergy (
	AllergyID INT IDENTITY PRIMARY KEY,
	AllergyName VARCHAR(80) NOT NULL,
	AllergyTypeID INT NOT NULL,
	CONSTRAINT fk_allergy_allergy_type FOREIGN KEY (AllergyTypeID) REFERENCES AllergyType(AllergyTypeID)
)

-- Creates the table that references the allergy to the child
CREATE TABLE ChildAllergy (
	ChildID INT NOT NULL,
	AllergyID INT NOT NULL,
	CONSTRAINT pk_child_allergies PRIMARY KEY (ChildID, AllergyID),
	CONSTRAINT fk_child_to_allergy FOREIGN KEY (ChildID) REFERENCES Child(ChildID),
	CONSTRAINT fk_allergy_to_child FOREIGN KEY (AllergyID) REFERENCES Allergy(AllergyID)
);

GO