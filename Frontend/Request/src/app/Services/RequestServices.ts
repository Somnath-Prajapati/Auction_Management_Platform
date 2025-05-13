import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RequestDetail } from '../Models/RequestDetail';  // Use the new model
import { Request } from '../Models/Request';
import { CreateRequest } from '../Models/CreateRequest';

@Injectable({
  providedIn: 'root'
})
export class RequestServices {
  private apiUrl = 'https://localhost:56334/api/Request';  // backend URL

  constructor(private http: HttpClient) {}

  // Get all requests
  getAllRequests(): Observable<Request[]> {
    return this.http.get<Request[]>(this.apiUrl);
  }

  // Get single request by ID
  getRequestById(id: number): Observable<RequestDetail> {
    return this.http.get<RequestDetail>(`${this.apiUrl}/${id}`);
  }
/** Create a new request */
  createRequest(request: RequestDetail): Observable<RequestDetail> {
  return this.http.post<RequestDetail>(this.apiUrl, request);
}
  // Update an existing request
  updateRequest(id: number, request: RequestDetail): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, request);
  }

  // Delete a request
  deleteRequest(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
