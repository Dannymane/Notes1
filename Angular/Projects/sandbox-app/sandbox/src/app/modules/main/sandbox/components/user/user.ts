import { Component, input, output, signal } from '@angular/core';
import { UserInterface } from '../../models/user.model';


@Component({
  selector: 'app-user',
  imports: [],
  templateUrl: './user.html',
  styleUrl: './user.scss'
})

export class User {

  id = input<number>();         //id is a read-only signal, it works with computed/effect,  
  name = input<string>();       //but you can't assign by id(5);
  surname = input<string>();
  age = input<number | null>();


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
