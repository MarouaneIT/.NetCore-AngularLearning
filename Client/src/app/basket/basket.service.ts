import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Basket, BasketItem, BasketTotal } from '../shared/models/basket';
import { Product } from '../shared/models/Product';

@Injectable({
  providedIn: 'root'
})export class BasketService {

  baseUrl = environment.apiUrl;

  private basketSource =new BehaviorSubject<Basket|null>(null);
  basketSource$ = this.basketSource.asObservable();

  private basketTotalSource = new BehaviorSubject<BasketTotal | null>(null);
  basketTotalSource$ = this.basketTotalSource.asObservable();

  constructor(private httpclient:HttpClient) { }

  getBasket(id:string){   
    this.httpclient.get<Basket>(this.baseUrl+'basket?id='+id).subscribe({
      next:basket=>{
        this.basketSource.next(basket);
        this.calculateTotal();
      }
    })
  }

  setBasket(basket:Basket)
  {
    this.httpclient.post<Basket>(this.baseUrl+'basket',basket).subscribe({
      next:basket=>{
        this.basketSource.next(basket);
        this.calculateTotal();
      }
    })
  }

  getCurrentBasketValue()
  {
    return this.basketSource.value;
  }

  addItemToBasket(item:Product | BasketItem,quantity=1)
  {
      if(this.isProduct(item)) item =this.mapProductItemToBasketItem(item);
    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.items = this.addOrUpdateItem(basket.items,item,quantity);
    this.setBasket(basket);
  }

  removeItemFromBasket(id:number , quantity=1)
  {
    const basket = this.getCurrentBasketValue();
    if(!basket) return;
   
    const item = basket.items.find(x=>x.id===id);
    if(item)
    {
       item.quantity -=quantity;
       if(item.quantity===0)
       {
         basket.items = basket.items.filter(x=>x.id!==id);
       }

       if(basket.items.length>0) this.setBasket(basket);
       else this.deleteBasket(basket);
    }
  }

  deleteBasket(basket:Basket){
     return this.httpclient.delete(this.baseUrl+'basket?id='+basket.id).subscribe({
      next:()=>
      {
        this.basketSource.next(null);
        this.basketTotalSource.next(null);
        localStorage.removeItem('basket_id');
      }
     })

  }

  private addOrUpdateItem(items:BasketItem[],itemToadd:BasketItem,quantity:number) : BasketItem[]
  {
    const item = items.find(x=>x.id===itemToadd.id);
    if(item) item.quantity+=quantity;
    else {
      itemToadd.quantity = quantity;
      items.push(itemToadd);
    }
    return items;
  }


  private createBasket(): Basket {
    const basket = new Basket();
    localStorage.setItem("basket_id",basket.id);
    return  basket;
  }


 private mapProductItemToBasketItem(item: Product):BasketItem {    
    return {
      id:item.id,
      productName:item.name,
      price : item.price,
      brand:item.productBrand,
      type:item.productType,
      quantity:0,
      pictureUrl:item.pictureUrl
    }
   }

   private calculateTotal()
   {
    const basket = this.getCurrentBasketValue();
    if(!basket) return;
    const shipping = 0;
    const subTotal = basket.items.reduce((a,b)=>(b.price*b.quantity)+a,0);
    const total = shipping+subTotal;
    return this.basketTotalSource.next({shipping,subTotal,total})
   }

  private isProduct(item:Product | BasketItem) :item is Product
   {
     return (item as Product).productBrand !==undefined;
   }

}
   



