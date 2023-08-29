import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControl, FormControlName, FormGroup, Validators } from '@angular/forms';
import { FormBaseComponent } from 'app/bases/form-base-component';
import { User } from '../models/user';
import { UserService } from '../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CustomValidators } from '@narik/custom-validators';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent extends FormBaseComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  errors: any[] = [];
  signInForm: FormGroup;
  user: User;

  returnUrl: string;

  constructor(private formBuilder: FormBuilder,
    private service: UserService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private toastr: ToastrService) {

    super();

    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'];

    this.ValidationFormMessagesConfiguration()
  }

  ngOnInit(): void {
    let password = new FormControl('', [Validators.required, CustomValidators.rangeLength([6, 15])]);

    this.signInForm = this.formBuilder.group({
      email: ['', [Validators.required, CustomValidators.email]],
      password: password,
    });
  }

  ngAfterViewInit(): void {
    super.baseValidationFormConfiguration(this.formInputElements, this.signInForm);
  }

  private ValidationFormMessagesConfiguration(): void {
    this.validationMessages = {
      email: {
        required: 'E-mail is required',
        email: 'E-mail is inválid'
      },
      password: {
        required: 'Password is required',
        rangeLength: 'The password must be between 6 and 15 characters'
      }
    };
    super.baseValidationMessagesConfiguration(this.validationMessages);
  }

  private validateFormIsValid() {
    return this.signInForm?.dirty && this.signInForm?.valid;
  }

  private processRequestSuccessfully(response: any) {
    this.signInForm.reset();
    this.errors = [];

    this.service.localStorage.saveUserData(response);

    this.returnUrl
      ? this.router.navigate([this.returnUrl])
      : this.router.navigate(['/home']);
  }

  private processFailRequest(fail: any){
    this.errors = fail.error.errors;
    this.toastr.error("An error has occurred", "Error performing this operation")
  }

  signIn(){
    if(this.validateFormIsValid()){
      this.user = Object.assign({}, this.user, this.signInForm.value);

      this.service.signIn(this.user)
        .subscribe({
          next: (v) => {
            this.processRequestSuccessfully(v)
          },
          error: (e) => {
            this.processFailRequest(e)
          },
          complete: () => console.info('complete')
        });

      this.refreshFlagChangesNotSavedToFalse();
    }
  }
}
