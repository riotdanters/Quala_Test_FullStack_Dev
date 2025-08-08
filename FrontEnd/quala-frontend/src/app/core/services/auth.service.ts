import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, BehaviorSubject, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.apiBaseUrl}/auth`;
  private readonly TOKEN_NAME = 'quala_auth_token';
  private userLoggedIn = new BehaviorSubject<boolean>(this.hasToken());

  constructor(private http: HttpClient, private router: Router) { }

  login(credentials: any): Observable<any> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/login`, credentials).pipe(
      tap(response => {
        this.setToken(response.token);
        this.userLoggedIn.next(true);
      }),
      catchError(err => {
        this.clearToken();
        this.userLoggedIn.next(false);
        throw err;
      })
    );
  }

  logout(): void {
    this.clearToken();
    this.userLoggedIn.next(false);
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_NAME);
  }

  isLoggedIn(): boolean {
    return this.hasToken();
  }

  private setToken(token: string): void {
    localStorage.setItem(this.TOKEN_NAME, token);
  }

  private clearToken(): void {
    localStorage.removeItem(this.TOKEN_NAME);
  }

  private hasToken(): boolean {
    return !!localStorage.getItem(this.TOKEN_NAME);
  }
}