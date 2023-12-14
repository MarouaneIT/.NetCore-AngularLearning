import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.scss']
})
export class AddressComponent {

  @Input() checkoutForm?:FormGroup;

constructor(private accountService:AccountService , private toastr : ToastrService)
{}

SaveUserAddress()
{
   this.accountService.updateUserAddress(this.checkoutForm?.get('addressForm')?.value).subscribe({
   next:()=>{
    this.toastr.success('Address saved');
    this.checkoutForm?.get('addressForm')?.reset(this.checkoutForm?.get('addressForm')?.value);
  }
   })
}

}
