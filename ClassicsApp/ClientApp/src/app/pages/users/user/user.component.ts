import { Component, OnInit } from "@angular/core"
import { UserService } from './user.service';
import { UserModalComponent } from './user-modal/user-modal.component';
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";



@Component({
  selector: "user",
  templateUrl: "./user.html",
  styleUrls: ['./user.scss'],
  providers: [UserService]
})

export class UserComponent implements OnInit {

  public breadCrumbItems: Array<{}>;
  tableData: any;
  service: UserService;

  public usersTable: any;
  formData: FormData;
  public successMessage: string;
  public errorMessage: string;
  public showSuccessMessage: boolean;
  public showErrorMessage: boolean;
  public loaderData: any;

  constructor(public _service: UserService, private modalService: NgbModal) {

  }

  ngOnInit(): void {
    this.breadCrumbItems = [{ label: 'Cadastros Gerais', path: '/' }, { label: 'Permissões', path: '/', active: true }];  
    this.formData = new FormData();
    this.loaderData = { visible: false, text: "" };
    this.getUsers();
  }

  getUsers() {
    this.loaderData = { visible: true, text: "" };

    this._service.getUsers().subscribe(
      result => {
        this.usersTable = result;
        this.loaderData = { visible: false, text: "" };
      },
      error => {
        console.log(error);
      }
    );
  }

  showEditUserModal(profileType, status, name, userId) {
    const modal = this.modalService.open(UserModalComponent, { size: 'md' });
    modal.componentInstance.userIdInput = userId;
    modal.componentInstance.profileTypeInput = profileType;
    modal.componentInstance.statusInput = status;
    modal.componentInstance.userNameInput = name;

    modal.result.then(() => { this.getUsers(); }, () => { console.log('Backdrop click') });
  }
}
