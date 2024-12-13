import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  registerMode=false; 
  http=inject(HttpClient); 
  users:any; 

  ngOnInit(): void {
    this.users = this.getUsers(); 
  }

  getUsers(){
    this.http.get('http://localhost:5000/api/users').subscribe({
      next: reponse => this.users = reponse, 
      error: error => console.log(error), 
      complete: () => console.log("Good Request !")
    })
  }

  registerToggle(){

    this.registerMode=!this.registerMode
  }
}
