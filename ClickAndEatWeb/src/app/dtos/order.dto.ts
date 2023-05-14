import {FoodTypeDto} from "./food-type.dto";

export class OrderDto {
  Identifier: string;
  OrderedItems: FoodTypeDto[];
  UserEmail: string;
  OrderDeliverState: string;
}
