import { Directive } from '@angular/core';
import { NG_VALIDATORS, Validator, AbstractControl, ValidationErrors } from '@angular/forms';

@Directive({
  selector: '[futureDate]',
  standalone: true, 
  providers: [{
    provide: NG_VALIDATORS,
    useExisting: FutureDateValidatorDirective,
    multi: true
  }]
})
export class FutureDateValidatorDirective implements Validator {
  validate(control: AbstractControl): ValidationErrors | null {
    console.log("new")
    if (!control.value) {
      return null; // If empty, don't validate here
    }
    
    const selectedDate = new Date(control.value);
    const today = new Date();
    today.setHours(0, 0, 0, 0);

    // extra safety: if selectedDate is invalid
    if (isNaN(selectedDate.getTime())) {
      return { dateInvalid: true };
    }

    return selectedDate > today ? null : { dateInvalid: true };
  }
}
