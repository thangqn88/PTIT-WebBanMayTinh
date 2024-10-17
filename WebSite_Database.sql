CREATE DATABASE DBWebSite
GO
USE DBWebSite
GO
DROP DATABASE DBWebSite
CREATE TABLE NhaSX(
	MaSX INT,
	TenSX NVARCHAR(50)
	CONSTRAINT PK_Nhasx PRIMARY KEY(MaSX)
)
GO

CREATE TABLE LoaiSP(
	MaLoai INT,
	TenLoai NVARCHAR(50),
	 CONSTRAINT PK_Loaisp PRIMARY KEY(MaLoai)
)
GO

CREATE TABLE SanPham(
	MaSP INT,
	TenSP NVARCHAR(50),
	SoLuong INT,
	GiaSP INT,
	BaoHanh INT,
	ManHinh FLOAT,
	CPU NVARCHAR(50),
	Ram INT,
	BoNho INT,
	VGA NVARCHAR(50),
	Pin INT,
	MaLoai INT,
	MaSX INT,
)
GO

CREATE TABLE TaiKhoan(
	MaTK INT,
	TenTK VARCHAR(50),
	MatKhau VARCHAR(50),
	HoTen NVARCHAR(50),
	DiaChi NVARCHAR(100),
	SDT INT,
	Email VARCHAR(100),
	Quyen INT,
	CONSTRAINT PK_TaiKhoan PRIMARY KEY(MaTK)
)
GO


CREATE TABLE HoaDon(
	MaHD INT,
	MaSP INT,
	MaTK INT,
	SoLuong INT,
	TongTien FLOAT,
	NgayDat DATE,
	TinhTrang INT,
	CONSTRAINT PK_HoaDon PRIMARY KEY(MaHD)
)

ALTER TABLE dbo.SanPham ADD CONSTRAINT FK_Sanpham FOREIGN KEY(MaLoai) REFERENCES dbo.LoaiSP(MaLoai), FOREIGN KEY(MaSX) REFERENCES dbo.NhaSX(MaSX)
GO
ALTER TABLE dbo.SanPham alter column MaSP int NOT NULL
ALTER TABLE dbo.SanPham ADD PRIMARY KEY(MaSP)
ALTER TABLE dbo.HoaDon ADD FOREIGN KEY(MaSP) REFERENCES dbo.SanPham(MaSP), FOREIGN KEY(MaTK) REFERENCES dbo.TaiKhoan(MaTK)

--Sản Phẩm

CREATE PROC loadSanPham
AS
BEGIN
	SELECT MaSP, TenSP, SoLuong, GiaSP, BaoHanh, ManHinh, CPU, Ram, BoNho, VGA, Pin, SanPham.MaLoai, tb1.TenLoai, SanPham.MaSX, tb2.TenSX
	FROM dbo.SanPham, (SELECT * FROM dbo.LoaiSP) AS tb1, (SELECT * FROM dbo.NhaSX) AS tb2
	WHERE SanPham.MaSX = tb2.MaSX AND SanPham.MaLoai = tb1.MaLoai
END
GO

CREATE PROC searchSanPham(@key NVARCHAR(50))
AS
BEGIN
	SELECT MaSP, TenSP, SoLuong, GiaSP, BaoHanh, ManHinh, CPU, Ram, BoNho, VGA, Pin, SanPham.MaLoai, tb1.TenLoai, SanPham.MaSX, tb2.TenSX
	FROM dbo.SanPham, (SELECT * FROM dbo.LoaiSP) AS tb1, (SELECT * FROM dbo.NhaSX) AS tb2
	WHERE SanPham.MaSX = tb2.MaSX AND SanPham.MaLoai = tb1.MaLoai AND TenSP LIKE '%'+@key+'%'
END
GO



CREATE PROC getSanPham(@ma INT)
AS
BEGIN
	SELECT MaSP, TenSP, SoLuong, GiaSP, BaoHanh, ManHinh, CPU, Ram, BoNho, VGA, Pin, SanPham.MaLoai, tb1.TenLoai, SanPham.MaSX, tb2.TenSX
	FROM dbo.SanPham, (SELECT * FROM dbo.LoaiSP) AS tb1, (SELECT * FROM dbo.NhaSX) AS tb2
	WHERE SanPham.MaSX = tb2.MaSX AND SanPham.MaLoai = tb1.MaLoai AND MaSP = @ma
