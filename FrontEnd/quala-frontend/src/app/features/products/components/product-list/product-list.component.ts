import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

import { Producto } from '../../models/producto.model';
import { ProductService } from '../../services/product.service';
import { AuthService } from '../../../../core/services/auth.service';
import { NzMessageService } from 'ng-zorro-antd/message';

import { NgZorroAntdModule } from '../../../../shared/ng-zorro-antd.module';
import { ProductFormComponent } from '../product-form/product-form.component';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    NgZorroAntdModule,
    ProductFormComponent
  ],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products: Producto[] = [];
  isLoading = true;
  isModalVisible = false;
  selectedProduct: Producto | null = null;

  constructor(
    private productService: ProductService,
    private authService: AuthService,
    private message: NzMessageService
  ) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.isLoading = true;
    this.productService.getProductos().subscribe({
      next: (data) => {
        this.products = data;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
        this.message.error('No se pudieron cargar los productos.');
      }
    });
  }

  openNewModal(): void {
    this.selectedProduct = null;
    this.isModalVisible = true;
  }

  openEditModal(product: Producto): void {
    this.selectedProduct = product;
    this.isModalVisible = true;
  }

  deleteProduct(id: number): void {
    this.productService.deleteProducto(id).subscribe({
      next: () => {
        this.message.success('Producto eliminado correctamente.');
        this.loadProducts();
      },
      error: () => {
        this.message.error('Error al eliminar el producto.');
      }
    });
  }

  handleModalSave(): void {
    this.isModalVisible = false;
    this.loadProducts();
  }

  handleModalClose(): void {
    this.isModalVisible = false;
  }

  logout(): void {
    this.authService.logout();
  }
}