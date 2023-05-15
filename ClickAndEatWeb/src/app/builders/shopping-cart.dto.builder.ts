import {ShoppingCartDto} from "../dtos/shopping-cart.dto";
import {FoodTypeDto} from "../dtos/food-type.dto";

export class ShoppingCartDtoBuilder {
  shoppingCartDto: ShoppingCartDto;

  public static init(): ShoppingCartDtoBuilder {
    return new ShoppingCartDtoBuilder();
  }

  public aShoppingCartDto(): ShoppingCartDtoBuilder {
    this.shoppingCartDto = new ShoppingCartDto();
    return this;
  }

  public withCartItems(cartItems: FoodTypeDto[]): ShoppingCartDtoBuilder {
    this.shoppingCartDto.CartItems = cartItems;
    return this;
  }

  public build(): ShoppingCartDto {
    return this.shoppingCartDto;
  }
}
