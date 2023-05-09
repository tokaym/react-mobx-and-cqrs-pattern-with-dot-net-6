import { makeAutoObservable } from 'mobx';
import { SearchProps } from 'semantic-ui-react';
import agent from '../../api/agent';
import { Report } from '../../models/report';
import { User } from '../../models/user';
import { ColumnDate, ReportTable } from '../../models/last3day';
import { OrderFulfillment, Zm20Material } from '../../models/orderfulfillment';

interface FulFillmentSummary {
    diff01: number,
    diff23: number,
    diff47: number,
    diffmore: number,
    totalOpenOrder: number,
    totalClosedOrder: number,
    averageDiff: string
}

export default class FulFillmentStore {
    user: User | null = null;

    reportRegistry = new Map<string, Zm20Material>();
    reportPageRegistry = new Map<string, Zm20Material>();
    search: string = "";
    open: boolean | undefined = false;
    pageIndex: number | string | undefined = 1; pageSize: number = 25; totalPages: number = 0;
    column: string = "materialSKU"; direction: "ascending" | "descending" = "ascending";
    reportDate: string | undefined = undefined;
    editMode = false;
    loading = false;
    loadingInitial = false;
    isActive: boolean = false;
    selectedMaterialSKU: string = "";
    isOpen: boolean = false;
    sliderChecked: boolean = true;
    totalOpenOrders: number = 1;

    constructor() {
        makeAutoObservable(this);
    }

    handleToggle = (materialSKU: string) => {
        if (this.selectedMaterialSKU == materialSKU) {
            this.isOpen = !this.isOpen;
        } else {
            this.isOpen = true;
        }
        this.selectedMaterialSKU = materialSKU;
    }

    public toggleStyle(materialSKU: string) {
        if (this.selectedMaterialSKU == materialSKU)
            return { display: this.isOpen ? "table-row" : "none" }
        else
            return { display: "none" }
    };

    handleClick = (e: any, titleProps: any) => {
        this.isActive = this.isActive === true ? false : true;
    }
    changeDropdown = (value: string | undefined) => {
        this.loadReports();
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    handleSearch = (e: React.MouseEvent, data: SearchProps) => {
        let key: string = data.value == undefined ? "" : data.value;
        this.search = key;
        this.loadReportswithPage();
    }

    handleSort = (column: string) => {
        this.direction = this.direction === 'ascending' ? 'descending' : 'ascending';
        this.column = column;
        this.loadReportswithPage();
    }

    setPageIndex = (index: number | string | undefined) => {
        this.pageIndex = index;
        this.loadReportswithPage();
    }

    get reports() {
        return Array.from(this.reportPageRegistry.values());
    }

    get allReports() {
        return this.reports
    }

    dynamicSort(property: string) {
        var sortOrder = this.direction == "descending" ? -1 : 1;
        if (property[0] === "-") {
            sortOrder = -1;
            property = property.substr(1);
        }
        return function (a: any, b: any) {
            var result = (a[property] < b[property]) ? -1 : (a[property] > b[property]) ? 1 : 0;
            return result * sortOrder;
        }
    }

    loadReportswithPage = () => {
        this.clearPageReport();
        let results = Array.from(this.reportRegistry.values()).filter((obj) => {
            return Object.keys(obj).reduce((acc: boolean, curr: string) => {
                return acc || obj.materialName.includes(this.search) || obj.materialSKU.includes(this.search);
            }, false);
        });

        results.sort(this.dynamicSort(this.column))

        this.totalPages = Math.round(results.length / 25) == 0 ? 1 : Math.round(results.length / 25);
        if (Number(this.pageIndex) > this.totalPages) {
            this.pageIndex = 1;
        }
        let start: number = ((Number(this.pageIndex ?? 0) - 1) * 25);
        let end: number = (((Number(this.pageIndex ?? 0) - 1) + 1) * 25 - 1);

        results.slice(start, end).forEach(report => {
            this.setPageReport(report);
        });
    }

    loadReports = async () => {
        this.loadingInitial = true;
        try {
            this.reportRegistry.clear();
            const report = await agent.Reports.orderFulfillment();
            this.totalOpenOrders = report.totalOpenOrder;
            report.zm20Materials.forEach(material => {
                this.setReport(material);
            })

            this.setLoadingInitial(false);
            this.loadReportswithPage();

        } catch (error) {
            console.log(error)
            this.setLoadingInitial(false);
        }
    }
    private setReport = (report: Zm20Material) => {
        this.reportRegistry.set(report.materialSKU.toString(), report);
    }

    private setPageReport = (report: Zm20Material) => {
        this.reportPageRegistry.set(report.materialSKU.toString(), report);
    }

    private clearPageReport = () => {
        this.reportPageRegistry.clear();
    }

    get summary() {
        let materialArray = Array.from(this.reportRegistry.values())
        let satSass = 0;
        let _diff01 = 0; let _diff23 = 0; let _diff47 = 0; let _diffmore = 0; let totalDay: number = 0;
        for (let i = 0; i < materialArray.length; i++) {
            for (let a = 0; a < materialArray[i].materialSatSass.length; a++) {
                satSass++;
                let satSasArray = materialArray[i];
                if (satSasArray.materialSatSass[a].dateDayDiff <= 1)
                    _diff01++
                else if (satSasArray.materialSatSass[a].dateDayDiff <= 3)
                    _diff23++
                else if (satSasArray.materialSatSass[a].dateDayDiff <= 7)
                    _diff47++
                else
                    _diffmore++

                totalDay = (totalDay + satSasArray.materialSatSass[a].dateDayDiff);
            }
        }
        const result: FulFillmentSummary = {
            diff01: _diff01,
            diff23: _diff23,
            diff47: _diff47,
            diffmore: _diffmore,
            totalOpenOrder: this.totalOpenOrders,
            totalClosedOrder: satSass,
            averageDiff: parseFloat((totalDay / satSass).toString()).toFixed(2).toString()
        };

        return result;
    }
}