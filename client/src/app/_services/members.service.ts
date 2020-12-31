import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

const httpOtions = {
  headers : new HttpHeaders({
    Authorization: 'Bearer' + JSON.parse(localStorage.getItem('user'))?.token
  })
}

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getMembers(){
    return this.http.get(this.baseUrl + 'users', httpOptions);
  }

  getMember(username){

    return this.http.get<Member>(this.baseUrl + 'users/' + username, httpOptions)
  }
}
