import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Country, Role, Status, User, UserView } from '../model/user';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://localhost:7118/api/User';
  private countryUrl = 'https://localhost:7118/api/Country';
  // https://localhost:7118/api/User/update/1

  constructor(private http:HttpClient) { }

  addUser(formData: FormData): Observable<any> {
    return this.http.post(`${this.apiUrl}/Add`, formData);
  }
  getRoles(): Observable<Role[]> {
    return this.http.get<Role[]>(`${this.apiUrl}/roles`);
  }

  getStatuses(): Observable<Status[]> {
    return this.http.get<Status[]>(`${this.apiUrl}/statuses`);
  }
  getCountry(): Observable<Country[]>{
    return this.http.get<Country[]>(this.countryUrl)
  }
  getAllUser(): Observable<UserView[]>{
    return this.http.get<UserView[]>(this.apiUrl);
  }
  updateUser(userId: number, formData: FormData): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/update/${userId}`,formData);
  }
}