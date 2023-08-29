import { Router } from "@angular/router";
import { LocalStorageUtils } from "app/utils/localstorage";

export abstract class BaseGuard {
  private localStorageUtils = new LocalStorageUtils();

  constructor(protected router: Router){}

  protected validateIfUserIsLoggedIn(){
    return this.localStorageUtils.getUserToken();
  }

}
