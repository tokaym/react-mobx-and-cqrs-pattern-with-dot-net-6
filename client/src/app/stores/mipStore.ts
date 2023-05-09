import { Profile } from '../models/profile';
import { makeAutoObservable, runInAction } from 'mobx';
import agent from "../api/agent";
import { Activity, ActivityFormValues } from '../models/activity';
import { format } from "date-fns";
import { store } from './store';
import { Mip, MipFormValues, MipReturnModel } from '../models/mip';
import { User } from '../models/user';
import { SearchProps } from 'semantic-ui-react';
import { useHistory } from 'react-router-dom';
import { toast } from 'react-toastify';
import ToastHelper from '../helpers/ToastHelper';



export default class MipStore {
      user: User | null = null;

      mipRegistry = new Map<string, Mip>();
      selectedMip: Mip | undefined = undefined;
      isLoaded: boolean = false;
      confirmOpen: boolean = false;
      confirmHeader: string = "";
      open: boolean | undefined = false;
      zm20Loading: boolean | undefined = undefined;
      pageIndex: number | string | undefined = 1; pageSize: number = 25; totalPages: number = 0;
      column: string = "materialSKU"; direction: "ascending" | "descending" = "descending";
      search: string | undefined = "";
      reportDate: string | undefined = undefined;
      editMode = false;
      loading = false;
      loadingInitial = false;
      mipReturnModel: MipReturnModel | undefined = undefined;
      constructor() {
            makeAutoObservable(this);
      }

      sleep = async (delay: number) => {
            return new Promise(() => {
                  setTimeout(() => {
                        this.loadList();
                  }, delay);
            })
      }

      setLoadingInitial = (state: boolean) => {
            this.loadingInitial = state;
      }

      setReturnModel = (data: MipReturnModel | undefined) => {
            this.mipReturnModel = data;
            ToastHelper.ToastShow(this.mipReturnModel?.status, String(this.mipReturnModel?.message))
      }
      
      removeReturnModel = () => {
            this.mipReturnModel = undefined;
      }



      handleSearch = async (e: React.MouseEvent, data: SearchProps) => {
            this.search = data.value?.toString();
            await this.sleep(500);
      }

      handleSort = (column: string) => {
            this.direction = this.direction === 'ascending' ? 'descending' : 'ascending';
            this.column = column;
            this.loadList();
      }

      setPageIndex = (index: number | string | undefined) => {
            this.pageIndex = index;
            this.loadList();
      }
      get list() {
            return Array.from(this.mipRegistry.values());
      }

      get allList() {
            return this.list
      }

      loadList = async () => {
            this.loadingInitial = true;
            try {
                  this.mipRegistry.clear();
                  const _list = await agent.Mips.list(Number(this.pageIndex) - 1,
                        this.pageSize,
                        this.column,
                        this.direction === 'descending' ? "desc" : "asc",
                        this.search == undefined ? "" : this.search);
                  _list.items.forEach(item => {
                        this.setItem(item);
                  })
                  this.totalPages = _list.pages;

                  this.setLoadingInitial(false);

            } catch (error) {
                  console.log(error)
                  this.setLoadingInitial(false);
            }
      }
      private setItem = (Mip: Mip) => {
            this.mipRegistry.set(Mip.id.toString(), Mip);
      }

      redirectFormPage = () => {
            let path = 'createMip';
            let history = useHistory();
            history.push(path);
      }

      private getMip = (id: string) => {
            return this.mipRegistry.get(id);
      }


      loadMip = async (id: string) => {
            let Mip = this.getMip(id);

            if (Mip) {
                  this.selectedMip = Mip;
                  return Mip;

            } else {
                  this.loadingInitial = true;

                  try {
                        Mip = await agent.Mips.getById(id);
                        this.setMip(Mip);
                        runInAction(() => {
                              this.selectedMip = Mip;
                        })
                        this.setLoadingInitial(false);
                        return Mip;
                  } catch (error) {
                        console.log(error);
                        this.setLoadingInitial(false);
                  }
            }
      }

      private setMip = (Mip: Mip) => {
            this.mipRegistry.set(Mip.id, Mip);
      }


      deleteMip = async () => {
            let result: MipReturnModel = await agent.Mips.delete(String(this.selectedMip?.id));
            this.confirmOpen = false;
            this.setReturnModel(result);
      }


      createMip = async (mip: MipFormValues) => {
            try {
                  let result: MipReturnModel = await agent.Mips.create(mip);
                  const newMip = new Mip(mip);
                  this.setMip(newMip);
                  runInAction(() => {
                        this.selectedMip = newMip;
                  })
                  return result;
            } catch (error) {
                  console.log(error);
            }
      }

      updateMip = async (mip: MipFormValues) => {
            try {
                  let result: MipReturnModel = await agent.Mips.update(mip);
                  runInAction(() => {
                        if (mip.id) {
                              let updatedMip = { ...this.getMip(mip.id), ...Mip };
                              this.mipRegistry.set(mip.id, updatedMip as Mip);
                              this.selectedMip = updatedMip as Mip;
                        }
                  })
                  return result;
            } catch (error) {
                  console.log(error);
            }
      }

      openConfirm = async (id: string) => {
            this.confirmOpen = true;
            this.selectedMip = this.getMip(id);
            this.confirmHeader = '"' + this.selectedMip?.code + '" kodlu mipi sil'
      }
      closeConfirm = () => {
            this.confirmOpen = false;
      }

}