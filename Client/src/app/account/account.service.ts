import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, ReplaySubject, map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Address, User } from '../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {


  baseUrl =environment.apiUrl;
  currenUserSource = new ReplaySubject<User| null>(1);
  currentUser$ = this.currenUserSource.asObservable();

  constructor(private httpClient:HttpClient,private router:Router ) { }


  loadCurrentUser(token:string | null){   
    if(token===null)
    {
      this.currenUserSource.next(null);
      return of(null);
    }
    let headers = new HttpHeaders();
    headers = headers.set('Authorization',`Bearer ${token}`);
 
    return this.httpClient.get<User>(this.baseUrl+'account',{headers}).pipe(
      map(user=>{
        if(user)
        {
        localStorage.setItem('token',user.token);
        this.currenUserSource.next(user);
        return user
        }
        else{
          return null;
        }
      })
    )
  }

   login(values:any)
   {
      return this.httpClient.post<User>(this.baseUrl+'account/login',values).pipe(
        map(user=>{
          localStorage.setItem('token',user.token);
          this.currenUserSource.next(user);
        })
      )
   }

   register(values:any)
   {
    return this.httpClient.post<User>(this.baseUrl+'account/register',values).pipe(
      map(user=>{
        localStorage.setItem('token',user.token);
        this.currenUserSource.next(user);
      })
    )
   } 

   logout()
   {
     localStorage.removeItem('token');
     this.currenUserSource.next(null);
     this.router.navigateByUrl('/');
   }


   checkEmailExists(email:string)
   {
     return this.httpClient.get<boolean>(this.baseUrl+'account/emailExists?email='+email);
   }

   getUserAddress()
   {
     return this.httpClient.get<Address>(this.baseUrl+'account/address');
   }

   updateUserAddress(address:Address)
   {
     return this.httpClient.put(this.baseUrl+'account/address',address);
   }

}
