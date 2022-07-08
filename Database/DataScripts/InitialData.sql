-- Insert User Roles
INSERT INTO Role (RoleName) VALUES ('Caretaker'), ('Parent'), ('Admin')

-- Insert users
INSERT INTO ApplicationUser (FirstName, LastName, UserName, EmailAddress, Password, PhoneNumber, Salt, IsVerified)
 Values ('Jason', 'Test', 'Admin', 'jason@jason.com', 'ZRxBa7n1wdGcxyPsTmpCjCZ0AWs=', '555-555-5555', 'YsHl2Z3eNWI=', 1)
 ,('Ruth', 'Test', 'Rudi', 'ruth@ruth.com', 'ZRxBa7n1wdGcxyPsTmpCjCZ0AWs=', '555-555-55555', 'YsHl2Z3eNWI=', 0)
 ,('Megan', 'Test', 'Meg', 'megan@megan.com', 'ZRxBa7n1wdGcxyPsTmpCjCZ0AWs=', '555-555-5555', 'YsHl2Z3eNWI=', 0)

INSERT INTO UserRole (UserID, RoleID)
	VALUES ((SELECT UserID FROM ApplicationUser WHERE UserName = 'Admin'), (SELECT RoleID FROM Role WHERE RoleName = 'Admin'))
	,((SELECT UserID FROM ApplicationUser WHERE UserName = 'Rudi'), (SELECT RoleID FROM Role WHERE RoleName = 'Caretaker'))
	,((SELECT UserID FROM ApplicationUser WHERE UserName = 'Meg'), (SELECT RoleID FROM Role WHERE RoleName = 'Parent'))

 INSERT INTO Address (Address1, Locality, Region, PostalCode, County, Country)
 VALUES ('1397 Old House Drive', 'Milledgeville', 'OH', '43142', 'Fayette', 'United States Of America')
 ,('1951 Olive Street', 'Elida', 'OH', '45807', 'Allen', 'United States Of America')
 ,('4344 Horner Street', 'Youngstown', 'OH', '44503', 'Mahoning', 'United States Of America')

 INSERT INTO UserAddress (UserID, AddressID)
 VALUES ((SELECT UserID FROM ApplicationUser WHERE UserName = 'Admin'), (SELECT AddressID FROM Address WHERE Address1 = '1397 Old House Drive'))
,((SELECT UserID FROM ApplicationUser WHERE UserName = 'Rudi'), (SELECT AddressID FROM Address WHERE Address1 = '1951 Olive Street'))
,((SELECT UserID FROM ApplicationUser WHERE UserName = 'Meg'), (SELECT AddressID FROM Address WHERE Address1 = '4344 Horner Street'))
,((SELECT UserID FROM ApplicationUser WHERE UserName = 'Admin'), (SELECT AddressID FROM Address WHERE Address1 = '1951 Olive Street'))

INSERT INTO Gender (GenderCode, GenderName)
	VALUES ('M', 'Male')
	,('F', 'Female')
	,('N', 'Nonbinary')
	,('O', 'Other')

-- Add children
INSERT INTO Child (FirstName, LastName, GenderID, DateOfBirth, RatePerHour, NeedsDiapers, Active)
	VALUES ('Ellie', 'Test', 2, '2018-09-25', 10, 0, 1)
DECLARE @ChildID INT
SET @ChildID = @@IDENTITY

INSERT INTO ChildUser (UserID, ChildID)
	VALUES ((SELECT UserID FROM ApplicationUser WHERE UserName = 'Rudi'), @ChildID)
	,((SELECT UserID FROM ApplicationUser WHERE UserName = 'Meg'), @ChildID)

INSERT INTO Image (ImageURL)
	VALUES ('https://media.istockphoto.com/photos/frustrated-young-child-sulking-with-crossed-arms-and-dirty-look-picture-id475710000?k=20&m=475710000&s=612x612&w=0&h=-XJ5IojQ5cFlYtUPbC5SNWal5eqzE3DRiBDf2hljfO0=')
DECLARE @ImageID INT
SET @ImageID = @@IDENTITY

INSERT INTO ImageChild (ImageID, ChildID)
	VALUES (@ImageID, @ChildID)

-- Insert common types of allergies
INSERT INTO AllergyType (AllergyTypeName) VALUES ('Drug');
INSERT INTO AllergyType (AllergyTypeName) VALUES ('Food');
INSERT INTO AllergyType (AllergyTypeName) VALUES ('Insect');
INSERT INTO AllergyType (AllergyTypeName) VALUES ('Mold');
INSERT INTO AllergyType (AllergyTypeName) VALUES ('Pet');
INSERT INTO AllergyType (AllergyTypeName) VALUES ('Pollen');
INSERT INTO AllergyType (AllergyTypeName) VALUES ('Other');

-- Insert common allergies
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Penicillin', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Drug'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Amoxicillin', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Drug'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Tetracycline', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Drug'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Ibuprofen', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Drug'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Naproxen', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Drug'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Asprin', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Drug'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Cetuximab', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Drug'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Rituximab', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Drug'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Insulin', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Drug'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Carbamazepine', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Drug'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Lamotrigine', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Drug'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Phenytoin', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Drug'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Atracurium', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Drug'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Succinycholine', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Drug'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Vecuronium', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Drug'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Cow''s Milk', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Food'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Eggs', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Food'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Tree Nuts', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Food'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Peanuts', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Food'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Shellfish', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Food'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Fish', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Food'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Soy', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Food'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Wheat', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Food'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Linseed', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Food'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Sesame Seed', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Food'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Peach', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Food'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Banana', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Food'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Avodcado', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Food'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Kiwi', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Food'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Passion Fruit', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Food'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Celery', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Food'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Garlic', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Food'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Mustard Seed', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Food'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Aniseed', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Food'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Chamomile', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Food'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Bees', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Insect'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Wasps', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Insect'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Hornets', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Insect'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Yellow-Jackets', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Insect'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Fire Ants', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Insect'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Mosquitoes', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Insect'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Kissing Bugs', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Insect'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Bedbugs', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Insect'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Fleas', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Insect'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Flies', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Insect'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Cockroaches', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Insect'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Dust Mites', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Insect'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Aspergillus', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Mold'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Penicillium', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Mold'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Cladosporium', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Mold'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Alternaria', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Mold'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Stachybotrys', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Mold'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Cats', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pet'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Dogs', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pet'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Mice', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pet'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Gerbils', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pet'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Hamsters', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pet'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Guinea Pigs', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pet'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Rabits', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pet'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Birch', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pollen'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Oak', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pollen'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Grass', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pollen'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Ragweed', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pollen'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Elm', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pollen'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Cedar', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pollen'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Pine', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pollen'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Poplar', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pollen'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Walnut', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pollen'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Nettle', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pollen'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Sagebrush', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pollen'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Tumbleweed', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pollen'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Lamb''s Quarters', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pollen'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('English Plantain', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Pollen'));
INSERT INTO Allergy (AllergyName, AllergyTypeID) VALUES ('Latex', (SELECT AllergyTypeID FROM AllergyType WHERE AllergyTypeName = 'Other'));

GO