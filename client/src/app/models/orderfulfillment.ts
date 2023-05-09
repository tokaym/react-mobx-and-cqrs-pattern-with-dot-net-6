

export interface OrderFulfillment {
  totalOpenOrder: number,
  zm20Materials: Zm20Material[]
}

export interface Zm20Material {
  materialName: string,
  materialSKU: string,
  quantity: number,
  materialSatSass: MaterialSatSas[]
}

export interface MaterialSatSas {
  satSasNo: string,
  openDate: string,
  closedDate: string,
  dateDayDiff: number,
  quantity: number,
}
