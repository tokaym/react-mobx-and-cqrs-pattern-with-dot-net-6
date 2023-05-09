import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useHistory, useParams } from "react-router-dom";
import { Grid } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import ReportList from "./ReportList";

export default observer(function ReportDashboard() {
    const history = useHistory();
    const { reportStore } = useStore();
    const { loadReports, reportRegistry, loadingInitial, setPlantCode, plantCode,loadDropdownList } = reportStore;

    const { id } = useParams<{ id: string }>();

    useEffect(() => {
        if (reportRegistry.size < 1) {
            setPlantCode(id);
            loadReports();
        };
    }, [reportRegistry.size, loadReports])

    return (
        <Grid>
            <Grid.Column width={16}>
                <ReportList
                />
            </Grid.Column>
        </Grid>

    )
})