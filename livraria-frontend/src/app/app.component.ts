import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  standalone: false,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'livraria-frontend';
  sideNavAberto = false;

  toggleSidenav() {
    this.sideNavAberto = !this.sideNavAberto;
  }
}
