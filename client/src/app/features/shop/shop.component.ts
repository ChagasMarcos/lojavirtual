import { Component, inject, OnInit } from '@angular/core';
import { Product } from '../../shared/models/product';
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';

import { ProductItemComponent } from "./product-item/product-item.component";
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { ShopService } from '../../core/services/shop.service';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [ProductItemComponent, MatButton, MatIcon],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss',
})
export class ShopComponent implements OnInit {

private shopService = inject(ShopService);
private dialogService = inject(MatDialog);
productList: Product[] = [];
selectedBrands: string[] = [];
selectedTypes: string[] = [];


  ngOnInit(): void {
     this.initializeShop();
  }

    initializeShop(){
      this.shopService.getProducts().subscribe({
        next: response => this.productList = response.data,
        error: error => console.log(error)
      });
      this.shopService.getBrands();
      this.shopService.getTypes();
    }

    openFilterDialog() {
      const dialogRef = this.dialogService.open(FiltersDialogComponent, {
        minWidth: '500px',
        data: {
          selectedBrands: this.selectedBrands,
          selectedTypes: this.selectedTypes
        }
      });

      dialogRef.afterClosed().subscribe({
        next: result => {
          if (result) {
            console.log(result);
            this.selectedBrands = result.selectedBrands;
            this.selectedTypes = result.selectedTypes;
            this.initializeShop();
          }
        }
      });
    }
}
