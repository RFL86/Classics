import { Component, OnInit, Input } from '@angular/core';
import { RouterModule, Router } from '@angular/router';

@Component({
  selector: 'app-page-title',
  templateUrl: './pagetitle.component.html',
  styleUrls: ['./pagetitle.component.scss']
})
export class PagetitleComponent implements OnInit {

  @Input() breadcrumbItems: Array<{}>;
  @Input() title: string;

  constructor(private router: Router) { }

  ngOnInit() {
  }

}
