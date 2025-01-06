using Microsoft.AspNetCore.Http;
using RepairManagement.Application.Payloads.Requests.Auth;
using RepairManagement.Application.Payloads.Requests.Device;
using RepairManagement.Application.Payloads.ResponseDatas;
using RepairManagement.Application.Payloads.Responses.Device;
using RepairManagement.Application.Payloads.Responses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Service.Interface
{
    public interface IThietBiService
    {
        Task<ResponseObject<DataResponseThietBi>> CreateThietBi(Request_CreateThietBi request);
        Task<ResponseObject<DataResponseThietBi>> UpdateThietBi(Request_UpdateThietBi request);
        Task<ResponseObject<DataResponseThietBi>> XoaThietBi(int id);
        Task<IQueryable<DataResponseThietBi>> GetAllThietBi(FilterThietBi filter);
        Task<IQueryable<DataResponseThietBi>> GetAllThietBiOfCustomer(FilterThietBiKhachHang filter);
        Task<DataResponseThietBi> GetThietBiById(int id);
        Task<ResponseObject<DataResponseThietBiSuaChua>> CreateThietBiSuaChua(Request_CreateThietBiSuaChua request);
        Task<ResponseObject<DataResponseLichSuSuaChua>> CreateLichSuSuaChua(Request_CreateLichSuSuaChua request);
        Task<ResponseObject<DataResponseLinhKienSuaChua>> CreateLinhKienSuaChua(Request_CreateLinhKienSuaChua request);
        Task<ResponseObject<DataResponsePhanCongCongViec>> CreatePhanCongCongViec(Request_CreatePhanCongCongViec request);
        Task<ResponseObject<DataResponseThietBiSuaChua>> UpdateThietBiSuaChua(Request_UpdateThietBiSuaChua request);
        Task<ResponseObject<DataResponseLinhKienSuaChua>> UpdateLinhKienSuaChua(Request_UpdateLinhKienSuaChua request);
        Task<ResponseObject<DataResponsePhanCongCongViec>> UpdatePhanCongCongViec(Request_UpdatePhanCongCongViec request);
        Task<ResponseObject<DataResponseXuatNhapKho>> CreateXuatNhapKho(Request_CreateXuatNhapKho request);
        Task<IQueryable<DataResponseThietBiSuaChua>> GetAllThietBiSuaChua(FilterThietBiKhachHang filter);
        Task<IQueryable<DataResponsePhanCongCongViec>> GetAllPhanCong(int thietBiSuaChuaId);
        Task<IQueryable<DataResponseUser>> GetAllUser(string? keyword);
        Task<DataResponseThietBiSuaChua> GetThietBiSuaChuaById(int id);
        Task<IQueryable<DataResponsePhanCongCongViec>> GetPhanCongCongViecByNhanVien(int nhanVienId);
        Task<IQueryable<DataResponseLinhKienSuaChua>> GetAllLinhKienSuaChua(int thietBiSuaChuaId);
        Task<IQueryable<DataResponseLinhKien>> GetAllLinhKien();
        Task<DataResponsePhanCongCongViec> GetPhanCongCongViecById(int id);
        Task<IQueryable<DataResponseLichSuSuaChua>> GetAllLichSuSuaChua(int khachHangId);
        Task<ResponseObject<DataResponseLinhKien>> CreateLinhKien(Request_CreateLinhKien request);
        Task<ResponseObject<DataResponseLinhKien>> UpdateLinhKien(Request_UpdateLinhKien request);
        Task<ResponseObject<DataResponseLinhKien>> XoaLinhKien(int id); 
        Task<ResponseObject<DataResponseUser>> CreateNhanVien(Request_register request);
        Task<ResponseObject<DataResponseLinhKienSuaChua>> XoaLinhKienSuaChua(int id);
        Task<DataResponseUser> GetUserById(int id);
        Task<ResponseObject<DataResponseUser>> DeleteUser(int id);
        Task<IQueryable<DataResponseHieuSuatNhanVien>> GetAllHieuSuat(int userId);
        Task<IQueryable<DataResponsePhanCongCongViec>> GetPhanCongCongViecChoXuLyBy();
        Task<IQueryable<DataResponsePhanCongCongViec>> GetPhanCongCongViecDangXuLy();
        Task<IQueryable<DataResponsePhanCongCongViec>> GetPhanCongCongViecDaHoanThanh();
        Task<ResponseObject<string>> CreateUrlPayment(int billId, HttpContext httpContext);
        Task<string> VnPayReturn(IQueryCollection vnpayData);
        Task<ResponseObject<DataResponseHoaDon>> CreateHoaDon(Request_CreateHoaDon request);
        Task<IQueryable<DataResponseThongBao>> GetAllThongBaoByKhachHang(int khachHangId);
        Task<DataResponseGetDataLinhKien> GetDataLinhKien(int billId);
        Task<DataResponseGetDataLinhKien> GetDataLinhKienByNguoiDung(int nguoiDungId);
        Task<IQueryable<DataResponseStatistics>> GetStatistics();
        
    }
}
