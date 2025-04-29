import { Observable } from "rxjs";
import { Country, Role, Status } from "../modals/add-user";
import { HttpClient } from "@angular/common/http";

export class UserService {
    private apiUrl = 'https://localhost:7118/api/User';
    private countryUrl = 'https://localhost:7118/api/Country';
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
  }