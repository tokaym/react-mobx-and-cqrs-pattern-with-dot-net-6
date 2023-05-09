import { observer } from "mobx-react-lite";
import moment from "moment";
import { Dimmer, Divider, Header, Loader, Segment, Table, TransitionablePortal } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";

export default observer(function ReportRowZm20List() {
    const { reportStore } = useStore();
    const { open, zm20s, zm20Loading } = reportStore;

    return (
        <TransitionablePortal open={open} transition={{ animation: "fade left", duration: 200 }}>

            <Segment
                style={{
                    right: '0%',
                    position: 'fixed',
                    top: '3em',
                    zIndex: 1000,
                    width: "60em",
                    height: "100%",
                    "overflow-x":"scroll"
                }}
            >

                <Divider horizontal>
                    <Header as='h4'>
                        ZM20
                    </Header>
                </Divider>
                <Table>

                    <Dimmer active={zm20Loading} inverted={true}>
                        <Loader size="medium" inverted={true} active inline>Yükleniyor...</Loader>
                    </Dimmer>

                    <Table.Header>
                        <Table.Row>
                            <Table.HeaderCell>Malzeme SKU</Table.HeaderCell>
                            <Table.HeaderCell>Malzeme Adı</Table.HeaderCell>
                            <Table.HeaderCell>Adet</Table.HeaderCell>
                            <Table.HeaderCell>Tarih</Table.HeaderCell>
                            <Table.HeaderCell>Sat/Sas No</Table.HeaderCell>
                        </Table.Row>
                    </Table.Header>

                    <Table.Body>
                        {zm20s.map((zm20) =>
                            <Table.Row key={zm20.materialSKU}>
                                <Table.Cell>{zm20.materialSKU}</Table.Cell>
                                <Table.Cell>{zm20.materialName}</Table.Cell>
                                <Table.Cell>{zm20.openAmount}</Table.Cell>
                                <Table.Cell>{moment(new Date(zm20.releaseDate)).format("DD/MM/YYYY")}</Table.Cell>
                                <Table.Cell>{zm20.satSasNo}</Table.Cell>
                            </Table.Row>
                        )}
                    </Table.Body>
                </Table>
            </Segment>
        </TransitionablePortal>

    )
})