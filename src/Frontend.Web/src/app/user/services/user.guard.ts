import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { BaseGuard } from "app/bases/base.guard";
import { SignUpComponent } from "../sign-up/sign-up.component";

@Injectable()
export class UserGuard extends BaseGuard {

  constructor(protected override router: Router){
    super(router);
  }

  canDeactivate(component: SignUpComponent) {
    if(component.changesNotSaved) {
      return window.confirm('Are you sure?');
    }

    return true;
  }

  canActivate() {
    if(super.validateIfUserIsLoggedIn()){
      this.router.navigate(['/users/sign-in']);
    }

    return true;
  }
}
