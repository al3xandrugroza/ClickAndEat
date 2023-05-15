import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {FoodTypeDto} from "../dtos/food-type.dto";
import {FoodTypeCreateRequestDto} from "../dtos/food-type-create.request.dto";
import {FoodTypeUpdateRequestDto} from "../dtos/food-type-update.request.dto";
import {MenuDto} from "../dtos/menu.dto";
import {MenuUpdateRequestDto} from "../dtos/menu-update.request.dto";
import {OrderDto} from "../dtos/order.dto";
import {ShoppingCartDto} from "../dtos/shopping-cart.dto";
import {OrderSummaryDto} from "../dtos/order-summary.dto";

@Injectable({
  providedIn: 'root'
})
export class ClickAndEatApiService {
  private clickAndEatApiUrl = '/api';

  constructor(private http: HttpClient) {}

  getAllFoodTypes(): Observable<FoodTypeDto[]> {
    return this.http.get<FoodTypeDto[]>(`${this.clickAndEatApiUrl}/FoodTypes/all`);
  }

  getFoodType(identifier: string): Observable<FoodTypeDto> {
    return this.http.get<FoodTypeDto>(`${this.clickAndEatApiUrl}/FoodTypes/item?identifier=${identifier}`);
  }

  createFoodType(foodTypeCreateRequest: FoodTypeCreateRequestDto): Observable<FoodTypeDto> {
    return this.http.post<FoodTypeDto>(`${this.clickAndEatApiUrl}/FoodTypes`, foodTypeCreateRequest);
  }

  updateFoodType(foodTypeUpdateRequest: FoodTypeUpdateRequestDto): Observable<FoodTypeDto> {
    return this.http.put<FoodTypeDto>(`${this.clickAndEatApiUrl}/FoodTypes`, foodTypeUpdateRequest);
  }

  deleteFoodType(identifier: string): Observable<any> {
    return this.http.delete(`${this.clickAndEatApiUrl}/FoodTypes?identifier=${identifier}`);
  }

  getMenu(): Observable<MenuDto> {
    return this.http.get<MenuDto>(`${this.clickAndEatApiUrl}/Menu`);
  }

  updateMenu(menuUpdateRequestDto: MenuUpdateRequestDto): Observable<MenuDto> {
    return this.http.put<MenuDto>(`${this.clickAndEatApiUrl}/Menu`, menuUpdateRequestDto);
  }

  lockMenu(): Observable<any> {
    return this.http.post(`${this.clickAndEatApiUrl}/Menu/lock`, null);
  }

  unlockMenu(): Observable<any> {
    return this.http.post(`${this.clickAndEatApiUrl}/Menu/unlock`, null);
  }

  getAllOrders(): Observable<OrderSummaryDto[]> {
    return this.http.get<OrderSummaryDto[]>(`${this.clickAndEatApiUrl}/Order/all`);
  }

  getOrder(): Observable<OrderDto | null> {
    return this.http.get<OrderDto | null>(`${this.clickAndEatApiUrl}/Order`);
  }

  placeOrder(): Observable<OrderDto> {
    return this.http.post<OrderDto>(`${this.clickAndEatApiUrl}/Order`, null);
  }

  cancelOrder(): Observable<any> {
    return this.http.delete(`${this.clickAndEatApiUrl}/Order`);
  }

  deleteAllOrders(): Observable<any> {
    return this.http.delete(`${this.clickAndEatApiUrl}/Order/all`);
  }

  getShoppingCart(): Observable<ShoppingCartDto> {
    return this.http.get<ShoppingCartDto>(`${this.clickAndEatApiUrl}/ShoppingCart`);
  }

  updateShoppingCart(shoppingCartDto: ShoppingCartDto): Observable<ShoppingCartDto> {
    return this.http.put<ShoppingCartDto>(`${this.clickAndEatApiUrl}/ShoppingCart`, shoppingCartDto);
  }
}
