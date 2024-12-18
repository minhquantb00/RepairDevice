using RepairManagement.Application.Payloads.Requests.Booking;
using RepairManagement.Application.Payloads.ResponseDatas;
using RepairManagement.Application.Payloads.Responses.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Service.Interface
{
    public interface IDatLichService
    {
        Task<ResponseObject<DataResponseDatLich>> DatLichSuaChua(Request_DatLichSuaChua request);
        Task<IQueryable<DataResponseDatLich>> GetAllBookings(int khachHangId);
    }
}
