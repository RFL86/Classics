////import { NgModule } from '@angular/core';
////import { CommonModule } from '@angular/common';
////import { FormsModule } from '@angular/forms';
////import { NgbDropdownModule, NgbModalModule, NgbPaginationModule, NgbCollapseModule, NgbTypeaheadModule, NgbDatepickerModule, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
////import { NgxEditorModule } from 'ngx-editor';
////import { HttpClientModule } from '@angular/common/http';
////import { ChartsRoutingModule } from './charts-routing.module';
////import { ChartsComponent } from './charts.component';
////import { PdfViewerModule } from 'ng2-pdf-viewer';
////import { UiSwitchModule } from 'ngx-ui-switch';
////import { NgApexchartsModule } from 'ng-apexcharts';
////import { ChartsModule } from 'ng2-charts';



////import { UIModule } from '../../shared/ui/ui.module';
////import { CurrencyMaskModule } from "ng2-currency-mask";
////import { MatMomentDateModule } from "@angular/material-moment-adapter";
////import { MatDatepickerModule } from '@angular/material/datepicker';
////import { NgxMaskModule, IConfig } from 'ngx-mask'
////import { NgSelectModule } from '@ng-select/ng-select';


//const maskConfig: Partial<IConfig> = {
//  validation: false,
//};




import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NgApexchartsModule } from 'ng-apexcharts';
import { NgxChartistModule } from 'ngx-chartist';
import { ChartsModule } from 'ng2-charts';
import { ChartComponent } from './chart/chart.component';
import { ReportsRoutingModule } from './reports-routing.module';


import { UIModule } from '../../shared/ui/ui.module';

////import { ChartsComponent } from './charts.component';


@NgModule({
  declarations: [ChartComponent],
  imports: [
    CommonModule,
    ReportsRoutingModule,
    UIModule,
    NgApexchartsModule,
    CommonModule,
    UIModule,
    NgApexchartsModule,
    NgxChartistModule,
    ChartsModule
  ]
})
export class ReportsModule { }
