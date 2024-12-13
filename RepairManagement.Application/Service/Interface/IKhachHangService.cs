using RepairManagement.Application.Payloads.Requests.Customer;
using RepairManagement.Application.Payloads.ResponseDatas;
using RepairManagement.Application.Payloads.Responses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Service.Interface
{
    public interface IKhachHangService
    {
        Task<ResponseObject<DataResponseKhachHang>> CreateKhachHang(Request_CreateKhachHang request);
        Task<ResponseObject<DataResponseKhachHang>> UpdateKhachHang(Request_UpdateKhachHang request);
        Task<ResponseObject<DataResponseKhachHang>> DeleteKhachHang(int id);
        Task<IQueryable<DataResponseKhachHang>> GetAllKhachHang(FilterCustomer? filter);
        Task<DataResponseKhachHang> GetKhachHangById(int id);
    }
}
