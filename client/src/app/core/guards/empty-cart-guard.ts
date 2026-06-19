import { CanActivateFn, Router } from '@angular/router';
import { CartService } from '../services/cart.service';
import { inject } from '@angular/core';
import { SnackbarService } from '../services/snackbar.service';

export const emptyCartGuard: CanActivateFn = (route, state) => {
  const cartService = inject(CartService);
  const router = inject(Router);
  const snack = inject(SnackbarService);

  if(!cartService.cart() || cartService.itemCount() === 0){
    snack.error('Seu carrinho está vazio');
    router.navigateByUrl('/cart');
    return false;
  }
  return true;
};
