import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { Bar, BarChart, CartesianGrid, Cell, Legend, Pie, PieChart, ResponsiveContainer, Sector, Tooltip, XAxis, YAxis } from "recharts";
import { Header, Label, Segment, Table } from 'semantic-ui-react';
import { useStore } from "../../app/stores/store";

export default observer(function UrgentNotHaveHFChart() {
    const { homeStore } = useStore();
    const { UrgentNotHaveHFChart, loadUrgentNotHaveHFChart,activePlantCode,urgentNotHaveHFChart } = homeStore;

    let keys = ['cari', 'demode'];
    let colors = ['#f3a935', '#55596a'];
    if (activePlantCode == "909") {
        keys = ['toplam'];
        colors = ['#55596a'];
    }
    useEffect(() => {
        if (urgentNotHaveHFChart.size < 1) loadUrgentNotHaveHFChart();
    }, [urgentNotHaveHFChart.size, loadUrgentNotHaveHFChart])
    return (
        <Segment>
            <Header content="Mal grubu bazında Acillerin detayı – HFsi olmayan aciller  (601S)" size="large" sub color="black" />
            <br></br>
            <ResponsiveContainer width="100%" height={350}>
                <BarChart width={730} height={250} data={UrgentNotHaveHFChart}>
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="name" />
                    <YAxis />
                    <Tooltip />
                    <Legend />
                    {keys.map((key: string, index: number): any => {
                        const bars = [];
                        bars.push(<Bar dataKey={key} stackId="a" fill={colors[index]} />);
                        bars.push(<Bar dataKey='gap' stackId="a" fill='transparent' />);
                        return bars;
                    })}
                </BarChart>
            </ResponsiveContainer>
        </Segment>
    )
})