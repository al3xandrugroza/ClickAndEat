import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PortalRoutingModule } from './portal-routing.module';
import { PortalComponent } from './portal/portal.component';
import { MenuComponent } from './menu/menu.component';
import {AuthGuard} from "../auth/auth.guard";
import { InvitationComponent } from './invitation/invitation.component';
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatSelectModule} from "@angular/material/select";
import {MatInputModule} from "@angular/material/input";
import {ReactiveFormsModule} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";
import {ErrorSnackbarComponent} from "../snackbars/error-snackbar/error-snackbar.component";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {MatIconModule} from "@angular/material/icon";
import {MatCardModule} from "@angular/material/card";
import {ClipboardModule} from "@angular/cdk/clipboard";
import {MatTabsModule} from "@angular/material/tabs";
import {MatProgressBarModule} from "@angular/material/progress-bar";
import { AdminPortalComponent } from './admin-portal/admin-portal.component';
import { EmpPortalComponent } from './emp-portal/emp-portal.component';
import { FoodTypeListComponent } from './food-type-list/food-type-list.component';
import { FoodTypeComponent } from './food-type/food-type.component';
import { FoodTypeCreateComponent } from './food-type-create/food-type-create.component';
import { OrderListComponent } from './order-list/order-list.component';
import {MatTableModule} from "@angular/material/table";
import { FoodTypeEditComponent } from './food-type-edit/food-type-edit.component';
import { MenuViewComponent } from './menu-view/menu-view.component';
import { MenuEditComponent } from './menu-edit/menu-edit.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';
import { ShoppingCartEditComponent } from './shopping-cart-edit/shopping-cart-edit.component';
import { ShoppingCartViewComponent } from './shopping-cart-view/shopping-cart-view.component';
import { OrderViewComponent } from './order-view/order-view.component';

@NgModule({
  declarations: [
    PortalComponent,
    MenuComponent,
    InvitationComponent,
    AdminPortalComponent,
    EmpPortalComponent,
    FoodTypeListComponent,
    FoodTypeComponent,
    FoodTypeCreateComponent,
    OrderListComponent,
    FoodTypeEditComponent,
    MenuViewComponent,
    MenuEditComponent,
    ShoppingCartComponent,
    ShoppingCartEditComponent,
    ShoppingCartViewComponent,
    OrderViewComponent
  ],
  imports: [
    CommonModule,
    PortalRoutingModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatIconModule,
    MatSnackBarModule,
    MatCardModule,
    ClipboardModule,
    MatTabsModule,
    MatProgressBarModule,
    MatTableModule
  ],
  providers: [
    AuthGuard
  ]
})
export class PortalModule { }
