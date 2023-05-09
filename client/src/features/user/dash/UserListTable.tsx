import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";
import { Button, Confirm, Dimmer, Grid, Loader, Table } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import "../../user/dash/userstyle.css";
import ReportListPaging from "./UserListPaging";

export default observer(function UserListTable() {
    const { userStore } = useStore();
    const { allList, column, direction, handleSort, loadingInitial, confirmOpen, deleteUser, openConfirm, closeConfirm, confirmHeader } = userStore;

    if (loadingInitial) return (<Dimmer active={loadingInitial} inverted={true}>
        <Loader size="medium" inverted={true} active inline>Yükleniyor...</Loader>
    </Dimmer>)
    return (
        <>

            <Table color="grey" selectable sortable>
                <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell textAlign="left" sorted={column === 'employeeNo' ? direction : undefined}
                            onClick={() => handleSort("employeeNo")} >Sicil</Table.HeaderCell>
                        <Table.HeaderCell textAlign="left" sorted={column === 'name' ? direction : undefined}
                            onClick={() => handleSort("name")} >Ad</Table.HeaderCell>
                        <Table.HeaderCell textAlign="left" sorted={column === 'surname' ? direction : undefined}
                            onClick={() => handleSort("surname")} >Soyad</Table.HeaderCell>
                        <Table.HeaderCell textAlign="center" onClick={() => handleSort("openAmount")} >İşlem</Table.HeaderCell>
                    </Table.Row>
                </Table.Header>

                <Table.Body>
                    {allList.map(({ id, name, surname, employeeNo }) => (
                        <Table.Row key={id} >
                            <Table.Cell width={4}>{employeeNo}</Table.Cell>
                            <Table.Cell width={5}>{name}</Table.Cell>
                            <Table.Cell width={4}>{surname}</Table.Cell>
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
                                            as={Link} to={'/edituser/' + id}
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
                    onConfirm={deleteUser}//Silme işlemini gerçekleştircem
                />
                <ReportListPaging />
            </Table>
        </>
    )
})