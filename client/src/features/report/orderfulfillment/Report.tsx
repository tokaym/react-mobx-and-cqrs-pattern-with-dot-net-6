import { observer } from "mobx-react-lite";
import { Accordion, Dimmer, Divider, Dropdown, Grid, Header, Icon, Label, Loader, Segment, Table } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import "../mainreport/style.css";
import ReportListSearch from "./Searching";
import ListTable from "./ListTable";
import FulfillmentSummary from "./FulfillmentSummary";

export default observer(function Report() {
    const { fulfillmentStore } = useStore();
    const { reportDate, loading, changeDropdown, isActive, handleClick } = fulfillmentStore;
    const type = [
        { key: 'openAmount', text: 'Açık Miktar', value: 'OpenAmount' },
        { key: 'hf', text: 'HF', value: 'Hf' },
        { key: 'urgent', text: 'Acil', value: 'Urgent' },
    ]
    return (
        <>
            <Segment color="red" size="small">
                <Header content={new Date().toLocaleString('tr-TR', { month: 'long' }) + " Ayı Karşılanan Siparişler"} size="huge" sub color="black" />

                <Divider horizontal>
                    <Header as='h4'>
                        <Icon name="pie chart" />
                        Özet
                    </Header>
                </Divider>

                <FulfillmentSummary />

                <Accordion active={isActive}
                >
                    <Accordion.Title
                        onClick={handleClick}
                        icon=""
                        content={
                            <Divider horizontal>
                                <Header as='h4'>
                                    <Icon name="info" />
                                    Detay için tıklayın
                                </Header>
                            </Divider>}>

                    </Accordion.Title>
                    <Accordion.Content active={isActive}>

                        <ReportListSearch />
                        <ListTable />
                    </Accordion.Content>
                </Accordion>
            </Segment>
        </>
    )
})