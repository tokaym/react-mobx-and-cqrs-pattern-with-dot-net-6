import { observer } from "mobx-react-lite";
import { Icon, Pagination, Table } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";

export default observer(function ReportListPaging() {
    const { fulfillmentStore } = useStore();
    const { pageIndex, totalPages, setPageIndex } = fulfillmentStore;
    return (
        <>

            <Table.Footer>
                <Table.Row>
                    <Table.HeaderCell colSpan='20'>
                        <Pagination
                            defaultActivePage={pageIndex}
                            onPageChange={(event, data) => setPageIndex(data.activePage)}
                            ellipsisItem={{ content: <Icon name='ellipsis horizontal' />, icon: true }}
                            firstItem={{ content: <Icon name='angle double left' />, icon: true }}
                            lastItem={{ content: <Icon name='angle double right' />, icon: true }}
                            prevItem={{ content: <Icon name='angle left' />, icon: true }}
                            nextItem={{ content: <Icon name='angle right' />, icon: true }}
                            totalPages={totalPages}
                        />
                    </Table.HeaderCell>
                </Table.Row>
            </Table.Footer>
        </>
    )
})