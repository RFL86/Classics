import { Injectable, Component, OnInit, AfterViewInit, Input } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { PhotoModalService } from './photo-modal.service';


@Injectable({
  providedIn: "root"
})

@Component({
  selector: "photo-modal",
  templateUrl: "./photo-modal.html",
  styleUrls: ['./photo-modal.scss']
})

export class PhotoModalComponent implements OnInit, AfterViewInit {
  @Input()
  productIdInput: string;
  photoUrlInput: string;

  public productId: string;
  public photoUrl: string;
  public formData: FormData;
  public successMessage: string;
  public errorMessage: string;
  public showSuccessMessage: boolean;
  public showErrorMessage: boolean;
  public loaderData: any;
  public imgURL: any;
  public warning: any;
  public fileInput: string;
  public showImage: boolean;

  constructor(public _activeModal: NgbActiveModal, public _service: PhotoModalService) {

  }

  ngAfterViewInit(): void {
  }

  ngOnInit(): void {
    this.warning = { type: false, number: false, value: false, issued: false, file: false }
    this.productId = this.productIdInput;
    this.photoUrl = this.photoUrlInput;
    this.formData = new FormData();
    this.formData.append('ProductId', this.productId);
    this.showErrorMessage = false;
    this.showSuccessMessage = false;
    this.showImage = false;   
  }

  close() {
    this._activeModal.close();
  }

  getFileDetails(event) {
    const document = event.target.files[0];
    this.formData.delete('PhotoContent');
    this.formData.append('PhotoContent', document);

    this.preview(event.target.files, true);

    if (event.target.files[0] !== undefined) {
      this.preview(event.target.files, false);
    }
  }

  preview(files, hideAll: boolean) {
    if (!hideAll) {
      if (files[0].type === "image/png" || files[0].type === "image/jpeg") {
        const reader = new FileReader();
        reader.readAsDataURL(files[0]);
        reader.onload = (_event) => {
          this.imgURL = reader.result;
          this.showImage = true;
        }
      }
    }
  }

  public uploadPhoto() {
    this.errorMessage = "Erro ao enviar foto";
    this.successMessage = "Foto enviada com sucesso";
    this._service.uploadPhoto(this.formData).subscribe(
      data => {
        if (data) {
          this.showSuccessMessage = true;
          setTimeout(() => {
            this.showSuccessMessage = false;
            this.close()
          },
            3000);
        }
      },
      error => {
        console.log(error);
        this.showErrorMessage = true;
        setTimeout(() => {
          this.showErrorMessage = false;
        },
          3000);
      });
  }

  public removePhoto() {   
    this.errorMessage = "Erro ao remover foto";
    this.successMessage = "Foto removida com sucesso";
    this._service.removePhoto(this.formData).subscribe(
      data => {
        if (data) {
          this.photoUrl = 'assets/images/not-found.png';
          this.showSuccessMessage = true;
          setTimeout(() => {
            this.showSuccessMessage = false;
            this.close()
          },
            1500);
        }
      },
      error => {
        console.log(error);
        this.showErrorMessage = true;
        setTimeout(() => {
          this.showErrorMessage = false;
        },
          3000);
      });
  }

}
