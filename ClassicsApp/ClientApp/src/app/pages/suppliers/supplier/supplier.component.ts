import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SupplierService } from './supplier.service'
import { SupplierModalComponent } from './supplier-modal/supplier-modal.component'

@Component({
  selector: 'supplier',
  templateUrl: './supplier.html',
  styleUrls: ['./supplier.css'],
  providers: [SupplierService]
})

export class SupplierComponent implements OnInit {
  public breadCrumbItems: Array<{}>;
  tableData: any;

  public supTable: any;
  formData: FormData;
  public successMessage: string;
  public errorMessage: string;
  public showSuccessMessage: boolean;
  public showErrorMessage: boolean;
  public loaderData: any;

  constructor(public _service: SupplierService, private modalService: NgbModal) {

  }

  ngOnInit(): void {
    this.breadCrumbItems = [{ label: 'Cadastros Gerais', path: '/' }, { label: 'Forcenedores', path: '/', active: true }];
    this.formData = new FormData();
    this.loaderData = { visible: false, text: "" };
    this.getSuppliers();
  }

  getSuppliers() {
    this.loaderData = { visible: true, text: "" };

    this._service.getSuppliers().subscribe(
      result => {
        this.supTable = result;
        this.loaderData = { visible: false, text: "" };
      },
      error => {
        console.log(error);
      }
    );
  }

  showEditSupplierModal(supplierId, titleInput, descriptionInput, emailInput, cnpjInput, phoneInput, statusInput) {
    const modal = this.modalService.open(SupplierModalComponent, { size: 'md' });
    modal.componentInstance.supplierIdInput = supplierId;
    modal.componentInstance.titleInput = titleInput;
    modal.componentInstance.descriptionInput = descriptionInput;
    modal.componentInstance.emailInput = emailInput;
    modal.componentInstance.cnpjInput = cnpjInput;
    modal.componentInstance.phoneInput = phoneInput;
    modal.componentInstance.statusInput = statusInput;
    modal.componentInstance.function = 'edit';

    modal.result.then(() => { this.getSuppliers(); }, () => { console.log('Backdrop click') });
  }

  showCreateSupplierModal() {
    const modal = this.modalService.open(SupplierModalComponent, { size: 'md' });
    modal.componentInstance.supplierIdInput = '';
    modal.componentInstance.titleInput = '';
    modal.componentInstance.descriptionInput = '';
    modal.componentInstance.emailInput = '';
    modal.componentInstance.cnpjInput = '';
    modal.componentInstance.phoneInput = '';
    modal.componentInstance.statusInput = '1';
    modal.componentInstance.function = 'create';


    modal.result.then(() => { this.getSuppliers(); }, () => { console.log('Backdrop click') });
  }
}
