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
  public loaderData: any;
  public showLoginForm: boolean;



  constructor(public _authService: AuthService, private _activatedRouter: ActivatedRoute, private _router: Router, private modalService: NgbModal, public _bindingService: BindingService) {

  }

  ngOnInit(): void {
    this.loaderData = { visible: true, text: "" };
    this.email = '';
    this.password = '';
    this.showError = false;
    this.formData = new FormData();
    const token = localStorage.getItem('token');

    if (token == null || token == '') {
      this.showLoginForm = true;
    } else {
      this.showLoginForm = false;
    }
    this.loaderData = { visible: false, text: "" };
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
      this.loaderData = { visible: true, text: "" };

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
            this.loaderData = { visible: false, text: "" };

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

  async googleSignin() {

    await this._authService.googleSignin();
    this.loaderData = { visible: true, text: "" };
    this._authService.googleAuthenticate().subscribe(
      result => {
        if (result != null) {
          const obj = JSON.parse(result);
          localStorage.setItem('token', obj.token);
          localStorage.setItem('name', obj.name);
          localStorage.setItem('profile', obj.profileTypeValue);
          localStorage.setItem('postalCode', obj.postalCode);
          this._bindingService.handleUserName();
          this._bindingService.handleSideBar();
          this.loaderData = { visible: false, text: "" };

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

  ResetFormData() {
    this.formData.delete('Email');
    this.formData.delete('Password');
  }

  logout() {
    localStorage.setItem('token', '');
    localStorage.setItem('name', '');
    localStorage.setItem('profile', '');
    localStorage.setItem('postalCode', '');
    localStorage.setItem('googleToken', '');
    this.showLoginForm = true;
    this._bindingService.handleUserName();
    this._bindingService.handleSideBar();
  }

}


