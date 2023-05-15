import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {PortalComponent} from "./portal/portal.component";
import {AuthGuard} from "../auth/auth.guard";
import {InvitationComponent} from "./invitation/invitation.component";
import {AdminGuard} from "../auth/admin.guard";
import {FoodTypeEditComponent} from "./food-type-edit/food-type-edit.component";

const routes: Routes = [
  {
    path: '',
    component: PortalComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'invitation',
    component: InvitationComponent,
    canActivate: [AdminGuard]
  },
  {
    path: 'foodType/edit',
    component: FoodTypeEditComponent,
    canActivate: [AdminGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PortalRoutingModule { }
