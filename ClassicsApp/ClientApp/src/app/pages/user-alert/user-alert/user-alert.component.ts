import { Component, OnInit } from "@angular/core"
import { UserAlertService } from './user-alert.service';
import { UserAlertModalComponent } from './user-alert-modal/user-alert-modal.component';
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";


@Component({
  selector: "user-alert",
  templateUrl: "./user-alert.html",
  styleUrls: ['./user-alert.scss'],
  providers: [UserAlertService]
})

export class UserAlertComponent implements OnInit {

  public breadCrumbItems: Array<{}>;
  tableData: any;
  service: UserAlertService;

  public data: any;
  formData: FormData;
  public loaderData: any;

  constructor(public _service: UserAlertService, private modalService: NgbModal) {

  }

  ngOnInit(): void {
    this.breadCrumbItems = [{ label: 'Minha Conta', path: '/' }, { label: 'Meus Alertas', path: '/', active: true }];  
    this.formData = new FormData();
    this.loaderData = { visible: false, text: "" };
    this.getAlerts();
  }

  getAlerts() {
    this.loaderData = { visible: true, text: "" };

    this._service.getAlerts().subscribe(
      result => {
        this.data = result;
        this.loaderData = { visible: false, text: "" };
      },
      error => {
        console.log(error);
      }
    );
  }


  changeAlertStatus(alertId) {

    this.resetFormData();
    this.formData.append('UserAlertId', alertId);

    this.loaderData = { visible: true, text: "" };
    this._service.changeAlertStatus(this.formData).subscribe(
      result => {
        console.log(result);
        this.loaderData = { visible: false, text: "" };
      },
      error => {
        console.log(error);
      }
    );
    this.resetFormData();
  }

  showAlertModal(userAlertId, subject, message) {
    const modal = this.modalService.open(UserAlertModalComponent, { size: 'md' });
    this.changeAlertStatus(userAlertId);
    modal.componentInstance.subject = subject;
    modal.componentInstance.message = message;
    modal.result.then(() => { this.getAlerts(); }, () => { console.log('Backdrop click') });
  }

  resetFormData() {
    this.formData.delete('UserAlertId');
  }
}
