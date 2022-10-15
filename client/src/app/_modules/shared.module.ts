import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FileUploadModule } from 'ng2-file-upload';


//npm install ng2-file-upload --legacy-peer-dependancies
//npm install @kolkov/ngx-gallery --legacy-peer-deps
//npm install ng2-file-upload --legacy-peer-deps
@NgModule({
  declarations: [],
  imports: [
    CommonModule, 
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-left'
    }), 
    TabsModule.forRoot(), 
    NgxGalleryModule,
    NgxSpinnerModule, 
    FileUploadModule
  ], 
  exports: [
    BsDropdownModule,
    ToastrModule, 
    TabsModule, 
    NgxGalleryModule,
    NgxSpinnerModule, 
    FileUploadModule
  ]
})
export class SharedModule { }
