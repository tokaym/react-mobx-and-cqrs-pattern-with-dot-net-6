import { date } from 'yup';
import { Profile } from './profile';


export interface Report {
    materialSKU: number,
    materialName: string,
    openAmount: number,
    item: number,
    hf: number,
    urgent: number,
    firstOrderDate: Date,
    company: string,
    productClass: string,
    cd: string,
    stock: number,
    sasCloses: string,
    urgentCloses: string,
    hfCloses: string,
    thStock: number,
    mip: number,
    mipLiable: string,
    sent: number,
    tt: string,
    reportDate: Date
}

export class Report implements Report {
    constructor(init?: ReportFormValues) {
        Object.assign(this, init);
    }
}

export class ReportFormValues {
    materialSKU: number = 0;
    materialName: string = "";
    openAmount: number = 0;
    item: number = 0;
    hf: number = 0;
    urgent: number = 0;
    firstOrderDate: Date = new Date();
    company: string = "";
    productClass: string = "";
    cd: string = "";
    stock: number = 0;
    sasCloses: string = "";
    urgentCloses: string ="";
    hfCloses: string ="";
    thStock: number=0;
    mip: number=0;
    mipLiable: string= "";
    sent: number = 0;
    tt: string = "";
    reportDate : Date = new Date();

    constructor(report?: ReportFormValues) {
        if (report) {
            this.materialSKU = report.materialSKU;
            this.materialName = report.materialName;
            this.openAmount = report.openAmount;
            this.item = report.item;
            this.hf = report.hf;
            this.urgent = report.urgent;
            this.firstOrderDate = report.firstOrderDate;
            this.company = report.company;
            this.productClass = report.productClass;
            this.cd = report.cd;
            this.stock = report.stock;
            this.sasCloses = report.sasCloses;
            this.urgentCloses = report.urgentCloses;
            this.hfCloses = report.hfCloses;
            this.thStock = report.thStock;
            this.mip = report.mip;
            this.mipLiable = report.mipLiable;
            this.sent = report.sent;
            this.tt = report.tt;
            this.reportDate = report.reportDate;
        }
    }


}