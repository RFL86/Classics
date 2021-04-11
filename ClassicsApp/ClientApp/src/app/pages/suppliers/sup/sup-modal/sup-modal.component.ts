import { Injectable, Component, OnInit, AfterViewInit, Input } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";


@Injectable({
  providedIn: "root"
})

@Component({
  selector: "sup-modal",
  templateUrl: "./sup-modal.html",
  styleUrls: ['./sup-modal.scss']
})

export class SupModalComponent implements OnInit, AfterViewInit {
  @Input()
  titleInput: string;
  descriptionInput: string;
  emailInput: string;
  cnpjInput: string;
  phoneInput: string;
  statusInput: string;

  public loaderData: any;
  public status: string;
  public title: string;
  public description: string;
  public phoneNumber: string;
  public cnpj: string;
  public email: string;

  constructor(public _activeModal: NgbActiveModal) {

  }

  ngAfterViewInit(): void {
  }

  ngOnInit(): void {
    this.status = this.statusInput;
    this.title = this.titleInput;
    this.description = this.descriptionInput;
    this.cnpj = this.cnpjInput;
    this.email = this.emailInput;
    this.phoneNumber = this.phoneInput;
  }

  close() {
    this._activeModal.close();
  }

}


