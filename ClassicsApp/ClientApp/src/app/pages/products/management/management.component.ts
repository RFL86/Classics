import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ManagementService } from './management.service'
import { ManagementModalComponent } from './management-modal/management-modal.component';

@Component({
  selector: 'management',
  templateUrl: './management.html',
  styleUrls: ['./management.css'],
  providers: [ManagementService]
})

export class ManagementComponent implements OnInit {
  public breadCrumbItems: Array<{}>;
  tableData: any;

  public productsTable: any;
  formData: FormData;
  public successMessage: string;
  public errorMessage: string;
  public showSuccessMessage: boolean;
  public showErrorMessage: boolean;
  public loaderData: any;

  constructor(public _service: ManagementService, private modalService: NgbModal) {

  }

  ngOnInit(): void {
    this.breadCrumbItems = [{ label: 'Cadastros', path: '/' }, { label: 'Gerenciamento de Produtos', path: '/', active: true }];
    this.formData = new FormData();
    this.loaderData = { visible: false, text: "" };
    this.getProducts();
  }

  getProducts() {
    this.loaderData = { visible: true, text: "" };

    this._service.getProducts().subscribe(
      result => {
        this.productsTable = result;
        this.loaderData = { visible: false, text: "" };
      },
      error => {
        console.log(error);
      }
    );
  }

  showDetailsModal(productId, titleInput, descriptionInput, priceInput, photoUrlInput, emailInput, addressInput, phoneNumberInput, carModel) {
    const modal = this.modalService.open(ManagementModalComponent, { size: 'md' });
    modal.componentInstance.titleInput = titleInput;
    modal.componentInstance.descriptionInput = descriptionInput;
    modal.componentInstance.priceInput = priceInput;
    modal.componentInstance.photoUrlInput = photoUrlInput == null ? 'assets/images/not-found.png' : photoUrlInput;
    modal.componentInstance.emailInput = emailInput;
    modal.componentInstance.addressInput = addressInput;
    modal.componentInstance.phoneNumberInput = phoneNumberInput;
    modal.componentInstance.carModelInput = carModel; 
    modal.componentInstance.productIdInput = productId;
    modal.result.then(() => { this.getProducts(); }, () => { console.log('Backdrop click') });

  }
}
