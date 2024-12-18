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
        Task<IQueryable<DataResponseLinhKienSuaChua>> GetAllLinhKienSuaChua();
        Task<IQueryable<DataResponseLinhKien>> GetAllLinhKien();
        Task<DataResponsePhanCongCongViec> GetPhanCongCongViecById(int id);
    }
}
