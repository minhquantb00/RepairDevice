
import axios from "axios";
const authorization = localStorage.getItem("accessToken")
  ? localStorage.getItem("accessToken")
  : "";
const CONTROLLER_NAME = "DichVu";

const createService = async (params) => {
  try {
      const result = await axios.post(`https://localhost:7183/api/${CONTROLLER_NAME}/CreateService`, params);
      return result.data;
  } catch (error) {
      throw error;
  }
};

const getAllServices = async () => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetAllServices`);
    return result;
  } catch (error) {
    throw error;
  }
};

const getServiceById = async (id) => {
  try {
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetServiceById/${id}`);
    return result;
  } catch (error) {
    throw error;
  }
};

export const ServiceApi = {
  createService,
  getAllServices,
  getServiceById
}
