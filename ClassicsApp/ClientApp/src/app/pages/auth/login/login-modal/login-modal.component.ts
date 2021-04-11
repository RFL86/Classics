import { Injectable, Component, OnInit, AfterViewInit } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { LoginModalService } from './login-modal.service';
import { Address } from './login-modal.model';


@Injectable({
  providedIn: "root"
})

@Component({
  selector: "login-modal",
  templateUrl: "./login-modal.html",
  styleUrls: ['./login-modal.scss']
})

export class LoginModalComponent implements OnInit, AfterViewInit {

  public formData: FormData;
  public successMessage: string;
  public errorMessage: string;
  public showSuccessMessage: boolean;
  public showErrorMessage: boolean;
  public loaderData: any;
  public email: string;
  public name: string;
  public phone: string;
  public postalCode: string;
  public postalCodeInput: string;
  public password1: string;
  public password2: string;
  public address: Address;
  public isAddressValid: boolean;


  constructor(public _activeModal: NgbActiveModal, public _service: LoginModalService) {

  }

  ngAfterViewInit(): void {
  }

  ngOnInit(): void {

    this.email = '';
    this.name = '';
    this.phone = '';
    this.postalCode = '';
    this.password1 = '';
    this.password2 = '';
    this.errorMessage = '';
    this.successMessage = '';
    this.address = {} as Address;
    this.address.City = '';
    this.address.StateCode = '';
    this.address.PostalCode = '';
    this.postalCodeInput = '';

    this.formData = new FormData();
    this.showErrorMessage = false;
    this.showSuccessMessage = false;
    this.loaderData = { visible: false, text: "" };
  }

  close() {
    this._activeModal.close();
  }

 checkBeforeSave() {
    if (!this.validateEmail()) {
      this.errorMessage = "E-mail inválido.";
      this.showError();
    } else if (this.name.length < 5) {
      this.errorMessage = "Campo nome deve ter pelo menoa 5 letras.";
      this.showError();
    } else if (this.phone.length < 11) {
      this.errorMessage = "Formato de telefone inválido.";
      this.showError();
    } else if (!this.validadePostalCode()) {
      this.errorMessage = "Cep não encontrado.";
      this.showError();
    } else if (!this.validatePassword()) {
      this.showError();
    } else {
      this.createUser();
    }
  }

  validateEmail() {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    const result = re.test(String(this.email).toLowerCase());
    return result;
  }

  validadePostalCode() {
    if (this.address.PostalCode.length < 8 || this.address.City == '' || this.address.StateCode == '' || this.address.City == null || this.address.StateCode == null) {
      return false;
    }
    return true;
  }

  validatePassword() {
    if (this.password1 != this.password2) {
      this.errorMessage = "A confirmação de senha deve ser igual à senha";
      return false;
    } else if (this.password1.length < 6) {
      this.errorMessage = "A senha deve ter no mínimo 6 caracteres.";
      return false;
    } else {
      return true;
    }
  }

  showError() {
    this.showErrorMessage = true;
    setTimeout(() => {
      this.errorMessage = '';
      this.showErrorMessage = false;
    },
      2000);
  }

  async getAddress(event: any) {
    this.postalCodeInput += event.target.value;  

    if (this.postalCodeInput.length == 8) {
      this.address.City = '';
      this.address.StateCode = '';
      this.address.PostalCode = '';
      this.loaderData = { visible: true, text: "" };
      await this._service.getAddress(this.postalCode).then(
        (data) => {
          if (data != null) {
            this.address.City = data.city;
            this.address.StateCode = data.stateCode;
            this.address.PostalCode = data.postalCode;
            console.log(data);
          }
          this.loaderData = { visible: false, text: "" };
        }
      );
    } else {
      this.address.City = '';
      this.address.StateCode = '';
      this.address.PostalCode = '';
    }
    this.postalCodeInput = '';
  }

  createUser() {
    this.resetFormData();
    this.formData.append('Name', this.name);
    this.formData.append('Email', this.email);
    this.formData.append('PostalCode', this.postalCode);
    this.formData.append('StateCode', this.address.StateCode);
    this.formData.append('City', this.address.City);
    this.formData.append('PhoneNumber', this.phone);
    this.formData.append('Password', this.password1);
    this.loaderData = { visible: true, text: "" };
    this._service.createUser(this.formData).subscribe(
      result => {
        if (result == null || result == '') {
          this.showSuccessMessage = true;
          this.successMessage = 'Usuário criado com sucesso.';
          this.loaderData = { visible: false, text: "" };
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
    this.loaderData = { visible: false, text: "" };
  }

  resetFormData() {
    this.formData.delete('Name');
    this.formData.delete('Email');
    this.formData.delete('PostalCode');
    this.formData.delete('PhoneNumber');
    this.formData.delete('Password');

    this.formData.append('Name', this.name);
    this.formData.append('Email', this.email);
    this.formData.append('PostalCode', this.postalCode);
    this.formData.append('PhoneNumber', this.phone);
    this.formData.append('Password', this.password1);
  }

}


