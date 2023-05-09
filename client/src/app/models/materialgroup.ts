import { ReturnModel } from "./returnmodel";

export interface MaterialGroup {
    id: string
    materialSKU: string;
    groupName: string;
}

export interface MaterialGroupListPaging {
    index: number,
    size: number,
    count: number,
    pages: number,
    hasPrevious: boolean,
    hasNext: boolean
    items: MaterialGroup[]
}

export class MaterialGroupListPaging implements MaterialGroupListPaging {
    constructor(init?: MaterialGroupPagingValues) {
        Object.assign(this, init);
    }
}


export class MaterialGroup implements MaterialGroup {
    constructor(init?: MaterialGroupFormValues) {
        Object.assign(this, init);
    }
}

export class MaterialGroupPagingValues {
    index: number = 0;
    size: number = 0;
    count: number = 0;
    pages: number = 0;
    hasPrevious: boolean = false;
    hasNext: boolean = false;
    items: MaterialGroup[] = [];

    constructor(reportPaging?: MaterialGroupPagingValues) {
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

export class MaterialGroupFormValues {
    id?: string = undefined;
    materialSKU: string = "";
    groupName: string = "";

    constructor(materialGroup?: MaterialGroupFormValues) {
        if (materialGroup) {
            this.id = materialGroup.id;
            this.materialSKU = materialGroup.materialSKU;
            this.groupName = materialGroup.groupName;
        }
    }
}

export interface MaterialGroupReturnModel extends Omit<ReturnModel, 'data'>{
    data: MaterialGroup;
}