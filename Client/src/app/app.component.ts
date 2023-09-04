import { Component } from '@angular/core';
import {OnInit} from '@angular/core'
import { Product } from 'src/app/shared/models/Product';
import { AccountService } from './account/account.service';
import { BasketService } from './basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit  {

  constructor(private basketService:BasketService,private accountService:AccountService)
  {

  }
  title = 'Skinet';
  products : Product[] =[];

  ngOnInit(): void {
   
    this.loadBasket();
    this.loadCurrenUser();  
  }


  loadBasket()
  {
    const basketId = localStorage.getItem("basket_id");
    if(basketId) this.basketService.getBasket(basketId);
  }

  loadCurrenUser()
  {
     const token = localStorage.getItem('token');
    this.accountService.loadCurrentUser(token).subscribe();
  }
}
