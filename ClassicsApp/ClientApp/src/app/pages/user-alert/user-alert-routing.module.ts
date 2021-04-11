import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UserAlertComponent } from './user-alert/user-alert.component';

const routes: Routes = [
  {
    path: 'user-alert',
    component: UserAlertComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserAlertRoutingModule { }
