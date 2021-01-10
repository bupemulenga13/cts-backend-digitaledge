import { Injectable } from "@angular/core";

@Injectable()
export class AppConfig {
  public ApiMethodURL = {
    GetTemplates: "api/Template/GetTemplates"
  };
}
