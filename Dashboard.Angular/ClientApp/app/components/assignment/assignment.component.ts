import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'assignment',
    templateUrl: './assignment.component.html'
})
export class AssignmentComponent {
    public employees: Assignment[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/dashboard/assignments').subscribe(result => {
            this.employees = result.json() as Assignment[];
        }, error => console.error(error));
    }
}

interface Assignment {
    firstName: string;
    lastName: number;
    PersonNr: number;
    //assingments: Array;
}
