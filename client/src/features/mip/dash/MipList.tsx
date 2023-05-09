import { observer } from "mobx-react-lite";
import { NavLink } from "react-router-dom";
import { Button, Divider, Grid, Header, Icon, Segment } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import "../../mip/dash/mipstyle.css";
import ReportListSearch from "./MipListSearch";
import ReportListTable from "./MipListTable";

export default observer(function MipList() {
    const { mipStore } = useStore();
    const { loading, redirectFormPage } = mipStore;

    return (
        <>
            <Segment color="red" size="large">
                <Header content={"Mip Listesi"} size="huge" sub color="black" />
                <br></br>


                <Divider horizontal>
                    <Header as='h4'>
                        <Icon size="small" name="object group" />

                    </Header>
                </Divider>


                <Grid>
                    <Grid.Column width={13}>
                        <Button positive
                            as={NavLink} to="/createmip"
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