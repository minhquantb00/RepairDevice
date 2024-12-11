using RepairManagement.Application.Payloads.Requests.DeviceType;
using RepairManagement.Application.Payloads.ResponseDatas;
using RepairManagement.Application.Payloads.Responses.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Service.Interface
{
    public interface ILoaiThietBiService
    {
        Task<ResponseObject<DataResponseLoaiThietBi>> CreateLoaiThietBi(Request_CreateLoaiThietBi request);
        Task<IQueryable<DataResponseLoaiThietBi>> GetAllLoaiThietBi();
        Task<DataResponseLoaiThietBi> GetLoaiThietBiById(int id);
    }
}
