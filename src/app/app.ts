import { Component, OnInit } from '@angular/core';
import { Car, ApiService } from './api.service';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {

  cars: Car[] = [];

  searchPlate: string = '';
  searchStatus: string = '';

  newCarData: any = {
    marka_Model: '',
    plaka: '',
    gunluk_Fiyat: 0
  };

  constructor(private apiService: ApiService) { }

  ngOnInit() {
    this.loadCars();
  }

  loadCars() {
    this.apiService.getAllCars().subscribe({
      next: (data) => {
        this.cars = data;
      },
      error: (error) => {
        console.log('Araba getirilemedi', error);
      }
    });
  }

  searchCarByPlate() {
    if (this.searchPlate.trim() === '') {
      this.loadCars();
      return;
    }
    this.apiService.getCarByPlate(this.searchPlate).subscribe({
      next: (data) => {
        this.cars = [data];
      },
      error: (error) => {
        console.log('Araba bulunamadı', error);
        this.cars = [];
      }
    });
  }

  searchCarByStatus() {
    if (this.searchStatus.trim() === '') {
      this.loadCars();
      return;
    }
    this.apiService.getCarsByStatus(this.searchStatus).subscribe({
      next: (data) => {
        this.cars = data;
      },
      error: (error) => {
        console.log('Araba bulunamadı', error);
        this.cars = [];
      }
    });
  }

  saveNewCar() {
    this.apiService.addCar(this.newCarData).subscribe({
      next: (response) => {
        alert('Araç başarıyla eklendi.');

        this.loadCars();

        this.newCarData = {
          marka_Model: '',
          plaka: '',
          gunluk_Fiyat: 0
        };
      },
      error: (err) => {
        console.error('Araba ekleme hatası:', err);
        alert('Araba eklenemedi.Detaylı bilgi için konsola bakın.');
      }
    });
  }
}
