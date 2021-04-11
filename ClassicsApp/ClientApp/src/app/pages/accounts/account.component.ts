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
  constructor(private _modalService: NgbModal, public _bindingService: BindingService, public _service: AccountService) {

  }

  ngOnInit(): void {
    this.getUser()
  }

  getUser() {
    this._service.getUser().subscribe(
      result => {
        this.user = result;
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


