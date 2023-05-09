import { observer } from "mobx-react-lite";
import { Dimmer, Label, Loader, Progress, Table } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import "../mainreport/style.css";
import Paging from "./Paging";

export default observer(function ListTable() {
    const { estimateStore } = useStore();
    const { allReports, column, direction, handleSort, } = estimateStore;
    return (
        <>

            <Table color="grey" selectable sortable structured celled>
                <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell textAlign="center" sorted={column === 'materialSKU' ? direction : undefined}
                            onClick={() => handleSort("materialSKU")} >Malzeme SKU</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center" sorted={column === 'materialName' ? direction : undefined}
                            onClick={() => handleSort("materialName")} >Malzeme Adı</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center" sorted={column === 'cd' ? direction : undefined}
                            onClick={() => handleSort("cd")}>CD</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center" sorted={column === 'estimate' ? direction : undefined}
                            onClick={() => handleSort("estimate")}>{new Date().toLocaleString('tr-TR', { month: 'long' })} Ayı Tahmini</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center" sorted={column === 'order' ? direction : undefined}
                            onClick={() => handleSort("order")}>Sipariş</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center" sorted={column === 'sent' ? direction : undefined}
                            onClick={() => handleSort("sent")}>Gönderilen</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center" sorted={column === 'stock' ? direction : undefined}
                            onClick={() => handleSort("stock")}>Stok</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center" sorted={column === 'stockScore' ? direction : undefined}
                            onClick={() => handleSort("stockScore")}>Stok Skor</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center">Tahmin/Sipariş</Table.HeaderCell>
                    </Table.Row>
                </Table.Header>

                <Table.Body>
                    {allReports.map(({ materialSKU, materialName, cd, estimate, order, sent, stock, stockScore }) => (
                        <Table.Row key={materialSKU}>
                            <Table.Cell>{materialSKU}</Table.Cell>
                            <Table.Cell>{materialName}</Table.Cell>
                            <Table.Cell>{cd}</Table.Cell>
                            <Table.Cell>{estimate}</Table.Cell>
                            <Table.Cell>{order}</Table.Cell>
                            <Table.Cell>{sent}</Table.Cell>
                            <Table.Cell>{stock}</Table.Cell>
                            <Table.Cell>{
                                parseFloat(stockScore) < 1 ? (
                                    <Label circular color='red'>
                                        {parseFloat(stockScore.replace(",", ".")).toFixed(2)}
                                    </Label>) :
                                    (
                                        parseFloat(stockScore) < 2 ?
                                            (<Label circular color='yellow'>
                                                {parseFloat(stockScore.replace(",", ".")).toFixed(2)}
                                            </Label>) :
                                            (<Label circular color='green'>
                                                {parseFloat(stockScore.replace(",", ".")).toFixed(2)}
                                            </Label>)
                                    )
                            }</Table.Cell>
                            <Table.Cell>{
                                order > estimate ? (
                                    <Label color='red' tag>
                                        {estimate !== 0 ? 
                                            parseFloat((order / estimate).toString()).toFixed(2)
                                        : order}
                                    </Label>
                                ) : (
                                    <Label color='green' tag>
                                        {estimate !== 0 ? 
                                            parseFloat((order / estimate).toString()).toFixed(2)
                                        : order}
                                    </Label>
                                )
                            }
                            </Table.Cell>
                        </Table.Row>
                    ))}
                </Table.Body>
                <Paging />
            </Table>
        </>
    )
})