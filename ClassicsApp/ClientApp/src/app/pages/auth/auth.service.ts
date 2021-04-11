import { Injectable, Inject } from "@angular/core";

import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import firebase from 'firebase/app';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFirestore, AngularFirestoreDocument } from '@angular/fire/firestore';
import { switchMap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { User } from './login/login.model'



@Injectable({
  providedIn: 'root'
})
export class AuthService {

  user$: Observable<User>;
  private _baseUrl: string;
  private _jwtToken: string;

  formData: FormData;

  constructor(private client: HttpClient, private afAuth: AngularFireAuth, private afs: AngularFirestore, private router: Router, @Inject('BASE_URL') baseUrl: string)
  {
    this._baseUrl = baseUrl;
    this.formData = new FormData();
    this._jwtToken = "";

    this.user$ = this.afAuth.authState.pipe(
      switchMap(user => {
        // Logged in
        if (user) {
          return this.afs.doc<User>(`users/${user.uid}`).valueChanges();
        } else {
          // Logged out
          return of(null);
        }
      })
    )
  }

  async googleSignin() {
    const provider = new firebase.auth.GoogleAuthProvider();
    provider.addScope('https://www.googleapis.com/auth/contacts.readonly');

    firebase.auth().signInWithPopup(provider).then(function (result) {

      const user = result.user;
      localStorage.setItem('email', user.email);
      localStorage.setItem('name', user.displayName);
      localStorage.setItem('photoURL', user.photoURL);

      user.getIdToken().then((idToken) => {
        localStorage.setItem('token', idToken);
      });

    }).catch(function (error) {
      console.log(error);
    });

  }

  //public updateUserData(user) {
  //  // Sets user data to firestore on login
  //  const userRef: AngularFirestoreDocument<User> = this.afs.doc(`users/${user.uid}`);

  //  const data = {
  //    uid: user.uid,
  //    email: user.email,
  //    displayName: user.displayName,
  //    photoURL: user.photoURL
  //  }

  //  return userRef.set(data, { merge: true })

  //}

  async signOut() {
    await firebase.auth().signOut();
    this.router.navigate(['/']);
  }

  public googleAuthenticate(): Observable<any> {

    const response = this.client.post<any>(this._baseUrl + 'api/User/verify', this.formData, { responseType: 'text' as 'json' });
    this.formData.delete('token');

    return response;
  }

  public GetToken(): string {
    var testde = firebase.auth().currentUser;

    const teste = localStorage.getItem('token');
    return teste;
  }


  async acessor(url, body) {

    let httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*',
        'x-access-token': localStorage.getItem('token')
      })
    };
    /*     const res = await this.httpClient.post(url, body, httpOptions).toPromise();
        console.log('resposta servidor', res);
        
        return res; */
    let resposta: any;
    await this.client.post(url, body, httpOptions).toPromise().then((resp) => {
      resposta = resp;
    }).catch((err) => {
      console.log('erro', err.status);
      if (err.status == 401) {
        localStorage.clear();
        this.router.navigate(['']);
        resposta = {
          auth: false
        }
      }
    });
    return resposta;
  }


  public authenticate(formData: FormData): Observable<any> {

    const response = this.client.post<any>(this._baseUrl + 'api/User/Authenticate', formData, { responseType: 'text' as 'json' });
    return response;
  }  

}
