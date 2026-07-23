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

export interface Rental {
  Kiralama_ID: number;
  Arac_ID: number;
  Musteri_Adi: string;
  Baslangic_Tarihi: Date;
  Bitis_Tarihi: Date;
  Toplam_Tutar: number;
}

@Injectable({
  providedIn: 'root',
})
export class ApiService {

  private baseUrl = 'http://localhost:5000/api';

  constructor(private http: HttpClient) { }

  getAllCars(): Observable<Car[]> {
    return this.http.get<Car[]>(`${this.baseUrl}/Cars`);
  }
  getCarByPlate(plate: string): Observable<Car> {
    return this.http.get<Car>(`${this.baseUrl}/Cars/${plate}`);
  }
  getCarsByStatus(status: string): Observable<Car[]> {
    return this.http.get<Car[]>(`${this.baseUrl}/Cars/?status=${status}`);
  }
  addCar(newCar: Car): Observable<Car> {
    return this.http.post<Car>(`${this.baseUrl}/Cars`, newCar);
  }
  rentCar(rentalData: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Rentals`, rentalData);
  }
  getAllRentals(): Observable<Rental[]>{
    return this.http.get<Rental[]>(`${this.baseUrl}/Rentals`);
  }
}
