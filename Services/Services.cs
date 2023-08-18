using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTask.Services
{
    public class Services
    {
        public static async Task ReadJsonFromServerAsync()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://jsonplaceholder.typicode.com/users");
            var content = await response.Content.ReadAsStringAsync();
            var users = JArray.Parse(content);

            Console.WriteLine("Users:");
            foreach (var user in users)
            {
                Console.WriteLine(user["username"]);
            }

            Console.Write("\nEnter a username to view their posts: ");
            var username = Console.ReadLine();
            var userId = "";
            foreach (var user in users)
            {
                if (user["username"].ToString() == username)
                {
                    userId = user["id"].ToString();
                    break;
                }
            }

            if (userId == "")
            {
                Console.WriteLine("Invalid username");
                return;
            }

            response = await client.GetAsync($"https://jsonplaceholder.typicode.com/posts?userId={userId}");
            content = await response.Content.ReadAsStringAsync();
            var posts = JArray.Parse(content);

            Console.WriteLine("\nPosts:");
            foreach (var post in posts)
            {
                Console.WriteLine(post["title"]);
            }

            Console.Write("\nEnter a post title to view its comments: ");
            var postTitle = Console.ReadLine();
            var postId = "";
            foreach (var post in posts)
            {
                if (post["title"].ToString() == postTitle)
                {
                    postId = post["id"].ToString();
                    break;
                }
            }

            if (postId == "")
            {
                Console.WriteLine("Invalid post title");
                return;
            }

            response = await client.GetAsync($"https://jsonplaceholder.typicode.com/comments?postId={postId}");
            content = await response.Content.ReadAsStringAsync();
            var comments = JArray.Parse(content);

            Console.WriteLine("\nComments:");
            foreach (var comment in comments)
            {
                Console.WriteLine(comment["body"]);
            }
        }
    
    }
}
