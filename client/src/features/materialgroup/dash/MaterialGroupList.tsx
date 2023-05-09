import { dir } from "console";
import { fi } from "date-fns/locale";
import { observer } from "mobx-react-lite";
import moment from "moment";
import { useEffect } from "react";
import { NavLink, Redirect, useHistory } from "react-router-dom";
import { Button, Checkbox, Divider, Dropdown, Grid, Header, Icon, Label, Menu, Pagination, Portal, Search, Segment, Table, TransitionablePortal } from "semantic-ui-react";
import { number } from "yup/lib/locale";
import { useStore } from "../../../app/stores/store";
import "../../materialgroup/style.css";
import ReportListPaging from "./MaterialGroupListPaging";
import ReportListSearch from "./MaterialGroupListSearch";
import ReportListTable from "./MaterialGroupListTable";

export default observer(function MaterialGroupList() {
    const { materialgroupStore } = useStore();
    const { loading, redirectFormPage } = materialgroupStore;

    return (
        <>
            <Segment color="red" size="large">
                <Header content={"Malzeme Grubu Listesi"} size="huge" sub color="black" />
                <br></br>


                <Divider horizontal>
                    <Header as='h4'>
                        <Icon size="small" name="object group" />

                    </Header>
                </Divider>


                <Grid>
                    <Grid.Column width={13}>
                        <Button positive
                            as={NavLink} to="/creatematerialGroup"
                            content="Ekle"/>
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