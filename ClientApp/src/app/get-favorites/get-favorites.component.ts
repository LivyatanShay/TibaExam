import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-get-favorites',
  templateUrl: './get-favorites.component.html',
  styleUrls: ['./get-favorites.component.css']
})
export class GetFavoritesComponent {

  public favorites: FavoritItem[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<FavoritItem[]>(baseUrl + 'GitHubSearch/GetFavorites').subscribe(result => {
      this.favorites = result;
    }, error => console.error(error));
  }
}

interface FavoritItem {
  id: string;
  name: string;
  url: string;
}
