import { Report } from "./report"

export interface ReportPaging {
    index: number,
    size: number,
    count: number,
    pages: number,
    hasPrevious: boolean,
    hasNext: boolean
    items: Report[]
}

export class ReportPaging implements ReportPaging{
    constructor(init?: ReportPagingValues){
        Object.assign(this,init);
    }
}

export class ReportPagingValues {
    index: number = 0;
    size: number = 0;
    count: number = 0;
    pages: number = 0;
    hasPrevious: boolean = false;
    hasNext: boolean = false;
    items: Report[] = [];

    constructor(reportPaging?: ReportPagingValues){
        if(reportPaging){
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