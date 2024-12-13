import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { HttpClient } from '@angular/common/http';
import { NgIf, TitleCasePipe } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown'; 
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, BsDropdownModule, RouterLink, RouterLinkActive, TitleCasePipe],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  model: any = {}; 
  accountService = inject(AccountService); 
  http= inject(HttpClient); 
  loggedIn=false; 
  private router = inject(Router); 
  

  login(){

    this.accountService.login(this.model).subscribe({
        next: _ =>{
          this.router.navigateByUrl('/members'); 
        }, 
        error: error => this.accountService.toastr.error(error.error)
      });

  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/')
  }

}
