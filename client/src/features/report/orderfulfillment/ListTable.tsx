import { observer } from "mobx-react-lite";
import React from "react";
import { Checkbox, Dimmer, Grid, Header, Icon, Label, Loader, Progress, Segment, Step, Table } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import "../mainreport/style.css";
import Paging from "./Paging";

export default observer(function ListTable() {
    const { fulfillmentStore } = useStore();
    const { allReports, column, direction, handleSort, handleToggle, isOpen, selectedMaterialSKU,sliderChecked } = fulfillmentStore;
    const toggleStyle = {
        display: isOpen ? "table-row" : "none"
    };
    return (
        <>

            <Table color="grey" selectable sortable structured celled>
                <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell width={1}></Table.HeaderCell>
                        <Table.HeaderCell width={8} textAlign="center" sorted={column === 'materialSKU' ? direction : undefined}
                            onClick={() => handleSort("materialSKU")} >Malzeme SKU</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center" sorted={column === 'materialName' ? direction : undefined}
                            onClick={() => handleSort("materialName")} >Malzeme Adı</Table.HeaderCell>
                        <Table.HeaderCell>Adet</Table.HeaderCell>
                    </Table.Row>
                </Table.Header>

                <Table.Body>
                    {allReports.map(({ materialSKU, materialName, quantity, materialSatSass }) => (
                        <React.Fragment>
                            <Table.Row key={"row-data-" + materialSKU}>
                                <Table.Cell collapsing>
                                    <Checkbox slider checked={materialSKU == selectedMaterialSKU ? isOpen : false} onClick={() => handleToggle(materialSKU)} />
                                </Table.Cell>
                                <Table.Cell>{materialSKU}</Table.Cell>
                                <Table.Cell>{materialName}</Table.Cell>
                                <Table.Cell>{quantity}</Table.Cell>
                            </Table.Row >

                            {materialSatSass.map(({ satSasNo, openDate, closedDate, dateDayDiff,quantity }) => (
                                selectedMaterialSKU == materialSKU ? (
                                    <React.Fragment>
                                        <Table.Row style={toggleStyle} key={"row-expanded-" + materialSKU}>
                                            <Table.Cell width={1}></Table.Cell>
                                            <Table.Cell width={16} colSpan="3">

                                                <Segment basic>
                                                    <Step.Group>
                                                        <Step active>
                                                            <Icon name='info' />
                                                            <Step.Content>
                                                                <Step.Title>Sat/Sas No: {satSasNo}</Step.Title>
                                                            </Step.Content>
                                                        </Step>
                                                        <Step completed>
                                                            <Step.Content>
                                                                <Icon name='truck' />
                                                                <Step.Title>Açılıs Tarihi</Step.Title>
                                                                <Step.Description>{openDate}</Step.Description>
                                                            </Step.Content>
                                                        </Step>

                                                        <Step completed>
                                                            <Step.Content>
                                                                <Icon name='shipping fast' />
                                                                <Step.Title>Kapanıs Tarihi</Step.Title>
                                                                <Step.Description>{closedDate}</Step.Description>
                                                            </Step.Content>
                                                        </Step>

                                                        <Step completed>
                                                            <Step.Content>
                                                            <Icon name='boxes' />
                                                                <Step.Title>Sipariş Adeti</Step.Title>
                                                                <Step.Description>{quantity} Adet</Step.Description>
                                                            </Step.Content>
                                                        </Step>


                                                        <Step completed>
                                                            <Icon name='add circle' />
                                                            <Step.Content>
                                                                <Step.Title>Kapanma Gün</Step.Title>
                                                                <Step.Description>{dateDayDiff} Gün</Step.Description>
                                                            </Step.Content>
                                                        </Step>

                                                    </Step.Group>
                                                </Segment>
                                            </Table.Cell>
                                        </Table.Row>
                                    </React.Fragment>
                                ) : <></>

                            ))}
                        </React.Fragment>
                    ))}
                </Table.Body>
                <Paging />
            </Table>
        </>
    )
})