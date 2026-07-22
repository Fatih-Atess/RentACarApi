import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Car {
  arac_ID: number;
  marka_Model: string;
  plaka: string;
  gunluk_Fiyat: number;
  durum: string
}

@Injectable({
  providedIn: 'root',
})
export class ApiService {

  private baseUrl = 'https://localhost:7101/api';

  constructor(private http: HttpClient) { }

  getAllCars(): Observable<Car[]> {
    return this.http.get<Car[]>('${this.baseUrl}/cars');
  }
}
