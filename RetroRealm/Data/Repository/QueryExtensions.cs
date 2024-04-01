namespace RetroRealm.Data.Repository
{
    public static class QueryExtensions
    {
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, 
            int pageNum, int pageSize)
        {
            return query
                .Skip((pageNum -1) * pageSize)
                .Take(pageSize);
        }
    }
}
