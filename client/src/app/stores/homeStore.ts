import { makeAutoObservable } from 'mobx';
import { toast } from 'react-toastify';
import { history } from "../..";
import { TodayTable } from '../models/todaytable';
import agent from '../api/agent';
import { BarChart } from '../models/barchart';
import { PieChart } from '../models/piechart';
import CommonStore from './commonStore';
import { useStore } from './store';

export default class HomeStore {
      monthNames: Array<string> = ["Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran",
            "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"
      ];
      activeIndex: number = 1;

      pieChartRegistry = new Map<string, PieChart>();
      todayTable: TodayTable | undefined;
      todayTableRegistery = new Map<string, TodayTable>();
      month: string = "";
      amountByCompanyChart = new Map<string, BarChart>();
      amountByMaterialGroupChart = new Map<string, BarChart>();
      urgentHaveHFChart = new Map<string, BarChart>();
      urgentNotHaveHFChart = new Map<string, BarChart>();
      loadingInitial = false;
      activePlantCode: string = "643";
      tabActiveIndex: number = 0;
      constructor() {
            makeAutoObservable(this);
      }

      setLoadingInitial = (state: boolean) => {
            this.loadingInitial = state;
      }

      onPieEnter = (data: any, index: number) => {
            this.activeIndex = index;
      };

      loadOrderRates = async () => {
            this.pieChartRegistry.clear();
            const charts = await agent.Charts.getOrderRates(this.activePlantCode);
            charts.forEach(chart => {
                  this.setChart(chart);
            });
            return charts;
      }

      get OrderRates() {
            return Array.from(this.pieChartRegistry.values());
      }

      private setChart = (chart: PieChart) => {
            this.pieChartRegistry.set(chart.name + chart.value, chart);
      }

      //Firmalara göre toplam değerleri getiriyor
      loadAmountByCompany = async () => {
            this.amountByCompanyChart.clear();
            const charts = await agent.Charts.getOpenAmountByCompany(this.activePlantCode);
            charts.forEach(chart => {
                  if (chart.name != "Total") {
                        if (chart.name != null || chart.name != undefined)
                              chart.name = chart.name.split(' ')[0]
                        else
                              chart.name = "Belirsiz";
                        this.setAmountByCompanyChart(chart);
                  }
            });
            return charts;
      }

      get AmountByCompany() {
            return Array.from(this.amountByCompanyChart.values());
      }

      private setAmountByCompanyChart = (chart: BarChart) => {
            this.amountByCompanyChart.set(chart.name + chart.toplam, chart);
      }

      //Mal Grubuna göre toplam değerleri getiriyor
      loadAmountByMaterialGroup = async () => {
            this.amountByMaterialGroupChart.clear();
            const charts = await agent.Charts.getOpenAmountByMaterialGroup(this.activePlantCode);
            charts.forEach(chart => {
                  if (chart.name != "Total") {
                        chart.name = chart.name.split('-')[0]
                        this.setAmountByMaterialGroupChart(chart);
                  }
            });
            return charts;
      }

      get AmountByMaterialGroup() {
            return Array.from(this.amountByMaterialGroupChart.values());
      }

      private setAmountByMaterialGroupChart = (chart: BarChart) => {
            this.amountByMaterialGroupChart.set(chart.name + chart.toplam, chart);
      }

      //HF'li Acillerin Malzeme Grubuna göre sayıları
      loadUrgentHaveHFChart = async () => {
            this.urgentHaveHFChart.clear();
            const charts = await agent.Charts.getUrgentHaveHF(this.activePlantCode);
            charts.forEach(chart => {
                  if (chart.name != "Total") {
                        chart.name = chart.name.split('-')[0]
                        this.setUrgentHaveHFChart(chart);
                  }
            });
            return charts;
      }

      get UrgentHaveHFChart() {
            return Array.from(this.urgentHaveHFChart.values());
      }

      private setUrgentHaveHFChart = (chart: BarChart) => {
            this.urgentHaveHFChart.set(chart.name + chart.toplam, chart);
      }

      //HF'li Acillerin Malzeme Grubuna göre sayıları
      loadUrgentNotHaveHFChart = async () => {
            this.urgentNotHaveHFChart.clear();
            const charts = await agent.Charts.getUrgentNotHaveHF(this.activePlantCode);
            charts.forEach(chart => {
                  if (chart.name != "Total") {
                        chart.name = chart.name.split('-')[0]
                        this.setUrgentNotHaveHFChart(chart);
                  }
            });
            return charts;
      }

      get UrgentNotHaveHFChart() {
            return Array.from(this.urgentNotHaveHFChart.values());
      }

      private setUrgentNotHaveHFChart = (chart: BarChart) => {
            this.urgentNotHaveHFChart.set(chart.name + chart.toplam, chart);
      }

      //Bugünün özeti
      loadTodayTable = async () => {
            this.todayTableRegistery.clear();
            this.setLoadingInitial(true);
            const table = await agent.Charts.getTodayTable(this.activePlantCode);
            this.setMonth(table[0].date);
            table.forEach(day => {
                  this.setloadTodayTable(day);
            })
            this.setLoadingInitial(false);
            return table;
      }

      setMonth = (date: string) => {
            let dateArray = date.split('.');
            let _date = dateArray[1] + "." + dateArray[0] + "." + dateArray[2]
            this.month = new Date(_date).toLocaleString("tr-TR", { month: 'long' });
      }

      get TodayTable() {
            return Array.from(this.todayTableRegistery.values());
      }

      private setloadTodayTable = (table: TodayTable) => {
            this.todayTable = table;

            this.todayTableRegistery.set(table.date.toString(), table);
      }

      handleTabChange = (e: React.MouseEvent<HTMLDivElement, MouseEvent>) => {
            const target = e.target as HTMLTextAreaElement;
            this.activePlantCode = target.innerHTML == "BMI - 601S" ? "643" : "909"
            this.tabActiveIndex = target.innerHTML == "BMI - 601S" ? 0 : 1

            this.loadTodayTable();
            this.loadOrderRates();
            this.loadAmountByCompany();
            this.loadAmountByMaterialGroup();
            this.loadUrgentHaveHFChart();
            this.loadUrgentNotHaveHFChart();
      }
}
