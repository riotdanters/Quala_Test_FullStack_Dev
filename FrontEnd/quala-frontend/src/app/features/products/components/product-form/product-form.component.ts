import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, AbstractControl, ValidationErrors, ReactiveFormsModule } from '@angular/forms';
import { ProductService } from '../../services/product.service';
import { NzMessageService } from 'ng-zorro-antd/message';
import { Producto } from '../../models/producto.model';
import { NgZorroAntdModule } from '../../../../shared/ng-zorro-antd.module';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NgZorroAntdModule
  ],
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit, OnChanges {
  @Input() isVisible = false;
  @Input() productData: Producto | null = null;
  @Output() formSaved = new EventEmitter<void>();
  @Output() formClosed = new EventEmitter<void>();

  productForm!: FormGroup;
  isLoading = false;
  isEditMode = false;

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private message: NzMessageService
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.isVisible) {
      if (this.productData) {
        this.isEditMode = true;
        this.productForm.patchValue(this.productData);
        this.productForm.controls['codigoProducto'].disable();
      } else {
        this.isEditMode = false;
        this.productForm.reset();
        this.productForm.controls['codigoProducto'].enable();
        this.productForm.controls['estado'].setValue(true);
        this.productForm.controls['fechaCreacion'].setValue(new Date());
      }
    }
  }

  private initForm(): void {
    this.productForm = this.fb.group({
      codigoProducto: [null, [Validators.required]],
      nombre: [null, [Validators.required, Validators.maxLength(250)]],
      descripcion: [null, [Validators.required, Validators.maxLength(250)]],
      referenciaInterna: [null, [Validators.required, Validators.maxLength(100)]],
      precioUnitario: [null, [Validators.required, Validators.min(0.01)]],
      estado: [true, [Validators.required]],
      unidadMedida: [null, [Validators.required, Validators.maxLength(50)]],
      fechaCreacion: [new Date(), [Validators.required, this.dateNotPastValidator]]
    });
  }
  
  dateNotPastValidator(control: AbstractControl): ValidationErrors | null {
    const today = new Date();
    today.setHours(0, 0, 0, 0);
    if (new Date(control.value) < today) {
      return { pastDate: true };
    }
    return null;
  }

  submitForm(): void {
    if (this.productForm.invalid) {
      Object.values(this.productForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
      return;
    }

    this.isLoading = true;
    const formData = this.productForm.getRawValue();

    let apiCall$: import('rxjs').Observable<any>;
    if (this.isEditMode) {
      apiCall$ = this.productService.updateProducto(formData.codigoProducto, formData);
    } else {
      apiCall$ = this.productService.createProducto(formData);
    }

    apiCall$.subscribe({
      next: () => {
        this.isLoading = false;
        this.message.success(`Producto ${this.isEditMode ? 'actualizado' : 'creado'} correctamente.`);
        this.formSaved.emit();
      },
      error: (err: { error: { message: string; }; }) => {
        this.isLoading = false;
        const errorMessage = err.error?.message || `Error al ${this.isEditMode ? 'actualizar' : 'crear'} el producto.`;
        this.message.error(errorMessage);
      }
    });
  }

  handleCancel(): void {
    this.formClosed.emit();
  }
}