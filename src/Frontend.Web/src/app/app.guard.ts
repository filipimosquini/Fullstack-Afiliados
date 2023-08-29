import { Injectable } from "@angular/core";
import { Router } from "@angular/router";

import { BaseGuard } from "./bases/base.guard";

@Injectable()
export class AppGuard extends BaseGuard {

  constructor(protected override router: Router) { super(router); }

  canLoad() {
    return super.validateIfUserIsLoggedIn();
  }
}
