import * as cuid from "cuid";

export interface BasketItem {
    id: number;
    productName: string;
    price: number;
    quantity: number;
    brand: string;
    type: string;
    pictureUrl: string;
}

export interface Basket {
    id: string;
    items: BasketItem[];
}

export class Basket implements Basket{
    id=cuid()
    items:BasketItem[]=[]
}


export interface BasketTotal
{
  shipping:number;
  subTotal:number;
  total:number;
}