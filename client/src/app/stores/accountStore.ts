import { User, UserFormValues } from '../models/user';
import { makeAutoObservable, runInAction } from 'mobx';
import agent from '../api/agent';
import { store } from './store';
import { history } from '../..';
import { toast } from 'react-toastify';
import axios from 'axios';

export default class AccountStore {
      user: User | null = null;
      constructor() {
            this.user = null
            makeAutoObservable(this);
      }

      get isLoggedIn() {
            return !!this.user;
      }
      
      login = async (creds: UserFormValues) => {
            try {
                  const user = await agent.Accounts.login(creds);
                  if (user.isSuccess) {
                        await store.commonStore.setToken(user.accessToken.token);
                        runInAction(() => this.user = user);
                        history.push("/home");
                        store.modalStore.closeModal();
                  } else {
                        toast.error(user.message);
                        
                  }
            } catch (error) {
                  throw error;
            }
      }

      logout = () => {
            store.commonStore.setToken(null);
            localStorage.removeItem("jwt");
            this.user = null;
            history.push("/");
      }

      getUser = async () => {
            try {
                  const user = await agent.Accounts.current();
                  runInAction(() => this.user = user);

            } catch (error) {
                  console.log(error);
            }
      }

      register = async (creds: UserFormValues) => {
            try {
                  const user = await agent.Accounts.register(creds);
                  store.commonStore.setToken(user.accessToken.token);
                  runInAction(() => this.user = user);
                  history.push("/activities");
                  store.modalStore.closeModal();
            } catch (error) {
                  throw error;
            }
      }

      setImage = (image: string) => {
            // if (this.user) this.user.image = image;

      }

}