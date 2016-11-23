namespace EuroPallets.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Collections;
    using System.Globalization;
    using EuroPallets.Resources;
    using System.Resources;

    using System.Web.Hosting;

    public static class Extensions
    {
        private const string RelativeFilePath = @"/Resources/";
        public static string Translate(this HtmlHelper helper, string word)
        {
            var language = HttpContext.Current.Request.RequestContext.RouteData.Values["language"].ToString();
            var resourceSet = DefaultLang.ResourceManager.GetResourceSet(new CultureInfo(language), true, true);
            foreach (DictionaryEntry entry in resourceSet)
            {
                if (entry.Key.ToString() == word)
                {
                    return entry.Value.ToString();
                }
            }
            var list = new List<string>();
            list.Add(HostingEnvironment.MapPath(RelativeFilePath) + "DefaultLang.bg.resx");
            list.Add(HostingEnvironment.MapPath(RelativeFilePath) + "DefaultLang.en.resx");
            list.Add(HostingEnvironment.MapPath(RelativeFilePath) + "DefaultLang.resx");

            foreach (var item in list)
            {
                var newNode = new ResXDataNode(word, (object)word);
                using (var reader = new ResXResourceReader(item))
                {
                    var node = reader.GetEnumerator();
                    var writer = new ResXResourceWriter(item);
                    while (node.MoveNext())
                    {
                        writer.AddResource(node.Key.ToString(), node.Value.ToString());
                    }

                    writer.AddResource(newNode);
                    writer.Generate();
                    writer.Close();
                }
            }
            return word;
        }
    }
}