import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SupService } from './sup.service';
import { SupModalComponent } from './sup-modal/sup-modal.component';

@Component({
  selector: 'sup',
  templateUrl: './sup.html',
  styleUrls: ['./sup.css'],
  providers: [SupService]
})

export class SupComponent implements OnInit {
  public breadCrumbItems: Array<{}>;
  tableData: any;

  public supTable: any;
  public loaderData: any;

  constructor(public _service: SupService, private modalService: NgbModal) {

  }

  ngOnInit(): void {
    this.breadCrumbItems = [{ label: 'Início', path: '/' }, { label: 'Forcenedores', path: '/', active: true }];
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

  showDetails(titleInput, descriptionInput, emailInput, cnpjInput, phoneInput, statusInput) {
    const modal = this.modalService.open(SupModalComponent, { size: 'md' });
    modal.componentInstance.titleInput = titleInput;
    modal.componentInstance.descriptionInput = descriptionInput;
    modal.componentInstance.emailInput = emailInput;
    modal.componentInstance.cnpjInput = cnpjInput;
    modal.componentInstance.phoneInput = phoneInput;
    modal.componentInstance.statusInput = statusInput;
  }  
}
