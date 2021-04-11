import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { NgbCollapseModule, NgbDatepickerModule, NgbTimepickerModule, NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { ClickOutsideModule } from 'ng-click-outside';

import { SlimscrollDirective } from './slimscroll.directive';
import { CountToDirective } from './count-to.directive';


import { PagetitleComponent } from './pagetitle/pagetitle.component';
import { LoaderComponent } from './loader/loader.component';
import { UserNameComponent } from './user-name/user-name.component';
import { PortletComponent } from './portlet/portlet.component';



@NgModule({
    // tslint:disable-next-line: max-line-length
  declarations: [SlimscrollDirective, PagetitleComponent, CountToDirective, LoaderComponent, UserNameComponent, PortletComponent],
    imports: [
        CommonModule,
        FormsModule,
        ClickOutsideModule,
        NgbCollapseModule,
        NgbDatepickerModule,
        NgbTimepickerModule,
        NgbDropdownModule
    ],
    // tslint:disable-next-line: max-line-length
  exports: [SlimscrollDirective, PagetitleComponent, CountToDirective, LoaderComponent, UserNameComponent, PortletComponent]
})
export class UIModule { }
