import { Component, OnInit } from '@angular/core';
import { IProduct } from './models/product';
import { ProductService } from './services/products.service';
import { Observable, tap } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Bookify';
  //products: IProduct[];
  products$: Observable<IProduct[]>;
  loading: boolean = false;

  constructor(private productService: ProductService) {
  }

  ngOnInit(): void {
    this.loading = true;
    this.products$ = this.productService.getAll().pipe(
      tap(() => this.loading = false)
    );
    // this.productService.getAll().subscribe(products => {
    //   this.products = products;
    //   this.loading = false;
    // });
  }
}
