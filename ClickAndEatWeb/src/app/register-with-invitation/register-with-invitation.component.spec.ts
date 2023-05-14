import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterWithInvitationComponent } from './register-with-invitation.component';

describe('RegisterWithInvitationComponent', () => {
  let component: RegisterWithInvitationComponent;
  let fixture: ComponentFixture<RegisterWithInvitationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegisterWithInvitationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterWithInvitationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
