select * from PhieuTra
-------------------------------------------------------------------------------------
------------------------------ Hoá Đơn ----------------------------------------------

alter proc [dbo].[ChiTietHoaDon_Find]
@MaHoaDon nvarchar(50),
@MaSach nvarchar(50),
@TenSach nvarchar(50),
@SoLuong int,
@Gia bigint,
@ThanhTien bigint
as
begin
select ROW_NUMBER() over(order by MaHoaDon) Stt,MaHoaDon,MaSach,TenSach,SoLuong,Gia,ThanhTien
from ChiTietHoaDon
where MaHoaDon like '%' + @MaHoaDon+'%' and
	  MaSach like '%'+@MaSach+'%' and
	  TenSach like '%'+@TenSach+'%' and
	  SoLuong like '%'+@SoLuong+'%' and
	  Gia like '%' + @Gia+'%' and
	  ThanhTien like '%' + @ThanhTien+'%'
end

select * from Sach

ALTER PROCEDURE [dbo].[ChiTietHoaDon_Ins]
	-- Add the parameters for the stored procedure here
	@MaHoaDon nvarchar(50),
	@MaSach nvarchar(50),
	@TenSach nvarchar(50),
	@SoLuong int,
	@Gia bigint,
	@ThanhTien bigint,
	@NgayBan date
AS
BEGIN
	insert into ChiTietHoaDon values(@MaHoaDon,@MaSach,@TenSach,@SoLuong,@Gia,@ThanhTien,@NgayBan)
	update Sach set SoLuong = Sach.SoLuong - @SoLuong
	from Sach
	where Sach.MaSach = @MaSach
END

alter proc [dbo].[ChiTietHoaDon_deleteall]
@MaSach nvarchar(50)
as
begin
delete from ChiTietHoaDon;
end

alter proc Sach_TKS
@TacGia nvarchar(50)
as
begin
	select MaSach, TenSach, TacGia, NXB
	from Sach
	where TacGia like '%'+@TacGia+'%'
end
--------------------------------------------------------------------------------------------------------
---------- ChiTietPhieuTra
alter proc CTPhieuTra_INS
@MaPhieuTra nvarchar(50),
@MaPhieuMuon nvarchar(50),
@MaThuThu nvarchar(50),
@TienPhat bigint,
@NgayPhaiTra date
As
Begin
	insert into PhieuTra values(@MaPhieuTra,@MaPhieuMuon,@MaThuThu,@TienPhat,@NgayPhaiTra)
End

alter proc CTPhieuTra_INS2
@MaPhieuTra nvarchar(50),
@MaPhieuMuon nvarchar(50),
@MaThuThu nvarchar(50),
@TienPhat bigint,
@NgayPhaiTra date,
@SoLuong int,
@MaSach nvarchar(50)
As
Begin
	insert into PhieuTra values(@MaPhieuTra,@MaPhieuMuon,@MaThuThu,@TienPhat,@NgayPhaiTra)
	update Sach set SoLuong = Sach.SoLuong + @SoLuong
	from Sach
	where Sach.MaSach = @MaSach
End

alter proc CTPhieuTra_UPDATE
@MaPhieuTra nvarchar(50),
@MaPhieuMuon nvarchar(50),
@MaThuThu nvarchar(50),
@TienPhat bigint,
@NgayPhaiTra date
As
Begin
	update PhieuTra set MaPhieuTra=@MaPhieuTra,MaPhieuMuon=@MaPhieuMuon,
	 MaThuThu=@MaThuThu, TienPhat=@TienPhat, NgayPhaiTra=@NgayPhaiTra
	where MaPhieuTra=@MaPhieuTra
End

alter proc CTPhieuTra_DELETE
@MaPhieuTra nvarchar(50)
As
Begin
	delete from PhieuTra
	where MaPhieuTra=@MaPhieuTra
End

alter proc CTPhieuTra_find
@MaPhieuTra nvarchar(50)
As
Begin
	select *
	from PhieuTra
	where MaPhieuTra like '%'+@MaPhieuTra+'%'	  
End

alter proc CTPhieuTra_CheckMaTrung
@MaPhieuTra nvarchar(50),
@kq int output
As
Begin
	Set @kq=0;
	if exists(select MaPhieuTra from PhieuTra where MaPhieuTra=@MaPhieuTra)
	begin
		set @kq=1;
	end
End

-------------------------------------------------------------------------------------
-- Thủ Thư

alter proc [dbo].[ThuThu_Update]
	@MaThuThu nvarchar(50),
	@TenThuThu nvarchar(50),
	@NamSinh date,
	@GioiTinh nvarchar(50),
	@ChucVu nvarchar(50),
	@DiaChi nvarchar(50),
	@MatKhau nvarchar(50)
