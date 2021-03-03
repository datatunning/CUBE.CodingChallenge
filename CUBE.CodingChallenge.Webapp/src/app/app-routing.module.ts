import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TemperatureConverterComponent } from './components/temperature-converter.component/temperature-converter.component';

const routes: Routes = [
  { path: '', component: TemperatureConverterComponent }
];
@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
