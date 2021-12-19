
if exists (select name from sysdatabases where name = N'HyperGear')
drop database HyperGear
go
CREATE DATABASE HyperGear
go
USE HyperGear
go

CREATE TABLE KhachHang
(
	MaKH INT IDENTITY(1,1) NOT NULL,
	HoTen nVarchar(100) NOT NULL,
	Taikhoan Varchar(100) UNIQUE,
	Matkhau Varchar(100) NOT NULL,
	Email Varchar(100) UNIQUE,
	DiachiKH nVarchar(200),
	DienthoaiKH Varchar(50),	
	Ngaysinh DATETIME,
	CONSTRAINT PK_Khachhang PRIMARY KEY(MaKH)
)
GO

Create Table Menu
(
	MaMn int Identity(1,1) NOT NULL,
	TenMn nvarchar(255) NOT NULL,
	Slug NVARCHAR(MAX) NOT NULL,
	Created_at datetime NULL,
	Created_by int NULL,
	Updated_at datetime NULL,
	Updated_by int NULL,
	Status int NULL,
	CONSTRAINT PK_Menu PRIMARY KEY(MaMn)
)
GO

Create Table DanhMuc
(
	MaDM int Identity(1,1) NOT NULL,
	TenDM nvarchar(255) NOT NULL,
	Slug NVARCHAR(MAX) NOT NULL,
	Created_at datetime NULL,
	Created_by int NULL,
	Updated_at datetime NULL,
	Updated_by int NULL,
	MaMn int,
	Status int NULL,
	CONSTRAINT PK_DanhMuc PRIMARY KEY(MaDM),
	constraint Fk_MaMn foreign key(MaMn) references Menu(MaMn),
)
GO

/*ALTER TABLE DanhMuc Add MaMn int ;*/
/*ALTER TABLE DanhMuc
Add constraint Fk_MaMn
foreign key(MaMn) references Menu(MaMn);*/
/*ALTER TABLE DanhMuc ALTER COLUMN TenDM NVARCHAR (255) NOT NULL;*/

CREATE TABLE DonDatHang
(
	MaDonHang INT IDENTITY(1,1) NOT NULL,
	Dathanhtoan bit,
	Tinhtranggiaohang  bit,
	Ngaydat Datetime,
	Ngaygiao Datetime,	
	MaKH INT,
	CONSTRAINT PK_DonDH PRIMARY KEY (MaDonHang),
	CONSTRAINT FK_Khachhang FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
)
GO
/*ALTER TABLE DonDatHang ADD DeliveryName nvarchar(max) NULL;
ALTER TABLE DonDatHang ADD DeliveryPhone nvarchar(11) NULL;
ALTER TABLE DonDatHang ADD  DeliveryAdd nvarchar(max) NULL;
ALTER TABLE DonDatHang ADD  DeliveryEmail nvarchar(max) NULL;

ALTER TABLE DonDatHang Alter COLUMN Tinhtranggiaohang int NULL;*/

CREATE TABLE SanPham
(
	MaSP INT IDENTITY(1,1) NOT NULL,
	TenSP Nvarchar(MAX) NOT NULL,
	Gia Decimal(18,0) CHECK (Gia>=0),
	Slug nvarchar(MAX) NOT NULL,
	Mota NTEXT NULL,
	ChiTiet NTEXT NULL,
	Anh Text NULL,
	Ngaycapnhat DATETIME NULL,
	Soluongton INT NULL,
	MaDM INT,
	Created_at datetime NULL,
	Created_by nvarchar(255) NULL,
	Updated_at datetime NULL,
	Updated_by nvarchar(255) NULL,
	Status int NULL,

	Constraint PK_SanPham Primary Key(MaSP),
	Constraint FK_DanhMuc Foreign Key(MaDM) References DanhMuc(MaDM),
)
GO

/*ALTER TABLE SanPham ALTER COLUMN Anh nvarchar(max) NULL;*/


CREATE TABLE ChiTietDonHang
(
	MaDonHang INT,
	MaSP INT,
	Soluong Int Check(Soluong>0),
	Dongia Decimal(18,0) Check(Dongia>=0),	
	CONSTRAINT PK_CTDatHang PRIMARY KEY(MaDonHang,MaSP),
	CONSTRAINT FK_Donhang FOREIGN KEY (MaDonHang) REFERENCES DonDatHang(MaDonHang),
	CONSTRAINT FK_SP FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)	
)
GO

/*ALTER TABLE ChiTietDonHang Add TongTien Decimal(18,0) NUll;*/

CREATE TABLE Admin
(
	Id INT IDENTITY(1,1) NOT NULL,
	Username Varchar(255) UNIQUE,
	Password Varchar(50) NOT NULL,
	HoTen NVARCHAR(100) NOT NULL,
	Created_at datetime NULL,
	Created_by int NULL,
	Updated_at datetime NULL,
	Updated_by int NULL,
	Status int NULL,
	Constraint PK_ID Primary Key(ID)
)
GO


CREATE TABLE TinTuc
(
	MaTT INT IDENTITY(1,1) NOT NULL,
	TieuDe nvarchar(MAX) NOT NULL,
	Slug nvarchar(MAX) not null,
	NoiDung ntext NOT NULL,
	Anh nvarchar(MAX) NULL,
	Created_at datetime NULL,
	Created_by nvarchar(255) NULL,
	Updated_at datetime NULL,
	Updated_by nvarchar(255) NULL,
	Status int NULL,

	Constraint PK_TinTuc Primary Key(MaTT)
)
GO
/*ALTER TABLE TinTuc ALTER COLUMN Anh nvarchar(MAX) NULL;*/

CREATE TABLE LienHe
(
	MaLH INT IDENTITY(1,1) NOT NULL,
	TenCH Nvarchar(255) ,
	NoiDung ntext ,
	DienThoai VARCHAR(11),
	DiaChi ntext,
	Email VARCHAR(100),
	Created_at datetime NULL,
	Created_by int NULL,
	Updated_at datetime NULL,
	Updated_by int NULL,
	Status int NULL,

	Constraint PK_LienHe Primary Key(MaLH)
)
GO

/*ALTER TABLE LienHe ALTER COLUMN DiaChi ntext Null;*/

CREATE TABLE Feedback
(
	MaFb INT IDENTITY(1,1) NOT NULL,
	TenKH Nvarchar(255) ,
	Email VARCHAR(100),
	NoiDung ntext null,
	Reply ntext null,
	Status int null,
	Constraint PK_Feedback Primary Key(MaFb)
)
GO
--ALTER TABLE Feedback Add TieuDe nvarchar(max) Null;
--ALTER TABLE Feedback Add NgayNhan datetime Null;
--ALTER TABLE Feedback Add NgayGui datetime Null;