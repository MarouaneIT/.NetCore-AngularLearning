import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {

  busyRequestTime=0;

  constructor(private spinnerService:NgxSpinnerService) { }

  busy()
  {
    this.busyRequestTime++;
    this.spinnerService.show(undefined,{
      type:'ball-clip-rotate-pulse',
      bdColor:'rgba(225,225,225,0.7)',
      color:'#333333',
      size :"medium"
    })
  }

  idle()
  {
    this.busyRequestTime--;
    if(this.busyRequestTime<=0)
    {
      this.busyRequestTime=0;
      this.spinnerService.hide();
    }
  }
}
