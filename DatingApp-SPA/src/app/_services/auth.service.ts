import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

// baseUrl: 'http://localhost:5000/api/auth/';

constructor(private http: HttpClient) { }

login(model: any) {
  // console.log(this.baseUrl);
  return this.http.post('http://localhost:5000/api/auth/login',  model).pipe(
    map((response: any) => {
      // response is the token object
      const user = response;
      if (user) {
        localStorage.setItem('token', user.token);
      }
    })
  );
}

}
