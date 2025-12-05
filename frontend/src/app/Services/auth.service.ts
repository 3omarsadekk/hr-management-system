import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  //private apiUrl = 'http://localhost:5093/api/Account'; // Backend API URL
  private apiUrl = 'https://localhost:7005/api/Account'; // Backend API URL

  // BehaviorSubject for reactive username
  private userNameSubject = new BehaviorSubject<string | null>(
    localStorage.getItem('userName')
  );
  public userName$ = this.userNameSubject.asObservable();

  constructor(private http: HttpClient) { }

  login(data: any): Observable<any> {
    console.log("LOGIN URL:", `${this.apiUrl}/login`);
    console.log("LOGIN DATA:", data);
    return this.http.post(`${this.apiUrl}/login`, data);
  }

  register(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/register-employee`, data);
  }

  changePassword(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/change-password`, data);
  }

  saveToken(token: string) {
    localStorage.setItem('jwtToken', token);
  }

  saveUserId(userId: number) {
    localStorage.setItem('userId', userId.toString());
  }

  saveUserName(name: string) {
    localStorage.setItem('userName', name);
    // Emit the new username to all subscribers
    this.userNameSubject.next(name);
  }

  getToken(): string | null {
    return localStorage.getItem('jwtToken');
  }

  getUserName(): string | null {
    return localStorage.getItem('userName');
  }

  logout() {
    localStorage.removeItem('jwtToken');
    localStorage.removeItem('userId');
    localStorage.removeItem('userName');
    // Clear the username in the subject
    this.userNameSubject.next(null);
  }
}
