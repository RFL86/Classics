import { Component, OnInit } from "@angular/core"
import { LoginService } from "./login.service";
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { LoginModalComponent } from './login-modal/login-modal.component'
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BindingService } from '../../../services/binding.service';


@Component({
  selector: "login",
  templateUrl: "./login.html",
  styleUrls: ['./login.scss'],
  providers: [LoginService]
})

export class LoginComponent implements OnInit {

  public email: string;
  public password: string;
  public errorMsg: string;
  public showError: boolean;
  public formData: FormData;

  constructor(public _authService: AuthService, private _activatedRouter: ActivatedRoute, private _router: Router, private modalService: NgbModal, public _bindingService: BindingService) {

  }

  ngOnInit(): void {
    this.email = '';
    this.password = '';
    this.showError = false;
    this.formData = new FormData();
  }

  showCreateUserModal() {
    this.modalService.open(LoginModalComponent, { size: 'md' });

    /* modal.result.then(() => { this.getSuppliers(); }, () => { console.log('Backdrop click') });*/
  }


  onClickLogin() {

    if (this.email == '' || this.password == '' || this.email == null || this.password == null) {
      this.showError = true;

      setTimeout(() => {
        this.showError = false;
      },
        3000);
    } else {

      this.ResetFormData();
      this.formData.append('Email', this.email);
      this.formData.append('Password', this.password);

      this._authService.authenticate(this.formData).subscribe(
        result => {
          if (result != null) {
            const obj = JSON.parse(result);
            localStorage.setItem('token', obj.token);
            localStorage.setItem('name', obj.name);
            localStorage.setItem('profile', obj.profileTypeValue); 
            localStorage.setItem('postalCode', obj.postalCode);
            this._bindingService.handleUserName();
            this._bindingService.handleSideBar();

            this._router.navigate(['../store'], { relativeTo: this._activatedRouter })
          } else {
            this.showError = true;
            setTimeout(() => {
              this.showError = false;
            },
              3000);
          }
        },
        error => {
          console.log(error);
        }
      );
      this.ResetFormData();
    }
  }

  //googleSignin() {
  //  this._auth.googleSignin();
  //}

  ResetFormData() {
    this.formData.delete('Email');
    this.formData.delete('Password');
  }

}


