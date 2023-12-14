import { Address } from "./user";


export interface OrderToCreate{
    BasketId:string;
    DeliveryMethodId:number;
    ShipToAddress : Address;
}


export interface Order {
    id:             number;
    buyerEmail:     string;
    orderDate:      Date;
    shipToAddress:  Address;
    deliveryMethod: string;
    shippingPrice:  number;
    orderItems:     OrderItem[];
    subtotal:       number;
    total:          number;
    status:         string;
}

export interface OrderItem {
    productId:   number;
    productName: string;
    pictureUrl:  string;
    price:       number;
    quantity:    number;
}