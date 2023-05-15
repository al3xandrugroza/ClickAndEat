import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./home/home.component";
import {RegisterWithInvitationComponent} from "./register-with-invitation/register-with-invitation.component";
import {RegisterOrganizationComponent} from "./register-organization/register-organization.component";
import {NonAuthGuard} from "./auth/non-auth.guard";

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [NonAuthGuard]
  },
  {
    path: 'register/invitation',
    component: RegisterWithInvitationComponent
  },
  {
    path: 'register/organization',
    component: RegisterOrganizationComponent
  },
  {
    path: 'dashboard',
    loadChildren: () => import('./portal/portal.module').then((m) => m.PortalModule)
  },
  {
    path: '**',
    redirectTo: 'home'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
