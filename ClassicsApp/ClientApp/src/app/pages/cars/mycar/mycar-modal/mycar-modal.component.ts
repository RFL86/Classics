import { Injectable, Component, OnInit, AfterViewInit, Input } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { MyCarModalService } from './mycar-modal.service';


@Injectable({
  providedIn: "root"
})

@Component({
  selector: "mycar-modal",
  templateUrl: "./mycar-modal.html",
  styleUrls: ['./mycar-modal.scss']
})

export class MyCarModalComponent implements OnInit, AfterViewInit {
  @Input()
  nameInput: string;
  typeInput: string;
  statusInput: string;
  idInput: string;

  public name: string;
  public itemId: string;
  public status: boolean;
  public type: string;
  public formData: FormData;
  public successMessage: string;
  public errorMessage: string;
  public showSuccessMessage: boolean;
  public showErrorMessage: boolean;

  constructor(public _activeModal: NgbActiveModal, public _service: MyCarModalService) {

  }

  ngAfterViewInit(): void {
  }

  ngOnInit(): void {
    this.itemId = this.idInput;
    this.name = this.nameInput;
    this.formData = new FormData();
    this.showErrorMessage = false;
    this.showSuccessMessage = false;
  }

  removeMyCar() {
    this.formData.append('MyCarId', this.itemId);
    this._service.removeMyCar(this.formData);
    this.close();
  }

  close() {
    this._activeModal.close();
  }

  editMyCar() {

    this.resetFormData();
    this.formData.append('NickName', this.name);
    this.formData.append('MyCarId', this.itemId);

    this._service.editMyCar(this.formData).subscribe(
      result => {
        if (result == null || result == '') {
          this.showSuccessMessage = true;
          this.successMessage = 'Item editado com sucesso.';
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
    this.formData.delete('NickName');
    this.formData.delete('MyCarId');
  }

}


