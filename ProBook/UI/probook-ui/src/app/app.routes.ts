import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { MainComponent } from './layout/main/main.component';
import { NotebookComponent } from './pages/notebook/notebook.component';
import { NotebookPreviewComponent } from './pages/notebook-preview/notebook-preview.component';
import { AddPageComponent } from './pages/add-page/add-page.component';
import { CollectionComponent } from './pages/collection/collection.component';
import { SharedNotebookComponent } from './pages/shared-notebook/shared-notebook.component';
import { RegisterComponent } from './pages/register/register.component';

export const routes: Routes = [
    { path: '', pathMatch: 'full', component: LoginComponent },
    {path:'register',component:RegisterComponent},
    {
        path: 'app',
        component: MainComponent,
        children: [
            { path: '', redirectTo: 'notebooks', pathMatch: 'full' },
            { path: 'notebooks', component: NotebookComponent },
            { path: 'notebook/:id', component: NotebookPreviewComponent },
            { path: 'notebook/:id/add-page', component: AddPageComponent },
            { path: 'collections', component: CollectionComponent },
            { path: 'sharedNotebooks', component: SharedNotebookComponent }
        ]
    },
    { path: '**', redirectTo: '' }
];
