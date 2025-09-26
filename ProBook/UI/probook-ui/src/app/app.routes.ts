import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { MainComponent } from './layout/main/main.component';
import { NotebookComponent } from './pages/notebook/notebook.component';
import { CollectionComponent } from './pages/collection/collection.component';
import { SharedNotebookComponent } from './pages/shared-notebook/shared-notebook.component';

export const routes: Routes = [
    { path: '', pathMatch: 'full', component: LoginComponent },
    {
        path: 'app',
        component: MainComponent,
        children: [
            { path: '', redirectTo: 'notebooks', pathMatch: 'full' },
            { path: 'notebooks', component: NotebookComponent },
            { path: 'collections', component: CollectionComponent },
            { path: 'sharedNotebooks', component: SharedNotebookComponent }
        ]
    },
    { path: '**', redirectTo: '' }
];
