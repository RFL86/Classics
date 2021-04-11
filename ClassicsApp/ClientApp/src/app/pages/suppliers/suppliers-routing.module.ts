import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SupplierComponent } from './supplier/supplier.component';
import { SupComponent } from './sup/sup.component';


const routes: Routes = [
  {
    path: 'supplier',
    component: SupplierComponent
  },
  {
    path: 'sup',
    component: SupComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SupplierRoutingModule { }