END
GO


CREATE PROC insertSanPham(@ten NVARCHAR(50), @sl INT, @gia INT, @bh INT, @mh FLOAT, @cpu NVARCHAR(50), @ram INT, @bonho INT, @vga NVARCHAR(50), @pin INT, @loai INT, @sx INT)
AS
BEGIN
	DECLARE @ma INT
	SET @ma = (SELECT MAX(MaSP) FROM dbo.SanPham) + 1
	INSERT dbo.SanPham
	VALUES  ( @ma ,@ten ,@sl ,@gia ,@bh ,@mh ,@cpu , @ram , @bonho, @vga , @pin , @loai ,@sx)
END
GO

CREATE PROC updateSanPham(@ma INT, @ten NVARCHAR(50), @sl INT, @gia INT, @bh INT, @mh FLOAT, @cpu NVARCHAR(50), @ram INT, @bonho INT, @vga NVARCHAR(50), @pin INT, @loai INT, @sx INT)
AS
BEGIN
	UPDATE dbo.SanPham
	SET TenSP = @ten, SoLuong = @sl, GiaSP = @gia, BaoHanh = @bh, ManHinh = @mh, CPU = @cpu, Ram = @ram, BoNho = @bonho, VGA = @vga, Pin = @pin, MaLoai = @loai, MaSX = @sx
	WHERE MaSP = @ma
END
GO

CREATE PROC deleteSanPham(@ma INT)
AS
BEGIN
	DELETE dbo.SanPham WHERE MaSP = @ma
END
GO

-- Nhà sản xuất

CREATE PROC loadSanXuat
AS
BEGIN
	SELECT * FROM dbo.NhaSX
END
GO

CREATE PROC insertSanXuat(@ten NVARCHAR(50))
AS
BEGIN
	DECLARE @ma INT
	SET @ma = (SELECT MAX(MaSX) FROM dbo.NhaSX) + 1
	INSERT dbo.NhaSX
	        ( MaSX, TenSX )
	VALUES  ( @ma, @ten )
END
GO

CREATE PROC updateSanXuat(@ma INT, @ten NVARCHAR(50))
AS
BEGIN
	UPDATE dbo.NhaSX
	SET TenSX = @ten
	WHERE MaSX = @ma
END
GO

CREATE PROC deleteSanXuat(@ma INT)
AS
BEGIN
	DELETE dbo.NhaSX WHERE MaSX = @ma
END
GO

-- Loại sản phẩm

CREATE PROC loadLoaiSP
AS
BEGIN
	SELECT * FROM dbo.LoaiSP
END
GO

CREATE PROC insertLoaiSP(@ten NVARCHAR(50))
AS
BEGIN
	DECLARE @ma INT
	SET @ma = (SELECT MAX(MaLoai) FROM dbo.LoaiSP) + 1
	INSERT dbo.LoaiSP
	        ( MaLoai, TenLoai )
	VALUES  ( @ma,@ten )
END
GO

CREATE PROC updateLoaiSP(@ma INT, @ten NVARCHAR(50))
AS
BEGIN
	UPDATE dbo.LoaiSP
	SET TenLoai = @ten
	WHERE MaLoai = @ma
END
GO

CREATE PROC deleteLoaiSP(@ma INT)
AS
BEGIN
	DELETE dbo.LoaiSP WHERE MaLoai = @ma
END
GO


-- Tài Khoản

CREATE PROC dangNhap(@user VARCHAR(50), @pass VARCHAR(50))
AS
BEGIN
	SET @pass = @pass + '_ahihihi'
	SET @pass = CONVERT(VARCHAR(32), HashBytes('MD5', @pass), 2)
	SELECT * FROM dbo.TaiKhoan WHERE TenTK = @user AND MatKhau = @pass
END
GO


CREATE PROC dangKy(@user VARCHAR(50), @pass VARCHAR(50), @hoten NVARCHAR(50), @email VARCHAR(100), @quyen INT)
AS
BEGIN
	DECLARE @ma  INT
	SET @ma = (SELECT MAX(MaTK) FROM dbo.TaiKhoan) + 1
	SET @pass = @pass + '_ahihihi'
	SET @pass = LOWER(CONVERT(VARCHAR(32), HashBytes('MD5', @pass), 2))
	IF @user IN (SELECT TenTK FROM dbo.TaiKhoan)
		PRINT 'Lỗi'
	ELSE
	BEGIN
		INSERT dbo.TaiKhoan
			( MaTK ,TenTK ,MatKhau ,HoTen ,Email ,Quyen)
		VALUES  ( @ma , -- MaTK - int
				  @user , -- TenTK - varchar(50)
				  @pass , -- MaKhau - varchar(50)
				  @hoten , -- HoTen - nvarchar(50)
				  @email , -- Email - varchar(100)
				  @quyen  -- Quyen - int
				)
	END
	
