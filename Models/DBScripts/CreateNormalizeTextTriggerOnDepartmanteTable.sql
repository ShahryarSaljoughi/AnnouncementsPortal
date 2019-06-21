use AnnouncementsPortalDB
go
create trigger NormalizeText
on AnnouncementsPortalDB.AP.Department
after insert, update
as
	begin
		declare @insertedName nvarchar(max);
		select @insertedName = PersianName from inserted;
		declare @edited nvarchar(max);
		select @edited = REPLACE(@insertedName, N'ي', N'ی')
		update Department 
			set PersianName = @edited
			where Id = (select Id from inserted)
	end