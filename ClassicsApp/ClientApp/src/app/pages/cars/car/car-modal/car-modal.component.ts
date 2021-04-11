import { Injectable, Component, OnInit, AfterViewInit, Input } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { CarModalService } from './car-modal.service';




@Injectable({
  providedIn: "root"
})

@Component({
  selector: "car-modal",
  templateUrl: "./car-modal.html",
  styleUrls: ['./car-modal.scss']
})

export class CarModalComponent implements OnInit, AfterViewInit {
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

  constructor(public _activeModal: NgbActiveModal, public _service: CarModalService) {

  }

  ngAfterViewInit(): void {
  }

  ngOnInit(): void {
    this.itemId = this.idInput;
    this.type = this.typeInput;
    this.name = this.nameInput;
    this.status = Boolean(Number(this.statusInput));
    this.formData = new FormData();
    this.showErrorMessage = false;
    this.showSuccessMessage = false;
  }

  close() {
    this._activeModal.close();
  }

  editItem() {

    this.resetFormData();
    this.formData.append('Name', this.name);
    this.formData.append('ItemId', this.itemId);
    this.formData.append('Type', this.type);
    this.formData.append('Status', String(this.status));

    this._service.editItem(this.formData).subscribe(
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
    this.formData.delete('Name');
    this.formData.delete('ItemId');
    this.formData.delete('Type');
    this.formData.delete('Status');
  }

}


