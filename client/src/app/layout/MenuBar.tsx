import { NavLink, Route, Switch, useLocation } from "react-router-dom";
import { Container, Menu, Dropdown, Icon, Sidebar, Segment } from "semantic-ui-react";
import "../layout/sytles.css";
import { useStore } from '../stores/store';
import { observer } from "mobx-react-lite";
import TestErrors from "../errors/TestErrors";
import ServerError from "../errors/ServerError";
import LoginForm from "../../features/login/LoginForm";
import NotFound from "../errors/NotFound";
import GettingReady from "../errors/GettingReady";
import ReportDashboard from "../../features/report/mainreport/ReportDashboard";
import FileUploadForm from "../../features/file/FileUploadForm";
import HomePage from "../../features/home/LoginPage";
import HomeDash from "../../features/homedash/HomeDash";
import { useEffect } from "react";
import { toast } from "react-toastify";
import { history } from "../..";
import Last3Day from "../../features/report/last3dayreport/Last3Day";
import MaterialGroupDashboard from "../../features/materialgroup/dash/MaterialGroupDashboard";
import MaterialGroupForm from "../../features/materialgroup/form/MaterialGroupForm";
import MipForm from "../../features/mip/form/MipForm";
import MipDashboard from "../../features/mip/dash/MipDashboard";
import EstimationFileUploadForm from "../../features/file/EstimationFileUploadForm";
import Estimate from "../../features/report/estimate/Estimate";
import OrderFulFillment from "../../features/report/orderfulfillment/OrderFulFillment";
import MailSettingForm from "../../features/mailsetting/MailSettingForm";
import RomaniaFileUploadForm from "../../features/file/RomaniaFileUploadForm";
import UserForm from "../../features/user/form/UserForm";
import UserDashboard from "../../features/user/dash/UserDashboard";


export default observer(function MenuBar() {
    const { menuStore: { sidebarVisible, toggleSidebar, logout }, commonStore: { apiSetToken } } = useStore();
    const location = useLocation();
    useEffect(() => {
        if (localStorage.getItem("jwt") === undefined
            || localStorage.getItem("jwt") === null
            || localStorage.getItem("jwt") === "") {

            history.push("");
            toast.error("Oturum için giriş yapınız.");
        }
    })
    return (
        <div style={{ height: '100vh' }}>
            <Sidebar.Pushable style={{ transform: "none" }} as={Segment}>
                <Sidebar
                    as={Menu}
                    animation="overlay"
                    width="thin"
                    visible={sidebarVisible}
                    icon="labeled"
                    vertical
                    className="sidebar-menu"
                    style={{
                        position: "fixed",
                        top: "0px",
                        bottom: "0px",
                        overflowY: "auto",
                        width: "14vh",
                    }}
                >

                    <Menu.Item name="brand" className="item" onClick={toggleSidebar}>
                        <Icon name="bars" />
                    </Menu.Item>
                    <Menu.Item as={NavLink} to="/home" name="home">
                        Ana Sayfa
                        <Icon style={{ "marginTop": "10px" }} name="home" />
                    </Menu.Item>

                    <Dropdown item={true} icon="upload" text='Dosya Yükleme'>
                        <Dropdown.Menu inverted={true}>
                            <Dropdown.Item as={NavLink} icon='factory' to="/upload" text='BMİ - 601S' />
                            <Dropdown.Item as={NavLink} icon='map' to="/uploadromania" text='BMİ - Romanya Hub' />
                            <Dropdown.Item as={NavLink} icon='question' to="/estimationupload" text='Öngörü' />
                        </Dropdown.Menu>
                    </Dropdown>
                    <Dropdown item={true} icon="file word" text='Raporlar'>
                        <Dropdown.Menu>
                            <Dropdown.Item onClick={() => window.location.href="/report/643"} icon='factory' text='BMİ - 601S Servis Raporu' />
                            <Dropdown.Item onClick={() => window.location.href="/report/909"} icon='warehouse' text='BMİ - Romanya Hub Servis Raporu' />
                            <Dropdown.Item as={NavLink} icon='calendar alternate' to="/last3day" text='Son 3 Gün' />
                            <Dropdown.Item as={NavLink} icon='question circle outline' to="/estimation" text='Tahminleme' />
                            <Dropdown.Item as={NavLink} icon='dolly flatbed' to="/orderfulfillment" text='Sipariş Karşılama' />
                        </Dropdown.Menu>
                    </Dropdown>
                    <Dropdown item={true} icon="settings" text='Ayarlar'>
                        <Dropdown.Menu>
                            <Dropdown.Item as={NavLink} icon='boxes' to="/materialgroups" text='Malzeme Grubu' />
                            <Dropdown.Item as={NavLink} icon='clipboard list' to="/mips" text='Mip' />
                            <Dropdown.Item as={NavLink} icon='envelope outline' to="/mailsettings" text='Email' />                            
                            <Dropdown.Item as={NavLink} icon='user' to="/users" text='Kullanıcı İşlemleri' />
                        </Dropdown.Menu>
                    </Dropdown>
                    <Menu.Item icon="sign out" onClick={logout}>
                        Çıkış
                        <Icon name="sign out" />
                    </Menu.Item>
                </Sidebar>
                <Menu inverted={true} borderless fixed="top" className="header-menu" style={{ height: "4em" }}>
                    <Menu.Item
                        position="left"
                        link
                        icon
                        active={sidebarVisible}
                        onClick={toggleSidebar}
                    >
                        <Icon
                            name="bars"
                            aria-label="menu"
                            className="menu-icon"
                            size="large"
                        />
                    </Menu.Item>
                    <Menu.Item as={NavLink} to="/home" exact header>
                        <img src={window.location.protocol + "//" + window.location.host + "/assets/arlogo.png"} alt="logo" style={{ width: "7em", marginRight: "10px" }} />
                    </Menu.Item>
                </Menu>
                <Sidebar.Pusher style={{ overflow: "auto", "backgroundColor": "white" }} dimmed={false}>
                    <div style={{ overflow: "hidden" }}>
                        <Container style={{ marginTop: "7em", width: "85%" }} >
                            <Switch>
                                <Route exact path="/" component={HomePage} />
                                <Route path={"/report/:id"} component={ReportDashboard} />
                                <Route path="/upload" component={FileUploadForm} />
                                <Route key={location.key} path={["/creatematerialGroup", "/editmaterialgroup/:id"]} component={MaterialGroupForm} />
                                <Route key={location.key} path={["/createmip", "/editmip/:id"]} component={MipForm} />
                                <Route key={location.key} path={["/edituser/:id"]} component={UserForm} />
                                <Route path="/errors" component={TestErrors} />
                                <Route path="/last3day" component={Last3Day} />
                                <Route path="/server-error" component={ServerError} />
                                <Route path="/login" component={LoginForm} />
                                <Route path="/home" component={HomeDash} />
                                <Route path="/gettingready" component={GettingReady} />
                                <Route path="/materialgroups" component={MaterialGroupDashboard} />
                                <Route path="/mips" component={MipDashboard} />
                                <Route path="/users" component={UserDashboard} />
                                <Route path="/estimationupload" component={EstimationFileUploadForm} />
                                <Route path="/estimation" component={Estimate} />
                                <Route path="/orderfulfillment" component={OrderFulFillment} />
                                <Route path="/mailsettings" component={MailSettingForm} />
                                <Route path="/uploadromania" component={RomaniaFileUploadForm} />
                                <Route component={NotFound} />
                            </Switch>
                        </Container>
                    </div>
                </Sidebar.Pusher>
            </Sidebar.Pushable>
        </div>
    )
})