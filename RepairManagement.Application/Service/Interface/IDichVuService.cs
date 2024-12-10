using RepairManagement.Application.Payloads.Requests.Service;
using RepairManagement.Application.Payloads.ResponseDatas;
using RepairManagement.Application.Payloads.Responses.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Service.Interface
{
    public interface IDichVuService
    {
        Task<ResponseObject<DataResponseService>> CreateService(Request_CreateService request);
        Task<IQueryable<DataResponseService>> GetAllServices();
        Task<DataResponseService> GetServiceById(int id);
    }
}
