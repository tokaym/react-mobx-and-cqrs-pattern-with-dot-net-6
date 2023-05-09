import { observer } from "mobx-react-lite";
import moment from "moment";
import React from "react";
import { Checkbox, Confirm, Dimmer, Label, Loader, Table } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import "../mainreport/style.css";
import ReportListPaging from "./ReportListPaging";

export default observer(function ReportListTable() {
    const { reportStore } = useStore();
    const { allReports, setPortalControl, column, direction, handleSort, loadingInitial, confirmOpen, openConfirm, closeConfirm, sendMail, plantCode } = reportStore;
    if (loadingInitial) return (<Dimmer active={loadingInitial} inverted={true}>
        <Loader size="medium" inverted={true} active inline>Yükleniyor...</Loader>
    </Dimmer>)
    return (
        <>

            <Table color="grey" selectable sortable>
                <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell>Sat/Sas</Table.HeaderCell>
                        <Table.HeaderCell sorted={column === 'materialSKU' ? direction : undefined}
                            onClick={() => handleSort("materialSKU")} >Malzeme SKU</Table.HeaderCell>
                        <Table.HeaderCell sorted={column === 'materialName' ? direction : undefined}
                            onClick={() => handleSort("materialName")} >Malzeme Adı</Table.HeaderCell>
                        <Table.HeaderCell sorted={column === 'openAmount' ? direction : undefined}
                            onClick={() => handleSort("openAmount")} >Açık Miktar</Table.HeaderCell>
                        <Table.HeaderCell sorted={column === 'item' ? direction : undefined}
                            onClick={() => handleSort("item")} >Kalem</Table.HeaderCell>
                        {plantCode == "643" ? (
                            <React.Fragment>
                                <Table.HeaderCell sorted={column === 'hf' ? direction : undefined}
                                    onClick={() => handleSort("hf")} ><Label color="red">HF</Label></Table.HeaderCell>
                                <Table.HeaderCell sorted={column === 'urgent' ? direction : undefined}
                                    onClick={() => handleSort("urgent")} ><Label color="red">Acil</Label></Table.HeaderCell>
                            </React.Fragment>
                        ) : <></>}
                        <Table.HeaderCell sorted={column === 'firstOrderDate' ? direction : undefined}
                            onClick={() => handleSort("firstOrderDate")} >İlk Sipariş Tarihi</Table.HeaderCell>
                        <Table.HeaderCell sorted={column === 'company' ? direction : undefined}
                            onClick={() => handleSort("company")} >Firma</Table.HeaderCell>
                        {/* <Table.HeaderCell sorted={column === 'productClass' ? direction : undefined}
                            onClick={() => handleSort("productClass")}>Mal Grubu</Table.HeaderCell> */}
                        <Table.HeaderCell sorted={column === 'cd' ? direction : undefined}
                            onClick={() => handleSort("cd")} >CD</Table.HeaderCell>
                        <Table.HeaderCell sorted={column === 'stock' ? direction : undefined}
                            onClick={() => handleSort("stock")} >Stok</Table.HeaderCell>
                        <Table.HeaderCell sorted={column === 'sasCloses' ? direction : undefined}
                            onClick={() => handleSort("sasCloses")} ><Label color="blue">Kapat(Sas)</Label></Table.HeaderCell>
                        <Table.HeaderCell sorted={column === 'urgentCloses' ? direction : undefined}
                            onClick={() => handleSort("urgentCloses")} ><Label color="blue">Kapat(Acil)</Label></Table.HeaderCell>
                        <Table.HeaderCell sorted={column === 'hfCloses' ? direction : undefined}
                            onClick={() => handleSort("hfCloses")} ><Label color="blue">Kapat(HF)</Label></Table.HeaderCell>
                        <Table.HeaderCell sorted={column === 'thStock' ? direction : undefined}
                            onClick={() => handleSort("thStock")} >Th Stok</Table.HeaderCell>
                        {/* <Table.HeaderCell sorted={column === 'mip' ? direction : undefined}
                            onClick={() => handleSort("mip")} >Mip</Table.HeaderCell> */}
                        {/* <Table.HeaderCell>Mip Sorumlusu</Table.HeaderCell> */}
                        <Table.HeaderCell sorted={column === 'sent' ? direction : undefined}
                            onClick={() => handleSort("sent")} >Gönderilen Miktar</Table.HeaderCell>
                        {/* <Table.HeaderCell>TT</Table.HeaderCell> */}
                    </Table.Row>
                </Table.Header>

                <Table.Body>
                    {allReports.map(({ materialSKU, materialName, openAmount, item, hf, urgent, firstOrderDate, company, cd,
                        stock, thStock, mip, sent, productClass, sasCloses, urgentCloses, hfCloses }) => (
                        <Table.Row key={materialSKU}>
                            <Table.Cell>
                                <Checkbox onChange={(e, { checked }) => { setPortalControl(checked, materialSKU.toString()); }} slider />
                            </Table.Cell>
                            <Table.Cell>{materialSKU}</Table.Cell>
                            <Table.Cell>{materialName}</Table.Cell>
                            <Table.Cell>{openAmount}</Table.Cell>
                            <Table.Cell>{item}</Table.Cell>
                            {plantCode == "643" ? (
                                <React.Fragment>
                                    <Table.Cell>{hf}</Table.Cell>
                                    <Table.Cell>{urgent}</Table.Cell>
                                </React.Fragment>
                            ) : <></>}
                            <Table.Cell>{moment(new Date(firstOrderDate)).format("DD/MM/YYYY")}</Table.Cell>
                            <Table.Cell>{company}</Table.Cell>
                            {/* <Table.Cell>{productClass}</Table.Cell> */}
                            <Table.Cell>{cd}</Table.Cell>
                            <Table.Cell>{stock}</Table.Cell>
                            <Table.Cell>{sasCloses}</Table.Cell>
                            <Table.Cell>{urgentCloses}</Table.Cell>
                            <Table.Cell>{hfCloses}</Table.Cell>
                            <Table.Cell>{thStock}</Table.Cell>
                            {/* <Table.Cell>{mip}</Table.Cell> */}
                            {/* <Table.Cell>{mipLiable}</Table.Cell> */}
                            <Table.Cell>{sent}</Table.Cell>
                            {/* <Table.Cell>{report.tt}</Table.Cell> */}
                        </Table.Row>
                    ))}
                </Table.Body>
                <Confirm
                    header="Email Gönderimi"
                    content="Emin misiniz?"
                    cancelButton="İptal"
                    confirmButton="Gönder"
                    open={confirmOpen}
                    onCancel={closeConfirm}
                    onConfirm={sendMail}
                />
                <ReportListPaging />
            </Table>
        </>
    )
})