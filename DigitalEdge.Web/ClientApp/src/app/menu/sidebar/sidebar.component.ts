import { Component } from "@angular/core";
import * as $ from "jquery";

@Component({
    selector: "side-bar",
    templateUrl: "./sidebar.component.html"
})
export class SidebarComponent {
    private $BODY;
    private $MENU_TOGGLE;
    private $SIDEBAR_MENU;
    private $SIDEBAR_FOOTER;
    private $LEFT_COL;
    private $RIGHT_COL;
    private $NAV_MENU;
    private $FOOTER;
    IsAdmin: boolean = false;
    IsBu: boolean = false;
    public RoleName = "";
    public Name = "";
  constructor() {
  }

    ngOnInit() {
        this.Name = localStorage.getItem('Name');
        this.RoleName = localStorage.getItem('RoleName');
        if (this.RoleName === "Admin") {
          this.IsAdmin = true
        }
        else if (this.RoleName === "Facility User")
        {
          this.IsBu = true;
        }
        else {
          this.IsAdmin = false;
          this.IsBu = false;
        }
    }
    ngAfterViewInit(): void {
        //this.plot();
    }
    SendbulkClicked(event: MouseEvent) {
        let target = (<HTMLInputElement>event.target).id;

        let $li = $("#" + target.replace("chevron", "li")).parent();

        if ($li.is(".active")) {
            $li.removeClass("active active-sm");
            $("ul:first", $li).slideUp(function () { });
        } else {
            if (!$li.parent().is(".child_menu")) {
                $("#sidebar-menu")
                    .find("li")
                    .removeClass("active active-sm");
                $("#sidebar-menu")
                    .find("li ul")
                    .slideUp();
            }

            $li.addClass("active");

            $("ul:first", $li).slideDown(function () { });
        }
  }
    ReportsClicked(event: MouseEvent) {
    let target = (<HTMLInputElement>event.target).id;

    let $li = $("#" + target.replace("chevron", "li")).parent();

    if ($li.is(".active")) {
      $li.removeClass("active active-sm");
      $("ul:first", $li).slideUp(function () { });
    } else {
      if (!$li.parent().is(".child_menu")) {
        $("#side-menu")
          .find("li")
          .removeClass("active active-sm");
        $("#side-menu")
          .find("li ul")
          .slideUp();
      }
      $li.addClass("active");
      $("ul:first", $li).slideDown(function () { });
    }
  }
    plot() {
        console.log("in sidebar");
        let set = this;
        this.$BODY = $("body");
        this.$MENU_TOGGLE = $("#menu_toggle");
        this.$SIDEBAR_MENU = $("#sidebar-menu");
        this.$SIDEBAR_FOOTER = $(".sidebar-footer");
        this.$LEFT_COL = $(".left_col");
        this.$RIGHT_COL = $(".right_col");
        this.$NAV_MENU = $(".nav_menu");
        this.$FOOTER = $("footer");

        let $a = this.$SIDEBAR_MENU.find("a");
        this.$SIDEBAR_MENU.find("a").on("click", function (ev) {
            let $li = $(this).parent();

            if ($li.is(".active")) {
                $li.removeClass("active active-sm");
                $("ul:first", $li).slideUp(function () {
                    set.setContentHeight();
                });
            } else {
                // prevent closing menu if we are on child menu
                if (!$li.parent().is(".child_menu")) {
                    this.$SIDEBAR_MENU.find("li").removeClass("active active-sm");
                    this.$SIDEBAR_MENU.find("li ul").slideUp();
                }

                $li.addClass("active");

                $("ul:first", $li).slideDown(function () {
                    set.setContentHeight();
                });
            }
        });

        // toggle small or large menu
        this.$MENU_TOGGLE.on("click", function () {
            if (this.$BODY.hasClass("nav-md")) {
                this.$SIDEBAR_MENU.find("li.active ul").hide();
                this.$SIDEBAR_MENU
                    .find("li.active")
                    .addClass("active-sm")
                    .removeClass("active");
            } else {
                this.$SIDEBAR_MENU.find("li.active-sm ul").show();
                this.$SIDEBAR_MENU
                    .find("li.active-sm")
                    .addClass("active")
                    .removeClass("active-sm");
            }

            this.$BODY.toggleClass("nav-md nav-sm");

            this.setContentHeight();
        });
    }
    setContentHeight() {
        // reset height
        this.$RIGHT_COL.css("min-height", $(window).height());

        const bodyHeight = this.$BODY.outerHeight();
        const footerHeight = this.$BODY.hasClass("footer_fixed") ? -10 : this.$FOOTER.height();
        const leftColHeight = this.$LEFT_COL.eq(1).height() + this.$SIDEBAR_FOOTER.height();
        let contentHeight = bodyHeight < leftColHeight ? leftColHeight : bodyHeight;

        // normalize content
        contentHeight -= this.$NAV_MENU.height() + footerHeight;

        this.$RIGHT_COL.css("min-height", contentHeight);
    }
}
