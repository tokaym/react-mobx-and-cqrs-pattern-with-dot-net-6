import { makeAutoObservable } from 'mobx';
import moment from 'moment';
import { report } from 'process';
import agent from '../../api/agent';
import { User } from '../../models/user';

export default class EstimateUploadStore {
    user: User | null = null;
    uploading = false;
    estimateisUpload: boolean = false;

    constructor() {
        makeAutoObservable(this);
    }

    uploadEstimate = async (file: Blob) => {
        try {
            this.uploading = true;
            let response = await agent.Estimates.upload(file);
            this.uploading = false;
            if (response.data) {
                this.estimateisUpload = true;
            }
            return response.data
        } catch (error) {
            throw error;
        }
    }
}