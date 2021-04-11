import { Injectable, Component, OnInit, AfterViewInit, Input } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

@Injectable({
  providedIn: "root"
})

@Component({
  selector: "store-modal",
  templateUrl: "./store-modal.html",
  styleUrls: ['./store-modal.scss']
})

export class StoreModalComponent implements OnInit, AfterViewInit {
  @Input()
  titleInput: string;
  descriptionInput: string;
  priceInput: string;
  photoUrlInput: string;
  emailInput: string;
  addressInput: string;
  phoneNumberInput: string;
  carModelInput: string;
  productIdInput: string

  public title: string;
  public description: string;
  public phoneNumber: string;
  public price: string;
  public email: string;
  public address: string;
  public photoUrl: string;
  public carModel: string;
  public productId: string;

  constructor(public _activeModal: NgbActiveModal) {

  }

  ngAfterViewInit(): void {
  }

  ngOnInit(): void {
    this.title = this.titleInput;
    this.description = this.descriptionInput;
    this.price = this.priceInput;
    this.photoUrl = this.photoUrlInput;
    this.address = this.addressInput;
    this.phoneNumber = this.phoneNumberInput;
    this.email = this.emailInput;
    this.carModel = this.carModelInput;
  
  }

  close() {
    this._activeModal.close();
  }

}


