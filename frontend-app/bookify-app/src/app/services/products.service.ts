import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http"
import { IProduct } from "../models/product";
import { Observable, delay } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class ProductService {
    constructor(private httpClient: HttpClient){
    }

    getAll(): Observable<IProduct[]> {
        return this.httpClient.get<IProduct[]>('https://fakestoreapi.com/products', {
            //params: new HttpParams().append('limit', 5)
            params: new HttpParams({
                //fromString: 'limit=5'
                fromObject: { limit: 5 }
            })
        }).pipe(
            delay(1000)
        );
    }
}