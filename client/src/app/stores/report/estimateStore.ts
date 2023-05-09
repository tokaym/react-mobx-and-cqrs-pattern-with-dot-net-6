import { makeAutoObservable } from 'mobx';
import { SearchProps } from 'semantic-ui-react';
import agent from '../../api/agent';
import { Report } from '../../models/report';
import { User } from '../../models/user';
import { ColumnDate, ReportTable } from '../../models/last3day';
import { Estimate } from '../../models/estimate';


export default class EstimateStore {
    user: User | null = null;

    reportRegistry = new Map<string, Estimate>();
    reportPageRegistry = new Map<string, Estimate>();
    search: string = "";
    open: boolean | undefined = false;
    pageIndex: number | string | undefined = 1; pageSize: number = 25; totalPages: number = 0;
    column: string = "materialSKU"; direction: "ascending" | "descending" = "ascending";
    reportDate: string | undefined = undefined;
    editMode = false;
    loading = false;
    loadingInitial = false;
    isActive: boolean = false;
    constructor() {
        makeAutoObservable(this);
    }

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
            const reports = await agent.Estimates.get();
            reports.forEach(report => {
                this.setReport(report);
            })

            this.setLoadingInitial(false);
            this.loadReportswithPage();

        } catch (error) {
            console.log(error)
            this.setLoadingInitial(false);
        }
    }
    private setReport = (report: Estimate) => {
        this.reportRegistry.set(report.materialSKU.toString(), report);
    }

    private setPageReport = (report: Estimate) => {
        this.reportPageRegistry.set(report.materialSKU.toString(), report);
    }

    private clearPageReport = () => {
        this.reportPageRegistry.clear();
    }
}