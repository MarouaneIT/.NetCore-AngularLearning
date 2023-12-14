import { Component, Input, OnInit } from '@angular/core';
import { CheckoutService } from '../checkout.service';
import { FormGroup } from '@angular/forms';
import { DeliveryMethod } from 'src/app/shared/models/DeliveryMethod';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-delivery',
  templateUrl: './delivery.component.html',
  styleUrls: ['./delivery.component.scss']
})
export class DeliveryComponent implements OnInit {

  @Input() checkoutForm?:FormGroup;
  deliveryMethods:DeliveryMethod[] = []
  
  constructor(private checkoutService: CheckoutService,private baketService:BasketService){

}

  ngOnInit(): void {
      this.checkoutService.getDeliveruMethods().subscribe({
        next:dm=>this.deliveryMethods =dm
              
      })
  }

  setShippingPrice(deliveryMethod:DeliveryMethod)
  {
    this.baketService.setshippingPrice(deliveryMethod);
  }
  

}
