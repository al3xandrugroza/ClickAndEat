import { TestBed } from '@angular/core/testing';

import { ClickAndEatApiService } from './click-and-eat-api.service';

describe('ClickAndEatApiService', () => {
  let service: ClickAndEatApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClickAndEatApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
