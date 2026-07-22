import { Component, OnInit } from '@angular/core';
import { Car, ApiService } from './api.service';


@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {

  cars: Car[] = [];

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
}
