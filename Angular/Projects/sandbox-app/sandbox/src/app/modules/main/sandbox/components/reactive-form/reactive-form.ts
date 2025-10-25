import { Component } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-reactive-form',
  imports: [ReactiveFormsModule],
  templateUrl: './reactive-form.html',
  styleUrl: './reactive-form.scss'
})

export class ReactiveForm {
  profileForm: FormGroup = new FormGroup({
    name: new FormControl('initial value'),
    surname: new FormControl(''),
    email: new FormControl(''),
  });

  handleProfileFormSubmit(){
    alert(this.profileForm.value.name + '|' 
    + this.profileForm.value.surname);
  }
}
