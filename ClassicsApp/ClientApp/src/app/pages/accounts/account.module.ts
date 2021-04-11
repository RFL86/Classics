import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbDropdownModule, NgbModalModule, NgbPaginationModule, NgbCollapseModule, NgbTypeaheadModule, NgbDatepickerModule, NgbAlertModule, NgbModule  } from '@ng-bootstrap/ng-bootstrap';
import { NgxEditorModule } from 'ngx-editor';
import { HttpClientModule } from '@angular/common/http';
import { AccountRoutingModule } from './account-routing.module';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { UiSwitchModule } from 'ngx-ui-switch';
import { AccountComponent } from './account.component';
import { AccountModalComponent } from './account-modal/account-modal.component';

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
  declarations: [AccountComponent, AccountModalComponent],
  exports: [AccountComponent, AccountModalComponent],
  entryComponents: [AccountComponent, AccountModalComponent],
  imports: [
    CommonModule,
    NgbDropdownModule,
    NgbModalModule,
    NgbPaginationModule,
    NgbCollapseModule,
    NgxEditorModule,
    NgbDatepickerModule,
    AccountRoutingModule,
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
    UiSwitchModule,
    NgbModule
  ]
})
export class AccountModule { }
