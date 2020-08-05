import { User } from './../models/user';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
    currentUser: User;

    constructor() {
        this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    }
}
