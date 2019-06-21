use [AnnouncementsPortalDB]
go
-- Engineering --
execute dbo.InsertDepartmentIfNotExists '8AEAED35-330A-4B90-8E8C-AE5F78799C07', N'مکانیک', 1;
execute dbo.InsertDepartmentIfNotExists 'F6E404D2-750B-420B-90CA-01E646EA9B7A', N'برق', 1;
execute dbo.InsertDepartmentIfNotExists 'D3F18430-0AFC-4215-AA2C-7DEF1E864FF2', N'عمران', 1;
execute dbo.InsertDepartmentIfNotExists '4F6D23A9-C45B-469F-B616-1AE913A601F1', N'نقشه برداری', 1;
execute dbo.InsertDepartmentIfNotExists 'D69B8E86-CFBC-488F-B95D-EAC171E37552', N'معدن', 1;
execute dbo.InsertDepartmentIfNotExists '4A6513D0-DEBE-44A3-B362-7FD53E559641', N'کامپیوتر', 1;
execute dbo.InsertDepartmentIfNotExists '5034B869-72EB-4D5B-BD07-9A3F94FFB7BB' , N'معماری', 1;
execute dbo.InsertDepartmentIfNotExists 'FB31DF68-9E52-4FEC-9E1A-1C1B7D89A547' , N'شیمی', 1;
execute dbo.InsertDepartmentIfNotExists '7A24AF00-0651-4558-8B8C-72FFDB434852' , N'مواد', 1;
execute dbo.InsertDepartmentIfNotExists '62E90831-57A6-4A93-9AF6-2CBB3F07F3D3' , N'صنایع', 1;
execute dbo.InsertDepartmentIfNotExists 'BB5CD3DE-F2B0-4A5E-8738-D91757BD0627' , N'هنر', 1;


-- Agricultire
execute dbo.InsertDepartmentIfNotExists 'CF47CD2C-1510-444D-B60E-35F3B539F889', N'ترویج، ارتباطات و توسعه روستایی' , 2;
execute dbo.InsertDepartmentIfNotExists '8E2ACC90-7E27-4A3E-A0AE-7FF2CBB4A3F5', N'مهندسی تولید و ژنتیک گیاهی' , 2;
execute dbo.InsertDepartmentIfNotExists '47F2A03E-8480-4388-98B4-38F377C411BB', N'علوم دامي' , 2;
execute dbo.InsertDepartmentIfNotExists 'B3F5F9F3-578D-42FB-BD70-DD1C6CC894DA', N'صنايع غذايي' , 2;
execute dbo.InsertDepartmentIfNotExists '7CD2B752-7BF1-4918-AB72-D3B30EC2C127', N'علوم باغباني' , 2;
execute dbo.InsertDepartmentIfNotExists '26917BDE-5C32-427F-8252-F71E59A0A40E' , N'علوم و مهندسي آب', 2;
execute dbo.InsertDepartmentIfNotExists 'D3A68650-74BF-4C4A-96BF-666D64692302' , N'گیاهپزشکی', 2;
execute dbo.InsertDepartmentIfNotExists '16769FD5-40AB-4463-88D7-A34131B8CB21', N'علوم و مهندسی خاک خاک شناسی', 2;
execute dbo.InsertDepartmentIfNotExists '0505D100-15E0-4ECE-ACC1-FB8751D52775', N'زراعت و اصلاح نباتات', 2;

-- Science
execute dbo.InsertDepartmentIfNotExists 'F5A1D948-E5C9-4D59-9736-5C2F140CD11F', N'فيزيک', 3;
execute dbo.InsertDepartmentIfNotExists 'C4057AC7-D839-4667-9EE4-68A1784910E2', N'رياضي', 3;
execute dbo.InsertDepartmentIfNotExists '63E5481A-6621-4DA4-82FC-10B0A72F4EF1', N'شيمي', 3;
execute dbo.InsertDepartmentIfNotExists 'FEC45349-AD79-45F9-A6BC-D97BF6D5127F', N'زمين شناسي', 3;
execute dbo.InsertDepartmentIfNotExists 'CE78E7D8-BDC6-4DAB-922F-33D294AD5ADF', N'زيست شناسي', 3;
execute dbo.InsertDepartmentIfNotExists 'F649B535-08A5-47A5-9EC2-CCF5A3A49367', N'آمار', 3;
execute dbo.InsertDepartmentIfNotExists 'AE925619-E6E4-434B-B0CD-475A5FE141A9', N'علوم محيط زيست', 3;


-- Humanities

execute dbo.InsertDepartmentIfNotExists '3B14CF5D-AB84-488C-AD3C-9D2849B42791', N'اقتصاد', 4;
execute dbo.InsertDepartmentIfNotExists '716FB0D7-F752-441B-8A5E-F44CF7699255', N'جغرافيا', 4;
execute dbo.InsertDepartmentIfNotExists '4B0D5332-1269-44D0-8749-CC5CE7A55C51', N' علوم ورزشي', 4;
execute dbo.InsertDepartmentIfNotExists 'A469E865-106A-40F2-8D5C-7264515328AC', N'حقوق', 4;
execute dbo.InsertDepartmentIfNotExists 'B65A28B2-D334-4B64-BCDD-7206540FCBCA', N'تاریخ تمدن ملل اسلامی', 4;
execute dbo.InsertDepartmentIfNotExists '2B37E704-9965-4538-886E-72E9AD6E19A8', N'مدیریت/ حسابداری', 4;
execute dbo.InsertDepartmentIfNotExists 'C8ED6D39-E3E7-4AF6-B817-E29F5712B3F1', N'فلسفه', 4;
execute dbo.InsertDepartmentIfNotExists 'C88D5AC6-73C2-4B18-A031-E336CBAAA6A5', N'زبان و ادبیات فارسی', 4;
execute dbo.InsertDepartmentIfNotExists 'C46FA675-C988-42D0-A7F9-8C8B1C52B8D5', N'معارف اسلامی', 4;
execute dbo.InsertDepartmentIfNotExists '314F844C-4CBB-4AB6-99D9-1066EE6B4AFA', N'روانشناسی', 4;
execute dbo.InsertDepartmentIfNotExists '71BB8526-1E27-411D-B122-5B28437C9AD5', N'زبان و ادبیات انگلیسی', 4;
execute dbo.InsertDepartmentIfNotExists '964F4B82-8E91-4445-BC24-881E7EDC9318', N'مدیریت / حسابداری', 4;
execute dbo.InsertDepartmentIfNotExists 'E3151E25-AB15-4AD3-B491-FE02B0324BE1', N'الهيات - تاريخ و تمدن ملل اسلامی', 4;

