import { BrowserModule } from '@angular/platform-browser';
import { registerLocaleData } from '@angular/common';
import localeFr from '@angular/common/locales/br';
import localeFrExtra from '@angular/common/locales/extra/pt';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';


import { AppComponent } from './app.component';
import { NgbModule, NgbDateParserFormatter, NgbDatepickerI18n } from '@ng-bootstrap/ng-bootstrap';
import { NgbDatePTParserFormatter } from './helpers/custom-datepicker/NgbDatePTParserFormatter';
import { CustomDatepickerI18n, I18n } from './helpers/custom-datepicker/CustomDatepickerI18n';
import { LayoutsModule } from './layouts/layouts.module';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularFireModule } from '@angular/fire';
import { AngularFirestoreModule } from '@angular/fire/firestore';
import { AngularFireAuthModule } from '@angular/fire/auth';
import { CurrencyMaskModule } from "ng2-currency-mask";
registerLocaleData(localeFr, 'pt', localeFrExtra);


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    LayoutsModule,
    CurrencyMaskModule,
    AppRoutingModule,
    NgbModule,
    BrowserAnimationsModule,
    AngularFireModule.initializeApp({
      apiKey: "AIzaSyAgzgERn4UotjcSeSAd-8Nl7iTKpqjUses",
      authDomain: "classics-8455c.firebaseapp.com",
      projectId: "classics-8455c",
      storageBucket: "classics-8455c.appspot.com",
      messagingSenderId: "503619324593",
      appId: "1:503619324593:web:8fc5bcd5f70f84e098f090"
    }),
    AngularFireAuthModule,
    AngularFirestoreModule
  ],
  providers: [[I18n, { provide: NgbDatepickerI18n, useClass: CustomDatepickerI18n }], 
  [{ provide: NgbDateParserFormatter, useClass: NgbDatePTParserFormatter }],],
  bootstrap: [AppComponent]
})
export class AppModule { }
