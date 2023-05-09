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



export default class MaterialGroupStore {
      user: User | null = null;

      materialGroupRegistry = new Map<string, MaterialGroup>();
      selectedMaterialGroup: MaterialGroup | undefined = undefined;
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
      materialGroupReturnModel: MaterialGroupReturnModel | undefined = undefined;
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
      setReturnModel = (data: MaterialGroupReturnModel | undefined) => {
            this.materialGroupReturnModel = data;
            ToastHelper.ToastShow(this.materialGroupReturnModel?.status, String(this.materialGroupReturnModel?.message))
      }
      removeReturnModel = () => {
            this.materialGroupReturnModel = undefined;
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
            return Array.from(this.materialGroupRegistry.values());
      }

      get allList() {
            return this.list
      }

      loadList = async () => {
            this.loadingInitial = true;
            try {
                  this.materialGroupRegistry.clear();
                  const _list = await agent.MaterialGroups.list(Number(this.pageIndex) - 1,
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
      private setItem = (materialgroup: MaterialGroup) => {
            this.materialGroupRegistry.set(materialgroup.id.toString(), materialgroup);
      }

      redirectFormPage = () => {
            let path = 'createMaterialGroup';
            let history = useHistory();
            history.push(path);
      }

      private getMaterialGroup = (id: string) => {
            return this.materialGroupRegistry.get(id);
      }


      loadMaterialGroup = async (id: string) => {
            let materialGroup = this.getMaterialGroup(id);

            if (materialGroup) {
                  this.selectedMaterialGroup = materialGroup;
                  return materialGroup;

            } else {
                  this.loadingInitial = true;

                  try {
                        materialGroup = await agent.MaterialGroups.getById(id);
                        this.setMaterialGroup(materialGroup);
                        runInAction(() => {
                              this.selectedMaterialGroup = materialGroup;
                        })
                        this.setLoadingInitial(false);
                        return materialGroup;
                  } catch (error) {
                        console.log(error);
                        this.setLoadingInitial(false);
                  }
            }
      }

      private setMaterialGroup = (materialGroup: MaterialGroup) => {
            this.materialGroupRegistry.set(materialGroup.id, materialGroup);
      }


      deleteMaterialGroup = async () => {
            let result: MaterialGroupReturnModel = await agent.MaterialGroups.delete(String(this.selectedMaterialGroup?.id));
            this.confirmOpen = false;
            this.setReturnModel(result);
      }


      createMaterialGroup = async (materialGroup: MaterialGroupFormValues) => {
            try {
                  let result: MaterialGroupReturnModel = await agent.MaterialGroups.create(materialGroup);
                  const newMaterialGroup = new MaterialGroup(materialGroup);
                  this.setMaterialGroup(newMaterialGroup);
                  runInAction(() => {
                        this.selectedMaterialGroup = newMaterialGroup;
                  })
                  return result;
            } catch (error) {
                  console.log(error);
            }
      }

      updateMaterialGroup = async (materialGroup: MaterialGroupFormValues) => {
            try {
                  let result: MaterialGroupReturnModel = await agent.MaterialGroups.update(materialGroup);
                  runInAction(() => {
                        if (materialGroup.id) {
                              let updatedMaterialGroup = { ...this.getMaterialGroup(materialGroup.id), ...materialGroup };
                              this.materialGroupRegistry.set(materialGroup.id, updatedMaterialGroup as MaterialGroup);
                              this.selectedMaterialGroup = updatedMaterialGroup as MaterialGroup;
                        }
                  })
                  return result;
            } catch (error) {
                  console.log(error);
            }
      }

      openConfirm = async (id: string) => {
            this.confirmOpen = true;
            this.selectedMaterialGroup = this.getMaterialGroup(id);
            this.confirmHeader = '"' + this.selectedMaterialGroup?.materialSKU + '" ve "' + this.selectedMaterialGroup?.groupName + '"' + ' Gruplamasını Sil'
      }
      closeConfirm = () => {
            this.confirmOpen = false;
      }

}