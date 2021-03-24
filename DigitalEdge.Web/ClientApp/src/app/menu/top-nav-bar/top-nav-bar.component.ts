import { Component } from "@angular/core";
import * as $ from "jquery";
import { Router } from '@angular/router';

@Component({
  selector: "topnav-bar", // <home></home>
  // We need to tell Angular's Dependency Injection which providers are in our app.
  providers: [],
  // Every Angular template is first compiled by the browser before Angular runs it's compiler
  templateUrl: "./top-nav-bar.component.html"
})
export class TopNavBarComponent {
  // TypeScript public modifier
  constructor(private router: Router ) {}
    public Name = localStorage.getItem('Name');
  toggleClicked(event: MouseEvent) {
      var target = (<HTMLInputElement>event.target).id;

    //var target=  "menu_toggle";
    var body = $("body");
    var menu = $("#sidebar-menu");

    // toggle small or large menu
    if (body.hasClass("nav-md")) {
      menu.find("li.active ul").hide();
      menu
        .find("li.active")
        .addClass("active-sm")
        .removeClass("active");
    } else {
      menu.find("li.active-sm ul").show();
      menu
        .find("li.active-sm")
        .addClass("active")
        .removeClass("active-sm");
    }
    body.toggleClass("nav-md nav-sm");
  }

  ngOnInit() {
  }
  logout() {
    localStorage.clear();
    this.router.navigate(['']);
  }

  ngAfterViewInit() {}
}
