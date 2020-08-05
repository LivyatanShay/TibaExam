import {
  FormControl,
  FormGroupDirective,
  NgForm,
  Validators,
} from '@angular/forms';
import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ErrorStateMatcher } from '@angular/material/core';

export class SearchErrorStateMatcher implements ErrorStateMatcher {
  private verbsList = [' and ', ' or ', ' not '];
  occurrences(str: string, subStr: string, allowOverlapping: boolean) {
    str = str.toLocaleLowerCase() + '';
    subStr += '';

    if (subStr.length <= 0) {
      return str.length + 1;
    }

    let n = 0,
      pos = 0;
    const step = allowOverlapping ? 1 : subStr.length;

    while (true) {
      pos = str.indexOf(subStr, pos);
      if (pos >= 0) {
        ++n;
        pos += step;
      } else {
        break;
      }
    }
    return n;
  }

  countVerbs(str: string): number {
    let totalCount = 0;
    this.verbsList.forEach((element) => {
      totalCount += this.occurrences(str, element, false);
    });

    return totalCount;
  }

  isErrorState(
    control: FormControl | null,
  ): boolean {
    return !!(this.countVerbs(control.value) > 5);
  }
}

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css'],
})
export class SearchComponent {
  public results: RepoResult[];
  public tooManyVerbs = false;
  public isDisabled = false;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {}

  public searchFormControl = new FormControl('', [
    Validators.minLength(2),
    Validators.maxLength(256),
  ]);

  public matcher = new SearchErrorStateMatcher();

  searchTerm(term: string): void {
    if (!term) {
    } else {
      this.isDisabled = true;
      this.http
        .get<RepoResult[]>(this.baseUrl + `GitHubSearch/Search?query=${term}`)
        .subscribe(
          (result) => {
            this.results = result;
          },
          (error) => console.error(error),
        );

        this.isDisabled = false;
    }
  }

  addToFavorites(item: RepoResult): void {
    item.isInFavorites = true;
    this.http
      .post(this.baseUrl + `GitHubSearch/AddToFavorites`, item)
      .subscribe((error) => console.error(error));
  }
}

interface RepoResult {
  isInFavorites: boolean;
  id: string;
  name: string;
  url: string;
}
