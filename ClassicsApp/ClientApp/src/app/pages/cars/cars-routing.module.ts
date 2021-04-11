import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MyCarComponent } from './mycar/mycar.component';
import { CarComponent } from './car/car.component';
import { CarModalComponent } from './car/car-modal/car-modal.component';




const routes: Routes = [
  {
    path: 'mycar',
    component: MyCarComponent
  },
  {
    path: 'car',
    component: CarComponent
  },
  {
    path: 'code-modal',
    component: CarModalComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CarsRoutingModule { }
