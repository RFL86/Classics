import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbDropdownModule, NgbModalModule, NgbPaginationModule, NgbCollapseModule, NgbTypeaheadModule, NgbDatepickerModule, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxEditorModule } from 'ngx-editor';
import { HttpClientModule } from '@angular/common/http';
import { UsersRoutingModule } from './users-routing.module';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { UiSwitchModule } from 'ngx-ui-switch';
import { UserComponent } from './user/user.component';
import { UserModalComponent } from './user/user-modal/user-modal.component';



import { UIModule } from '../../shared/ui/ui.module';
import { CurrencyMaskModule } from "ng2-currency-mask";
import { MatMomentDateModule } from "@angular/material-moment-adapter";
import { MatDatepickerModule } from '@angular/material/datepicker';
import { NgxMaskModule, IConfig } from 'ngx-mask'
import { NgSelectModule } from '@ng-select/ng-select';

const maskConfig: Partial<IConfig> = {
  validation: false,
};

@NgModule({
  declarations: [UserComponent, UserModalComponent],
  entryComponents: [UserComponent, UserModalComponent],
  exports: [UserComponent, UserModalComponent],
  imports: [
    CommonModule,
    NgbDropdownModule,
    NgbModalModule,
    NgbPaginationModule,
    NgbCollapseModule,
    NgxEditorModule,
    NgbDatepickerModule,
    UsersRoutingModule,
    UIModule,
    FormsModule,
    HttpClientModule,
    NgbTypeaheadModule,
    CurrencyMaskModule,
    MatMomentDateModule,
    NgbAlertModule,
    NgxMaskModule.forRoot(maskConfig),
    MatDatepickerModule,
    PdfViewerModule,
    NgSelectModule,
    UiSwitchModule
  ]
})
export class UsersModule { }
