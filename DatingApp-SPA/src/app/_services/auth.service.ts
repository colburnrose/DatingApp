import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import {JwtHelperService} from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

// baseUrl: 'http://localhost:5000/api/auth/';
jwtHelper = new JwtHelperService;
decodedToken: any;

constructor(private http: HttpClient) { }

  login(model: any) {
    // console.log(this.baseUrl);
    return this.http.post('http://localhost:5000/api/auth/login',  model).pipe(
      map((response: any) => {
        // response is the token object
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          console.log(this.decodedToken);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post('http://localhost:5000/api/auth/register', model);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    // if token has not expired it will return true.
    return !this.jwtHelper.isTokenExpired(token);
  }
}
