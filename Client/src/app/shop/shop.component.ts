import { Component, OnInit } from '@angular/core';
import { Brand } from '../shared/models/brand';
import { Product } from '../shared/models/Product';
import { Type } from '../shared/models/type';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})

export class ShopComponent implements OnInit {

  products:Product[]=[];
  brands:Brand[]=[];
  types:Type[]=[];
  brandIdSelected:number=0;
  typeIdSelected:number=0;
  sortSelected:string ='name';
  sortOptions=[
    {name:'Alphabetical',value:'name'},
    {name:'Price:Low to hight',value:'priceAsc'},
    {name:'Price:hight to Low',value:'priceDesc'}
  ]

constructor(private shopService:ShopService)
{}

  ngOnInit(): void {  
    this.getproducts();
    this.getbrands();
    this.gettypes();
  }


  getproducts()
  {
    this.shopService.getProducts(this.brandIdSelected,this.typeIdSelected,this.sortSelected).subscribe({
      next: response => this.products= response.data,
      error: error =>console.log(error)         
     })
  }

 getbrands()
 {
  this.shopService.getBrands().subscribe({
    next: response => this.brands= [{id:0,name:'All'},...response],
    error: error =>console.log(error)         
   })
 }

 gettypes()
 {
  this.shopService.getTypes().subscribe({
    next: response => this.types= [{id:0,name:'All'},...response],
    error: error =>console.log(error)         
   })
 }

OnBrandSelected(brandId:number)
{
  
  this.brandIdSelected=brandId;
  this.getproducts();
}

OntypeSelected(typeId:number)
{
  
  this.typeIdSelected=typeId;
  this.getproducts();
}

onSortSelected(event:any)
{
  this.sortSelected =event.target.value;
  this.getproducts();
}

}
