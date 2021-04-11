import { Injectable, Component, OnInit, AfterViewInit, Input } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { SupplierModalService } from './supplier-modal.service';
import { SelectItem } from "./supplier-modal.model"

@Injectable({
  providedIn: "root"
})

@Component({
  selector: "supplier-modal",
  templateUrl: "./supplier-modal.html",
  styleUrls: ['./supplier-modal.scss']
})

export class SupplierModalComponent implements OnInit, AfterViewInit {
  @Input()
  supplierIdInput: string;
  titleInput: string;
  descriptionInput: string;
  emailInput: string;
  cnpjInput: string;
  phoneInput: string;
  statusInput: string;
  function: string;


  public supplierId: string;
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
  public cnpj: string;
  public email: string;
  public header: string;


  constructor(public _activeModal: NgbActiveModal, public _service: SupplierModalService) {

  }

  ngAfterViewInit(): void {
  }

  ngOnInit(): void {
    this.supplierId = this.supplierIdInput;
    this.status = this.statusInput;
    this.title = this.titleInput;
    this.description = this.descriptionInput;
    this.cnpj = this.cnpjInput;
    this.email = this.emailInput;
    this.phoneNumber = this.phoneInput;
    if (this.function == 'edit') {
      this.header = 'Editar Fornecedor';
    } else {
      this.header = 'Novo Fornecedor';
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

  getSupplier() {
    this._service.getSupplier(this.supplierIdInput).subscribe(
      result => {
        this.data = result;
        this.loaderData = { visible: false, text: "" };
      },
      error => {
        console.log(error);
      }
    );
  }

  fillLists() {
    this.statusList = [{ text: 'Ativo', value: '1' }, { text: 'Inativo', value: '0' }]
  }

  onSelectStatus(item) {
    const selectedItem = item as SelectItem;
    this.status = selectedItem.value;
  }

  save() {
    this.resetFormData();
    this.formData.append('Title', this.status);
    this.formData.append('Description', this.description);
    this.formData.append('Email', this.email);
    this.formData.append('PhoneNumber', this.phoneInput);
    this.formData.append('Cnpj', this.cnpj);
    this.formData.append('Status', this.status);
    this.formData.append('SupplierId', this.supplierId);

    if (this.function == 'edit') {
      this.editSupplier();
    } else {
      this.createSupplier();
    }
  }

  createSupplier() {
    this._service.createSupplier(this.formData).subscribe(
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

  editSupplier() {
    this._service.editSupplier(this.formData).subscribe(
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
    this.formData.delete('Title');
    this.formData.delete('Description');
    this.formData.delete('Email');
    this.formData.delete('PhoneNumber');
    this.formData.delete('Cnpj');
    this.formData.delete('Status');
    this.formData.delete('SupplierId');
  }

}


