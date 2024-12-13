
import axios from "axios";
const authorization = localStorage.getItem("accessToken")
  ? localStorage.getItem("accessToken")
  : "";
const CONTROLLER_NAME = "KhachHang";

const createKhachHang = async (params) => {
  try {
      const result = await axios.post(`https://localhost:7183/api/${CONTROLLER_NAME}/CreateKhachHang`, params, {
        headers: {
          Authorization: `Bearer ${authorization}`,
        }
      });
      return result.data;
  } catch (error) {
      throw error;
  }
};

const getAllKhachHang = async (params) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetAllKhachHang`, {
      params: {
        keyword: params.keyword
      }
    });
    return result;
  } catch (error) {
    throw error;
  }
};

const updateKhachHang = async (params) => {
  try {
      const result = await axios.put(`https://localhost:7183/api/${CONTROLLER_NAME}/UpdateKhachHang`, params, {
        headers: {
          Authorization: `Bearer ${authorization}`,
        }
      });
      return result.data;
  } catch (error) {
      throw error;
  }
};

const getKhachHangById = async (id) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetKhachHangById/${id}`);
    return result;
  } catch (error) {
    throw error;
  }
};

const deleteKhachHang = async (id) => {
  try {
    const result = await axios.delete(`https://localhost:7183/api/${CONTROLLER_NAME}/DeleteKhachHang/${id}`, {
      headers: {
        Authorization: `Bearer ${authorization}`,
      }
    });
    return result;
  } catch (error) {
    throw error;
  }
};

export const CustomerApi = {
  createKhachHang,
  getAllKhachHang,
  getKhachHangById,
  updateKhachHang,
  deleteKhachHang,
}
