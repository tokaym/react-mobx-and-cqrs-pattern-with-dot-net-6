import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { Cell, Pie, PieChart, ResponsiveContainer, Sector } from "recharts";
import { Header, Label, Segment, Table } from 'semantic-ui-react';
import { useStore } from "../../app/stores/store";

export default observer(function OrderRates() {
    const { homeStore } = useStore();
    const { activeIndex, onPieEnter, loadOrderRates, OrderRates, month,tabActiveIndex,pieChartRegistry } = homeStore;

    useEffect(() => {
        if (pieChartRegistry.size < 1) loadOrderRates();
    }, [pieChartRegistry.size, loadOrderRates])
    
    const data01 = [
        { name: 'Gönderilen Sipariş' },
        { name: 'Kalan Sipariş' },
    ];

    const colors = ['#4BC103', '#3104B4'];

    const renderActiveShape = (props: any) => {
        const RADIAN = Math.PI / 180;
        const {
            cx, cy, midAngle, innerRadius, outerRadius, startAngle, endAngle,
            fill, payload, percent, value,
        } = props;
        const sin = Math.sin(-RADIAN * midAngle);
        const cos = Math.cos(-RADIAN * midAngle);
        const sx = cx + (outerRadius + 10) * cos;
        const sy = cy + (outerRadius + 10) * sin;
        const mx = cx + (outerRadius + 30) * cos;
        const my = cy + (outerRadius + 30) * sin;
        const ex = mx + (cos >= 0 ? 1 : -1) * 22;
        const ey = my;
        const textAnchor = cos >= 0 ? 'start' : 'end';

        return (
            <g>
                <text x={cx} y={cy} dy={8} textAnchor="middle" fill={fill}>{payload.name}</text>
                <Sector
                    cx={cx}
                    cy={cy}
                    innerRadius={innerRadius}
                    outerRadius={outerRadius}
                    startAngle={startAngle}
                    endAngle={endAngle}
                    fill={fill}
                />
                <Sector
                    cx={cx}
                    cy={cy}
                    startAngle={startAngle}
                    endAngle={endAngle}
                    innerRadius={outerRadius + 6}
                    outerRadius={outerRadius + 10}
                    fill={fill}
                />
                <path d={`M${sx},${sy}L${mx},${my}L${ex},${ey}`} stroke={fill} fill="none" />
                <circle cx={ex} cy={ey} r={2} fill={fill} stroke="none" />
                <text x={ex + (cos >= 0 ? 1 : -1) * 12} y={ey} textAnchor={textAnchor} fill="#333">{`Adet: ${value}`}</text>
                <text x={ex + (cos >= 0 ? 1 : -1) * 12} y={ey} dy={18} textAnchor={textAnchor} fill="#999">
                    {`(Rate ${(percent * 100).toFixed(2)}%)`}
                </text>
            </g>
        );
    };

    return (
        <Segment>
            <Header content={month + " Ayı Kalan/Gönderilen Sipariş"} size="large" sub color="black" />
            <ResponsiveContainer width="100%" height={300}>
                <PieChart width={500} height={500}>
                    <Pie
                        activeIndex={activeIndex}
                        activeShape={renderActiveShape}
                        data={OrderRates}
                        innerRadius={60}
                        outerRadius={80}
                        fill="#8884d8"
                        dataKey="value"
                        onMouseEnter={onPieEnter}
                    >
                        {
                            data01.map((entry, index) => <Cell key={`cell-${index}`} fill={colors[index % colors.length]} />)
                        }
                    </Pie>
                </PieChart>
            </ResponsiveContainer>
        </Segment>
    )
})