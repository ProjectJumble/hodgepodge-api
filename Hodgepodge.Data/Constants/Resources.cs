using Hodgepodge.Data.Enums.Audit;
using Hodgepodge.Data.Models;
using System;
using System.Collections.Generic;

namespace Hodgepodge.Data.Constants
{
    [Obsolete]
    public static class Resources
    {
        public static readonly IList<Resource> ResourceDocuments =
            new List<Resource>
            {
                new Resource{ Url = "food.good.fire.bad", Label = Label.Unsure, SeedTimeStamp = DateTime.Parse("0001-01-01") }
            };
    }
}
