import { makeAutoObservable } from 'mobx';
import moment from 'moment';
import { report } from 'process';
import agent from '../../api/agent';
import { User } from '../../models/user';

export default class RomaniaFileStore {
    user: User | null = null;
    uploading = false;
    reportDate: Date | Date[] | null | undefined = new Date();
    reportButtonDisabled: boolean = true;
    zm20isUpload: boolean = false;

    constructor() {
        makeAutoObservable(this);
    }

    private checkAllFileUpload = () => {
        if (this.zm20isUpload)
            this.reportButtonDisabled = false;

    }

    createReport = async () => {
        if (this.reportDate !== undefined) {
            let reportDate = moment(this.reportDate?.toString()).format("DD.MM.YYYY HH:mm:SS");
            this.uploading = true;
            if (await agent.Reports.create(reportDate,"909")) {
                this.uploading = false;
            };
        }
    }

    handleDateChange = async (value: Date | Date[] | null | undefined) => {
        this.reportDate = value;
    }

    uploadRomaniaZm20 = async (file: Blob) => {
        try {
            let reportDate = moment(this.reportDate?.toString()).format("DD.MM.YYYY HH:mm:SS");
            this.uploading = true;
            let response = await agent.Files.uploadRomaniaZm20(file, reportDate);
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
}