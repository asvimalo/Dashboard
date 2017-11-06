import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'employee',
    templateUrl: './employee.component.html'
})
export class EmployeeComponent {
    public employees: Employee[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/dashboard/employees').subscribe(result => {
            this.employees = result.json() as Employee[];
        }, error => console.error(error));
    }
}

interface Employee {
    firstName: string;
    lastName: number;
    PersonNr: number;
    //assingments: Array;
}
