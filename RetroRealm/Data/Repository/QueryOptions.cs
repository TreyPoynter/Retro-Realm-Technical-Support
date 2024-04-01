using System.Linq.Expressions;

namespace RetroRealm.Data.Repository
{
    public class QueryOptions<T>
    {
        public Expression<Func<T, Object>> OrderBy { get; set; } = null!;
        public Expression<Func<T, bool>> Where { get; set; } = null!;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        private string[] _includes = Array.Empty<string>();
        public string Includes { 
            set => _includes = value.Replace(" ", "").Split(',');
        }
        public string[] GetIncludes() => _includes;

        public bool HasOrderBy => OrderBy != null;
        public bool HasWhere => Where != null;
        public bool HasPaging => PageNumber > 0 && PageSize > 0;
    }
}
