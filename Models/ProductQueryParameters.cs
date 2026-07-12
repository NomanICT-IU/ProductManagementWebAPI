namespace ProductManagementWebAPI.Models
{
    public class ProductQueryParameters
    {
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value;
        }

        public string? Search { get; set; }

        public string SortBy { get; set; } = "Id";

        public string SortOrder { get; set; } = "asc";
    }
}
