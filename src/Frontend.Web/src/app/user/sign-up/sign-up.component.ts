import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControl, FormControlName, FormGroup, Validators } from '@angular/forms';
import { FormBaseComponent } from 'app/bases/form-base-component';
import { User } from '../models/user';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CustomValidators } from '@narik/custom-validators';

@Component({
    selector: 'app-sign-up',
    templateUrl: './sign-up.component.html',
    styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent extends FormBaseComponent implements OnInit, AfterViewInit {

    @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

    errors: any[] = [];
    signUpForm: FormGroup;
    user: User;

    constructor(private formBuilder: FormBuilder,
        private service: UserService,
        private router: Router,
        private toastr: ToastrService) {
        super();
        this.ValidationFormMessagesConfiguration();
    }

    ngOnInit(): void {
        let password = new FormControl('', [Validators.required, CustomValidators.rangeLength([6, 15])]);
        let confirmPassword = new FormControl('', [Validators.required, CustomValidators.rangeLength([6, 15]), CustomValidators.equalTo(password)]);

        this.signUpForm = this.formBuilder.group({
          email: ['', [Validators.required, CustomValidators.email]],
          password: password,
          confirmPassword: confirmPassword
        });
    }
    ngAfterViewInit(): void {
      super.baseValidationFormConfiguration(this.formInputElements, this.signUpForm);
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
            },
            confirmPassword: {
                required: 'Password is required',
                rangeLength: 'The password must be between 6 and 15 characters',
                equalTo: 'The passwords are different'
            }
        };
        super.baseValidationMessagesConfiguration(this.validationMessages);
    }

    private validateFormIsValid(){
      return this.signUpForm?.dirty && this.signUpForm?.valid;
    }

    private processRequestSuccessfully(response: any){
      this.signUpForm.reset();
      this.errors = [];

      this.service.localStorage.saveUserData(response);

      let toast = this.toastr.success("User created successfully", "Welcome!")

      if(toast){
        toast.onHidden.subscribe(() => {
          this.router.navigate(['/home']);
        });
      }
    }

    private processFailRequest(fail: any){
      this.errors = fail.error.errors;
      this.toastr.error("An error has occurred", "Error performing this operation")
    }

    signUp(){
      if(this.validateFormIsValid()){
        this.user = Object.assign({}, this.user, this.signUpForm.value);

        this.service.signUp(this.user)
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
