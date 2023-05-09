import { dir } from "console";
import { fi } from "date-fns/locale";
import { observer } from "mobx-react-lite";
import moment from "moment";
import { Button, Checkbox, Dimmer, Label, Loader, Progress, Segment, Table } from "semantic-ui-react";
import { number } from "yup/lib/locale";
import { useStore } from "../../../app/stores/store";
import "../mainreport/style.css";
import Paging from "./Paging";
import ReportListPaging from "./Paging";

export default observer(function ListTable() {
    const { last3dayStore } = useStore();
    const { allReports, column, direction, handleSort, loadingInitial, date1, date2, date3 } = last3dayStore;
    if (loadingInitial) return (<Dimmer active={loadingInitial} inverted={true}>
        <Loader size="medium" inverted={true} active inline>Yükleniyor...</Loader>
    </Dimmer>)
    return (
        <>

            <Table color="grey" selectable sortable structured celled>
                <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell textAlign="center" width={2} rowSpan='2' sorted={column === 'materialSKU' ? direction : undefined}
                            onClick={() => handleSort("materialSKU")} >Malzeme SKU</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center" width={3} rowSpan='2' sorted={column === 'materialName' ? direction : undefined}
                            onClick={() => handleSort("materialName")} >Malzeme Adı</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center" width={3} rowSpan='1'>{date1?.name}</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center" width={3} rowSpan='1'>{date2?.name}</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center" width={3} rowSpan='1'>{date3?.name}</Table.HeaderCell>
                    </Table.Row>
                    <Table.Row>
                        <Table.HeaderCell textAlign="center"><Label color="grey">Toplam : {date1?.sum}</Label></Table.HeaderCell>
                        <Table.HeaderCell textAlign="center"><Label color="grey">Toplam : {date2?.sum}</Label></Table.HeaderCell>
                        <Table.HeaderCell textAlign="center"><Label color="grey">Toplam : {date3?.sum}</Label></Table.HeaderCell>
                    </Table.Row>
                </Table.Header>

                <Table.Body>
                    {allReports.map(({ materialSKU, materialName, date1Value, date2Value, date3Value }) => (
                        <Table.Row key={materialSKU}>
                            <Table.Cell>{materialSKU}</Table.Cell>
                            <Table.Cell>{materialName}</Table.Cell>
                            <Table.Cell>
                                <Progress color="blue" percent= {date1Value != 0 ? (date1Value / (date1?.sum ?? 1) * 500) : 0}>
                                    {date1Value}
                                </Progress>
                            </Table.Cell>
                            <Table.Cell>
                                <Progress  color="blue" percent={date2Value != 0 ? (date2Value / (date2?.sum ?? 1) * 500) : 0}>
                                    {date2Value}
                                </Progress>
                            </Table.Cell>
                            <Table.Cell>
                                <Progress  color="blue" percent={date3Value != 0 ? (date3Value / (date3?.sum ?? 1) * 500) : 0}>
                                    {date3Value}
                                </Progress>
                            </Table.Cell>
                        </Table.Row>
                    ))}
                </Table.Body>
                <Paging />
            </Table>
        </>
    )
})