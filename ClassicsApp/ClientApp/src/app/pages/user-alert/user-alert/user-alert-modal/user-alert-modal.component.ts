import { Injectable, Component, AfterViewInit, Input } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";


@Injectable({
  providedIn: "root"
})

@Component({
  selector: "user-alert-modal",
  templateUrl: "./user-alert-modal.html",
  styleUrls: ['./user-alert-modal.scss']
})

export class UserAlertModalComponent implements AfterViewInit {
  @Input()
  public subject: string;
  public message: string;

  constructor(public _activeModal: NgbActiveModal) {

  }

  ngAfterViewInit(): void {
  }

  close() {
    this._activeModal.close();
  }

}


