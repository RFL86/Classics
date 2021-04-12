import { Injectable, Inject } from "@angular/core";

import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import firebase from 'firebase/app';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFirestore } from '@angular/fire/firestore';
import { Observable } from 'rxjs';




@Injectable({
  providedIn: 'root'
})
export class AuthService {

  //user$: Observable<User>;
  private _baseUrl: string;
  public _provider;

  constructor(private client: HttpClient, private afAuth: AngularFireAuth, private afs: AngularFirestore, private router: Router, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;  
  }

  async googleSignin() {

    const provider = new firebase.auth.GoogleAuthProvider();
    provider.addScope('https://www.googleapis.com/auth/contacts.readonly');

    await firebase.auth().signInWithPopup(provider).then(function (result) {
      const user = result.user;
      user.getIdToken().then((idToken) => {
        localStorage.setItem('googleToken', idToken);
      });

    }).catch(function (error) {
      console.log(error);
    });

  }

  async signOut() {
    await firebase.auth().signOut();
    this.router.navigate(['/']);
  }

  public googleAuthenticate(): Observable<any> {

    const formData = new FormData();
    formData.append('googleToken', localStorage.getItem('googleToken'));
    const response = this.client.post<any>(this._baseUrl + 'api/User/GoogleAuthenticate', formData, { responseType: 'text' as 'json' });

    return response;
  }

  public authenticate(formData: FormData): Observable<any> {

    const response = this.client.post<any>(this._baseUrl + 'api/User/Authenticate', formData, { responseType: 'text' as 'json' });
    return response;
  }

}
