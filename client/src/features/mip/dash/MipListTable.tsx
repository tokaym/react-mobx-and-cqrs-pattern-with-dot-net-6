import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";
import { Button, Confirm, Dimmer, Grid, Loader, Table } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import "../../mip/dash/mipstyle.css";
import ReportListPaging from "./MipListPaging";

export default observer(function MipListTable() {
    const { mipStore } = useStore();
    const { allList, column, direction, handleSort, loadingInitial, confirmOpen, deleteMip, openConfirm, closeConfirm,confirmHeader } = mipStore;

    if (loadingInitial) return (<Dimmer active={loadingInitial} inverted={true}>
        <Loader size="medium" inverted={true} active inline>Yükleniyor...</Loader>
    </Dimmer>)
    return (
        <>

            <Table color="grey" selectable sortable>
                <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell textAlign="center" sorted={column === 'code' ? direction : undefined}
                            onClick={() => handleSort("materialSKU")} >Mip Kodu</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center" sorted={column === 'cd' ? direction : undefined}
                            onClick={() => handleSort("groupName")} >CD</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center" onClick={() => handleSort("openAmount")} >İşlem</Table.HeaderCell>
                    </Table.Row>
                </Table.Header>

                <Table.Body>
                    {allList.map(({ id, code,cd  }) => (
                        <Table.Row key={id} >
                            <Table.Cell width={6}>{code}</Table.Cell>
                            <Table.Cell width={7}>{cd}</Table.Cell>
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
                                            as={Link} to={'/editmip/' + id}
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
                    onConfirm={deleteMip}//Silme işlemini gerçekleştircem
                />
                <ReportListPaging />
            </Table>
        </>
    )
})