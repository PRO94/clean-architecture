import { Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpParams } from "@angular/common/http"
import { IProduct } from "../models/product";
import { Observable, catchError, delay, retry, tap, throwError } from "rxjs";
import { ErrorService } from "./error.service";

@Injectable({
    providedIn: 'root'
})
export class ProductService {
    constructor(
        private httpClient: HttpClient,
        private errorService: ErrorService
    ) { }

    products: IProduct[] = [];

    getAll(): Observable<IProduct[]> {
        //return this.httpClient.get<IProduct[]>('https://fakestoreapi.XYZcom/products', {   // only for testing global error handling - wrong url
        return this.httpClient.get<IProduct[]>('https://fakestoreapi.com/products', {
            //params: new HttpParams().append('limit', 5)
            params: new HttpParams({
                //fromString: 'limit=5'
                fromObject: { limit: 5 }
            })
        }).pipe(
            delay(250),
            retry(2),
            tap(products => this.products = products),
            catchError(this.errorHandler.bind(this))
        );
    }

    create(product: IProduct): Observable<IProduct> {
        return this.httpClient.post<IProduct>('https://fakestoreapi.com/products', product)
            .pipe(
                tap(product => this.products.push(product))
            );
    }

    private errorHandler(error: HttpErrorResponse) {
        this.errorService.handle(error.message);
        return throwError(() => error.message);
    }
}