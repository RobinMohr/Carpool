USE [CarpoolApp]
GO

Update Carpools
Set CarpoolDriverID = 1

SELECT * FROM Carpools
WHILE
INSERT INTO [dbo].[Carpools]
            ([Password]
			,[CarpoolDriverID]
			,[Origin]
			,[Destination]
		   ,[FreeSpaces]
		   ,[DepartmentTime])
		   OUTPUT inserted.[CarpoolID]
     VALUES(
	 'asdasf',
	 0,
	 'Weikersheim',
	 'Tauberbischofsheim',
	 3,
	 GETDATE())
	 

	 INSERT INTO Carpools (Password, CarpoolDriverID, Origin, Destination, FreeSpaces, DepartmentTime) OUTPUT inserted.CarpoolID VALUES ('passord', 0, 'origin', 'Destination', 2, GetDate())
GO

create table TableWithIdentity
           ( IdentityColumnName int identity(1, 1) not null primary key)

-- type of this table's column must match the type of the-- identity column of the table you'll be inserting intodeclare @IdentityOutput table ( ID int )



