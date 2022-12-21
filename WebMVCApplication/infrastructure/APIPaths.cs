namespace WebMVCApplication.infrastructure
{
    public static class APIPaths
    {
        public static string GetAllTypes(string baseUrl)
        {
            return $"{baseUrl}/eventtypes";
        }
        public static string GetAllOrganizers(string baseUrl)
        {
            return $"{baseUrl}/eventorganizers";
        }
        public static string GetAllItems(string baseUrl, int page, int take)
        {
            return $"{baseUrl}/items?pageIndex={page}&pageSize={take}";
        }
    }
}
