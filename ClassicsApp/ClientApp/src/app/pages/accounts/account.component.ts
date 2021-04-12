import { Component, OnInit } from "@angular/core"
import { AccountService } from "./account.service";
import { AccountModalComponent } from './account-modal/account-modal.component'
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BindingService } from '../../services/binding.service';



@Component({
  selector: "account",
  templateUrl: "./account.html",
  styleUrls: ['./account.scss'],
  providers: [AccountService]
})

export class AccountComponent implements OnInit {

  public user: any;
  public loaderData: any;

  constructor(private _modalService: NgbModal, public _bindingService: BindingService, public _service: AccountService) {

  }

  ngOnInit(): void {
    this.loaderData = { visible: false, text: "" };
    this.getUser()
  }

  getUser() {
    this.loaderData = { visible: true, text: "" };

    this._service.getUser().subscribe(
      result => {
        this.user = result;
        this.loaderData = { visible: false, text: "" };
      },
      error => {
        console.log(error);
      }
    );
  }

  showEditUserModal(name, phone, postalCode) {
    const modal = this._modalService.open(AccountModalComponent, { size: 'md' });
    modal.result.then(() => { this.getUser(); }, () => { console.log('Backdrop click') });
  }

}


