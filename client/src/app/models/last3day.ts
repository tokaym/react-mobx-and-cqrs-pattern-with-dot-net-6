import { date } from 'yup';
import { Profile } from './profile';


export interface Last3Day {
      date1: ColumnDate,
      date2: ColumnDate,
      date3: ColumnDate,
      reportTables: ReportTable[]
}

export interface ColumnDate{
    name: string,
    sum: number
}

export interface ReportTable{
    materialName: string,
    materialSKU: string,
    date1Value : number,
    date2Value: number,
    date3Value: number
}