as
begin
update ThuThu set TenThuThu=@TenThuThu, NamSinh=@NamSinh,GioiTinh=@GioiTinh,ChucVu=@ChucVu,DiaChi=@DiaChi,MatKhau=@MaThuThu
where MaThuThu=@MaThuThu
end


alter PROCEDURE [dbo].[ThuThu_Ins]
	@MaThuThu nvarchar(50),
	@TenThuThu nvarchar(50),
	@NamSinh date,
	@GioiTinh nvarchar(50),
	@ChucVu nvarchar(50),
	@DiaChi nvarchar(50),
	@MatKhau nvarchar(50)
AS
BEGIN
	insert into ThuThu values(@MaThuThu,@TenThuThu,@NamSinh,@GioiTinh,@ChucVu,@DiaChi,@MatKhau)
END


alter proc [dbo].[ThuThu_Find]
	@MaThuThu nvarchar(50),
	@TenThuThu nvarchar(50),
	@GioiTinh nvarchar(50),
	@ChucVu nvarchar(50),
	@DiaChi nvarchar(50)
as
begin
select *
from ThuThu
where MaThuThu like '%'+@MaThuThu+'%' and
	  TenThuThu like '%'+@TenThuThu+'%' and
	  GioiTinh like '%'+@GioiTinh+'%' and
	  ChucVu like '%'+@ChucVu+'%' and
	  DiaChi like '%' +@DiaChi+'%'
End

alter proc [dbo].[ThuThu_Find1]
	@MaThuThu nvarchar(50)
as
begin
select *
from ThuThu
where MaThuThu like '%'+@MaThuThu+'%'
End

alter proc [dbo].[ThuThu_Del]
@MaThuThu nvarchar(50)
as
begin delete from ThuThu
where MaThuThu=@MaThuThu
end

alter proc [dbo].[Check_trungmaThuThu]
@MaThuThu nvarchar(50),
@kq int output
As
Begin
	Set @kq=0;
	if exists(select MaThuThu from ThuThu where MaThuThu=@MaThuThu)
	begin
	set @kq=1; --Trùng mã lớp
	end
End

-------------------------------------------------------------------------------------
-- Phiếu Mượn

alter proc PhieuMuon_INS
@MaPhieuMuon nvarchar(50),
@MaDocGia nvarchar(50),
@MaSach nvarchar(50),
@NgayMuon date,
@NgayTra date
As
Begin
	insert into PhieuMuon values(@MaPhieuMuon,@MaDocGia,@MaSach,@NgayMuon,@NgayTra)
End


select SoLuong from Sach

alter proc PhieuMuon_INS2
@MaPhieuMuon nvarchar(50),
@MaDocGia nvarchar(50),
@MaSach nvarchar(50),
@NgayMuon date,
@NgayTra date,
@SoLuong int
As
Begin
	insert into PhieuMuon values(@MaPhieuMuon,@MaDocGia,@MaSach,@NgayMuon,@NgayTra)
	update Sach set SoLuong = Sach.SoLuong - @SoLuong
	from Sach
	where Sach.MaSach = @MaSach
End

alter proc PhieuMuon_UPDATE
@MaPhieuMuon nvarchar(50),
@MaDocGia nvarchar(50),
@MaSach nvarchar(50),
@NgayMuon date,
@NgayTra date
As
Begin
	update PhieuMuon set MaDocGia=@MaDocGia, MaSach=@MaSach, NgayMuon=@NgayMuon, NgayTra=@NgayTra
	where MaPhieuMuon=@MaPhieuMuon
End

alter proc PhieuMuon_DELETE
@MaPhieuMuon nvarchar(50)
As
Begin
	delete from PhieuMuon
	where MaPhieuMuon=@MaPhieuMuon
End

alter proc PhieuMuon_find
@MaDocGia nvarchar(50)
As
Begin
	select *
	from PhieuMuon
	where MaDocGia like '%'+@MaDocGia+'%'	  
End


-------------------------------------------------------------------------------------
-- Quản Lý Sách

alter PROCEDURE [dbo].[Sach_Ins]
	-- Add the parameters for the stored procedure here
	@MaSach nvarchar(50),
	@TenSach nvarchar(50),
	@TacGia nvarchar(50),
	@NXB nvarchar(50),
	@Gia bigint,
	@ChuyenMuc nvarchar(50),
	@ViTri nvarchar(50),
	@SoLuong int
AS
BEGIN
	insert into Sach values(@MaSach,@TenSach,@TacGia,@NXB,@Gia,@ChuyenMuc,@ViTri,@SoLuong)
END

alter proc [dbo].[Sach_Update]
	@MaSach nvarchar(50),
	@TenSach nvarchar(50),
	@TacGia nvarchar(50),
	@NXB nvarchar(50),
	@Gia bigint,
	@ChuyenMuc nvarchar(50),
	@ViTri nvarchar(50),
	@SoLuong int
