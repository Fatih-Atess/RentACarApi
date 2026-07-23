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

  selectedCarForRent: Car | null = null;

  rentalData: any = {
    arac_ID: 0,
    musteri_Adi: '',
    baslangic_Tarihi: '',
    bitis_Tarihi: '',
    toplam_Tutar: 0

  }

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

  selectCarForRent(car: Car) {
    this.selectedCarForRent = car;
    this.rentalData.arac_ID = car.arac_ID;
  }

  confirmRental() {
    this.apiService.rentCar(this.rentalData).subscribe({
      next: (response) => {
        alert('Araba başarıyla kiralandı!');

        this.selectedCarForRent = null;

        this.rentalData = {
          arac_ID: 0,
          musteri_Adi: '',
          baslangic_Tarihi: '',
          bitis_Tarihi: '',
          toplam_Tutar: 0
        };

        this.loadCars();
      },
      error: (err) => {
        console.error('Hata:', err);
        alert('Araba kiralama başarısız.');
      }
    });
  }
}
