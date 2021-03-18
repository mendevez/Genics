import { Component, OnInit } from '@angular/core';
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
  projects: Project[] = [];
  projectsLoaded: boolean = false;
  constructor(private projectsService: ProjectsService) {}
  displayedColumns: string[] = ['name', 'lead', 'actions'];

  ngOnInit(): void {
    this.projectsService.fetchProjects().subscribe((projects: Project[]) => {
      this.projects = projects;
      this.projectsLoaded = true;
    });
  }
}
