import {FoodTypeDto} from "./food-type.dto";

export class MenuDto {
  Identifier: string;
  ShoppingLimit: number;
  ChoiceList: FoodTypeDto[];
  OrderLockState: string;
}
