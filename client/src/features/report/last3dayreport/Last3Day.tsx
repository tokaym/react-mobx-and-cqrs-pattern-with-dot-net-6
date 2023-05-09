import { group } from "console";
import { Formik } from "formik";
import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useHistory } from "react-router-dom";
import { Button, Form, Grid, Header, Icon, Label, Search, Segment, Table } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponents";
import { useStore } from "../../../app/stores/store";
import Report from "./Report";

export default observer(function Last3Day() {
    const { last3dayStore } = useStore();
    const { loadReports, reportRegistry, loadingInitial } = last3dayStore;

    useEffect(() => {
        if (reportRegistry.size < 1) loadReports();
    }, [reportRegistry.size, loadReports])

    return (
        <Grid>
            <Grid.Column width={16}>
                <Report
                />
            </Grid.Column>
        </Grid>

    )
})