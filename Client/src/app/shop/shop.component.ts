import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Brand } from '../shared/models/brand';
import { Product } from '../shared/models/Product';
import { ShopParams } from '../shared/models/ShopParams';
import { Type } from '../shared/models/type';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})

export class ShopComponent implements OnInit 
{
    products:Product[]=[];
    brands:Brand[]=[];
    types:Type[]=[]; 
    shopParams = new ShopParams();
    sortOptions=[
      {name:'Alphabetical',value:'name'},
      {name:'Price:Low to hight',value:'priceAsc'},
      {name:'Price:hight to Low',value:'priceDesc'}
    ];
    totalItems=0;

    @ViewChild('search') searchTerm?:ElementRef;

  constructor(private shopService:ShopService)
  {}

    ngOnInit(): void {  
      this.getproducts();
      this.getbrands();
      this.gettypes();
    }


    getproducts()
    {
      this.shopService.getProducts(this.shopParams).subscribe({
        next: response => {
          this.products= response.data;
          this.shopParams.pageNumber =response.pageIndex;
          this.shopParams.pageSize = response.pageSize;
          this.totalItems = response.count;
        },
     
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
      
      this.shopParams.brandId=brandId;
      this.shopParams.pageNumber=1;
      this.getproducts();
    }

    OntypeSelected(typeId:number)
    {
      
      this.shopParams.typeId=typeId;
      this.shopParams.pageNumber=1;
      this.getproducts();
    }

    onSortSelected(event:any)
    {
      this.shopParams.sort =event.target.value;
      this.getproducts();
    }

    onPageChanged(event:any)
    {
      if(this.shopParams.pageNumber!==event)
      {
      
        this.shopParams.pageNumber=event;
        this.getproducts();
      }
    }

    onSearch()
    {
      
      this.shopParams.search=this.searchTerm?.nativeElement.value;
      this.shopParams.pageNumber=1;
      this.getproducts();
    }

    onReset()
    {
      if(this.searchTerm) this.searchTerm.nativeElement.value='';
      this.shopParams = new ShopParams();
      this.getproducts();
    }

}
