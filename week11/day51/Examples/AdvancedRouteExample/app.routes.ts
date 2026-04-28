import { Routes } from '@angular/router';
import { Home } from './home/home';
import { Emps } from './emps/emps';
import { authGuard } from './auth-guard';
import { Details } from './details/details';
import { Depts } from './depts/depts';
import { Login } from './login/login';
import { NotFound } from './not-found/not-found';

export const routes: Routes = [
    {path:"",component: Home},
    
    {path:"about",
        loadComponent:()=>import('./aboutus-component/aboutus-component').then(m=>m.AboutusComponent)
    },
    {path:"emps",component:Emps,canActivate:[authGuard]},
    {path:"details/:id",component:Details},
    {path:"depts",component:Depts,canActivate:[authGuard]},
    {path:"login",component:Login},
    //wildcard route (always  last) " ** " matches any route that is not already defined
    // withput this angular will not show proper console.error mrssage     
    {path:"**",component:NotFound}
];
