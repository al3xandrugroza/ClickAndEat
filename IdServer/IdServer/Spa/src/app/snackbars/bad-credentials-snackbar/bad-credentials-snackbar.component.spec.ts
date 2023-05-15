import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BadCredentialsSnackbarComponent } from './bad-credentials-snackbar.component';

describe('BadCredentialsSnackbarComponent', () => {
  let component: BadCredentialsSnackbarComponent;
  let fixture: ComponentFixture<BadCredentialsSnackbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BadCredentialsSnackbarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BadCredentialsSnackbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
