import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Project } from './project.model';

@Injectable()
export class ProjectsService {
  private projects: Project[];

  private readonly url: string = 'https://localhost:5001/api/projects/';
  constructor(private http: HttpClient) {}
  fetchProjects(): Observable<Project[]> {
    return this.http.get<Project[]>(this.url);
  }
}
