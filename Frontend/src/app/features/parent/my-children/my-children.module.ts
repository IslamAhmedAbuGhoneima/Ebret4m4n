import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AddChildComponent } from './components/add-child/add-child.component';


const routes: Routes = [{ path: '', component: AddChildComponent },
];


@NgModule({
  declarations: [AddChildComponent],
  imports: [
    CommonModule, RouterModule.forChild(routes)
  ]
})
export class MyChildrenModule { }
