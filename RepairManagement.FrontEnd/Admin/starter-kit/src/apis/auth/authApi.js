
import axiosIns from "@/plugins/axios";
import axios from "axios";
const authorization = localStorage.getItem("accessToken")
  ? localStorage.getItem("accessToken")
  : "";
const CONTROLLER_NAME = "Auth";

const login = async (params) => {
  try {
      const result = await axios.post(`https://localhost:7183/api/${CONTROLLER_NAME}/Login`, params);
      return result.data;
  } catch (error) {
      throw error;
  }
};

const register = async (params) => {
  try {
    const result = await axios.post(`https://localhost:7183/api/${CONTROLLER_NAME}/Register`, params);
    return result.data;
  } catch (error) {
    throw error;
  }
};

const forgotPassword = async (params) => {
  try {
    const result = await axios.post(`https://localhost:7183/api/${CONTROLLER_NAME}/ForgotPassword`, params);
    console.log(result);
    return result;
  } catch (error) {
    throw error;
  }
};

const confirmCreateNewPassword = async (params) => {
  try {
    const result = await axios.post(`https://localhost:7183/api/${CONTROLLER_NAME}/ConfirmCreateNewPassword`, params);
    return result;
  } catch (error) {
    throw error;
  }
};

const changePassword = async (params) => {
  try {
    const result = await axiosIns.put(`${CONTROLLER_NAME}/ChangePassword`, params, {
      headers: {
        Authorization: `Bearer ${authorization}`,
      }
    });
    return result.data;
  } catch (error) {
    throw error;
  }
}

export const AuthApi = {
  register,
  login,
  forgotPassword,
  confirmCreateNewPassword,
  changePassword,
}
