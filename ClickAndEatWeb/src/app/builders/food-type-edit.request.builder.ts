import {FoodTypeUpdateRequestDto} from "../dtos/food-type-update.request.dto";

export class FoodTypeEditRequestBuilder {
  foodTypeUpdateRequestDto: FoodTypeUpdateRequestDto;

  public static init(): FoodTypeEditRequestBuilder {
    return new FoodTypeEditRequestBuilder();
  }

  public aFoodTypeEditRequest(identifier: string): FoodTypeEditRequestBuilder {
    this.foodTypeUpdateRequestDto = new FoodTypeUpdateRequestDto();
    this.foodTypeUpdateRequestDto.Identifier = identifier;
    return this;
  }

  public withType(type: string): FoodTypeEditRequestBuilder {
    this.foodTypeUpdateRequestDto.Type = type;
    return this;
  }

  public withDescription(description: string): FoodTypeEditRequestBuilder {
    this.foodTypeUpdateRequestDto.Description = description;
    return this;
  }

  public withImageLink(imageLink: string): FoodTypeEditRequestBuilder {
    this.foodTypeUpdateRequestDto.ImageLink = imageLink;
    return this;
  }

  public build(): FoodTypeUpdateRequestDto {
    return this.foodTypeUpdateRequestDto;
  }
}
