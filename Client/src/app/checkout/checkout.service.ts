import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DeliveryMethod } from '../shared/models/DeliveryMethod';
import { map } from 'rxjs';
import { Order, OrderToCreate } from '../shared/models/order';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  baseUrl = environment.apiUrl;

  constructor(private httpClient:HttpClient) { }

  createOrder(order: OrderToCreate)
  {
    return this.httpClient.post<Order>(this.baseUrl+'orders',order);
  }

  getDeliveruMethods()
  {
    return this.httpClient.get<DeliveryMethod[]> (this.baseUrl+'orders/deliveryMethods').pipe(
      map(dm=>{
        return dm.sort((a,b)=>b.price-a.price)
      })
    )
  }
 

}
