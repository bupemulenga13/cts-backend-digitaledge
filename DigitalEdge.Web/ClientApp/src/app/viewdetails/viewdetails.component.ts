import { Component, Input, AfterViewInit, Output, EventEmitter } from '@angular/core'
import { CommonServices } from '../Services/common-services';


@Component({
    selector: 'app-viewdetails',
    templateUrl: './viewdetails.component.html',
})
export class ViewDetailsComponent implements AfterViewInit {
    @Input() viewdatadetails;
    loading: true;
    viewdetails: any;

    constructor(private _commonServices: CommonServices) {

    }

    ngAfterViewInit() {

        //this.viewdetails = this.viewdatadetails;
    }
    redirectToAppointments()
    {
        this._commonServices.CallAppoinementOninit(false);
    }
    //ngOnView() {
    //  debugger;
    // this.viewdetails = this.viewdatadetails;
    //}
}



