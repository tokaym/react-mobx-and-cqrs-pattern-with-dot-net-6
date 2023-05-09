import { makeAutoObservable } from 'mobx';
import moment from 'moment';
import { report } from 'process';
import agent from '../../api/agent';
import { User } from '../../models/user';

export default class FileStore {
    user: User | null = null;
    uploading = false;
    reportDate: Date | Date[] | null | undefined = new Date();
    reportButtonDisabled: boolean = true;
    zm20isUpload: boolean = false;
    zs14isUpload: boolean = false;
    mb51isUpload: boolean = false;

    constructor() {
        makeAutoObservable(this);
    }

    private checkAllFileUpload = () => {
        if (this.zm20isUpload && this.zs14isUpload && this.mb51isUpload)
            this.reportButtonDisabled = false;

    }

    createReport = async () => {
        if (this.reportDate !== undefined) {
            let reportDate = moment(this.reportDate?.toString()).format("DD.MM.YYYY HH:mm:SS");
            this.uploading = true;
            if (await agent.Reports.create(reportDate, "643")) {
                this.uploading = false;
            };
        }
    }

    handleDateChange = async (value: Date | Date[] | null | undefined) => {
        this.reportDate = value;
    }

    uploadZm20 = async (file: Blob) => {
        try {
            let reportDate = moment(this.reportDate?.toString()).format("DD.MM.YYYY HH:mm:SS");
            this.uploading = true;
            let response = await agent.Files.uploadZm20(file, reportDate);
            this.uploading = false;
            if (response.data) {
                this.zm20isUpload = true;
                this.checkAllFileUpload();
            }
            return response.data
        } catch (error) {
            throw error;
        }
    }

    uploadZs14 = async (file: Blob) => {
        try {
            this.uploading = true;
            let response = await agent.Files.uploadZs14(file);
            this.uploading = false;
            if (response.data) {
                this.zs14isUpload = true;
                this.checkAllFileUpload();
            }
            return response.data;
        } catch (error) {
            throw error;
        }
    }

    uploadMb51 = async (file: Blob) => {
        try {
            let reportDate = moment(this.reportDate?.toString()).format("DD.MM.YYYY HH:mm:SS");
            this.uploading = true;
            let response = await agent.Files.uploadMb51(file, reportDate);
            this.uploading = false;
            if (response.data) {
                this.mb51isUpload = true;
                this.checkAllFileUpload();
            }
            return response.data;
        } catch (error) {
            throw error;
        }
    }
}