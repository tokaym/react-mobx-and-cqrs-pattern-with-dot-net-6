import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { Dimmer, Grid, Loader } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import Report from "./Report";

export default observer(function Estimate() {
    const { estimateStore } = useStore();
    const { loadReports, reportRegistry, loadingInitial } = estimateStore;

    useEffect(() => {
        if (reportRegistry.size < 1) loadReports();
    }, [reportRegistry.size, loadReports])
    
    
    if (loadingInitial) return (<Dimmer active={loadingInitial} inverted={true}>
        <Loader size="medium" inverted={true} active inline>Bu ayın tahminlemesi yapılıyor lütfen bekleyiniz.</Loader>
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