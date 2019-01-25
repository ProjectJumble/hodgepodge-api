using Hodgepodge.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Hodgepodge.Data.Repositories
{
    public class StaticResourceRepository : IStaticResourceRepository
    {
        public IList<Resource> Resources { get; }

        private readonly IHostingEnvironment _hostingEnvironment;

        public StaticResourceRepository(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment ??
                throw new ArgumentNullException(nameof(hostingEnvironment));

            Resources = new List<Resource>();

            PopulateResources();
        }

        private void PopulateResources()
        {
            if (Resources == default(IList<Resource>))
                throw new NullReferenceException(nameof(Resources));

            var contentRootPath = _hostingEnvironment.ContentRootPath;

            var directoryInfo = new DirectoryInfo(contentRootPath);

            foreach (var file in directoryInfo.GetFiles("resources*.json"))
            {
                using (var streamReader = new StreamReader(file.FullName))
                {
                    JsonConvert.PopulateObject(streamReader.ReadToEnd(), Resources);
                }
            }
        }
    }
}
