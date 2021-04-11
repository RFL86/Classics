import { Injectable, Component, OnInit, AfterViewInit, Input } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { ManagementService } from './management-modal.service';


@Injectable({
  providedIn: "root"
})

@Component({
  selector: "management-modal",
  templateUrl: "./management-modal.html",
  styleUrls: ['./management-modal.scss']
})

export class ManagementModalComponent implements OnInit, AfterViewInit {
  @Input()
  titleInput: string;
  descriptionInput: string;
  priceInput: string;
  photoUrlInput: string;
  emailInput: string;
  addressInput: string;
  phoneNumberInput: string;
  carModelInput: string;
  productIdInput: string

  public title: string;
  public description: string;
  public phoneNumber: string;
  public price: string;
  public email: string;
  public address: string;
  public photoUrl: string;
  public carModel: string;
  public productId: string;
  public formData: FormData;
  public successMessage: string;
  public errorMessage: string;
  public showSuccessMessage: boolean;
  public showErrorMessage: boolean;


  constructor(public _activeModal: NgbActiveModal, public _service: ManagementService) {

  }

  ngAfterViewInit(): void {
  }

  ngOnInit(): void {
    this.title = this.titleInput;
    this.description = this.descriptionInput;
    this.price = this.priceInput;
    this.photoUrl = this.photoUrlInput;
    this.address = this.addressInput;
    this.phoneNumber = this.phoneNumberInput;
    this.email = this.emailInput;
    this.carModel = this.carModelInput;
    this.formData = new FormData();
    this.formData.append('ProductId', this.productId);
    this.showErrorMessage = false;
    this.showSuccessMessage = false;
    this.errorMessage = "Erro ao mudar status";
    this.successMessage = "Item alterado com sucesso";
  }

  close() {
    this._activeModal.close();
  }

  manage(status) {
    this.formData.append('Status', status);
    this._service.manageProduct(this.formData).subscribe(
      data => {
        if (data) {
          this.showSuccessMessage = true;
          setTimeout(() => {
            this.showSuccessMessage = false;
            this.close()
          },
            3000);
        }
      },
      error => {
        console.log(error);
        this.showErrorMessage = true;
        setTimeout(() => {
          this.showErrorMessage = false;
        },
          3000);
      });

  }
  

}


