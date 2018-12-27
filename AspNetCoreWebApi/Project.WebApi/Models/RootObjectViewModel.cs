using System.Collections.Generic;

namespace Project.WebApi.Models
{
    /// <summary>
    /// A View model that represents the external API response.
    /// </summary>
    public class RootObjectViewModel
    {
        public int count { get; set; }
        public object next { get; set; }
        public object previous { get; set; }
        public List<ResultViewModel> results { get; set; }
    }
}
