import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { ResponsiveContainer } from "recharts";
import { Header, Label, Segment, Table } from 'semantic-ui-react';
import { useStore } from "../../app/stores/store";

export default observer(function TodayTable() {
    const { homeStore } = useStore();
    const { todayTableRegistery, loadTodayTable, TodayTable, tabActiveIndex } = homeStore;

    useEffect(() => {
        if (todayTableRegistery.size < 1) loadTodayTable();
    }, [todayTableRegistery.size, loadTodayTable])
    return (
        <Segment>
            <Header content="Özetle Son 3 Gün" size="large" sub color="black" />
            <ResponsiveContainer width="100%" height={300}>
                <Table verticalAlign="middle" color={"orange"} key={"orange"} celled style={{ "height": "15px" }}>
                    <Table.Header>
                        <Table.Row>
                            <Table.HeaderCell>Tarih</Table.HeaderCell>
                            <Table.HeaderCell>Açık Miktar</Table.HeaderCell>
                            <Table.HeaderCell>Kalem</Table.HeaderCell>
                            <Table.HeaderCell>HF</Table.HeaderCell>
                            <Table.HeaderCell>Acil</Table.HeaderCell>
                        </Table.Row>
                    </Table.Header>

                    <Table.Body>
                        <Table.Row>
                            <Table.Cell><Label ribbon>{TodayTable[0]?.date}</Label></Table.Cell>
                            <Table.Cell>{TodayTable[0]?.openAmount}</Table.Cell>
                            <Table.Cell>{TodayTable[0]?.item}</Table.Cell>
                            <Table.Cell>{TodayTable[0]?.hf}</Table.Cell>
                            <Table.Cell>{TodayTable[0]?.urgent}</Table.Cell>
                        </Table.Row>
                        {TodayTable.slice(1, 3).map(({ date, openAmount, item, hf, urgent }) => (
                            <Table.Row>
                                <Table.Cell>{date}</Table.Cell>
                                <Table.Cell>{openAmount}</Table.Cell>
                                <Table.Cell>{item}</Table.Cell>
                                <Table.Cell>{hf}</Table.Cell>
                                <Table.Cell>{urgent}</Table.Cell>
                            </Table.Row>
                        ))}

                    </Table.Body>
                </Table>
            </ResponsiveContainer>
        </Segment>
    )
})