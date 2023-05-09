import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useHistory } from "react-router-dom";
import { Grid } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import UserList from "./UserList";

export default observer(function UserDashboard() {
    const { userStore } = useStore();
    const { loadList, userRegistry, userReturnModel,removeReturnModel } = userStore;
    const history = useHistory();
    
    useEffect(() => {
        if (userRegistry.size < 1) {
            loadList();
        };
    }, [userRegistry.size, userRegistry])
    
    if(userReturnModel != undefined){
        removeReturnModel();
        loadList();
    }
    return (
        <Grid>
            <Grid.Column width={16}>
                <UserList
                />
            </Grid.Column>
        </Grid>

    )
})