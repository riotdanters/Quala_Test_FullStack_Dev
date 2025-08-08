import { Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { LoginComponent } from './pages/login/login.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    {
        path: 'products',
        canActivate: [AuthGuard],
        // Lazy loading para rutas standalone
        loadChildren: () => import('./features/products/products.routes').then(m => m.PRODUCT_ROUTES)
    },
    // Redirecciones
    { path: '', redirectTo: 'products', pathMatch: 'full' },
    { path: '**', redirectTo: 'products' }
];