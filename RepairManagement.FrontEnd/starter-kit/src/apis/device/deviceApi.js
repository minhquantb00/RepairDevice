
import axiosIns from "@/plugins/axios";
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

export const DeviceApi = {
  createThietBi,
  getAllThietBis,
  getThietBiById,
  createLoaiThietBi,
  getAllLoaiThietBis,
  getLoaiThietBiById
}
