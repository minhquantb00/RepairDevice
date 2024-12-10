
import axios from "axios";
const authorization = localStorage.getItem("accessToken")
  ? localStorage.getItem("accessToken")
  : "";
const CONTROLLER_NAME = "DatLich";

const datLichSuaChua = async (params) => {
  try {
      const result = await axios.post(`https://localhost:7183/api/${CONTROLLER_NAME}/DatLichSuaChua`, params, {
        headers: {
          Authorization: `Bearer ${authorization}`,
        }
      });
      return result.data;
  } catch (error) {
      throw error;
  }
};

const getAllBookings = async () => {
  try{
    const result = await axios.get(`https://localhost:7183/api/${CONTROLLER_NAME}/GetAllBookings`, {
      headers: {
        Authorization: `Bearer ${authorization}`,
      }
    });
    return result;
  }catch (error) {
    throw error;
}
}


export const BookingApi = {
  datLichSuaChua,
  getAllBookings
}
