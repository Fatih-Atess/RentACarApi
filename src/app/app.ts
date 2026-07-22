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
}
