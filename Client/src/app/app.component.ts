import { Component } from '@angular/core';
import {OnInit} from '@angular/core'
import { Product } from 'src/app/shared/models/Product';
import { BasketService } from './basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit  {

  constructor(private basketService:BasketService)
  {

  }
  title = 'Skinet';
  products : Product[] =[];

  ngOnInit(): void {
   
    const basketId = localStorage.getItem("basket_id");
    if(basketId) this.basketService.getBasket(basketId);
     
  }
}
