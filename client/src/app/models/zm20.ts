import { date } from 'yup';
import { Profile } from './profile';


export interface Zm20 {
    id:string,
    materialSKU: number,
    materialName: string,
    openAmount: number,
    satSasNo: number,
    releaseDate: Date,
}

export class Zm20 implements Zm20 {
    constructor(init?: Zm20FormValues) {
        Object.assign(this, init);
    }
}

export class Zm20FormValues {
    id: string = "";
    materialSKU: number = 0;
    materialName: string = "";
    openAmount: number = 0;
    satSasNo: number = 0;
    releaseDate : Date = new Date();

    constructor(report?: Zm20FormValues) {
        if (report) {
            this.id = report.id
            this.materialSKU = report.materialSKU;
            this.materialName = report.materialName;
            this.openAmount = report.openAmount;
            this.satSasNo = report.satSasNo;
            this.releaseDate = report.releaseDate;
        }
    }


}