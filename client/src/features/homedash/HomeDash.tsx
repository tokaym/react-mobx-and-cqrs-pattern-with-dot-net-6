import { observer } from "mobx-react-lite";
import { Grid, Header, Label, Segment, Tab, Table } from "semantic-ui-react";
import 'react-semantic-toasts/styles/react-semantic-alert.css';
import "../file/style.css";
import TodayTable from "./TodayTable";
import OrderRates from "./OrderRates";
import AmountByCompanyChart from "./AmountByCompanyChart";
import AmountByMaterialGroupChart from "./AmountByMaterialGroupChart";
import UrgentHaveHFChart from "./UrgentHaveHFChart";
import UrgentNotHaveHFChart from "./UrgentNotHaveHFChart";
import { useStore } from "../../app/stores/store";

const panes = [{
    menuItem: 'BMI - 601S',
    render: () => <Tab.Pane active><Segment color="red" clearing>
        <Grid>
            <Grid.Row columns={2}>
                <Grid.Column>
                    <TodayTable />
                </Grid.Column>
                <Grid.Column>
                    <OrderRates />
                </Grid.Column>
            </Grid.Row>

            <Grid.Row columns={2}>
                <Grid.Column>
                    <AmountByCompanyChart />
                </Grid.Column>
                <Grid.Column>
                    <AmountByMaterialGroupChart />
                </Grid.Column>
            </Grid.Row>

            <Grid.Row columns={2}>
                <Grid.Column>
                    <UrgentHaveHFChart />
                </Grid.Column>
                <Grid.Column>
                    <UrgentNotHaveHFChart />
                </Grid.Column>
            </Grid.Row>
        </Grid>
    </Segment></Tab.Pane>
},
{
    menuItem: 'BMI - Romanya Hub',
    render: () => <Tab.Pane><Segment color="black" clearing>
        <Grid>
            <Grid.Row columns={2}>
                <Grid.Column>
                    <TodayTable />
                </Grid.Column>
                <Grid.Column>
                    <OrderRates />
                </Grid.Column>
            </Grid.Row>

            <Grid.Row columns={2}>
                <Grid.Column>
                    <AmountByCompanyChart />
                </Grid.Column>
                <Grid.Column>
                    <AmountByMaterialGroupChart />
                </Grid.Column>
            </Grid.Row>

            <Grid.Row columns={2}>
                <Grid.Column>
                    <UrgentHaveHFChart />
                </Grid.Column>
                <Grid.Column>
                    <UrgentNotHaveHFChart />
                </Grid.Column>
            </Grid.Row>
        </Grid>
    </Segment></Tab.Pane>
}]

export default observer(function HomeDash() {
    const { homeStore } = useStore();
    const { tabActiveIndex, handleTabChange } = homeStore;

    return (
        <Tab panes={panes} activeIndex={tabActiveIndex} onTabChange={(e) => handleTabChange(e)} />
    )
})