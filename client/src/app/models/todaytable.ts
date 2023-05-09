import { date } from 'yup';
import { Profile } from './profile';


export interface TodayTable {
      date: string,
      openAmount: number,    
      item: number,
      hf: number,
      urgent: number
}
