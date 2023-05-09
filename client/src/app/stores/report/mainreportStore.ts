import { debug } from 'console';
import { th } from 'date-fns/locale';
import { makeAutoObservable } from 'mobx';
import moment from 'moment';
import { DropdownItemProps, SearchProps } from 'semantic-ui-react';
import agent from '../../api/agent';
import { Report } from '../../models/report';
import { User } from '../../models/user';
import { Zm20 } from '../../models/zm20';
import { store } from '../store';
import { history } from "../../..";
import { boolean } from 'yup';
import ToastHelper from '../../helpers/ToastHelper';


export default class MainReportStore {
    user: User | null = null;

    reportRegistry = new Map<string, Report>();
    zm20Registry = new Map<string, Zm20>();
    selectedReport: Report | undefined = undefined;
    isLoaded: boolean = false;
    open: boolean | undefined = false;
    zm20Loading: boolean | undefined = undefined;
    pageIndex: number | string | undefined = 1; pageSize: number = 25; totalPages: number = 0;
    column: string = "openAmount"; direction: "ascending" | "descending" = "descending";
    search: string | undefined = "";
    reportDate: string | undefined = undefined;
    editMode = false;
    loading = false;
    loadingInitial = false;
    dropdownSelected: string | undefined = "";
    dropdownList: DropdownItemProps[] = [];
    confirmOpen: boolean = false;
    plantCode: string = "643";
    constructor() {
        makeAutoObservable(this);
    }

    sleep = async (delay: number) => {
        return new Promise(() => {
            setTimeout(() => {
                this.loadReports();
            }, delay);
        })
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    handleSearch = async (e: React.MouseEvent, data: SearchProps) => {
        this.search = data.value?.toString();
        await this.sleep(500);
    }

    setPortalControl = (setOpen: boolean | undefined, materialSKU: string) => {
        this.open = setOpen;
        if (setOpen)
            this.loadZm20s(materialSKU);
        else
            this.zm20Registry.clear();
    }

    downloadExcel = async () => {
        this.loading = true;
        let x = await agent.Files.mainReportExcel(0,
            10000, //Page Size 10000
            this.column,
            this.direction === 'descending' ? "desc" : "asc",
            this.search == undefined ? "" : this.search,
            this.plantCode);
        const url = window.URL.createObjectURL(new Blob([x.data]));
        const link = document.createElement('a');
        link.href = url;
        link.setAttribute('download', "Yedek Parça Servis Ana Rapor-" + this.reportDate + ".xls"); //or any other extension
        document.body.appendChild(link);
        link.click();
        this.loading = false;
    }

    handleSort = (column: string) => {
        this.direction = this.direction === 'ascending' ? 'descending' : 'ascending';
        this.column = column;
        this.loadReports();
    }

    setPageIndex = (index: number | string | undefined) => {
        this.pageIndex = index;
        this.loadReports();
    }
    get reports() {
        return Array.from(this.reportRegistry.values());
    }

    get allReports() {
        return this.reports
    }

    get zm20s() {
        return Array.from(this.zm20Registry.values());
    }

    loadZm20s = async (materialSKU: string) => {
        this.zm20Loading = true;
        try {
            this.zm20Registry.clear();
            const zm20s = await agent.Reports.zm20s(materialSKU);
            zm20s.forEach(zm20 => {
                this.setZm20(zm20);
            });
            this.zm20Loading = false;
        } catch (error) {
            console.log(error);
            this.zm20Loading = false;
        }
    }

    // createReport = async () => {
    //     this.loadingInitial = true;
    //     if (await agent.Reports.create()) {
    //         this.loadReports();
    //     };
    // }

    loadReports = async () => {
        this.loadingInitial = true;
        try {
            this.reportRegistry.clear();
            const reports = await agent.Reports.list(Number(this.pageIndex) - 1,
                this.pageSize,
                this.column,
                this.direction === 'descending' ? "desc" : "asc",
                this.search == undefined ? "" : this.search,
                String(this.dropdownSelected),
                this.plantCode ?? "643");
            reports.items.forEach(report => {
                report.hfCloses = report.hfCloses == "2" ? "Kısmen" : report.hfCloses;
                report.sasCloses = report.sasCloses == "2" ? "Kısmen" : report.sasCloses;
                report.urgentCloses = report.urgentCloses == "2" ? "Kısmen" : report.urgentCloses;
                this.setReport(report);
                this.reportDate = moment(new Date(report.reportDate)).format("DD.MM.YYYY HH:mm:ss").toString();
            })
            this.totalPages = reports.pages;
            this.setLoadingInitial(false);

        } catch (error) {
            console.log(error)
            this.setLoadingInitial(false);
        }
    }
    private setReport = (report: Report) => {
        this.reportRegistry.set(report.materialSKU.toString(), report);
    }

    private setZm20 = (zm20: Zm20) => {
        this.zm20Registry.set(zm20.id, zm20);
    }

    loadDropdownList = async () => {
        const dates = await agent.Reports.reportDates(this.plantCode);
        this.dropdownSelected = dates[0];
        dates.forEach(a => {
            let item = { key: a.valueOf(), text: a.valueOf(), value: a.valueOf() };
            this.dropdownList.push(item)
        })
    }

    showToast = (isSend: boolean) => {
        ToastHelper.ToastShow(isSend == true ? 1 : 0, isSend == true ? "Mail gönderildi" : "Mail gönderilirken hata oluştu");
    }

    changeDropdown = (value: string) => {
        this.dropdownSelected = value;
        this.loadReports();
    }

    openConfirm = async () => {
        this.confirmOpen = true;
    }

    closeConfirm = () => {
        this.confirmOpen = false;
    }

    sendMail = async () => {
        this.closeConfirm();
        // ToastHelper.ToastShow(2, "Mail gönderiliyor lütfen bekleyiniz.")
        let isSend = await agent.Mails.send(this.dropdownSelected as string);
        this.showToast(isSend);
    }

    setPlantCode = async (param: string) => {
        this.plantCode = param;
    }
}