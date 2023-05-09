import { dir } from "console";
import { fi } from "date-fns/locale";
import { observer } from "mobx-react-lite";
import moment from "moment";
import { Link, NavLink, useHistory } from "react-router-dom";
import { Button, Checkbox, Confirm, Dimmer, Grid, Label, Loader, Segment, Table } from "semantic-ui-react";
import { number } from "yup/lib/locale";
import { useStore } from "../../../app/stores/store";
import "../../materialgroup/style.css";
import ReportListPaging from "./MaterialGroupListPaging";
import "../dash/style.css"

export default observer(function MaterialGroupListTable() {
    const { materialgroupStore } = useStore();
    const { allList, column, direction, handleSort, loadingInitial, confirmOpen, deleteMaterialGroup, openConfirm, closeConfirm,confirmHeader } = materialgroupStore;

    if (loadingInitial) return (<Dimmer active={loadingInitial} inverted={true}>
        <Loader size="medium" inverted={true} active inline>Yükleniyor...</Loader>
    </Dimmer>)
    return (
        <>

            <Table color="grey" selectable sortable>
                <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell textAlign="center" sorted={column === 'materialSKU' ? direction : undefined}
                            onClick={() => handleSort("materialSKU")} >Malzeme SKU</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center" sorted={column === 'groupName' ? direction : undefined}
                            onClick={() => handleSort("groupName")} >Grup Adı</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center" onClick={() => handleSort("openAmount")} >İşlem</Table.HeaderCell>
                    </Table.Row>
                </Table.Header>

                <Table.Body>
                    {allList.map(({ id, materialSKU, groupName }) => (
                        <Table.Row key={id} >
                            <Table.Cell width={6}>{materialSKU}</Table.Cell>
                            <Table.Cell width={7}>{groupName}</Table.Cell>
                            <Table.Cell textAlign="center">
                                <Grid>
                                    <Grid.Column width={8}>
                                        <Button fluid color="red"
                                            onClick={() => openConfirm(id)}
                                            content="Sil"
                                        />
                                    </Grid.Column>
                                    <Grid.Column width={8}>
                                        <Button fluid color="blue"
                                            as={Link} to={'/editmaterialgroup/' + id}
                                            content="Düzenle" />
                                    </Grid.Column>
                                </Grid>
                            </Table.Cell>
                        </Table.Row>
                    ))}
                </Table.Body>
                <Confirm
                    header={confirmHeader} //{materialSKU + " ve " + groupName}
                    content="Emin misiniz?"
                    cancelButton="İptal"
                    confirmButton="Sil"
                    open={confirmOpen}
                    onCancel={closeConfirm}
                    onConfirm={deleteMaterialGroup}//Silme işlemini gerçekleştircem
                />
                <ReportListPaging />
            </Table>
        </>
    )
})