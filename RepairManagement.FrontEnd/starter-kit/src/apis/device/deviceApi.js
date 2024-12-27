
import axios from "axios";
const authorization = localStorage.getItem("accessToken")
  ? localStorage.getItem("accessToken")
  : "";
const CONTROLLER_NAME = "ThietBi";

const createLoaiThietBi = async (params) => {
  try {
      const result = await axios.post(`https://localhost:7183/api/${CONTROLLER_NAME}/CreateLoaiThietBi`, params, {
        headers: {
          "Content-Type": "multipart/form-data",
          Authorization: `Bearer ${authorization}`,
        }
      });
      return result.data;
  } catch (error) {
      throw error;
  }
};

const getAllLoaiThietBis = async (params) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetAllLoaiThietBis`);
    return result;
  } catch (error) {
    throw error;
  }
};

const createThietBi = async (params) => {
  try {
      const result = await axios.post(`https://localhost:7183/api/${CONTROLLER_NAME}/CreateThietBi`, params, {
        headers: {
          "Content-Type": "multipart/form-data",
          Authorization: `Bearer ${authorization}`,
        }
      });
      return result.data;
  } catch (error) {
      throw error;
  }
};
const createHoaDon = async (params) => {
  try {
      const result = await axios.post(`https://localhost:7183/api/${CONTROLLER_NAME}/CreateHoaDon`, params, {
        headers: {
          Authorization: `Bearer ${authorization}`,
        }
      });
      return result.data;
  } catch (error) {
      throw error;
  }
};

const createVnPayUrl = async (billId) => {
  try {
    const result = await axios.post(
      `https://localhost:7183/api/${CONTROLLER_NAME}/CreateVnPayUrl?billId=${billId}`,
      {}, // Body của request (trống trong trường hợp này)
      {
        headers: {
          Authorization: `Bearer ${authorization}`, // Đảm bảo truyền vào đúng token
        },
      }
    );
    return result.data;
  } catch (error) {
    console.error('Error creating VnPay URL:', error.response?.data || error.message);
    throw error;
  }
};
const getUserById = async (id) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetUserById/${id}`);
    return result;
  } catch (error) {
    throw error;
  }
};

const getAllThongBaoByKhachHang = async (khachHangId) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetAllThongBaoByKhachHang?khachHangId=${khachHangId}`);
    return result;
  } catch (error) {
    throw error;
  }
};

const getAllThietBis = async (params) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetAllThietBis`, {
      params: {
        tenThietBi: params.tenThietBi,
        loaiThietBiId: params.loaiThietBiId
      }
    });
    return result;
  } catch (error) {
    throw error;
  }
};

const getThietBiById = async (id) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetThietBiById/${id}`);
    return result;
  } catch (error) {
    throw error;
  }
};

const getLoaiThietBiById = async (id) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetLoaiThietBiById/${id}`);
    return result;
  } catch (error) {
    throw error;
  }
};

const getPhanCongCongViecDangXuLy = async (params) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetPhanCongCongViecDangXuLy`, {
      headers: {
        Authorization: `Bearer ${authorization}`,
      }
    });
    return result;
  } catch (error) {
    throw error;
  }
};

const getPhanCongCongViecChoXuLy = async (params) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetPhanCongCongViecChoXuLy`, {
      headers: {
        Authorization: `Bearer ${authorization}`,
      }
    });
    return result;
  } catch (error) {
    throw error;
  }
};

const getPhanCongCongViecDaHoanThanh = async (params) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetPhanCongCongViecDaHoanThanh`, {
      headers: {
        Authorization: `Bearer ${authorization}`,
      }
    });
    return result;
  } catch (error) {
    throw error;
  }
};

const getDataLinhKien = async (billId) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetDataLinhKien?billId=${billId}`);
    return result;
  } catch (error) {
    throw error;
  }
};

const getDataLinhKienByNguoiDung = async (nguoiDungId) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetDataLinhKienByNguoiDung?nguoiDungId=${nguoiDungId}`);
    return result;
  } catch (error) {
    throw error;
  }
};

export const DeviceApi = {
  getDataLinhKienByNguoiDung,
  getDataLinhKien,
  getPhanCongCongViecDaHoanThanh,
  getAllThongBaoByKhachHang,
  getPhanCongCongViecChoXuLy,
  getPhanCongCongViecDangXuLy,
  createThietBi,
  getAllThietBis,
  getThietBiById,
  createLoaiThietBi,
  getAllLoaiThietBis,
  getLoaiThietBiById,
  createHoaDon,
  createVnPayUrl,
  getUserById
}
