import { makeAutoObservable, runInAction } from 'mobx';
import agent from "../api/agent";
import { OperationClaim, User, UserReturnModel, UserUpdateFormValues } from '../models/user';
import { SearchProps } from 'semantic-ui-react';
import { useHistory } from 'react-router-dom';
import ToastHelper from '../helpers/ToastHelper';



export default class UserStore {
    user: User | null = null;
    dropdownOptions: any = [];
    userRegistry = new Map<string, User>();
    selectedUser: User | undefined = undefined;
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
    userReturnModel: UserReturnModel | undefined = undefined;
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

    setReturnModel = (data: UserReturnModel | undefined) => {
        this.userReturnModel = data;
        ToastHelper.ToastShow(this.userReturnModel?.status, String(this.userReturnModel?.message))
    }

    removeReturnModel = () => {
        this.userReturnModel = undefined;
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
        return Array.from(this.userRegistry.values());
    }

    get allList() {
        return this.list
    }

    loadList = async () => {
        this.loadingInitial = true;
        try {
            this.userRegistry.clear();
            const _list = await agent.Users.list(Number(this.pageIndex) - 1,
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
    private setItem = (User: User) => {
        this.userRegistry.set(User.id.toString(), User);
    }

    redirectFormPage = () => {
        let path = 'createUser';
        let history = useHistory();
        history.push(path);
    }

    private getUser = (id: string) => {
        return this.userRegistry.get(id);
    }


    loadUser = async (id: string) => {

        this.loadingInitial = true;

        try {
            let User = await agent.Users.getById(id);
            this.loadOperationDropdown(User);
            this.setUser(User);
            runInAction(() => {
                this.selectedUser = User;
            })
            this.setLoadingInitial(false);
            return User;
        } catch (error) {
            console.log(error);
            this.setLoadingInitial(false);
        }

    }

    loadOperationDropdown = (user: User) => {
        this.dropdownOptions = [];
        user.operations.map((operationClaim: OperationClaim) => {
            this.dropdownOptions.push({ value: operationClaim.id, text: operationClaim.name });
        });
    }

    private setUser = (User: User) => {
        this.userRegistry.set(User.id, User);
    }


    deleteUser = async () => {
        let result: UserReturnModel = await agent.Users.delete(String(this.selectedUser?.id));
        this.confirmOpen = false;
        this.setReturnModel(result);
    }


    //   createUser = async (user: UserUpdateFormValues) => {
    //         try {
    //               let result: UserReturnModel = await agent.Users.create(user);
    //               const newUser = new User(user);
    //               this.setUser(newUser);
    //               runInAction(() => {
    //                     this.selectedUser = newUser;
    //               })
    //               return result;
    //         } catch (error) {
    //               console.log(error);
    //         }
    //   }

    updateUser = async (user: UserUpdateFormValues) => {
        try {
            // user.operationClaimIds.map((operationClaimId: any) => {
            //     operationClaimId = operationClaimId.value;
            // });
            let result: UserReturnModel = await agent.Users.update(user);
            runInAction(() => {
                if (user.id) {
                    let updatedUser = { ...this.getUser(user.id), ...User };
                    this.userRegistry.set(user.id, updatedUser as User);
                    this.selectedUser = updatedUser as User;
                }
            })
            return result;
        } catch (error) {
            console.log(error);
        }
    }

    openConfirm = async (id: string) => {
        this.confirmOpen = true;
        this.selectedUser = this.getUser(id);
        this.confirmHeader = '"' + this.selectedUser?.name + " " + this.selectedUser?.surname + '"'
    }
    closeConfirm = () => {
        this.confirmOpen = false;
    }

}