END
GO


CREATE PROC updateTaiKhoan(@ma INT, @hoten NVARCHAR(50), @diachi NVARCHAR(100), @sdt INT, @email VARCHAR(100), @quyen INT)
AS
BEGIN
	UPDATE dbo.TaiKhoan
	SET HoTen = @hoten, DiaChi = @diachi, SDT = @sdt, Email = @email, Quyen = @quyen
	WHERE MaTK = @ma
END
GO


-- Hóa Đơn

CREATE PROC loadHoaDon
AS
BEGIN
	SELECT MaHD, HoaDon.MaSP, tb1.TenSP, HoaDon.MaTK, tb2.HoTen, SoLuong, TongTien, NgayDat, TinhTrang
	FROM dbo.HoaDon, (SELECT MaSP, TenSP FROM dbo.SanPham) AS tb1, (SELECT MaTK, HoTen FROM dbo.TaiKhoan) AS tb2
	WHERE HoaDon.MaSP = tb1.MaSP AND HoaDon.MaTK = tb2.MaTK
	ORDER BY TinhTrang
END
GO

CREATE PROC getHoaDon(@ma INT)
AS
BEGIN
	SELECT MaHD, HoaDon.MaSP, tb1.TenSP, HoaDon.MaTK, tb2.HoTen, SoLuong, TongTien, NgayDat, TinhTrang
	FROM dbo.HoaDon, (SELECT MaSP, TenSP FROM dbo.SanPham) AS tb1, (SELECT MaTK, HoTen FROM dbo.TaiKhoan) AS tb2
	WHERE HoaDon.MaSP = tb1.MaSP AND HoaDon.MaTK = tb2.MaTK AND HoaDon.MaTK = @ma
	ORDER BY TinhTrang
END
GO


CREATE PROC insertHoaDon(@masp INT, @matk INT, @sl INT, @ngay DATE)
AS
BEGIN
	DECLARE @ma INT, @tong FLOAT
	SET @ma = (SELECT MAX(MaHD) FROM dbo.HoaDon) + 1
	SET @tong = (SELECT GiaSP FROM dbo.SanPham WHERE MaSP = @masp) * @sl
	INSERT dbo.HoaDon
	VALUES  ( @ma , -- MaHD - int
	          @masp , -- MaSP - int
	          @matk , -- MaTK - int
	          @sl , -- SoLuong - int
	          @tong , -- TongTien - float
	          @ngay , -- NgayDat - date
	          0  -- TinhTrang - int
	        )
END
GO

CREATE PROC updateDonHang(@ma INT, @tt INT)
AS
BEGIN
	UPDATE dbo.HoaDon 
	SET TinhTrang = @tt
	WHERE MaHD = @ma
END
GO

CREATE PROC deleteDonHang(@ma INT)
AS
BEGIN
	DELETE dbo.HoaDon WHERE MaHD = @ma
END
GO

SELECT * FROM dbo.SanPham
GO

PRINT LOWER(CONVERT(VARCHAR(32), HashBytes('MD5', 'admin_ahihihi'), 2))
GO

EXEC dbo.loadLoaiSP


SELECT MaSP, TenSP, SoLuong, GiaSP, BaoHanh, ManHinh, CPU, Ram, BoNho, VGA, Pin, SanPham.MaLoai, tb1.TenLoai, SanPham.MaSX, tb2.TenSX FROM dbo.SanPham, (SELECT * FROM dbo.LoaiSP) AS tb1, (SELECT * FROM dbo.NhaSX) AS tb2 WHERE SanPham.MaSX = tb2.MaSX AND SanPham.MaLoai = tb1.MaLoai AND SanPham.MaSX = SanPham.MaSX AND SanPham.MaLoai = SanPham.MaLoai AND GiaSP > 0 AND GiaSP < 9999999999 ORDER BY GiaSP

SELECT * FROM dbo.TaiKhoan