import axios, { AxiosError, AxiosRequestConfig, AxiosResponse } from "axios";
import { setaxiosURL } from "../api/apiurl";
import { useEffect } from "react";
import { toast } from "react-toastify";
import { bool } from "yup";
import { history } from "../..";
import { Activity, ActivityFormValues } from '../models/activity';
import { BarChart } from "../models/barchart";
import { Last3Day } from "../models/last3day";
import { PieChart } from "../models/piechart";
import { Photo, Profile } from "../models/profile";
import { Report } from "../models/report";
import { ReportPaging } from "../models/reportPaging";
import { TodayTable } from "../models/todaytable";
import { User, UserFormValues, UserPagingValues, UserReturnModel, UserUpdateFormValues } from '../models/user';
import { Zm20 } from "../models/zm20";
import CommonStore from '../stores/commonStore';
import { store } from "../stores/store";
import { Mip, MipFormValues, MipPagingValues, MipReturnModel } from "../models/mip";
import { MaterialGroup, MaterialGroupFormValues, MaterialGroupListPaging, MaterialGroupPagingValues, MaterialGroupReturnModel } from "../models/materialgroup";
import { Estimate } from "../models/estimate";
import { OrderFulfillment } from "../models/orderfulfillment";
import { MailSetting, MailSettingFormValues, MailSettingReturnModel } from "../models/mailsetting";




const sleep = (delay: number) => {
      return new Promise((resolve) => {
            setTimeout(resolve, delay);
      })
}

setaxiosURL();

axios.defaults.headers.common = { 'Authorization': `Bearer ${localStorage.getItem("jwt")}` }

axios.interceptors.response.use(async response => {
      try {
            // await sleep(1000);
            return response;
      } catch (error) {
            console.log(error);
            return await Promise.reject(error);
      }
}, (error: AxiosError) => {
      if (error.response == undefined) {
            history.push("/");
            toast.error("API'ye ulaşılamıyor lütfen BT'yi bilgilendiriniz!");
            return Promise.reject(error);
      }
      const { data, status, config } = error.response!;
      switch (status) {
            case 400:
                  if (typeof data === "string") {
                        toast.error(data)
                  }
                  if (config.method === "get" && data.errors.hasOwnProperty("id")) {
                        history.push("/not-found");
                  }
                  if (data.errors) {
                        const modalStateErrors = [];
                        for (const key in data.errors) {
                              if (data.errors[key]) {
                                    modalStateErrors.push(data.errors[key]);
                              }
                        }
                        throw modalStateErrors.flat();
                  }
                  break;
            case 401:
                  history.push("/");
                  break;
            case 403:
                  //history.goBack();
                  toast.error("Yetkiniz bulunmamaktadır!", {
                        position: toast.POSITION.TOP_CENTER
                  });
                  break;
            case 404:
                  history.push("not-found");
                  break;
            case 500:
                  store.commonStore.setServerError(data);
                  history.push("/server-error");
                  break;


            default:
                  break;
      }
      return Promise.reject(error);

})

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {

      get: <T>(url: string) => axios.get<T>(url).then(responseBody),
      post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
      put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
      del: <T>(url: string) => axios.delete<T>(url).then(responseBody),

}

const Activities = {
      list: () => requests.get<Activity[]>("/activities"),
      details: (id: string) => requests.get<Activity>(`/activities/${id}`),
      create: (activity: ActivityFormValues) => requests.post<void>("/activities", activity),
      update: (activity: ActivityFormValues) => requests.put<void>(`/activities/${activity.id}`, activity),
      delete: (id: string) => requests.del<void>(`/activities/${id}`),
      attend: (id: string) => requests.post<void>(`/activities/${id}/attend`, {})

}

const Accounts = {
      current: () => requests.get<User>("/auth"),
      login: (user: UserFormValues) => requests.post<User>("/auth/login", user),
      register: (user: UserFormValues) => requests.post<User>("/accounts/register", user),

}

const Files = {
      uploadZm20: (_file: Blob, reportDate: string): any => {
            let formData = new FormData();
            formData.append("_file", _file);
            return axios.post<boolean>("/File/UploadZm20?reportDate=" + reportDate, formData, {
                  headers: { "Content-type": "multipart/form-data " }
            });
      },
      uploadRomaniaZm20: (_file: Blob, reportDate: string): any => {
            let formData = new FormData();
            formData.append("_file", _file);
            return axios.post<boolean>("/File/UploadRomaniaZm20?reportDate=" + reportDate, formData, {
                  headers: { "Content-type": "multipart/form-data " }
            });
      },
      uploadZs14: (_file: Blob): any => {
            let formData = new FormData();
            formData.append("_file", _file);
            return axios.post<boolean>("/File/UploadZs14", formData, {
                  headers: { "Content-type": "multipart/form-data " }
            })
      },
      uploadMb51: (_file: Blob, reportDate: string): any => {
            let formData = new FormData();
            formData.append("_file", _file);
            return axios.post<boolean>("/File/UploadMb51?reportDate=" + reportDate, formData, {
                  headers: { "Content-type": "multipart/form-data " }
            })
      },
      mainReportExcel: (page: number | string | undefined, pageSize: number, columnName: string, type: string, search: string, plantCode: string) => {
            return axios.get<Blob>("/File/MainReportExcel?Page=" + page + "&PageSize=" + pageSize + "&ColumnName=" + columnName + "&Type=" + type + "&search=" + search + "&plantCode=" + plantCode, {
                  headers: { "Content-type": "application/vnd.ms-excel" }
            })
      },
}

