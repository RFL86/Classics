import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxEditorModule } from 'ngx-editor';
import { HttpClientModule } from '@angular/common/http';
import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './login/login.component';
import { LoginModalComponent } from './login/login-modal/login-modal.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { UIModule } from '../../shared/ui/ui.module';
import { NgxMaskModule, IConfig } from 'ngx-mask'

const maskConfig: Partial<IConfig> = {
  validation: false,
};

@NgModule({
  declarations: [LoginComponent, LoginModalComponent],
  entryComponents: [LoginModalComponent],
  exports: [],
  imports: [
    CommonModule,
    NgxEditorModule,
    AuthRoutingModule,
    HttpClientModule,
    NgbAlertModule,
    FormsModule,
    NgSelectModule,
    UIModule,
    NgxMaskModule.forRoot(maskConfig),
  ]
})
export class AuthModule { }
