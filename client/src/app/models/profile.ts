import { User } from "./user";

export interface Profile {
      mail: string;
      employeeNo: string;
      image?: string;
      // bio?: string;
      // photos?:Photo[];
}

export class Profile implements Profile {
      constructor(user: User) {
            this.mail = user.mail;
            this.employeeNo = user.employeeNo;
            // this.image = user.image;
      }
}


export interface Photo {
      id: string;
      url: string;
      isMain: boolean;
}