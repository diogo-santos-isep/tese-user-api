namespace Models.Filters
{
    public abstract class Filter
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; } = "Id";
        public bool SortAscending { get; set; } = true;
    }
}
