import {MenuUpdateRequestDto} from "../dtos/menu-update.request.dto";
import {FoodTypeDto} from "../dtos/food-type.dto";

export class MenuUpdateRequestBuilder {
  menuUpdateRequestDto: MenuUpdateRequestDto;

  public static init(): MenuUpdateRequestBuilder {
    return new MenuUpdateRequestBuilder();
  }

  public aMenuUpdateRequest(): MenuUpdateRequestBuilder {
    this.menuUpdateRequestDto = new MenuUpdateRequestDto();
    return this;
  }

  public withChoiceList(foodTypes: FoodTypeDto[]): MenuUpdateRequestBuilder {
    this.menuUpdateRequestDto.UpdatedChoiceList = foodTypes;
    return this;
  }

  public build(): MenuUpdateRequestDto {
    return this.menuUpdateRequestDto;
  }
}
