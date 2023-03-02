import { Component } from '@angular/core';
import {OnInit} from '@angular/core'
import { Product } from 'src/app/shared/models/Product';
import {HttpClient} from '@angular/common/http';
import { Pagination } from 'src/app/shared/models/Pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit  {

  constructor(private http:HttpClient)
  {

  }
  title = 'Skinet';
  products : Product[] =[];

  ngOnInit(): void {
   
     
  }
}
