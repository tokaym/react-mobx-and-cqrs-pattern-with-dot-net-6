import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { Dimmer, Grid, Loader } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import Report from "./Report";

export default observer(function OrderFulFillment() {
    const { fulfillmentStore } = useStore();
    const { loadReports, reportRegistry, loadingInitial } = fulfillmentStore;

    useEffect(() => {
        if (reportRegistry.size < 1) loadReports();
    }, [reportRegistry.size, loadReports])
    
    
    if (loadingInitial) return (<Dimmer active={loadingInitial} inverted={true}>
        <Loader size="medium" inverted={true} active inline>Lütfen bekleyiniz.</Loader>
    </Dimmer>)
    return (
        <Grid>
            <Grid.Column width={16}>
                <Report
                />
            </Grid.Column>
        </Grid>

    )
})