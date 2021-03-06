USE [HyperGear]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 20/7/2021 21:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NULL,
	[Password] [varchar](50) NOT NULL,
	[HoTen] [nvarchar](100) NOT NULL,
	[Created_at] [datetime] NULL,
	[Created_by] [int] NULL,
	[Updated_at] [datetime] NULL,
	[Updated_by] [int] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietDonHang]    Script Date: 20/7/2021 21:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDonHang](
	[MaDonHang] [int] NOT NULL,
	[MaSP] [int] NOT NULL,
	[Soluong] [int] NULL,
	[Dongia] [decimal](18, 0) NULL,
	[TongTien] [decimal](18, 0) NULL,
 CONSTRAINT [PK_CTDatHang] PRIMARY KEY CLUSTERED 
(
	[MaDonHang] ASC,
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DanhMuc]    Script Date: 20/7/2021 21:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DanhMuc](
	[MaDM] [int] IDENTITY(1,1) NOT NULL,
	[TenDM] [nvarchar](255) NOT NULL,
	[Slug] [nvarchar](max) NOT NULL,
	[Created_at] [datetime] NULL,
	[Created_by] [int] NULL,
	[Updated_at] [datetime] NULL,
	[Updated_by] [int] NULL,
	[Status] [int] NULL,
	[MaMn] [int] NULL,
 CONSTRAINT [PK_DanhMuc] PRIMARY KEY CLUSTERED 
(
	[MaDM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DonDatHang]    Script Date: 20/7/2021 21:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonDatHang](
	[MaDonHang] [int] IDENTITY(1,1) NOT NULL,
	[Dathanhtoan] [bit] NULL,
	[Tinhtranggiaohang] [int] NULL,
	[Ngaydat] [datetime] NULL,
	[Ngaygiao] [datetime] NULL,
	[MaKH] [int] NULL,
	[DeliveryName] [nvarchar](max) NULL,
	[DeliveryPhone] [nvarchar](11) NULL,
	[DeliveryAdd] [nvarchar](max) NULL,
	[DeliveryEmail] [nvarchar](max) NULL,
 CONSTRAINT [PK_DonDH] PRIMARY KEY CLUSTERED 
(
	[MaDonHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 20/7/2021 21:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[MaFb] [int] IDENTITY(1,1) NOT NULL,
	[TenKH] [nvarchar](255) NULL,
	[Email] [varchar](100) NULL,
	[NoiDung] [ntext] NULL,
	[Reply] [ntext] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[MaFb] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 20/7/2021 21:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[MaKH] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](100) NOT NULL,
	[Taikhoan] [varchar](100) NULL,
	[Matkhau] [varchar](100) NOT NULL,
	[Email] [varchar](100) NULL,
	[DiachiKH] [nvarchar](200) NULL,
	[DienthoaiKH] [varchar](50) NULL,
	[Ngaysinh] [datetime] NULL,
 CONSTRAINT [PK_Khachhang] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LienHe]    Script Date: 20/7/2021 21:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LienHe](
	[MaLH] [int] IDENTITY(1,1) NOT NULL,
	[TenCH] [varchar](255) NULL,
	[NoiDung] [ntext] NULL,
	[DienThoai] [varchar](11) NULL,
	[DiaChi] [ntext] NULL,
	[Email] [varchar](100) NULL,
	[Created_at] [datetime] NULL,
	[Created_by] [int] NULL,
	[Updated_at] [datetime] NULL,
	[Updated_by] [int] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_LienHe] PRIMARY KEY CLUSTERED 
(
	[MaLH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 20/7/2021 21:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[MaMn] [int] IDENTITY(1,1) NOT NULL,
	[TenMn] [nvarchar](255) NOT NULL,
	[Slug] [nvarchar](max) NOT NULL,
	[Created_at] [datetime] NULL,
	[Created_by] [int] NULL,
	[Updated_at] [datetime] NULL,
	[Updated_by] [int] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[MaMn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 20/7/2021 21:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSP] [int] IDENTITY(1,1) NOT NULL,
	[TenSP] [nvarchar](max) NOT NULL,
	[Gia] [decimal](18, 0) NULL,
	[Slug] [nvarchar](max) NOT NULL,
	[Mota] [ntext] NULL,
	[ChiTiet] [ntext] NULL,
	[Anh] [nvarchar](max) NULL,
	[Ngaycapnhat] [datetime] NULL,
	[Soluongton] [int] NULL,
	[MaDM] [int] NULL,
	[Created_at] [datetime] NULL,
	[Created_by] [nvarchar](255) NULL,
	[Updated_at] [datetime] NULL,
	[Updated_by] [nvarchar](255) NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TinTuc]    Script Date: 20/7/2021 21:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TinTuc](
	[MaTT] [int] IDENTITY(1,1) NOT NULL,
	[TieuDe] [nvarchar](max) NOT NULL,
	[NoiDung] [ntext] NOT NULL,
	[Anh] [nvarchar](max) NULL,
	[Created_at] [datetime] NULL,
	[Created_by] [nvarchar](255) NULL,
	[Updated_at] [datetime] NULL,
	[Updated_by] [nvarchar](255) NULL,
	[Status] [int] NULL,
	[Slug] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_TinTuc] PRIMARY KEY CLUSTERED 
(
	[MaTT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Admin] ON 

INSERT [dbo].[Admin] ([Id], [Username], [Password], [HoTen], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status]) VALUES (1, N'admin', N'THknPu09CV5V0SJPZSSukg==', N'HyperGear', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admin] ([Id], [Username], [Password], [HoTen], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status]) VALUES (9, N'AdTien', N'i/Qc6bf5Xjvp8vXa1koxbA==', N'Nguyễn Huỳnh Việt Tiến', NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Admin] OFF
GO
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [Soluong], [Dongia], [TongTien]) VALUES (1, 20, 2, CAST(7490000 AS Decimal(18, 0)), CAST(14980000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [Soluong], [Dongia], [TongTien]) VALUES (2, 21, 2, CAST(2990000 AS Decimal(18, 0)), CAST(5980000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [Soluong], [Dongia], [TongTien]) VALUES (3, 23, 1, CAST(14990000 AS Decimal(18, 0)), CAST(14990000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [Soluong], [Dongia], [TongTien]) VALUES (3, 26, 1, CAST(59999000 AS Decimal(18, 0)), CAST(59999000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [Soluong], [Dongia], [TongTien]) VALUES (4, 26, 1, CAST(59999000 AS Decimal(18, 0)), CAST(59999000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [Soluong], [Dongia], [TongTien]) VALUES (4, 27, 1, CAST(2000000 AS Decimal(18, 0)), CAST(2000000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [Soluong], [Dongia], [TongTien]) VALUES (6, 23, 1, CAST(14990000 AS Decimal(18, 0)), CAST(14990000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [Soluong], [Dongia], [TongTien]) VALUES (6, 24, 1, CAST(12990000 AS Decimal(18, 0)), CAST(12990000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [Soluong], [Dongia], [TongTien]) VALUES (7, 23, 1, CAST(14990000 AS Decimal(18, 0)), CAST(14990000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [Soluong], [Dongia], [TongTien]) VALUES (7, 24, 1, CAST(12990000 AS Decimal(18, 0)), CAST(12990000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [Soluong], [Dongia], [TongTien]) VALUES (10, 23, 1, CAST(14990000 AS Decimal(18, 0)), CAST(14990000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [Soluong], [Dongia], [TongTien]) VALUES (12, 24, 1, CAST(12990000 AS Decimal(18, 0)), CAST(12990000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [Soluong], [Dongia], [TongTien]) VALUES (13, 24, 1, CAST(12990000 AS Decimal(18, 0)), CAST(12990000 AS Decimal(18, 0)))
GO
SET IDENTITY_INSERT [dbo].[DanhMuc] ON 

INSERT [dbo].[DanhMuc] ([MaDM], [TenDM], [Slug], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status], [MaMn]) VALUES (20, N'Asus', N'asus', CAST(N'2021-07-09T20:58:56.000' AS DateTime), NULL, CAST(N'2021-07-10T22:49:41.637' AS DateTime), NULL, 1, 2)
INSERT [dbo].[DanhMuc] ([MaDM], [TenDM], [Slug], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status], [MaMn]) VALUES (21, N'MSI', N'msi', CAST(N'2021-07-09T20:59:04.467' AS DateTime), NULL, CAST(N'2021-07-09T20:59:04.467' AS DateTime), NULL, 1, 2)
INSERT [dbo].[DanhMuc] ([MaDM], [TenDM], [Slug], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status], [MaMn]) VALUES (22, N'HyperX', N'hyperx', CAST(N'2021-07-09T22:46:23.000' AS DateTime), NULL, CAST(N'2021-07-11T15:46:02.633' AS DateTime), NULL, 1, 3)
INSERT [dbo].[DanhMuc] ([MaDM], [TenDM], [Slug], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status], [MaMn]) VALUES (23, N'Màn Hình ASUS', N'man-hinh-asus', CAST(N'2021-07-10T22:41:56.000' AS DateTime), NULL, CAST(N'2021-07-11T23:38:49.163' AS DateTime), NULL, 1, 4)
INSERT [dbo].[DanhMuc] ([MaDM], [TenDM], [Slug], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status], [MaMn]) VALUES (25, N'Màn Hình MSI', N'man-hinh-msi', CAST(N'2021-07-10T23:45:15.000' AS DateTime), NULL, CAST(N'2021-07-11T15:40:43.677' AS DateTime), NULL, 1, 4)
INSERT [dbo].[DanhMuc] ([MaDM], [TenDM], [Slug], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status], [MaMn]) VALUES (27, N'Bàn Phím Logitech', N'ban-phim-logitech', CAST(N'2021-07-11T23:39:01.000' AS DateTime), NULL, CAST(N'2021-07-11T23:41:29.247' AS DateTime), NULL, 1, 7)
INSERT [dbo].[DanhMuc] ([MaDM], [TenDM], [Slug], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status], [MaMn]) VALUES (28, N'Tai Nghe Logitech', N'tai-nghe-logitech', CAST(N'2021-07-11T23:39:13.000' AS DateTime), NULL, CAST(N'2021-07-11T23:41:34.360' AS DateTime), NULL, 1, 3)
INSERT [dbo].[DanhMuc] ([MaDM], [TenDM], [Slug], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status], [MaMn]) VALUES (29, N'Ghế Gaming Asus', N'ghe-gaming-asus', CAST(N'2021-07-11T23:43:24.990' AS DateTime), NULL, CAST(N'2021-07-11T23:43:24.990' AS DateTime), NULL, 1, 8)
INSERT [dbo].[DanhMuc] ([MaDM], [TenDM], [Slug], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status], [MaMn]) VALUES (30, N'Màn Hình BenQ', N'man-hinh-benq', CAST(N'2021-07-11T23:45:38.147' AS DateTime), NULL, CAST(N'2021-07-11T23:45:38.147' AS DateTime), NULL, 1, 4)
SET IDENTITY_INSERT [dbo].[DanhMuc] OFF
GO
SET IDENTITY_INSERT [dbo].[DonDatHang] ON 

INSERT [dbo].[DonDatHang] ([MaDonHang], [Dathanhtoan], [Tinhtranggiaohang], [Ngaydat], [Ngaygiao], [MaKH], [DeliveryName], [DeliveryPhone], [DeliveryAdd], [DeliveryEmail]) VALUES (1, 1, 3, CAST(N'2021-07-16T16:26:39.947' AS DateTime), CAST(N'2021-07-20T20:28:32.597' AS DateTime), 1, N'tien', N'0123456789', N'Tphcm', N'tyowertmwer@gmasil.com')
INSERT [dbo].[DonDatHang] ([MaDonHang], [Dathanhtoan], [Tinhtranggiaohang], [Ngaydat], [Ngaygiao], [MaKH], [DeliveryName], [DeliveryPhone], [DeliveryAdd], [DeliveryEmail]) VALUES (2, 0, 0, CAST(N'2021-07-16T16:42:34.783' AS DateTime), NULL, 1, N'Nguyễn Huỳnh Việt Tiến', N'0123456789', N'TPHCM', N'viettien@nguyenhuynhviettien.name.vn')
INSERT [dbo].[DonDatHang] ([MaDonHang], [Dathanhtoan], [Tinhtranggiaohang], [Ngaydat], [Ngaygiao], [MaKH], [DeliveryName], [DeliveryPhone], [DeliveryAdd], [DeliveryEmail]) VALUES (3, 0, 1, CAST(N'2021-07-18T16:39:33.203' AS DateTime), NULL, 1, N'Nguyễn Huỳnh Việt Tiến', N'0123456789', N'tphcm', N'viettien@nguyenhuynhviettien.name.vn')
INSERT [dbo].[DonDatHang] ([MaDonHang], [Dathanhtoan], [Tinhtranggiaohang], [Ngaydat], [Ngaygiao], [MaKH], [DeliveryName], [DeliveryPhone], [DeliveryAdd], [DeliveryEmail]) VALUES (4, 0, 1, CAST(N'2021-07-19T16:11:46.683' AS DateTime), NULL, 1, N'Nguyễn Huỳnh Việt Tiến', N'0123456789', N'TP.HCM', N'checkpasssax@gmail.com')
INSERT [dbo].[DonDatHang] ([MaDonHang], [Dathanhtoan], [Tinhtranggiaohang], [Ngaydat], [Ngaygiao], [MaKH], [DeliveryName], [DeliveryPhone], [DeliveryAdd], [DeliveryEmail]) VALUES (6, 0, 1, CAST(N'2021-07-19T16:30:59.393' AS DateTime), NULL, 1, N'Nguyễn Huỳnh Việt Tiến', N'0123456789', N'TP.HCM', N'checkpasssax@gmail.com')
INSERT [dbo].[DonDatHang] ([MaDonHang], [Dathanhtoan], [Tinhtranggiaohang], [Ngaydat], [Ngaygiao], [MaKH], [DeliveryName], [DeliveryPhone], [DeliveryAdd], [DeliveryEmail]) VALUES (7, 0, 1, CAST(N'2021-07-19T16:36:31.480' AS DateTime), NULL, 1, N'Nguyễn Huỳnh Việt Tiến', N'0123456789', N'TP.HCM test', N'checkpasssax@gmail.com')
INSERT [dbo].[DonDatHang] ([MaDonHang], [Dathanhtoan], [Tinhtranggiaohang], [Ngaydat], [Ngaygiao], [MaKH], [DeliveryName], [DeliveryPhone], [DeliveryAdd], [DeliveryEmail]) VALUES (10, 0, 1, CAST(N'2021-07-19T17:00:36.660' AS DateTime), NULL, 11, N'Nguyễn Huỳnh Việt Tiến', N'0123456789', N'TP.HCM', N'checkpasssax@gmail.com')
INSERT [dbo].[DonDatHang] ([MaDonHang], [Dathanhtoan], [Tinhtranggiaohang], [Ngaydat], [Ngaygiao], [MaKH], [DeliveryName], [DeliveryPhone], [DeliveryAdd], [DeliveryEmail]) VALUES (12, 0, 1, CAST(N'2021-07-19T19:53:07.117' AS DateTime), NULL, 11, N'Nguyễn Huỳnh Việt Tiến', N'0123456789', N'TP.HCM test', N'checkpasssax@gmail.com')
INSERT [dbo].[DonDatHang] ([MaDonHang], [Dathanhtoan], [Tinhtranggiaohang], [Ngaydat], [Ngaygiao], [MaKH], [DeliveryName], [DeliveryPhone], [DeliveryAdd], [DeliveryEmail]) VALUES (13, 0, 1, CAST(N'2021-07-19T20:00:07.187' AS DateTime), NULL, 12, N'Nguyễn Huỳnh Việt Tiến', N'0123456789', N'test lan 2', N'checkpasssax@gmail.com')
SET IDENTITY_INSERT [dbo].[DonDatHang] OFF
GO
SET IDENTITY_INSERT [dbo].[KhachHang] ON 

INSERT [dbo].[KhachHang] ([MaKH], [HoTen], [Taikhoan], [Matkhau], [Email], [DiachiKH], [DienthoaiKH], [Ngaysinh]) VALUES (1, N'Nguyễn Huỳnh Việt Tiến', N'tien', N'i/Qc6bf5Xjvp8vXa1koxbA==', N'viettien@nguyenhuynhviettien.name.vn', NULL, N'0123456789', CAST(N'2021-07-12T15:38:27.307' AS DateTime))
INSERT [dbo].[KhachHang] ([MaKH], [HoTen], [Taikhoan], [Matkhau], [Email], [DiachiKH], [DienthoaiKH], [Ngaysinh]) VALUES (2, N'Tan', N'tan', N'oDncYr3l+etdMEzkx96OsQ==', N'thanhtan@name.vn', NULL, N'0123987654', CAST(N'2021-07-16T22:01:01.887' AS DateTime))
INSERT [dbo].[KhachHang] ([MaKH], [HoTen], [Taikhoan], [Matkhau], [Email], [DiachiKH], [DienthoaiKH], [Ngaysinh]) VALUES (4, N'tan pro haha', N'tanpro', N'oDncYr3l+etdMEzkx96OsQ==', N'tqqqan@gmail.com', NULL, N'123456789', CAST(N'2021-07-17T16:58:50.150' AS DateTime))
INSERT [dbo].[KhachHang] ([MaKH], [HoTen], [Taikhoan], [Matkhau], [Email], [DiachiKH], [DienthoaiKH], [Ngaysinh]) VALUES (5, N'Tien kk', N'tien123', N'i/Qc6bf5Xjvp8vXa1koxbA==', N'Etmail@gmial.com', NULL, N'1231231233', CAST(N'2021-07-17T22:08:36.330' AS DateTime))
INSERT [dbo].[KhachHang] ([MaKH], [HoTen], [Taikhoan], [Matkhau], [Email], [DiachiKH], [DienthoaiKH], [Ngaysinh]) VALUES (11, N'Nguyễn Huỳnh Việt Tiến', N'test', N'@Tien123', N'test', NULL, NULL, NULL)
INSERT [dbo].[KhachHang] ([MaKH], [HoTen], [Taikhoan], [Matkhau], [Email], [DiachiKH], [DienthoaiKH], [Ngaysinh]) VALUES (12, N'Nguyễn Huỳnh Việt Tiến', N'checkpasssax@gmail.com', N'@Tien123', N'checkpasssax@gmail.com', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[KhachHang] OFF
GO
SET IDENTITY_INSERT [dbo].[LienHe] ON 

INSERT [dbo].[LienHe] ([MaLH], [TenCH], [NoiDung], [DienThoai], [DiaChi], [Email], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status]) VALUES (1, N'Hyper Gear', N'Xuất thân từ cửa hàng kinh doanh máy tính được thành lập từ năm 1997, Hyper Gear được biết đến là đơn vị bán lẻ lâu đời và uy tín tại Việt Nam. Hyper Gear chuyên kinh doanh các sản phẩm công nghệ thông tin, thiết bị giải trí game, thiết bị văn phòng và thiết bị hi-tech của nhiều nhãn hàng lớn như Dell, Asus, HP, MSI, Lenovo…', N'0123888888', N'TP.HCM', N'hypergear@hypergear.name.vn', CAST(N'2021-07-13T23:53:47.000' AS DateTime), NULL, CAST(N'2021-07-13T23:55:02.380' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[LienHe] OFF
GO
SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([MaMn], [TenMn], [Slug], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status]) VALUES (2, N'Laptop', N'laptop', CAST(N'2021-07-09T20:49:04.047' AS DateTime), NULL, CAST(N'2021-07-09T20:49:04.047' AS DateTime), NULL, 1)
INSERT [dbo].[Menu] ([MaMn], [TenMn], [Slug], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status]) VALUES (3, N'Tai Nghe Gaming', N'tai-nghe-gaming', CAST(N'2021-07-09T21:29:00.213' AS DateTime), NULL, CAST(N'2021-07-09T21:29:00.213' AS DateTime), NULL, 1)
INSERT [dbo].[Menu] ([MaMn], [TenMn], [Slug], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status]) VALUES (4, N'Màn Hình Gaming', N'man-hinh-gaming', CAST(N'2021-07-10T22:41:26.000' AS DateTime), NULL, CAST(N'2021-07-10T22:41:34.870' AS DateTime), NULL, 1)
INSERT [dbo].[Menu] ([MaMn], [TenMn], [Slug], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status]) VALUES (7, N'Bàn Phím Gaming', N'ban-phim-gaming', CAST(N'2021-07-11T23:38:20.560' AS DateTime), NULL, CAST(N'2021-07-11T23:38:20.560' AS DateTime), NULL, 1)
INSERT [dbo].[Menu] ([MaMn], [TenMn], [Slug], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status]) VALUES (8, N'Ghế Gaming', N'ghe-gaming', CAST(N'2021-07-11T23:43:14.000' AS DateTime), NULL, CAST(N'2021-07-17T20:31:56.463' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[Menu] OFF
GO
SET IDENTITY_INSERT [dbo].[SanPham] ON 

INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Gia], [Slug], [Mota], [ChiTiet], [Anh], [Ngaycapnhat], [Soluongton], [MaDM], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status]) VALUES (11, N'Laptop ASUS ROG Strix Scar 15 G533QR-HQ098T', CAST(59990000 AS Decimal(18, 0)), N'laptop-asus-rog-strix-scar-15-g533qr-hq098t2411', N'Đang cập nhật', N'Đang cập nhật', N'laptop-asus-rog-strix-scar-15-g533qr-hq098t760.png', NULL, 11, 20, CAST(N'2021-07-09T21:27:38.000' AS DateTime), NULL, CAST(N'2021-07-17T23:07:43.580' AS DateTime), N'1', 1)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Gia], [Slug], [Mota], [ChiTiet], [Anh], [Ngaycapnhat], [Soluongton], [MaDM], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status]) VALUES (12, N'Laptop Gaming MSI Pulse GL76 11UEK 048VN', CAST(42990000 AS Decimal(18, 0)), N'laptop-gaming-msi-pulse-gl76-11uek-048vn3912', N'Đang cập nhật', N'Đang cập nhật', N'laptop-gaming-msi-pulse-gl76-11uek-048vn720.png', NULL, 11, 21, CAST(N'2021-07-09T21:28:14.000' AS DateTime), NULL, CAST(N'2021-07-17T20:17:52.037' AS DateTime), N'1', 1)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Gia], [Slug], [Mota], [ChiTiet], [Anh], [Ngaycapnhat], [Soluongton], [MaDM], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status]) VALUES (16, N'Màn hình Asus ROG Swift PG65UQ 65" VA 4K 144Hz G-Sync', CAST(170000000 AS Decimal(18, 0)), N'man-hinh-asus-rog-swift-pg65uq-65--va-4k-144hz-g-sync380', N'Thông tin chung:  -Nhà sản xuất : Asus  -Tình trạng : NEW - 100%  -Bảo hành : 36 tháng', N'Thông tin chung:  -Nhà sản xuất : Asus  -Tình trạng : NEW - 100%  -Bảo hành : 36 tháng', N'man-hinh-asus-rog-swift-pg65uq-65--va-4k-144hz-g-sync380.jpg', NULL, 13, 25, CAST(N'2021-07-11T15:56:54.520' AS DateTime), NULL, CAST(N'2021-07-11T15:56:54.520' AS DateTime), NULL, 1)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Gia], [Slug], [Mota], [ChiTiet], [Anh], [Ngaycapnhat], [Soluongton], [MaDM], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status]) VALUES (20, N'Tai nghe HyperX Cloud Orbit S', CAST(7490000 AS Decimal(18, 0)), N'tai-nghe-hyperx-cloud-orbit-s460', N'Hãng sản xuất: Kingston  Tình trạng: Mới 100%  Bảo hành: 24 tháng', N'Driver	Planar transducer, 100 mm Frequency Response	10Hz–50,000Hz Connection Type	USB Type A, USB Type C, 3.5mm Audio Type	Stereo+3D Audio+Headtracking Audio Controls	Onboard Frame Type	Plastic Memory Foam Ear Cushions	✔ Noise-Cancellation Mic Type	Detachable Battery Life	Analog 3.5mm mode: 10 hours Accessories	Cloth Bag PC Extension Cable	N/A', N'tai-nghe-hyperx-cloud-orbit-s460.jpg', NULL, 11, 22, CAST(N'2021-07-11T16:13:21.960' AS DateTime), NULL, CAST(N'2021-07-14T13:13:25.220' AS DateTime), N'1', 1)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Gia], [Slug], [Mota], [ChiTiet], [Anh], [Ngaycapnhat], [Soluongton], [MaDM], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status]) VALUES (21, N'Tai nghe Logitech G Pro X', CAST(2990000 AS Decimal(18, 0)), N'tai-nghe-logitech-g-pro-x160', N'-Nhà sản xuất : Logitech -Màu: Đen -Tình trạng : NEW - 100%  -Bảo hành : 24 tháng ', N'Tai nghe gaming giá rẻ Logitech G Pro X Tai nghe gaming giá rẻ Logitech G Pro X là một trong những dòng sản phẩm headphone gaming sở hữu các bộ lọc giọng nói trong thời gian thực để giảm tiếng ồn, thêm độ nén và giảm âm gió, đồng thời đảm bảo giọng nói của bạn phát ra có độ phong phú hơn, trong hơn và chuyên nghiệp hơn. Cảm nhận việc giao tiếp bằng giọng nói trong trò chơi ổn định, có chất lượng studio với BLUE VO!CE thông qua Phần mềm Chơi game HUB G', N'tai-nghe-logitech-g-pro-x160.jpg', NULL, 100, 28, CAST(N'2021-07-11T23:42:07.053' AS DateTime), NULL, CAST(N'2021-07-14T13:37:21.923' AS DateTime), N'1', 1)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Gia], [Slug], [Mota], [ChiTiet], [Anh], [Ngaycapnhat], [Soluongton], [MaDM], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status]) VALUES (22, N'Bàn phím Logitech G913 TKL Lightspeed Wireless', CAST(4490000 AS Decimal(18, 0)), N'ban-phim-logitech-g913-tkl-lightspeed-wireless900', N'Tình trạng: Fullbox 100% Bảo hành : 24 tháng Switch : CLICKY/TACTILE / LINEAR Led : RGB  ', N'G913 là một chiếc bàn phím cơ giá rẻ đặc biệt dành cho những ai tìm kiếm sự khác biệt. Có thể giá của nó không dễ chịu chút nào nhưng những gì nó đem lại thì thật sự xứng đáng.', N'ban-phim-logitech-g913-tkl-lightspeed-wireless900.jpg', NULL, 11, 27, CAST(N'2021-07-11T23:42:44.520' AS DateTime), NULL, CAST(N'2021-07-15T14:01:44.110' AS DateTime), N'1', 1)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Gia], [Slug], [Mota], [ChiTiet], [Anh], [Ngaycapnhat], [Soluongton], [MaDM], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status]) VALUES (23, N'Ghế chơi game ASUS ROG Chariot RGB', CAST(14990000 AS Decimal(18, 0)), N'ghe-choi-game-asus-rog-chariot-rgb70', N'Hãng sản xuất: ASUS  Bảo hành: 12 Tháng  Sản phẩm phù hợp với:  Cân nặng: Trên 80kg Chiều cao: Trên 1m75 Chất liệu:   Da PU', N'Hãng sản xuất: ASUS  Bảo hành: 12 Tháng  Sản phẩm phù hợp với:  Cân nặng: Trên 80kg Chiều cao: Trên 1m75 Chất liệu:   Da PU', N'ghe-choi-game-asus-rog-chariot-rgb70.jpg', NULL, 22, 29, CAST(N'2021-07-11T23:44:19.600' AS DateTime), NULL, CAST(N'2021-07-14T12:55:01.883' AS DateTime), N'1', 1)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Gia], [Slug], [Mota], [ChiTiet], [Anh], [Ngaycapnhat], [Soluongton], [MaDM], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status]) VALUES (24, N'Màn hình BenQ Zowie XL2740 27" 240Hz', CAST(12990000 AS Decimal(18, 0)), N'man-hinh-benq-zowie-xl2740-27--240hz750', N'-Nhà sản xuất : BenQ  -Tình trạng : NEW - 100%  -Bảo hành : 36 tháng  ★ Khuyến mãi cực khủng (khách hàng chọn 1 trong các gói KM sau)  🎁 Gói 1: Gói quà tặng hấp dẫn:  - Tặng ngay Tai nghe DareU EH416 RGB trị giá 590.000VND - Hoặc tặng ngay áo khoác GEARVN Limited áp dụng giá với giá Khuyến Mãi (số lượng có hạn)', N'-Nhà sản xuất : BenQ  -Tình trạng : NEW - 100%  -Bảo hành : 36 tháng  ★ Khuyến mãi cực khủng (khách hàng chọn 1 trong các gói KM sau)  🎁 Gói 1: Gói quà tặng hấp dẫn:  - Tặng ngay Tai nghe DareU EH416 RGB trị giá 590.000VND - Hoặc tặng ngay áo khoác GEARVN Limited áp dụng giá với giá Khuyến Mãi (số lượng có hạn)', N'man-hinh-benq-zowie-xl2740-27--240hz750.jpg', NULL, 40, 30, CAST(N'2021-07-11T23:46:23.393' AS DateTime), NULL, CAST(N'2021-07-14T12:55:01.233' AS DateTime), N'1', 1)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Gia], [Slug], [Mota], [ChiTiet], [Anh], [Ngaycapnhat], [Soluongton], [MaDM], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status]) VALUES (26, N'Màn hình Gaming BenQ Zowie XL2740 27" 240Hz', CAST(59999000 AS Decimal(18, 0)), N'man-hinh-gaming-benq-zowie-xl2740-27--240hz2926', N'<p><strong>&middot; Led:</strong><span style="background-color:#ffffff; color:#333333; font-family:Candara,sans-serif; font-size:18.6667px">&nbsp;</span><span style="background-color:#ffffff; color:#ff0000; font-family:Candara,sans-serif; font-size:18.6667px"><strong>R</strong></span><span style="background-color:#ffffff; color:#00ff00; font-family:Candara,sans-serif; font-size:18.6667px"><strong>G</strong></span><span style="background-color:#ffffff; color:#0000ff; font-family:Candara,sans-serif; font-size:18.6667px"><strong>B</strong></span></p>
', NULL, N'man-hinh-benq-zowie-xl2740-27--240hz420.jpg', NULL, 11, 30, CAST(N'2021-07-17T21:21:24.000' AS DateTime), NULL, CAST(N'2021-07-18T16:18:43.780' AS DateTime), NULL, 1)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Gia], [Slug], [Mota], [ChiTiet], [Anh], [Ngaycapnhat], [Soluongton], [MaDM], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status]) VALUES (27, N'Tai nghe HyperX Fight', CAST(2000000 AS Decimal(18, 0)), N'tai-nghe-hyperx-fight540', N'<p>dang cap nhat</p>
', N'<p>dang cap nhat</p>
', N'tai-nghe-hyperx-fight540.jpg', NULL, 7, 22, CAST(N'2021-07-18T14:58:00.980' AS DateTime), NULL, CAST(N'2021-07-18T14:59:08.330' AS DateTime), N'1', 1)
SET IDENTITY_INSERT [dbo].[SanPham] OFF
GO
SET IDENTITY_INSERT [dbo].[TinTuc] ON 

INSERT [dbo].[TinTuc] ([MaTT], [TieuDe], [NoiDung], [Anh], [Created_at], [Created_by], [Updated_at], [Updated_by], [Status], [Slug]) VALUES (1, N'Chuẩn bị săn seal các anh em', N'<p>Chuẩn bị săn seal c&aacute;c anh em</p>
', N'admin070.jpg', CAST(N'2021-07-20T20:39:33.000' AS DateTime), N'admin', CAST(N'2021-07-20T20:39:46.343' AS DateTime), N'admin', 1, N'chuan-bi-san-seal-cac-anh-em')
SET IDENTITY_INSERT [dbo].[TinTuc] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Admin__536C85E46567919B]    Script Date: 20/7/2021 21:13:53 PM ******/
ALTER TABLE [dbo].[Admin] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__KhachHan__7FB3F64FF0912FA7]    Script Date: 20/7/2021 21:13:53 PM ******/
ALTER TABLE [dbo].[KhachHang] ADD UNIQUE NONCLUSTERED 
(
	[Taikhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__KhachHan__A9D10534B506C152]    Script Date: 20/7/2021 21:13:53 PM ******/
ALTER TABLE [dbo].[KhachHang] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ChiTietDonHang]  WITH CHECK ADD  CONSTRAINT [FK_Donhang] FOREIGN KEY([MaDonHang])
REFERENCES [dbo].[DonDatHang] ([MaDonHang])
GO
ALTER TABLE [dbo].[ChiTietDonHang] CHECK CONSTRAINT [FK_Donhang]
GO
ALTER TABLE [dbo].[ChiTietDonHang]  WITH CHECK ADD  CONSTRAINT [FK_SP] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[ChiTietDonHang] CHECK CONSTRAINT [FK_SP]
GO
ALTER TABLE [dbo].[DanhMuc]  WITH CHECK ADD  CONSTRAINT [Fk_MaMn] FOREIGN KEY([MaMn])
REFERENCES [dbo].[Menu] ([MaMn])
GO
ALTER TABLE [dbo].[DanhMuc] CHECK CONSTRAINT [Fk_MaMn]
GO
ALTER TABLE [dbo].[DonDatHang]  WITH CHECK ADD  CONSTRAINT [FK_Khachhang] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KhachHang] ([MaKH])
GO
ALTER TABLE [dbo].[DonDatHang] CHECK CONSTRAINT [FK_Khachhang]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_DanhMuc] FOREIGN KEY([MaDM])
REFERENCES [dbo].[DanhMuc] ([MaDM])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_DanhMuc]
GO
ALTER TABLE [dbo].[ChiTietDonHang]  WITH CHECK ADD CHECK  (([Dongia]>=(0)))
GO
ALTER TABLE [dbo].[ChiTietDonHang]  WITH CHECK ADD CHECK  (([Soluong]>(0)))
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD CHECK  (([Gia]>=(0)))
GO
