import { Injectable, Component, OnInit, AfterViewInit, Input } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { UserModalService } from './user-modal.service';
import { SelectItem } from "./user-modal.model"


@Injectable({
  providedIn: "root"
})

@Component({
  selector: "user-modal",
  templateUrl: "./user-modal.html",
  styleUrls: ['./user-modal.scss']
})

export class UserModalComponent implements OnInit, AfterViewInit {
  @Input()
  statusInput: string;
  profileTypeInput: string;
  alertIdInput: string;
  alertNameInput: string;

  public status: string;
  public profileType: string;
  public userId: string;
  public profilesList: SelectItem[];
  public statusList: SelectItem[];
  public formData: FormData;
  public successMessage: string;
  public errorMessage: string;
  public showSuccessMessage: boolean;
  public showErrorMessage: boolean;
  public selectedStatus: SelectItem;
  public selectedProfile: SelectItem;

  constructor(public _activeModal: NgbActiveModal, public _service: UserModalService) {

  }

  ngAfterViewInit(): void {
  }

  ngOnInit(): void {
    this.status = this.statusInput;
    this.profileType = this.profileTypeInput;
    this.userId = this.alertIdInput;
    this.formData = new FormData();
    this.showErrorMessage = false;
    this.showSuccessMessage = false;
    this.fillLists();

    this.selectedProfile = this.profilesList.filter(x => x.value == this.profileTypeInput)[0];
    this.selectedStatus = this.statusList.filter(x => x.value == this.statusInput)[0];
  }

  close() {
    this._activeModal.close();
  }

  fillLists() {
    this.profilesList = [{ text: 'Cliente', value: '0' }, { text: 'Operador', value: '1' }, { text: 'Gerente', value: '2' }]
    this.statusList = [{ text: 'Ativo', value: '1' }, { text: 'Inativo', value: '0' }]
  }

  onSelectStatus(item) {
    const selectedItem = item as SelectItem;
    this.status = selectedItem.value;
  }

  onSelectProfile(item) {
    const selectedItem = item as SelectItem;
    this.profileType = selectedItem.value;
  }

  editAlert() {

    this.resetFormData();
    this.formData.append('Status', this.status);
    this.formData.append('ProfileType', this.profileType);
    this.formData.append('UserId', this.userId);

    this._service.editUser(this.formData).subscribe(
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
    this.formData.delete('ProfileType');
    this.formData.delete('UserId');
    this.formData.delete('Status');
  }

}


