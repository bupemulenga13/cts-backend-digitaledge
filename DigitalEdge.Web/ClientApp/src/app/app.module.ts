import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TokenInterceptorService } from './auth-Interceptor/token-interceptor.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AddUserComponent } from './adduser/adduser.component';
import { AddUserService } from './adduser/adduser.service';
import { AdminLayoutComponent } from './admin-layout/admin-layout.component';
import { ConfirmComponent } from './confirm/confirm.component';
import { SimpleModalModule } from 'ngx-simple-modal';
import { SidebarComponent } from './menu/sidebar/sidebar.component';
import { NoContentComponent } from './global/no-content/no-content.component';
import { ContentComponent } from './content/content.component';
import { TopNavBarComponent } from './menu/top-nav-bar/top-nav-bar.component';
import { Footer } from './global/footer/footer.component';
import { NetworkActivitiesComponent } from './content/network-activities/network-activities.component';
import * as jQuery from 'jquery';
import { SendBulkSMSComponent } from './send-bulk-sms/send-bulk-sms.component';
import { AppointmentsComponent } from './appointments/appointments.component';
import { NgxFileDropModule } from 'ngx-file-drop';
import { ViewDetailsComponent } from './viewdetails/viewdetails.component';
import { CommonServices } from './Services/common-services';
import { MessageTemplatesComponent } from './message-templates/message-templates.component';
import { MessageTemplateService } from './message-templates/message-template.service';
import { AppConfig } from './app.configuration';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FacilityManagmentComponent } from './building-managment/building-managment.component';
import { ChartsModule } from 'ng2-charts';
import { NewRegisteredUniqueComponent } from './new-registered-unique/new-registered-unique.component';
import { ActiveClientsComponent } from './active-clients/active-clients.component';
import { UpcommingAppointmentComponent } from './upcomming-appointment/upcomming-appointment.component';
import { UpcommingVisitsComponent } from './upcomming-visits/upcomming-visits.component';
import { MissedAppointmentComponent } from './missed-appointment/missed-appointment.component';
import { MissedVisitsComponent } from './missed-visits/missed-visits.component';
import { BUNewRegisteredUniqueComponent } from './bu-new-registered-unique/bu-new-registered-unique.component';
import { BUActiveClientsComponent } from './bu-active-clients/bu-active-clients.component';
import { ClientInfoComponent } from './client-info/client-info.component';
import { AppointmentCheckComponent } from './appointment-check/appointment-check.component';
import { VisitHistoryComponent } from './visit-history/visit-history.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';




@NgModule({
  declarations: [
    AppComponent,
    SidebarComponent,
    NoContentComponent,
    ContentComponent,
    TopNavBarComponent,
    Footer,
    NetworkActivitiesComponent,
    LoginComponent,
    DashboardComponent,
    AddUserComponent,
    AdminLayoutComponent,
    ConfirmComponent,
    SendBulkSMSComponent,
    AppointmentsComponent,
    ViewDetailsComponent,
    MessageTemplatesComponent,
    FacilityManagmentComponent,
    NewRegisteredUniqueComponent,
    ActiveClientsComponent,
    UpcommingAppointmentComponent,
    UpcommingVisitsComponent,
    MissedAppointmentComponent,
    MissedVisitsComponent,
    BUNewRegisteredUniqueComponent,
    BUActiveClientsComponent,
    ClientInfoComponent,
    AppointmentCheckComponent,
    VisitHistoryComponent,
    CounterComponent,
    FetchDataComponent,
    HomeComponent,
    NavMenuComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    CommonModule,
    NgbModule,
    NgxFileDropModule,
    ReactiveFormsModule,
    NgxSpinnerModule,
    ChartsModule,
    SimpleModalModule.forRoot({ container: "modal-container" }),
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(), // ToastrModule added
    ModalModule.forRoot(),
    RouterModule.forRoot([
    { path: '', component: LoginComponent, pathMatch: 'full' },
    { path: 'users', component: DashboardComponent },
    { path: 'register-user', component: AddUserComponent },
    { path: 'user/edit/:id', component: AddUserComponent },
    { path: 'confirm', component: ConfirmComponent },
    { path: 'sendbulksms', component: SendBulkSMSComponent },
    { path: 'appointments', component: AppointmentsComponent },
    { path: 'viewdetails', component: ViewDetailsComponent },
    { path: 'messagetemplates', component: MessageTemplatesComponent },
    { path: 'facilitymanagment', component: FacilityManagmentComponent },
    { path: 'bunewregisteredreports', component: BUNewRegisteredUniqueComponent },
    { path: 'buactiveclientsreports', component: BUActiveClientsComponent },
    { path: 'newregisteredreports', component: NewRegisteredUniqueComponent },
    { path: 'activeclientsreports', component: ActiveClientsComponent },
    { path: 'upcommingappointmentreports', component: UpcommingAppointmentComponent },
    { path: 'upcommingvisitsreports', component: UpcommingVisitsComponent },
    { path: 'missedappointmentreports', component: MissedAppointmentComponent },
    { path: 'missedvisitsreports', component: MissedVisitsComponent },
    { path: 'clientinformation', component: ClientInfoComponent },
    { path: 'appointmentcheck', component: AppointmentCheckComponent },
    { path: 'visithistory', component: VisitHistoryComponent },
], { relativeLinkResolution: 'legacy' })
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true
      },
    AddUserService, CommonServices, MessageTemplateService, AppConfig,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
