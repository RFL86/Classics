import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProductService } from './product.service'
import { ProductModalComponent } from './product-modal/product-modal.component'
import { PhotoModalComponent } from './photo-modal/photo-modal.component'
import { DetailsModalComponent } from './details-modal/details-modal.component'

@Component({
  selector: 'product',
  templateUrl: './product.html',
  styleUrls: ['./product.css'],
  providers: [ProductService]
})

export class ProductComponent implements OnInit {
  public breadCrumbItems: Array<{}>;
  tableData: any;

  public productsTable: any;
  formData: FormData;
  public successMessage: string;
  public errorMessage: string;
  public showSuccessMessage: boolean;
  public showErrorMessage: boolean;
  public loaderData: any;

  constructor(public _service: ProductService, private modalService: NgbModal) {

  }

  ngOnInit(): void {
    this.breadCrumbItems = [{ label: 'Minha Conta', path: '/' }, { label: 'Meus Anúncios', path: '/', active: true }];
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

  showEditProductModal(productId, titleInput, descriptionInput, priceInput, statusInput, modelId) {
    const modal = this.modalService.open(ProductModalComponent, { size: 'md' });
    modal.componentInstance.productIdInput = productId;
    modal.componentInstance.titleInput = titleInput;
    modal.componentInstance.descriptionInput = descriptionInput;
    modal.componentInstance.priceInput = priceInput;
    modal.componentInstance.statusInput = statusInput;
    modal.componentInstance.modelIdInput = modelId;
    modal.componentInstance.function = 'edit';

    modal.result.then(() => { this.getProducts(); }, () => { console.log('Backdrop click') });
  }

  showEditPhotoModal(productId, photoUrl) {
    const modal = this.modalService.open(PhotoModalComponent, { size: 'md' });
    modal.componentInstance.productIdInput = productId;
    modal.componentInstance.photoUrlInput = photoUrl == null ? 'assets/images/not-found.png' : photoUrl;

    modal.result.then(() => { this.getProducts(); }, () => { console.log('Backdrop click') });
  }

  showCreateProductModal() {
    const modal = this.modalService.open(ProductModalComponent, { size: 'md' });
    modal.componentInstance.productIdInput = '';
    modal.componentInstance.titleInput = '';
    modal.componentInstance.descriptionInput = '';
    modal.componentInstance.priceInput = '';
    modal.componentInstance.statusInput = '1';
    modal.componentInstance.function = 'create';

    modal.result.then(() => { this.getProducts(); }, () => { console.log('Backdrop click') });
  }

  showDetailsModal(titleInput, descriptionInput, priceInput, photoUrlInput, emailInput, addressInput, phoneNumberInput, carModel) {
    const modal = this.modalService.open(DetailsModalComponent, { size: 'md' });
    modal.componentInstance.titleInput = titleInput;
    modal.componentInstance.descriptionInput = descriptionInput;
    modal.componentInstance.priceInput = priceInput;
    modal.componentInstance.photoUrlInput = photoUrlInput == null ? 'assets/images/not-found.png' : photoUrlInput;
    modal.componentInstance.emailInput = emailInput;
    modal.componentInstance.addressInput = addressInput;
    modal.componentInstance.phoneNumberInput = phoneNumberInput;
    modal.componentInstance.carModelInput = carModel;
  }
}
