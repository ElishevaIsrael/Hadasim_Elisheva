import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
//HttpClientModule.forRoot({ baseUrl: 'http://localhost:52000/api' }) // Adjust base URL if needed

