import { Component, input, output } from '@angular/core';
import { UserInterface } from '../../models/user.model';


@Component({
  selector: 'app-user',
  imports: [],
  templateUrl: './user.html',
  styleUrl: './user.scss'
})

export class User {

  // id = input<number>();
  // name = input<string>();
  // surname = input<string>();
  // age = input<number>();

  // incrementUserAgeEvent = output<UserInterface>();


  // incrementUserAge() {
  //   this.age.set(this.age() ?? 0 + 1);
    
  //   this.incrementUserAgeEvent.emit({
  //     id: this.id() ?? 0,
  //     name: this.name() ?? '',
  //     surname: this.surname() ?? '',
  //     age: this.age() ?? 0
  //   });
  // }
}
