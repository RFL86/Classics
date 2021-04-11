import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { StoreService } from './store.service'
import { StoreModalComponent } from './store-modal/store-modal.component';

@Component({
  selector: 'store',
  templateUrl: './store.html',
  styleUrls: ['./store.css'],
  providers: [StoreService]
})

export class StoreComponent implements OnInit {
  public breadCrumbItems: Array<{}>;
  tableData: any;

  public productsTable: any;
  formData: FormData;
  public successMessage: string;
  public errorMessage: string;
  public showSuccessMessage: boolean;
  public showErrorMessage: boolean;
  public loaderData: any;

  constructor(public _service: StoreService, private modalService: NgbModal) {

  }

  ngOnInit(): void {
    this.breadCrumbItems = [{ label: 'Início', path: '/' }, { label: 'Gerenciamento de Produtos', path: '/', active: true }];
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
    const modal = this.modalService.open(StoreModalComponent, { size: 'md' });
    modal.componentInstance.titleInput = titleInput;
    modal.componentInstance.descriptionInput = descriptionInput;
    modal.componentInstance.priceInput = priceInput;
    modal.componentInstance.photoUrlInput = photoUrlInput == null ? 'assets/images/not-found.png' : photoUrlInput;
    modal.componentInstance.emailInput = emailInput;
    modal.componentInstance.addressInput = addressInput;
    modal.componentInstance.phoneNumberInput = phoneNumberInput;
    modal.componentInstance.carModelInput = carModel; 
    modal.componentInstance.productIdInput = productId; 
  }
}
