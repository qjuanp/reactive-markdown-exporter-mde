using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Mde.Net.Reactive.Extensions;
using Mde.Net.Reactive.Models;
using Newtonsoft.Json.Linq;

namespace Mde.Net.Reactive
{
    class Medium
    {
        private MediumApi api = new MediumApi();

        public IObservable<string> PostsUrlsFrom(string user) => 
            api
                .Call($"@{user}", new Dictionary<string, string> { { "format", "json" } })
                .Select(JObject.Parse)
                .Select(jObj => jObj["payload"]["references"]["Post"])
                .Cast<JObject>()
                .SelectMany(jPosts => jPosts.Properties())
                .Select(jProp => jProp.Value)
                .Cast<JObject>()
                .Select(jObj => $"@{user}/{jObj["uniqueSlug"].ToString()}");

        public IObservable<MediumPost> GetPost(string postPath) =>
          api
                .Call(postPath, new Dictionary<string, string> { { "format", "json" } })
                .Select(JObject.Parse)
                .Select(jObj =>  ( 
                    Title : jObj.SelectToken("payload.value.title"),
                    Slug : jObj.SelectToken("payload.value.slug"),
                    Paragraphs : jObj.SelectTokens("payload.value.content.bodyModel.paragraphs").Values()
                    ))
                .PrintAndContinue("Tuple")
                .Select(postTuple => new MediumPost{
                    Title = postTuple.Title.ToString(),
                    Slug = postTuple.Slug.ToString(),
                    Paragraphs = postTuple.Paragraphs.Select( jToken => jToken.ToObject<MediumParagraph>()).ToList()
                });
        
    }
}