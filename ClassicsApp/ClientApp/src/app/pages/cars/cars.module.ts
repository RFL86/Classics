import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbDropdownModule, NgbModalModule, NgbPaginationModule, NgbCollapseModule, NgbTypeaheadModule, NgbDatepickerModule, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxEditorModule } from 'ngx-editor';
import { HttpClientModule } from '@angular/common/http';
import { CarsRoutingModule } from './cars-routing.module';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { UiSwitchModule } from 'ngx-ui-switch';

import { MyCarComponent } from './mycar/mycar.component';
import { CarComponent } from './car/car.component';
import { CarModalComponent } from './car/car-modal/car-modal.component';
import { MyCarModalComponent } from './mycar/mycar-modal/mycar-modal.component';


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
  declarations: [MyCarComponent, CarComponent, CarModalComponent, MyCarModalComponent],
  entryComponents: [CarModalComponent, MyCarModalComponent],
  exports: [CarModalComponent, MyCarModalComponent],
  imports: [
    CommonModule,
    NgbDropdownModule,
    NgbModalModule,
    NgbPaginationModule,
    NgbCollapseModule,
    NgxEditorModule,
    NgbDatepickerModule,
    CarsRoutingModule,
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
export class CarsModule { }
