import { observer } from "mobx-react-lite";
import { parse } from "node:path/win32";
import { useEffect } from "react";
import { Dimmer, Label, Loader, Progress, Table } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import "../mainreport/style.css";
import Paging from "./Paging";

export default observer(function FulfillmentSummary() {
    const { fulfillmentStore } = useStore();
    const { summary, reportRegistry } = fulfillmentStore;

    return (
        <>
            <Table>
                <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell>Toplam Karşılanan Sat/Sas</Table.HeaderCell>
                        <Table.HeaderCell>Ortalama Karşılama Günü</Table.HeaderCell>
                    </Table.Row>
                </Table.Header>

                <Table.Body>
                    <Table.Row>
                        <Table.Cell>{summary.totalClosedOrder}</Table.Cell>
                        <Table.Cell>{summary.averageDiff}</Table.Cell>
                    </Table.Row>
                </Table.Body>
            </Table>

            <Table definition>
                <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell />
                        <Table.HeaderCell>Sat/Sas Adet</Table.HeaderCell>
                        <Table.HeaderCell>Toplam Karşılanan Sat/Sas Adet</Table.HeaderCell>
                        <Table.HeaderCell>Toplam Karşılanmayan Sat/Sas Adet</Table.HeaderCell>
                        <Table.HeaderCell>Total</Table.HeaderCell>
                        <Table.HeaderCell>Oran</Table.HeaderCell>
                    </Table.Row>
                </Table.Header>

                <Table.Body>
                    <Table.Row>
                        <Table.Cell>0-1 Gün</Table.Cell>
                        <Table.Cell>{summary.diff01}</Table.Cell>
                        <Table.Cell>{summary.totalClosedOrder}</Table.Cell>
                        <Table.Cell>{summary.totalOpenOrder}</Table.Cell>
                        <Table.Cell>{summary.totalOpenOrder + summary.totalClosedOrder}</Table.Cell>
                        <Table.Cell>{parseFloat((summary.diff01 / (summary.totalOpenOrder + summary.totalClosedOrder) * 100).toString()).toFixed(2)}%</Table.Cell>
                    </Table.Row>
                    <Table.Row>
                        <Table.Cell>2-3 Gün</Table.Cell>
                        <Table.Cell>{summary.diff23}</Table.Cell>
                        <Table.Cell>{summary.totalClosedOrder}</Table.Cell>
                        <Table.Cell>{summary.totalOpenOrder}</Table.Cell>
                        <Table.Cell>{summary.totalOpenOrder + summary.totalClosedOrder}</Table.Cell>
                        <Table.Cell>{parseFloat((summary.diff23 / (summary.totalOpenOrder + summary.totalClosedOrder) * 100).toString()).toFixed(2)}%</Table.Cell>
                    </Table.Row>
                    <Table.Row>
                        <Table.Cell>4-7 Gün</Table.Cell>
                        <Table.Cell>{summary.diff47}</Table.Cell>
                        <Table.Cell>{summary.totalClosedOrder}</Table.Cell>
                        <Table.Cell>{summary.totalOpenOrder}</Table.Cell>
                        <Table.Cell>{summary.totalOpenOrder + summary.totalClosedOrder}</Table.Cell>
                        <Table.Cell>{parseFloat((summary.diff47 / (summary.totalOpenOrder + summary.totalClosedOrder) * 100).toString()).toFixed(2)}%</Table.Cell>
                    </Table.Row>
                    <Table.Row>
                        <Table.Cell>7+ Gün</Table.Cell>
                        <Table.Cell>{summary.diffmore}</Table.Cell>
                        <Table.Cell>{summary.totalClosedOrder}</Table.Cell>
                        <Table.Cell>{summary.totalOpenOrder}</Table.Cell>
                        <Table.Cell>{summary.totalOpenOrder + summary.totalClosedOrder}</Table.Cell>
                        <Table.Cell>{parseFloat((summary.diffmore / (summary.totalOpenOrder + summary.totalClosedOrder) * 100).toString()).toFixed(2)}%</Table.Cell>
                    </Table.Row>
                </Table.Body>
            </Table>
        </>
    )
})