const Profiles = {
      get: (username: string) => requests.get<Profile>(`/profiles/${username}`),
      uploadPhoto: (file: Blob) => {
            let formData = new FormData();
            formData.append("File", file);
            return axios.post<Photo>("photos", formData, {
                  headers: { "Content-type": "multipart/form-data " }
            })
      },
      setMainPhoto: (id: string) => requests.post(`/photos/${id}/setMain`, {}),
      deletePhoto: (id: string) => requests.del(`/photos/${id}`)
}

const Reports = {
      list: (page: number | string | undefined, pageSize: number, columnName: string, type: string, search: string, date: string, plantCode: string) => {
            return requests.get<ReportPaging>("/MainReport?Page=" + page + "&PageSize=" + pageSize + "&ColumnName=" + columnName + "&Type=" + type + "&search=" + search + "&date=" + date + "&plantCode=" + plantCode)
      },
      create: (reportDate: string, plantCode: string) => requests.post<boolean>("/MainReport?reportDate=" + reportDate + "&plantCode=" + plantCode, {}),
      zm20s: (materialSKU: string) => requests.get<Zm20[]>("/MainReport/GetZm20s?materialSKU=" + materialSKU),
      last3days: (type: string | undefined) => requests.get<Last3Day>("MainReport/GetSumLast3Day?type=" + type),
      reportDates: (plantCode: string) => requests.get<string[]>("/MainReport/GetReportDates?plantCode=" + plantCode),
      orderFulfillment: () => requests.get<OrderFulfillment>("MainReport/GetOrderFulfillment")
}

const Charts = {
      getOrderRates: (plantCode: string) => { return requests.get<PieChart[]>("/HomeDash/GetOrderRates?plantCode=" + plantCode) },
      getOpenAmountByCompany: (plantCode: string) => requests.get<BarChart[]>("/HomeDash/GetOpenAmountByCompany?plantCode=" + plantCode),
      getOpenAmountByMaterialGroup: (plantCode: string) => requests.get<BarChart[]>("/HomeDash/GetOpenAmountByMaterialGroup?plantCode=" + plantCode),
      getUrgentHaveHF: (plantCode: string) => requests.get<BarChart[]>("/HomeDash/GetUrgentHaveHF?plantCode=" + plantCode),
      getUrgentNotHaveHF: (plantCode: string) => requests.get<BarChart[]>("/HomeDash/GetUrgentNotHaveHF?plantCode=" + plantCode),
      getTodayTable: (plantCode: string) => { return requests.get<TodayTable[]>("/HomeDash/GetTodayTable?plantCode=" + plantCode) },
}

const Mips = {
      list: (page: number | string | undefined, pageSize: number, columnName: string, type: string, search: string) => {
            return requests.get<MipPagingValues>("/Mip?Page=" + page + "&PageSize=" + pageSize + "&ColumnName=" + columnName + "&Type=" + type + "&search=" + search)
      },
      getById: (id: string) => requests.get<Mip>("/Mip/" + id),
      create: (mip: MipFormValues) => requests.post<MipReturnModel>("/Mip", mip),
      update: (mip: MipFormValues) => requests.put<MipReturnModel>("/Mip", mip),
      delete: (id: string) => requests.del<MipReturnModel>("/Mip/" + id),
}

const Users = {
      list: (page: number | string | undefined, pageSize: number, columnName: string, type: string, search: string) => {
            return requests.get<UserPagingValues>("/User?Page=" + page + "&PageSize=" + pageSize + "&ColumnName=" + columnName + "&Type=" + type + "&search=" + search)
      },
      getById: (id: string) => requests.get<User>("/User/" + id),
      // create: (mip: MipFormValues) => requests.post<MipReturnModel>("/User", mip),
      update: (user: UserUpdateFormValues) => requests.put<UserReturnModel>("/User", user),
      delete: (id: string) => requests.del<UserReturnModel>("/User/" + id),
}

const MaterialGroups = {
      getById: (id: string) => requests.get<MaterialGroup>("/MaterialGroup/" + id),
      list: (page: number | string | undefined, pageSize: number, columnName: string, type: string, search: string) => {
            return requests.get<MaterialGroupListPaging>("/MaterialGroup?Page=" + page + "&PageSize=" + pageSize + "&ColumnName=" + columnName + "&Type=" + type + "&search=" + search)
      },
      create: (materialGroup: MaterialGroupFormValues) => requests.post<MaterialGroupReturnModel>("/MaterialGroup", materialGroup),
      update: (materialGroup: MaterialGroupFormValues) => requests.put<MaterialGroupReturnModel>("/MaterialGroup", materialGroup),
      delete: (id: string) => requests.del<MaterialGroupReturnModel>("/MaterialGroup/" + id),
}

const Estimates = {
      upload: (_file: Blob): any => {
            let formData = new FormData();
            formData.append("_file", _file);
            return axios.post<boolean>("/Estimate/Upload", formData, {
                  headers: { "Content-type": "multipart/form-data " }
            });
      },
      get: () => requests.get<Estimate[]>("/Estimate/Get"),
}

const Mails = {
      send: (date: string) => requests.post<boolean>("/Email/Send?date=" + date, {}),
      getSettings: () => requests.get<MailSetting>("/Email/Settings"),
      update: (mailSettings: MailSettingFormValues) => requests.put<MailSettingReturnModel>("/Email/SettingsUpdate", mailSettings),
}

const agent = {
      Activities,
      Accounts,
      Profiles,
      Files,
      Reports,
      Charts,
      Mips,
      MaterialGroups,
      Estimates,
      Mails,
      Users
}

export default agent;