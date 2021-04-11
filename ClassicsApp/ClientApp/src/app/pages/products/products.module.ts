import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbDropdownModule, NgbModalModule, NgbPaginationModule, NgbCollapseModule, NgbTypeaheadModule, NgbDatepickerModule, NgbAlertModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxEditorModule } from 'ngx-editor';
import { HttpClientModule } from '@angular/common/http';
import { ProductRoutingModule } from './products-routing.module';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { UiSwitchModule } from 'ngx-ui-switch';
import { ProductComponent } from './product/product.component';
import { ManagementComponent } from './management/management.component';
import { StoreComponent } from './store/store.component';
import { StoreModalComponent } from './store/store-modal/store-modal.component';
import { ManagementModalComponent } from './management/management-modal/management-modal.component';
import { ProductModalComponent } from './product/product-modal/product-modal.component';
import { PhotoModalComponent } from './product/photo-modal/photo-modal.component';
import { DetailsModalComponent } from './product/details-modal/details-modal.component';
import { CurrencyMaskModule } from "ng2-currency-mask";

import { UIModule } from '../../shared/ui/ui.module';
import { MatMomentDateModule } from "@angular/material-moment-adapter";
import { MatDatepickerModule } from '@angular/material/datepicker';
import { NgxMaskModule, IConfig } from 'ngx-mask'
import { NgSelectModule } from '@ng-select/ng-select';

const maskConfig: Partial<IConfig> = {
  validation: false,
};

@NgModule({
  declarations: [
    ProductComponent,
    ProductModalComponent,
    PhotoModalComponent,
    DetailsModalComponent,
    ManagementComponent,
    ManagementModalComponent,
    StoreComponent, StoreModalComponent
  ],
  entryComponents: [
    ProductComponent,
    ProductModalComponent,
    PhotoModalComponent,
    DetailsModalComponent,
    ManagementComponent,
    ManagementModalComponent,
    StoreComponent, StoreModalComponent
  ],
  exports: [
    ProductComponent,
    ProductModalComponent,
    PhotoModalComponent,
    DetailsModalComponent,
    ManagementComponent,
    ManagementModalComponent,
    StoreComponent, StoreModalComponent
  ],
  imports: [
    CommonModule,
    NgbDropdownModule,
    NgbModalModule,
    NgbPaginationModule,
    NgbCollapseModule,
    NgxEditorModule,
    NgbDatepickerModule,
    ProductRoutingModule,
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
export class ProductsModule { }
