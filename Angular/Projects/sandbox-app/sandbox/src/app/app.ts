import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { User } from './modules/main/sandbox/components/user/user';

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

  changeCity() {
    this.city = "Kyiv";
    this.title.set("Kyiv signal");
    this.isServerRunning = !this.isServerRunning;
  }
}

