import { Injectable, NgModule } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommonServices {

    appoinementOninit: Subject<any> = new Subject();    

    CallAppoinementOninit(value) {
        this.appoinementOninit.next(value);
    }
}
