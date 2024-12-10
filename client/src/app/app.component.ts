import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./nav/nav.component";
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgFor, NavComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent implements OnInit{

  http = inject(HttpClient);  // Dependency Injection
  title = 'DatingApp';
  users: any; 
  private accountService=inject(AccountService); 

  ngOnInit(): void {
    this.getUsers(); 
  }



  setCurrentUser(){
    const userString = localStorage.getItem('user'); 
    if(!userString) return; 
    const user= JSON.parse(userString); 
    this.accountService.currentUser.set(user); 
  }

  getUsers(){
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: reponse => this.users = reponse, 
      error: error => console.log(error), 
      complete: () => console.log("Good Request !")
    })
  }
}

