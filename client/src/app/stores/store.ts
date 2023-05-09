import { createContext, useContext } from 'react';
import ActivityStore from './activityStore';
import CommonStore from './commonStore';
import AccountStore from './accountStore';
import ModalStore from './modalStore';
import ProfileStore from './profileStore';
import MenuStore from './menuStore';
import ReportStore from './report/mainreportStore';
import FileStore from './fileupload/fileStore';
import HomeStore from './homeStore';
import Last3dayStore from './report/last3dayStore';
import MaterialGroupStore from './materialgroupStore';
import MipStore from './mipStore';
import EstimateUploadStore from './fileupload/estimateuploadStore';
import EstimateStore from './report/estimateStore';
import FulFillmentStore from './report/fulfillmentStore';
import MailSettingStore from './mailsettingStore';
import RomaniaFileStore from './fileupload/romaniafileStore';
import UserStore from './userStore';


interface Store {
      activityStore: ActivityStore,
      commonStore: CommonStore,
      accountStore: AccountStore,
      modalStore: ModalStore,
      profileStore: ProfileStore,
      menuStore: MenuStore,
      reportStore: ReportStore,
      fileStore: FileStore,
      homeStore: HomeStore,
      last3dayStore: Last3dayStore,
      materialgroupStore: MaterialGroupStore,
      mipStore: MipStore,
      estimateUploadStore:EstimateUploadStore,
      estimateStore: EstimateStore,
      fulfillmentStore: FulFillmentStore,
      mailSettingStore: MailSettingStore,
      romaniaFileStore: RomaniaFileStore,
      userStore: UserStore
}

export const store: Store = {
      activityStore: new ActivityStore(),
      commonStore: new CommonStore(),
      accountStore: new AccountStore(),
      modalStore: new ModalStore(),
      profileStore: new ProfileStore(),
      menuStore: new MenuStore(),
      reportStore: new ReportStore(),
      fileStore: new FileStore(),
      homeStore: new HomeStore(),
      last3dayStore: new Last3dayStore(),
      materialgroupStore: new MaterialGroupStore(),
      mipStore: new MipStore(),
      estimateUploadStore: new EstimateUploadStore(),
      estimateStore: new EstimateStore(),
      fulfillmentStore: new FulFillmentStore(),
      mailSettingStore: new MailSettingStore(),
      romaniaFileStore: new RomaniaFileStore(),
      userStore: new UserStore(),
}


export const StoreContext = createContext(store);

export function useStore() {
      return useContext(StoreContext);
}