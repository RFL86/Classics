import { Injectable, Component, OnInit, AfterViewInit, Input } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { ProductModalService } from './product-modal.service';
import { SelectItem } from "./product-modal.model"

@Injectable({
  providedIn: "root"
})

@Component({
  selector: "product-modal",
  templateUrl: "./product-modal.html",
  styleUrls: ['./product-modal.scss']
})

export class ProductModalComponent implements OnInit, AfterViewInit {
  @Input()
  productIdInput: string;
  titleInput: string;
  descriptionInput: string;
  priceInput: string;
  statusInput: string;
  function: string;
  modelIdInput: string;

  public productId: string;
  public data: any;
  public statusList: SelectItem[];
  public formData: FormData;
  public successMessage: string;
  public errorMessage: string;
  public showSuccessMessage: boolean;
  public showErrorMessage: boolean;
  public selectedStatus: SelectItem;
  public loaderData: any;
  public status: string;
  public title: string;
  public description: string;
  public phoneNumber: string;
  public price: string;
  public header: string;
  public selectedModel: SelectItem;
  public modelsList: SelectItem[];
  public modelId: string;


  constructor(public _activeModal: NgbActiveModal, public _service: ProductModalService) {
  
  }

  ngAfterViewInit(): void {
    this.getCarModels();
  }

  ngOnInit(): void {
    this.productId = this.productIdInput;
    this.status = this.statusInput;
    this.title = this.titleInput;
    this.description = this.descriptionInput;
    this.price = this.priceInput;
    this.modelId = this.modelIdInput;
    if (this.function == 'edit') {
      this.header = 'Editar Produto';
    } else {
      this.header = 'Novo Produto';
    }

    this.formData = new FormData();
    this.showErrorMessage = false;
    this.showSuccessMessage = false;
    this.fillLists();
  
    this.selectedStatus = this.statusList.filter(x => x.value == this.statusInput)[0];
  }

  close() {
    this._activeModal.close();
  }

  fillLists() {
    this.statusList = [{ text: 'Ativo', value: '1'}, { text: 'Inativo', value: '3' }]
  }

  onSelectStatus(item) {
    const selectedItem = item as SelectItem;
    this.status = selectedItem.value;
  }

  onSelectModel(item) {
    const selectedItem = item as SelectItem;
    this.modelId = selectedItem.value;
  }

  getCarModels() {
    this._service.getCarModels().subscribe(
      result => {
        this.selectedModel = null;
        this.modelsList = null;
        this.modelsList = result.map(p => ({
          text: p.text,
          value: p.value
        }));
        this.selectedModel = this.modelsList.filter(x => x.value == this.modelIdInput)[0];
        this.onSelectModel(this.selectedModel);
      },
      error => {
        console.log(error);
      }
    );
  }


  save() {
    this.resetFormData();
    this.formData.append('Title', this.title);
    this.formData.append('Description', this.description);
    this.formData.append('Price', this.price);
    this.formData.append('Status', this.status);
    this.formData.append('ProductId', this.productId);

    if (this.function == 'edit') {
      this.editProduct();
    } else {
      this.createProduct();
    }
  }

  createProduct() {
    this._service.createProduct(this.formData).subscribe(
      result => {
        if (result == null || result == '') {
          this.showSuccessMessage = true;
          this.successMessage = 'Item criado com sucesso.';
          setTimeout(() => {
            this.successMessage = '';
            this.showSuccessMessage = false;
            this.close();
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

  editProduct() {
    this._service.editProduct(this.formData).subscribe(
      result => {
        if (result == null || result == '') {
          this.showSuccessMessage = true;
          this.successMessage = 'Item editado com sucesso.';
          setTimeout(() => {
            this.successMessage = '';
            this.showSuccessMessage = false;
            this.close();
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
    this.formData.delete('Title');
    this.formData.delete('Description');
    this.formData.delete('Price');
    this.formData.delete('Status');
    this.formData.delete('ProductId');
  }

}


