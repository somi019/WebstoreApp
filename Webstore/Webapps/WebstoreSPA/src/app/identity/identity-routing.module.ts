import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IdentityComponent } from './identity.component';
import { LoginFormComponent } from './feature-authentication/login-form/login-form.component';

// podrazumeva se da je pocetak rute /identity
const routes: Routes = [
  { path: '', component: IdentityComponent, children: [
    {path: 'login', component: LoginFormComponent}
  ] },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class IdentityRoutingModule { }
