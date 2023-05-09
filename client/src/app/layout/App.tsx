import "../layout/sytles.css";
import { observer } from 'mobx-react-lite';
import { Route, useLocation } from 'react-router-dom';
import LoginPage from '../../features/home/LoginPage';
import { ToastContainer } from 'react-toastify';
import { useStore } from '../stores/store';
import { useEffect } from 'react';
import LoadingComponent from './LoadingComponents';
import ModalContainer from '../common/modals/ModalContainer';
import MenuBar from './MenuBar';


function App() {
  const location = useLocation();
  const { commonStore, accountStore } = useStore();

  useEffect(() => {
    if (commonStore.accessToken) {
      // userStore.getUser().finally(() => 
      // commonStore.setAppLoaded()
      // );
      commonStore.setAppLoaded();
    } else {
      commonStore.setAppLoaded();
    }
  }, [commonStore, accountStore]);


  if (!commonStore.appLoaded) return <LoadingComponent content='Uygulama yükleniyor...' />


  return (
    <>
      <ToastContainer position='bottom-center' hideProgressBar />
      <ModalContainer />
      <Route exact path="/" component={LoginPage} />
      <Route
        path={"/(.+)"}
        render={() => (
          <>
            <MenuBar />
          </>
        )}
      />


    </>
  );
}

export default observer(App);
