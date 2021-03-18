import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Project } from '../project.model';
import { ProjectsService } from '../project.service';

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css'],
})
export class ProjectListComponent implements OnInit {
  projects: Observable<Project[]> = new Observable();

  constructor(private projectsService: ProjectsService) {}
  displayedColumns: string[] = ['name', 'lead', 'actions'];

  ngOnInit(): void {
    this.projects = this.projectsService.fetchProjects();
  }
}
