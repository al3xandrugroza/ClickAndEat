import {FoodTypeCreateRequestDto} from "../dtos/food-type-create.request.dto";

export class FoodTypeCreateRequestBuilder {
  foodTypeCreateRequestDto: FoodTypeCreateRequestDto;

  public static init(): FoodTypeCreateRequestBuilder {
    return new FoodTypeCreateRequestBuilder();
  }

  public aFoodTypeCreateRequestDto(): FoodTypeCreateRequestBuilder {
    this.foodTypeCreateRequestDto = new FoodTypeCreateRequestDto();
    return this;
  }

  public withType(type: string): FoodTypeCreateRequestBuilder {
    this.foodTypeCreateRequestDto.Type = type;
    return this;
  }

  public withDescription(description: string): FoodTypeCreateRequestBuilder {
    this.foodTypeCreateRequestDto.Description = description;
    return this;
  }

  public withImageLink(imageLink: string): FoodTypeCreateRequestBuilder {
     this.foodTypeCreateRequestDto.ImageLink = imageLink;
     return this;
  }

  public build(): FoodTypeCreateRequestDto {
    return this.foodTypeCreateRequestDto;
  }
}
