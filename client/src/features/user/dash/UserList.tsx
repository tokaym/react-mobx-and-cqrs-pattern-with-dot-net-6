import { observer } from "mobx-react-lite";
import { NavLink } from "react-router-dom";
import { Button, Divider, Grid, Header, Icon, Segment } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import "../../user/dash/userstyle.css";
import ReportListSearch from "./UserListSearch";
import ReportListTable from "./UserListTable";

export default observer(function UserList() {
    const { userStore } = useStore();
    const { loading, redirectFormPage } = userStore;

    return (
        <>
            <Segment color="red" size="large">
                <Header content={"Kullanıcı Listesi"} size="huge" sub color="black" />
                <br></br>


                <Divider horizontal>
                    <Header as='h4'>
                        <Icon size="small" name="users" />

                    </Header>
                </Divider>


                <Grid>
                    <Grid.Column width={13}>
                        {/* <Button positive
                            as={NavLink} to="/createuser"
                            content="Ekle"/> */}
                    </Grid.Column>
                    <Grid.Column width={2}>
                        <ReportListSearch />
                    </Grid.Column>
                </Grid>

                <ReportListTable />
                {/* <ReportRowZm20List /> */}
            </Segment>
        </>
    )
})