import { ReturnModel } from "./returnmodel";

export interface Mip {
    id: string
    code: string;
    cd: string;
}


export interface MipListPaging {
    index: number,
    size: number,
    count: number,
    pages: number,
    hasPrevious: boolean,
    hasNext: boolean
    items: Mip[]
}

export class MipListPaging implements MipListPaging {
    constructor(init?: MipPagingValues) {
        Object.assign(this, init);
    }
}


export class Mip implements Mip {
    constructor(init?: MipFormValues) {
        Object.assign(this, init);
    }
}

export class MipPagingValues {
    index: number = 0;
    size: number = 0;
    count: number = 0;
    pages: number = 0;
    hasPrevious: boolean = false;
    hasNext: boolean = false;
    items: Mip[] = [];

    constructor(reportPaging?: MipPagingValues) {
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

export class MipFormValues {
    id?: string = undefined;
    code: string = "";
    cd: string = "";

    constructor(mip?: MipFormValues) {
        if (mip) {
            this.id = mip.id;
            this.code = mip.code;
            this.cd = mip.cd;
        }
    }
}

export interface MipReturnModel extends Omit<ReturnModel, 'data'>{
    data: Mip;
}