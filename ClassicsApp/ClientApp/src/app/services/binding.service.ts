import { Injectable, EventEmitter } from "@angular/core";

import { HttpClient } from "@angular/common/http";



@Injectable({
  providedIn: "root"
})

export class BindingService {   

  updateSideBar = new EventEmitter<string>();
  updateUserName = new EventEmitter<string>();
  constructor(private client: HttpClient) {
 
  }

  handleSideBar() {
    this.updateSideBar.emit(localStorage.getItem('profile'));
  }

  handleUserName() {
    this.updateUserName.emit(localStorage.getItem('name'));
  }




}
