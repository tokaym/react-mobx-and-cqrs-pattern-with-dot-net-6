import { ReturnModel } from "./returnmodel";

export interface MailSetting {
    id: string
    name: string;
    to: string;
    cc: string;
    from: string;
    description: string;
}

export class MailSetting implements MailSetting {
    constructor(init?: MailSettingFormValues) {
        Object.assign(this, init);
    }
}

export class MailSettingFormValues {
    id?: string = undefined
    name: string = "";
    to: string = "";
    cc: string = "";
    from: string = "";
    description: string = "";

    constructor(mailSettings?: MailSettingFormValues) {
        if (mailSettings) {
            this.id = mailSettings.id;
            this.name = mailSettings.name;
            this.to = mailSettings.to;
            this.cc = mailSettings.cc;
            this.from = mailSettings.from
            this.description = mailSettings.description;
        }
    }
}

export interface MailSettingReturnModel extends Omit<ReturnModel, 'data'> {
    data: MailSetting;
}