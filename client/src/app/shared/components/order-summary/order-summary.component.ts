import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatLabel } from '@angular/material/form-field';
import { MatFormField, MatInput } from '@angular/material/input';
import { Router, RouterLink } from '@angular/router';
import { CartService } from '../../../core/services/cart.service';
import { CurrencyPipe } from '@angular/common';
import { SnackbarService } from '../../../core/services/snackbar.service';

@Component({
  selector: 'app-order-summary',
  imports: [
    MatButton,
    RouterLink,
    MatLabel,
    MatFormField,
    MatInput,
    CurrencyPipe
  ],
  templateUrl: './order-summary.component.html',
  styleUrl: './order-summary.component.scss',
})
export class OrderSummaryComponent {
  cartService = inject(CartService);
  private router = inject(Router);
  private snack = inject(SnackbarService);

//outra solução para proteger o checkout caso o carrinho esteja vazio
  // gotoCheckout(){
  //   if(this.cartService.itemCount() === 0){
  //     this.snack.error('O carrinho está vazio');
  //   } else {
  //     this.router.navigateByUrl('/checkout');
  //   }
  // }
}
