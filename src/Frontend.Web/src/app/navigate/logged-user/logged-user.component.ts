import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorageUtils } from 'app/utils/localstorage';

@Component({
  selector: 'app-logged-user',
  templateUrl: './logged-user.component.html',
  styleUrls: ['./logged-user.component.css']
})
export class LoggedUserComponent {
  token: string = "";
  user: any;
  email: string = "";
  localStorageUtils = new LocalStorageUtils();

  constructor(private router: Router) {  }

  loggedUser(): boolean {
    this.token = this.localStorageUtils.getUserToken();
    this.user = this.localStorageUtils.getUser();

    if (this.user)
      this.email = this.user.email;

    return this.token !== null;
  }

  logout() {
    this.localStorageUtils.cleanUserData();
    this.router.navigate(['/users/sign-in']);
  }
}
