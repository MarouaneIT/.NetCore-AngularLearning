import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  ReturnUrl :string

  constructor(private accountService:AccountService,private router:Router,
    private activatedRoute:ActivatedRoute)
  {
    this.ReturnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/shop';
  }

  loginForm =  new FormGroup({
    email : new FormControl('',[Validators.required,Validators.email]),
    password:  new FormControl('',Validators.required)
  })

  onSubmit()
  {
    this.accountService.login(this.loginForm.value).subscribe({

      next:()=> this.router.navigateByUrl(this.ReturnUrl)
    });
  }

}