as
begin
update Sach set MaSach=@MaSach,TenSach=@TenSach, TacGia=@TacGia, NXB=@NXB, Gia=@Gia, ChuyenMuc=@ChuyenMuc,ViTri=@ViTri,SoLuong=@SoLuong
where MaSach=@MaSach
end

alter proc [dbo].[Sach_Find]
	@MaSach nvarchar(50)
as
begin
select *
from Sach
where MaSach like '%'+@MaSach+'%'
end

alter proc [dbo].[Sach_Del]
@MaSach nvarchar(50)
as
begin delete from Sach
where MaSach=@MaSach
end

alter proc [dbo].[Check_trungmaSach]
@MaSach nvarchar(50),
@kq int output
As
Begin
	Set @kq=0;
	if exists(select MaSach from Sach where MaSach=@MaSach)
	begin
	set @kq=1; --Trùng mã lớp
	end
End


-------------------------------------------------------------------------------------
-- Độc Giả
alter proc [dbo].[Check_trungmaDocGia]
@MaDocGia nvarchar(50),
@kq int output
As
Begin
	Set @kq=0;
	if exists(select MaDocGia from DocGia where MaDocGia=@MaDocGia)
	begin
	set @kq=1; --Trùng mã lớp
	end
End


alter proc [dbo].[DocGia_Update]
	@MaDocGia nvarchar(50),
	@MatKhau nvarchar(50),
	@TenDocGia nvarchar(50),
	@NamSinh date,
	@GioiTinh nvarchar(50),
	@NgheNghiep nvarchar(50),
	@DiaChi nvarchar(50),
	@SoDienThoai nvarchar(50),
	@NgayMuaThe date,
	@NgayHetHan date
as
begin
update DocGia set MatKhau=@MatKhau, TenDocGia=@TenDocGia, NamSinh=@NamSinh,GioiTinh=@GioiTinh,NgheNghiep=@NgheNghiep,DiaChi=@DiaChi,SoDienThoai=@SoDienThoai,NgayMuaThe=@NgayMuaThe,NgayHetHan=@NgayHetHan
where MaDocGia=@MaDocGia
end

alter PROCEDURE [dbo].[DocGia_Ins2]
	-- Add the parameters for the stored procedure here
	@MaDocGia nvarchar(50),
	@MatKhau nvarchar(50),
	@TenDocGia nvarchar(50),
	@NamSinh date,
	@NgheNghiep nvarchar(50),
	@GioiTinh nvarchar(50),
	@DiaChi nvarchar(50),
	@SoDienThoai nvarchar(50),
	@NgayMuaThe date,
	@NgayHetHan date
AS
BEGIN
	insert into DocGia values(@MaDocGia,@MatKhau,@TenDocGia,@NamSinh,@NgheNghiep,@GioiTinh,@DiaChi,@SoDienThoai,@NgayMuaThe,@NgayHetHan)
END


alter proc [dbo].[DocGia_Find]
	@MaDocGia nvarchar(50)
as
begin
select *
from DocGia
where MaDocGia like '%'+@MaDocGia+'%'
End



alter proc [dbo].[DocGia_Del]
@MaDocGia nvarchar(50)
as
begin delete from DocGIa
where MaDocGia=@MaDocGia
end

alter proc [dbo].[SP_Update_Pass]
@User nvarchar(50),
@OldPass nvarchar(50),
@NewPass nvarchar(50)
as
begin
    if exists (select * from DocGia where MaDocGia = @User and MatKhau = @OldPass)
    begin
        update DocGia set MatKhau = @NewPass where MaDocGia = @User
        select 1 as code, N'Thay đổi mật khẩu thành công !!' as MSG
    end
    else
        select 0 as code, N'Tài Khoản hoặc mật khẩu sai !!' as MSG
end

alter proc [dbo].[SP_Update_Pass2]
@OldPass nvarchar(50),
@NewPass nvarchar(50)
as
begin
    if exists (select * from DocGia where MatKhau = @OldPass)
    begin
        update DocGia set MatKhau = @NewPass where MatKhau = @OldPass
        select 1 as code, N'Thay đổi mật khẩu thành công !!' as MSG
    end
    else
        select 0 as code, N'Tài Khoản hoặc mật khẩu sai !!' as MSG
end
--Tìm Kiếm Sách
alter proc Sach_TimKiemMaSach
@MaSach nvarchar(50)
As
Begin
	select *
	from Sach
	where MaSach like '%'+@MaSach+'%'
End

alter proc Sach_TimKiemTenSach
@TenSach nvarchar(50)
As
Begin
	select *
	from Sach
	where TenSach like '%'+@TenSach+'%'
End

alter proc Sach_TimKiemTacGia
@TacGia nvarchar(50)
As
Begin
	select *
	from Sach
	where TacGia like '%'+@TacGia+'%'
End