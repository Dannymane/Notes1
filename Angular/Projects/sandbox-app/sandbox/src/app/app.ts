import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { User } from './modules/main/sandbox/components/user/user';

type Visivility = 'visible' | 'hidden';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet, User],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {

  protected readonly title = signal('sandbox');
  city = "Vinnytsia";
  isServerRunning = true;
  divVisibility: Visivility = 'visible';

  changeCity() {
    this.city = "Kyiv";
    this.title.set("Kyiv signal");
    this.isServerRunning = !this.isServerRunning;
    this.divVisibility = this.divVisibility === 'visible' ? 'hidden' : 'visible';
  }

  protected users = [{name: 'John', id: 0}, {name: 'Alice', id: 1}, {name: 'Bob', id: 2}];
}

