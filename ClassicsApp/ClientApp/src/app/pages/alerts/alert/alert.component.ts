import { Component, OnInit } from "@angular/core"
import { AlertService } from './alert.service';
import { AlertModalComponent } from './alert-modal/alert-modal.component';
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";



@Component({
  selector: "alert",
  templateUrl: "./alert.html",
  styleUrls: ['./alert.scss'],
  providers: [AlertService]
})

export class AlertComponent implements OnInit {

  public breadCrumbItems: Array<{}>;
  tableData: any;
  service: AlertService;

  public data: any;
  formData: FormData;
  public successMessage: string;
  public errorMessage: string;
  public showSuccessMessage: boolean;
  public showErrorMessage: boolean;
  public loaderData: any;

  constructor(public _service: AlertService, private modalService: NgbModal) {

  }

  ngOnInit(): void {
    this.breadCrumbItems = [{ label: 'Cadastros Gerais', path: '/' }, { label: 'Alertas', path: '/', active: true }];  
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


  changeAlertStatus(status, alertId) {

    const realStatus = status == '1' ? '0' : '1';
    this.resetFormData();
    this.formData.append('AlertId', alertId);
    this.formData.append('Status', realStatus);

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

  showCreateAlertModal() {
    const modal = this.modalService.open(AlertModalComponent, { size: 'md' });
    modal.result.then(() => { this.getAlerts(); }, () => { console.log('Backdrop click') });
  }

  resetFormData() {
    this.formData.delete('AlertId');
    this.formData.delete('Status');
  }
}
