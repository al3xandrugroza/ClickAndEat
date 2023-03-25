import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PortalRoutingModule } from './portal-routing.module';
import { PortalComponent } from './portal/portal.component';
import { MenuComponent } from './menu/menu.component';
import {AuthGuard} from "../auth/auth.guard";


@NgModule({
  declarations: [
    PortalComponent,
    MenuComponent
  ],
  imports: [
    CommonModule,
    PortalRoutingModule
  ],
  providers: [
    AuthGuard
  ]
})
export class PortalModule { }
