import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useParams } from "react-router-dom";
import { Button, Divider, Dropdown, Grid, Header, Icon, Label, Popup, Segment } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import "../mainreport/style.css";
import ReportListSearch from "./ReportListSearch";
import ReportListTable from "./ReportListTable";
import ReportRowZm20List from "./ReportRowZm20List";

export default observer(function ReportList() {
    const { reportStore } = useStore();
    const { loadDropdownList, downloadExcel, loading, dropdownSelected, dropdownList, changeDropdown, openConfirm, setPlantCode, plantCode,loadReports } = reportStore;

    const { id } = useParams<{ id: string }>();

    useEffect(() => {
        if (dropdownList.length < 1) {
            setPlantCode(id);
            loadDropdownList()
        };
    }, [dropdownList.length, loadDropdownList])

    return (
        <>
            <Segment color="red" size="mini">
                <Header content={id == "643" ? "BMI Yedek Parça Servis Raporu" : "Romanya Yedek Parça Servis Raporu"} size="huge" sub color="black" />


                <Divider horizontal>
                    <Header as='h4'>
                        <Icon name="info" />

                    </Header>
                </Divider>

                <Dropdown placeholder='Raporlar' value={dropdownSelected} onChange={(e, data) => changeDropdown(String(data.value))} search selection options={dropdownList} />

                <Label as='a' basic color='orange' pointing='left'>
                    <Icon name='arrow left' /> Rapor Tarihi
                </Label>

                <Divider horizontal>
                    <Header as='h4'>
                        <Icon size="small" name="chart bar" />

                    </Header>
                </Divider>

                <Grid>
                    <Grid.Column width={13}>
                        <Button.Group basic size='large'>
                            <Button loading={loading} onClick={() => downloadExcel()} icon='file excel' />
                            <Popup content='Mail Gönder' trigger={<Button loading={loading} onClick={() => openConfirm()} icon='send' />} />
                        </Button.Group>
                    </Grid.Column>
                    <Grid.Column width={2}>
                        <ReportListSearch />
                    </Grid.Column>
                </Grid>

                <ReportListTable />
                <ReportRowZm20List />
            </Segment>
        </>
    )
})