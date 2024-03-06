create database C#3_ASM;
use C#3_ASM;
go
create table Student(
	MASV nvarchar(50) constraint PK_MSV primary key,
	HoTen nvarchar(50),
	Email nvarchar(50),
	SDT nvarchar(15),
	Gioitinh bit,
	DiaChi nvarchar(50),
	Hinh nvarchar(50),
);
create table Grade(
	ID int IDENTITY constraint PK_ID primary key ,
	MASV nvarchar(50) constraint FK_MSV foreign key (MASV) references Student(MASV),
	TiengAnh float,
	TinHoc float,
	GDTC float
);
create table Users(
	UserName nvarchar(50) constraint PK_UN primary key,
	PassWord nvarchar(50),
	Role nvarchar(50)
);

go

insert into Users values('qlsv001','123456','DT'),
('qld001','123456','GV');

SELECT Role FROM Users where UserName ='qlsv001' and PassWord = '123456';
go

insert into Student values('sv001',N'Trần Văn B','bvt@gmail.com',090912312,0,N'HCM',' ');
insert into Grade values ('sv001',5.7,6.3,7.3);

--select gr.MASV,
--		gr.TiengAnh,
--		gr.TinHoc,
--		gr.GDTC,
--		st.HoTen
--from Grade gr
--join Student st
--on st.MASV = gr.MASV
--where gr.MASV like 'sv001';

select gr.MASV,
		gr.TiengAnh,
		gr.TinHoc,
		gr.GDTC,
		st.HoTen 
from Grade gr
join Student st
on gr.MASV = st.MASV

SELECT
    MASV,
    HoTen,
    CASE WHEN Gioitinh = 0 THEN 'Nam' ELSE 'Nữ' END AS GioiTinh
FROM
    Student;
update Student set Hinh= 'ava2.jpg' where MASV = 'sv001';

insert into Student values('sv003',N'Trần Thị T','ttt@gmail.com',0909438768,1,N'Hà Nội','ava1.jpg'),
('sv004',N'Nguyễn Văn C','nvc@gmail.com',090974562,0,N'Đồng Nai','ava5.jpg'),
('sv005',N'Lê Văn Z','lvz@gmail.com',0909235358,1,N'Biên Hòa','ava4.jpg'),
('sv006',N'Nguyễn Hữu T','nht@gmail.com',0909845789,0,N'HCM','ava6.jpg');

insert into Grade values ('sv003',5.5,7.9,8.3),
('sv004',5,5.5,7.3),
('sv005',6.2,6.3,7.8),
('sv006',6.5,8.2,7.3);
