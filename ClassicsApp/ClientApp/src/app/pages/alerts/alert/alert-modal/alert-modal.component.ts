import { Injectable, Component, OnInit, AfterViewInit } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { AlertModalService } from './alert-modal.service';
import { SelectItem } from "./alert-modal.model"


@Injectable({
  providedIn: "root"
})

@Component({
  selector: "alert-modal",
  templateUrl: "./alert-modal.html",
  styleUrls: ['./alert-modal.scss']
})

export class AlertModalComponent implements OnInit, AfterViewInit {

  public subject: string;  
  public message: string;
  public receiverType: string;
  public receiversList: SelectItem[];
  public formData: FormData;
  public successMessage: string;
  public errorMessage: string;
  public showSuccessMessage: boolean;
  public showErrorMessage: boolean;
  public selectedReceiver: SelectItem;

  constructor(public _activeModal: NgbActiveModal, public _service: AlertModalService) {

  }

  ngAfterViewInit(): void {
  }

  ngOnInit(): void {
    this.fillLists();
    this.selectedReceiver = this.receiversList[0];
    this.receiverType = this.selectedReceiver.value;
    this.subject = '';
    this.message = '';
    this.formData = new FormData();
    this.showErrorMessage = false;
    this.showSuccessMessage = false;   
  }

  close() {
    this._activeModal.close();
  }

  fillLists() {
    this.receiversList = [{ text: 'Clientes', value: '0' }, { text: 'Todos', value: '1' }];  
  }
  onSelectReceiver(item) {
    const selectedItem = item as SelectItem;
    this.receiverType = selectedItem.value;
  }

  save() {

    console.log(this.subject);
    console.log(this.message);
    console.log(this.receiverType);

    this.resetFormData();
    this.formData.append('Subject', this.subject);
    this.formData.append('Message', this.message);
    this.formData.append('Receiver', this.receiverType);

    this._service.createAlert(this.formData).subscribe(
      result => {
        if (result == null || result == '') {
          this.showSuccessMessage = true;
          this.successMessage = 'Alerta com sucesso.';
          setTimeout(() => {
            this.successMessage = '';
            this.showSuccessMessage = false;
          },
            2000);
        } else {
          this.showErrorMessage = true;
          this.errorMessage = result;
          setTimeout(() => {
            this.errorMessage = '';
            this.showErrorMessage = false;
          },
            2000);
        }
      },
      error => {
        console.log(error);
      }
    );
    this.resetFormData();
    this.showErrorMessage = false;
    this.showSuccessMessage = false;
  }


  resetFormData() {
    this.formData.delete('Subject');
    this.formData.delete('Message');
    this.formData.delete('Receiver');
  }

}


