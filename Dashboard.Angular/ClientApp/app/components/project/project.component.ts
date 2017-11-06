import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html'
})
export class ProjectComponent {
    public projects: Project[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/dashboard/projects').subscribe(result => {
            this.projects = result.json() as Project[];
        }, error => console.error(error));
    }
}

interface Project {
    //TODO
}
