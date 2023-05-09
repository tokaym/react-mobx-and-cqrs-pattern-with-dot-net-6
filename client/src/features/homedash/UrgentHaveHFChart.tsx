import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { Bar, BarChart, CartesianGrid, Cell, Legend, Pie, PieChart, ResponsiveContainer, Sector, Tooltip, XAxis, YAxis } from "recharts";
import { Header, Label, Segment, Table } from 'semantic-ui-react';
import { useStore } from "../../app/stores/store";

export default observer(function HomePage() {
    const { homeStore } = useStore();
    const { UrgentHaveHFChart, loadUrgentHaveHFChart, activePlantCode,urgentHaveHFChart } = homeStore;

    let keys = ['cari', 'demode'];
    let colors = ['#f3a935', '#55596a'];
    if (activePlantCode == "909") {
        keys = ['toplam'];
        colors = ['#55596a'];
    }
    useEffect(() => {
        if (urgentHaveHFChart.size < 1) {
            loadUrgentHaveHFChart()
        };
    }, [urgentHaveHFChart.size, loadUrgentHaveHFChart])
    return (
        <Segment>
            <Header content="Mal grubu bazında Acillerin detayı– HF si olup acil olan (601S)" size="large" sub color="black" />
            <br></br>
            <ResponsiveContainer width="100%" height={350}>
                <BarChart width={730} height={250} data={UrgentHaveHFChart}>
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