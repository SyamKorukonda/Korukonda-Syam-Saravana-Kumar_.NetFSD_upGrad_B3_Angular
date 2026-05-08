import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  // Feature list displayed in UI (used in *ngFor)
  features = [
    {
      icon: 'bi-truck',
      title: 'Fast Delivery',
      desc: 'Delivered within 24 hours to your door'
    },
    {
      icon: 'bi-shield-check',
      title: 'Secure Payments',
      desc: '100% encrypted and safe transactions'
    },
    {
      icon: 'bi-arrow-counterclockwise',
      title: 'Easy Returns',
      desc: '7-day hassle-free return policy'
    },
    {
      icon: 'bi-headset',
      title: '24/7 Support',
      desc: 'Our team is always ready to help'
    }
  ];

}