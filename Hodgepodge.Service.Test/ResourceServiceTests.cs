using Hodgepodge.Data.Models;
using Hodgepodge.Data.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Hodgepodge.Service.Test
{
    public class ResourceServiceTests
    {
        private readonly IResourceService _resourceService;

        public ResourceServiceTests()
        {
            var resources = new List<Resource>
            {
                new Resource { Url = "d1.tld" },
                new Resource { Url = "d1.tld/s1/s2" },
                new Resource { Url = "x.d1.tld/s1" },
                new Resource { Url = "d2.tld/s1/s2" }
            };

            var staticResourceRepository = new Mock<IStaticResourceRepository>();
            staticResourceRepository.Setup(_ => _.Resources).Returns(resources);

            _resourceService = new ResourceService(staticResourceRepository.Object);
        }

        [Theory]
        [ClassData(typeof(TheoryData))]
        public void Test(string input, string output)
        {
            var ub = new UriBuilder(input);
            var result = _resourceService
                .GetStatic(ub.Uri, StringComparison.OrdinalIgnoreCase)?
                .Url;

            Assert.Equal(output, result, true); // Ignore case.
        }
    }
}
