using Microsoft.AspNetCore.Mvc.Rendering;

namespace LucrareDisertatie.Models.ViewModels
{
    public class AddContentRequest
    {
        public string Header { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public string ImageUrl { get; set; }
        public string HandleUrl { get; set; }
        public DateTime PostDate { get; set; }
        public string Author { get; set; }
        public bool Hidden { get; set; }

        //display tags

        public IEnumerable<SelectListItem> Tags { get; set; }

        //capture tag

        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
