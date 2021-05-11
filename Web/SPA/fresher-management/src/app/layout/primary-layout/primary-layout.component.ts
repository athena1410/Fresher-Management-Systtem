import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'primary-layout',
  templateUrl: './primary-layout.component.html',
  styleUrls: ['./primary-layout.component.scss']
})
export class PrimaryLayoutComponent implements OnInit {
  isCollapsed = false;
  constructor() { 
    console.log("hehe")
  }

  ngOnInit(): void {
  }

}
