import { debug } from 'console';
import { th } from 'date-fns/locale';
import { makeAutoObservable } from 'mobx';
import moment from 'moment';
import { SearchProps } from 'semantic-ui-react';
import agent from '../../api/agent';
import { Report } from '../../models/report';
import { User } from '../../models/user';
import { Zm20 } from '../../models/zm20';
import { store } from '../store';
import { history } from "../../..";
import { ColumnDate, ReportTable } from '../../models/last3day';
import { date, number } from 'yup';


export default class Last3dayStore {
    user: User | null = null;

    reportRegistry = new Map<string, ReportTable>();
    reportPageRegistry = new Map<string, ReportTable>();
    selectedReport: Report | undefined = undefined;
    search: string = "";
    dropdownSelected: string | undefined = "OpenAmount";
    date1: ColumnDate | undefined;
    date2: ColumnDate | undefined;
    date3: ColumnDate | undefined;
    open: boolean | undefined = false;
    pageIndex: number | string | undefined = 1; pageSize: number = 25; totalPages: number = 0;
    column: string = "openAmount"; direction: "ascending" | "descending" = "descending";
    reportDate: string | undefined = undefined;
    editMode = false;
    loading = false;
    loadingInitial = false;
    constructor() {
        makeAutoObservable(this);
    }

    changeDropdown = (value: string | undefined) => {
        this.dropdownSelected = value?.toString();
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

    loadReportswithPage = () => {
        this.clearPageReport();
        let results = Array.from(this.reportRegistry.values()).filter((obj) => {
            return Object.keys(obj).reduce((acc: boolean, curr: string) => {
                return acc || obj.materialName.includes(this.search) || obj.materialSKU.includes(this.search);
            }, false);
        });
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
            const reports = await agent.Reports.last3days(this.dropdownSelected?.toString());
            this.date1 = reports.date1;
            this.date2 = reports.date2;
            this.date3 = reports.date3;
            reports.reportTables.forEach(report => {
                this.setReport(report);
            })

            this.setLoadingInitial(false);
            this.loadReportswithPage();

        } catch (error) {
            console.log(error)
            this.setLoadingInitial(false);
        }
    }
    private setReport = (report: ReportTable) => {
        this.reportRegistry.set(report.materialSKU.toString(), report);
    }

    private setPageReport = (report: ReportTable) => {
        this.reportPageRegistry.set(report.materialSKU.toString(), report);
    }

    private clearPageReport = () => {
        this.reportPageRegistry.clear();
    }
}