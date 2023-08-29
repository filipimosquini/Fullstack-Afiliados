import { Router } from "@angular/router";

export abstract class BaseGuard {
  localStorageUtils: any;

  constructor(protected router: Router){}

  protected validateIfUserIsLoggedIn(){
    return this.localStorageUtils.getUserToken();
  }

}
