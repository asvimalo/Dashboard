import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'employee',
    templateUrl: './employee.component.html'
})
export class EmployeeComponent {
    public forecasts: Employee[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
            this.forecasts = result.json() as Employee[];
        }, error => console.error(error));
    }
}

interface Employee {
    employeeId: number;
    firstName: string;
    lastName: string;
    assignments: any[];
    acquiredKnowledges: any[];
   
}
