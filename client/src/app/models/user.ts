import { ReturnModel } from "./returnmodel";

export interface User {
      id: string;
      mail: string;
      employeeNo: string;
      accessToken: Token;
      name: string;
      surname: string;
      isSuccess: boolean,
      message: string,
      operations: OperationClaim[],
      operationClaimIds: string[];
}

export interface OperationClaim {
      id: string;
      name: string;
      selected: boolean;
}

export interface UserListPaging {
      index: number,
      size: number,
      count: number,
      pages: number,
      hasPrevious: boolean,
      hasNext: boolean
      items: User[]
}

export class UserListPaging implements UserListPaging {
      constructor(init?: UserPagingValues) {
            Object.assign(this, init);
      }
}


export class User implements User {
      constructor(init?: UserUpdateFormValues) {
            Object.assign(this, init);
      }
}

export class UserPagingValues {
      index: number = 0;
      size: number = 0;
      count: number = 0;
      pages: number = 0;
      hasPrevious: boolean = false;
      hasNext: boolean = false;
      items: User[] = [];

      constructor(reportPaging?: UserPagingValues) {
            if (reportPaging) {
                  this.index = reportPaging.index;
                  this.size = reportPaging.size;
                  this.count = reportPaging.count;
                  this.pages = reportPaging.pages;
                  this.hasPrevious = reportPaging.hasPrevious;
                  this.hasNext = reportPaging.hasNext;
                  this.items = reportPaging.items;
            }
      }
}

export class UserUpdateFormValues {
      id: string = "";
      employeeNo: string = "";
      mail: string = "";
      name: string = "";
      surname: string = "";
      operationClaimIds: string[] = [];

      constructor(user?: User) {
            if (user) {
                  this.id = user.id;
                  this.mail = user.mail;
                  this.name = user.name;
                  this.employeeNo = user.employeeNo;
                  this.surname = user.surname;
                  this.operationClaimIds = user.operationClaimIds;
            }
      }
}

export interface UserReturnModel extends Omit<ReturnModel, 'data'> {
      data: User;
}

export interface UserFormValues {
      mail: string;
      password: string;
      employeeNo: string;
}

export interface Token {
      token: string;
      expiration: string;
}