import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BaseService } from "app/bases/base.service";
import { Observable, catchError, map } from "rxjs";
import { User } from "../models/user";

@Injectable()
export class UserService extends BaseService {

  constructor(private http: HttpClient){
    super();
  }

  signUp(user: User) : Observable<User> {
    return this.http
      .post(this.urlApi+'users', user)
      .pipe(
        map(this.getResponseData),
        catchError(this.tratarErrosDoServidor))
  }

  signIn(user: User) : Observable<User>{
    return this.http
      .post(this.urlApi+'sign-in', user)
      .pipe(
        map(this.getResponseData),
        catchError(this.tratarErrosDoServidor))
  }
}
