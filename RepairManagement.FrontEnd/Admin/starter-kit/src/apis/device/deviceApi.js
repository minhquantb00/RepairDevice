
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

const getAllLoaiThietBis = async () => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetAllLoaiThietBis`);
    return result;
  } catch (error) {
    throw error;
  }
};

const createThietBiSuaChua = async (params) => {
  try {
      const result = await axios.post(`https://localhost:7183/api/${CONTROLLER_NAME}/CreateThietBiSuaChua`, params);
      return result.data;
  } catch (error) {
      throw error;
  }
};
const createPhanCongCongViec = async (params) => {
  try {
      const result = await axios.post(`https://localhost:7183/api/${CONTROLLER_NAME}/CreatePhanCongCongViec`, params, {
        headers: {
          Authorization: `Bearer ${authorization}`,
        }
      });
      return result.data;
  } catch (error) {
      throw error;
  }
};
const xoaThietBi = async (id) => {
  try {
      const result = await axios.delete(`https://localhost:7183/api/${CONTROLLER_NAME}/XoaThietBi/${id}`, {
        headers: {
          Authorization: `Bearer ${authorization}`,
        }
      });
      return result.data;
  } catch (error) {
      throw error;
  }
};
const updateThietBiSuaChua = async (params) => {
  try {
      const result = await axios.put(`https://localhost:7183/api/${CONTROLLER_NAME}/UpdateThietBiSuaChua`, params, {
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
const updateLinhKienSuaChua = async (params) => {
  try {
      const result = await axios.put(`https://localhost:7183/api/${CONTROLLER_NAME}/UpdateLinhKienSuaChua`, params, {
        headers: {
          Authorization: `Bearer ${authorization}`,
        }
      });
      return result.data;
  } catch (error) {
      throw error;
  }
};
const createLinhKienSuaChua = async (params) => {
  try {
      const result = await axios.post(`https://localhost:7183/api/${CONTROLLER_NAME}/CreateLinhKienSuaChua`, params, {
        headers: {
          Authorization: `Bearer ${authorization}`,
        }
      });
      return result.data;
  } catch (error) {
      throw error;
  }
};
const updatePhanCongCongViec = async (params) => {
  try {
      const result = await axios.put(`https://localhost:7183/api/${CONTROLLER_NAME}/UpdatePhanCongCongViec`, params, {
        headers: {
          Authorization: `Bearer ${authorization}`,
        }
      });
      return result.data;
  } catch (error) {
      throw error;
  }
};
const updateThietBi = async (params) => {
  try {
      const result = await axios.put(`https://localhost:7183/api/${CONTROLLER_NAME}/UpdateThietBi`, params, {
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

const createXuatNhapKho = async (params) => {
  try {
      const result = await axios.post(`https://localhost:7183/api/${CONTROLLER_NAME}/CreateXuatNhapKho`, params, {
        headers: {
          "Content-Type": "multipart/form-data",
        }
      });
      return result.data;
  } catch (error) {
      throw error;
  }
};

const getAllThietBis = async (params) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetAllThietBis`, {
      params: {
        tenThietBi: params.tenThietBi,
        loaiThietBiId: params.loaiThietBiId,
        khachHangId: params.khachHangId
      }
    });
    return result;
  } catch (error) {
    throw error;
  }
};
const getAllThietBiOfCustomer = async (params) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetAllThietBiOfCustomer`, {
      params: {
        keyword: params.keyword
      }
    });
    return result;
  } catch (error) {
    throw error;
  }
};

const getAllThietBiSuaChua = async (params) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetAllThietBiSuaChua`, {
      params: {
        keyword: params.keyword
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

const getThietBiSuaChuaById = async (id) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetThietBiSuaChuaById/${id}`);
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

const getAllPhanCong = async (thietBiSuaChuaId) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetAllPhanCong?thietBiSuaChuaId=${thietBiSuaChuaId}`);
    return result;
  } catch (error) {
    throw error;
  }
};

const getAllUser = async (keyword) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetAllUser?keyword=${keyword}`);
    return result;
  } catch (error) {
    throw error;
  }
};
const getPhanCongCongViecByNhanVien = async (nhanVienId) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetPhanCongCongViecByNhanVien?nhanVienId=${nhanVienId}`);
    return result;
  } catch (error) {
    throw error;
  }
};

const getAllLinhKienSuaChua = async () => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetAllLinhKienSuaChua`);
    return result;
  } catch (error) {
    throw error;
  }
};

const getAllLinhKien = async () => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetAllLinhKien`);
    return result;
  } catch (error) {
    throw error;
  }
};

const getPhanCongCongViecById = async (id) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetPhanCongCongViecById/${id}`);
    return result;
  } catch (error) {
    throw error;
  }
};

export const DeviceApi = {
  getAllLinhKien,
  getAllLinhKienSuaChua,
  createThietBi,
  getAllThietBis,
  getThietBiById,
  createLoaiThietBi,
  getAllLoaiThietBis,
  getLoaiThietBiById,
  getAllThietBiOfCustomer, createPhanCongCongViec,createThietBiSuaChua, createXuatNhapKho,updateLinhKienSuaChua,
  updatePhanCongCongViec,
  updateThietBi,
  updateThietBiSuaChua,
  xoaThietBi,
  getAllThietBiSuaChua,
  getAllPhanCong,
  getAllUser,
  getThietBiSuaChuaById,
  getPhanCongCongViecByNhanVien,
  createLinhKienSuaChua,
  getPhanCongCongViecById
}
