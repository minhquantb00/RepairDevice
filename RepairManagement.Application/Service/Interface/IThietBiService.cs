using RepairManagement.Application.Payloads.Requests.Device;
using RepairManagement.Application.Payloads.ResponseDatas;
using RepairManagement.Application.Payloads.Responses.Device;
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
        Task<DataResponseThietBi> GetThietBiById(int id);
    }
}
