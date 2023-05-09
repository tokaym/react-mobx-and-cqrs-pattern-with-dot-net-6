import { observer } from "mobx-react-lite";
import { Dimmer, Label, Loader, Progress, Table } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import "../mainreport/style.css";
import Paging from "./Paging";

export default observer(function ListTable() {
    const { estimateStore } = useStore();
    const { allReports, column, direction, handleSort,  } = estimateStore;
    return (
        <>

            <Table color="grey" selectable sortable structured celled>
                <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell textAlign="center">Tahmin/Sipariş</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center"></Table.HeaderCell>
                        <Table.HeaderCell textAlign="center">CD</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center">{new Date().toLocaleString('tr-TR', { month: 'long' })} Ayı Tahmini</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center">Sipariş</Table.HeaderCell>
                    </Table.Row>
                </Table.Header>

                <Table.Body>
                </Table.Body>
                <Paging />
            </Table>
        </>
    )
})