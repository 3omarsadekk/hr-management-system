import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [CommonModule, RouterOutlet],
  template: '<router-outlet></router-outlet>',
  styleUrl: './app.css',
})
// @Component({
//   selector: 'app-root',
//   standalone: true,
//   imports: [CommonModule, RouterOutlet],
//   template: '<router-outlet></router-outlet>',
//   styleUrl: './app.css',
// })
export class App {
  protected readonly title = signal('HRManagementSystem');
}
