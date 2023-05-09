import { dir } from "console";
import { fi } from "date-fns/locale";
import { observer } from "mobx-react-lite";
import moment from "moment";
import { Button, Checkbox, Divider, Dropdown, Grid, Header, Icon, Label, Menu, Pagination, Portal, Search, Segment, Table, TransitionablePortal } from "semantic-ui-react";
import { number } from "yup/lib/locale";
import { useStore } from "../../../app/stores/store";
import "../mainreport/style.css";
import ReportListPaging from "./Paging";
import ReportListSearch from "./Searching";
import ReportListTable from "./ListTable";
import ListTable from "./ListTable";

export default observer(function Report() {
    const { last3dayStore } = useStore();
    const { reportDate, loading, dropdownSelected, changeDropdown } = last3dayStore;
    const type = [
        { key: 'openAmount', text: 'Açık Miktar', value: 'OpenAmount' },
        { key: 'hf', text: 'HF', value: 'Hf' },
        { key: 'urgent', text: 'Acil', value: 'Urgent' },
    ]
    return (
        <>
            <Segment color="red" size="small">
                <Header content={"Son 3 Gün Raporu"} size="huge" sub color="black" />
                <Divider horizontal>
                    <Header as='h4'>
                        <Icon name="info" />

                    </Header>
                </Divider>

                <Dropdown placeholder='State' value={dropdownSelected} onChange={(e,data) => changeDropdown(data.value?.toString())} search selection options={type} />

                <Divider horizontal>
                    <Header as='h4'>
                        <Icon name="line graph" />

                    </Header>
                </Divider>

                <Grid>
                    <Grid.Column width={13}>
                    </Grid.Column>
                    <Grid.Column width={2}>
                        <ReportListSearch />
                    </Grid.Column>
                </Grid>

                <ListTable />
            </Segment>
        </>
    )
})