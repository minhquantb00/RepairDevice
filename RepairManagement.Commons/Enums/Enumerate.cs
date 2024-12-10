using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepairManagement.Commons.Enums
{
    public class Enumerate
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Gender
        {
            KhongXacDinh = 0,
            Nam = 1,
            Nu = 2
        }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum BillStatus
        {
            ChuaThanhToan = 0,
            DaThanhToan = 1,
            DaHuy = 2
        }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum ThietBiSuaChuaStatus
        {
            ChuaSua = 0,
            DangSua = 1,
            HoanThanh = 2
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Action
        {
            SuDungDiem = 0,
            TichDiem = 1
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum WarningLevelEnum
        {
            SapHetHang = 0,
            ConNhieu= 1,
            HetHang = 2
        }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum LoaiGiaoDichEnum
        {
            XuatHang = 0,
            NhapHang = 1
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum TrangThaiThietBi
        {
            DangHoatDong = 0,
            DangHong = 1,
            DaSuaXong = 2
        }
    }
}
