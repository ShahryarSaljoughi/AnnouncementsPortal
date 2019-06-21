create procedure InsertDepartmentIfNotExists @rowId uniqueIdentifier, @PersianName nvarchar(max), @collegeId int
as
	if exists (select * from AP.Department where Id = @rowId)
		begin
			update AP.Department
			set PersianName = @PersianName,
				College = @collegeId
			where Id = @rowId;
		end
	else
		begin
			insert into AP.Department (Departmane.Id, Department.PersianName, Department.College) values(@rowId, @PersianName, @collegeId);
		end