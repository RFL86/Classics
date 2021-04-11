import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ProductComponent } from './product/product.component';
import { ManagementComponent } from './management/management.component';
import { StoreComponent } from './store/store.component';


const routes: Routes = [
  {
    path: 'product',
    component: ProductComponent
  },
  {
    path: 'store',
    component: StoreComponent
  },
  {
    path: 'management',
    component: ManagementComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductRoutingModule { }
