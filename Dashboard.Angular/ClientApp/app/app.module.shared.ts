import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ProjectComponent } from './components/project/project.component';
import { EmployeeComponent } from './components/employee/employee.component';
import { AssignmentComponent } from './components/assignment/assignment.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        DashboardComponent,
        ProjectComponent,
        EmployeeComponent,
        AssignmentComponent,
        FetchDataComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
            { path: 'dashboard', component: DashboardComponent },
            { path: 'employee', component: EmployeeComponent },
            { path: 'project', component: ProjectComponent },
            { path: 'assignment', component: AssignmentComponent },
            { path: '**', redirectTo: 'dashboard' }
        ])
    ]
})
export class AppModuleShared {
}
