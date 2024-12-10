import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { HttpClient } from '@angular/common/http';
import { NgIf } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown'; 

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, BsDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  model: any = {}; 
  accountService = inject(AccountService); 
  http= inject(HttpClient); 
  loggedIn=false; 

  login(){

    this.http.post("https://localhost:5001/api/account/login", this.model).subscribe({
      next: response =>{
        console.log(response); 
      }, 
      error: error => console.log(error)
    });

  }

  logout(){
    this.accountService.logout();
  }

}
