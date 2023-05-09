import { group } from "console";
import { Formik } from "formik";
import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useHistory } from "react-router-dom";
import { Button, Form, Grid, Header, Icon, Label, Search, Segment, Table } from "semantic-ui-react";
import ToastHelper from "../../../app/helpers/ToastHelper";
import LoadingComponent from "../../../app/layout/LoadingComponents";
import { useStore } from "../../../app/stores/store";
import MaterialGroupList from "./MaterialGroupList";

export default observer(function MaterialGroupDashboard() {
    const { materialgroupStore } = useStore();
    const { loadList, materialGroupRegistry, materialGroupReturnModel,removeReturnModel } = materialgroupStore;
    const history = useHistory();
    
    useEffect(() => {
        if (materialGroupRegistry.size < 1) {
            loadList();
        };
    }, [materialGroupRegistry.size, materialGroupRegistry])
    
    if(materialGroupReturnModel != undefined){
        removeReturnModel();
        loadList();
    }
    return (
        <Grid>
            <Grid.Column width={16}>
                <MaterialGroupList
                />
            </Grid.Column>
        </Grid>

    )
})