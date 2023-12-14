import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BasketService } from 'src/app/basket/basket.service';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { Basket } from 'src/app/shared/models/basket';
import { Address } from 'src/app/shared/models/user';
import { NavigationExtras, Router } from '@angular/router';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss']
})
export class PaymentComponent {

  @Input() checkoutForm?:FormGroup;

  constructor(private basketService:BasketService,private checkoutService:CheckoutService,
    private toastrService:ToastrService,private router:Router)
  {}



    submitOrder()
    {
      debugger
       const basket = this.basketService.getCurrentBasketValue();
       if(!basket) return;
       const orderToCreate = this.getOrderToCreate(basket);
       if(!orderToCreate) return; 
       this.checkoutService.createOrder(orderToCreate).subscribe({
        next:order =>{        
          this.toastrService.success('order created successfully');
          this.basketService.deleteLocalBasket();
          const navigationExtras: NavigationExtras = {state:order}
          this.router.navigate(['checkout/success'],navigationExtras)
        }
       })
     }

  getOrderToCreate(basket: Basket) {    
    debugger
    const deliveryMethodId = this.checkoutForm?.get('deliveryForm')?.get('deliveryMethod')?.value;
    const shipToAddress  = this.checkoutForm?.get('addressForm')?.value as Address
    if(!deliveryMethodId || !shipToAddress) return;
     return {
      BasketId: basket.id,
      DeliveryMethodId: deliveryMethodId,
      ShipToAddress: shipToAddress
     }
  }
}
