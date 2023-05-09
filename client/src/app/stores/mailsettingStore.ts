import { Profile } from '../models/profile';
import { makeAutoObservable, runInAction } from 'mobx';
import agent from "../api/agent";
import { Activity, ActivityFormValues } from '../models/activity';
import { format } from "date-fns";
import { store } from './store';
import { MaterialGroup, MaterialGroupFormValues, MaterialGroupReturnModel } from '../models/materialgroup';
import { User } from '../models/user';
import { SearchProps } from 'semantic-ui-react';
import { useHistory } from 'react-router-dom';
import { toast } from 'react-toastify';
import ToastHelper from '../helpers/ToastHelper';
import { MailSetting, MailSettingFormValues, MailSettingReturnModel } from '../models/mailsetting';



export default class MailSettingStore {
      user: User | null = null;

      mailSettingRegistry = new MailSetting;
      selectedMaterialGroup: MaterialGroup | undefined = undefined;
      isLoaded: boolean = false;
      open: boolean | undefined = false;
      loading = false;
      loadingInitial = false;
      mailSettingReturnModel: MailSettingReturnModel | undefined = undefined;
      constructor() {
            makeAutoObservable(this);
      }

      setLoadingInitial = (state: boolean) => {
            this.loadingInitial = state;
      }
      setReturnModel = (data: MailSettingReturnModel | undefined) => {
            this.mailSettingReturnModel = data;
            ToastHelper.ToastShow(this.mailSettingReturnModel?.status, String(this.mailSettingReturnModel?.message))
      }
      removeReturnModel = () => {
            this.mailSettingReturnModel = undefined;
      }

      redirectFormPage = () => {
            let path = 'createMaterialGroup';
            let history = useHistory();
            history.push(path);
      }

      private getMailSettings = (id: string) => {
            return this.mailSettingRegistry;
      }


      loadMailSettings = async () => {
            this.loadingInitial = true;

            try {
                  let mailSettings = await agent.Mails.getSettings();
                  runInAction(() => {
                        this.mailSettingRegistry = mailSettings;
                  })
                  this.setLoadingInitial(false);
                  return mailSettings;
            } catch (error) {
                  console.log(error);
                  this.setLoadingInitial(false);
            }

      }

      updateMailSettings = async (mailSettings: MailSettingFormValues) => {
            try {
                  let result: MailSettingReturnModel = await agent.Mails.update(mailSettings);
                  runInAction(() => {
                        if (mailSettings.id) {
                              let updatedMaterialGroup = { ...this.getMailSettings(mailSettings.id), ...mailSettings };
                              this.mailSettingRegistry = updatedMaterialGroup as MailSetting;
                        }
                  })
                  return result;
            } catch (error) {
                  console.log(error);
            }
      }

}