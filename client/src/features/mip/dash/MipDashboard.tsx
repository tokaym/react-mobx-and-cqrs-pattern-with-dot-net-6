import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useHistory } from "react-router-dom";
import { Grid } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import MipList from "./MipList";

export default observer(function MipDashboard() {
    const { mipStore } = useStore();
    const { loadList, mipRegistry, mipReturnModel,removeReturnModel } = mipStore;
    const history = useHistory();
    
    useEffect(() => {
        if (mipRegistry.size < 1) {
            loadList();
        };
    }, [mipRegistry.size, mipRegistry])
    
    if(mipReturnModel != undefined){
        removeReturnModel();
        loadList();
    }
    return (
        <Grid>
            <Grid.Column width={16}>
                <MipList
                />
            </Grid.Column>
        </Grid>

    )
})