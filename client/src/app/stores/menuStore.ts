import { makeAutoObservable } from 'mobx';
import { toast } from 'react-toastify';
import { history } from "../..";


export default class MenuStore {

      constructor() {
            makeAutoObservable(this);
      }

      state = {
            sidebarVisible: true
      };

      sidebarVisible = this.state.sidebarVisible;

      toggleSidebar = () => {
            this.sidebarVisible = !this.sidebarVisible
      };

      logout = () =>{
            localStorage.removeItem("jwt");
            history.push("/");
            toast.info("Çıkış yapıldı");
      };
}