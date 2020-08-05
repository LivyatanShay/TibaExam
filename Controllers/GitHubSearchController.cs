using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Octokit;
using TibaExam.Models;

namespace TibaExam.Controllers
{
    [ApiController]
    [Route("GitHubSearch")]
    public partial class GitHubSearchController : ControllerBase
    {
        public GitHubSearchController(IServiceProvider serviceProvider)
        {
        }

        [HttpGet]
        [Route("Search")]
        public async Task<dynamic> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query) != false || query.Length == 0)
            {
                return null;
            }

            var result = await Task.Run(() =>
            {
                return Common.BL.SearchGitRepos(query);
            });

            return new JsonResult(result);
        }

        [HttpPost]
        [Route("AddToFavorites")]
        public void AddToFavorites([FromBody] dynamic item)
        {
            _ = Task.Run(() =>
            {
                var repoItem = JsonConvert.DeserializeObject<FavoriteItem>(item.ToString());
                Common.BL.AddToFavorites(repoItem);
            });
        }

        [HttpGet]
        [Route("GetFavorites")]
        public async Task<dynamic> GetFavorites()
        {
            var repos = await Task.Run(() =>
            {
                return Common.BL.GetFavorites();
            });

            return new JsonResult(repos);
        }
    }
}