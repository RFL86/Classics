import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgApexchartsModule } from 'ng-apexcharts';
import { PagesRoutingModule } from './pages-routing.module';
import { CarsModule } from './cars/cars.module';
import { AuthModule } from './auth/auth.module';
import { UsersModule } from './users/users.module';
import { SuppliersModule } from './suppliers/suppliers.module';
import { AlertsModule } from './alerts/alerts.module';
import { UserAlertModule } from './user-alert/user-alert.module';
import { ProductsModule } from './products/products.module';
import { ReportsModule } from './reports/reports.module';
import { AccountModule } from './accounts/account.module';
import { CurrencyMaskModule } from "ng2-currency-mask";

import { NgbDropdownModule, NgbModalModule, NgbPaginationModule, NgbCollapseModule, NgbTypeaheadModule, NgbDatepickerModule, NgbAlertModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgbDropdownModule,
    NgApexchartsModule,
    PagesRoutingModule,
    ReactiveFormsModule,
    CarsModule,
    AuthModule,
    UsersModule,
    SuppliersModule,
    NgbModalModule,
    NgbPaginationModule,
    NgbCollapseModule,
    NgbTypeaheadModule,
    NgbDatepickerModule,
    NgbAlertModule,
    NgbModule,
    CurrencyMaskModule,
    AlertsModule,
    ProductsModule,
    UserAlertModule,
    ReportsModule,
    AccountModule
  ]
})
export class PagesModule { }
