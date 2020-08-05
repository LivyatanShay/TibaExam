using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using TibaExam.Models;

namespace TibaExam.Logic
{
    public class BL
    {
        private FavoritesContext favoritesContext;
        public BL(IServiceProvider serviceProvider)
        {
            favoritesContext = serviceProvider.GetRequiredService<FavoritesContext>();
        }

        /// <summary>
        /// Will return top 100 results for 'term'
        /// </summary>
        public List<RepoItem> SearchGitRepos(string term)
        {
            var github = new GitHubClient(new Octokit.ProductHeaderValue("TibaExamApp"));
            var search = new SearchRepositoriesRequest(term);

            var repos = github.Search.SearchRepo(search).Result;
            var currentFavoriteIds = GetFavorites().Select(x => x.Id);

            var result = new List<RepoItem>();
            foreach (var item in repos.Items)
            {
                result.Add(new RepoItem
                {
                    RepoId = item.Id.ToString(),
                    Name = item.Name,
                    Url = item.HtmlUrl,
                    IsInFavorites = currentFavoriteIds.Any(x => x == item.Id.ToString())
                });
            }

            return result;
        }

        /// <summary>
        /// Will add 'FavoriteItem' row to the DB
        /// </summary>
        public void AddToFavorites(FavoriteItem item)
        {
            favoritesContext.Favorites.Add(new Models.Favorite { Id = item.repoId, Name = item.name, Url = item.url });
            favoritesContext.SaveChanges();
        }

        /// <summary>
        /// Will return all currently saved FavoriteItems in the DB
        /// </summary>
        public List<Favorite> GetFavorites()
        {
            return favoritesContext.Favorites.ToList();
        }
    }
}
