import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {PortalComponent} from "./portal/portal.component";
import {MenuComponent} from "./menu/menu.component";
import {AuthGuard} from "../auth/auth.guard";

const routes: Routes = [
  {
    path: '',
    component: PortalComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'menu',
        component: MenuComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PortalRoutingModule { }
