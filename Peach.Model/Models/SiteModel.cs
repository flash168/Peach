
namespace Peach.Model.Models
{
    /// <summary>
    /// 站点实体
    /// </summary>
    public class SiteModel
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string? Api { get; set; }
        public int Searchable { get; set; }
        public int QuickSearch { get; set; }
        public int PlayerType { get; set; }
        public int Filterable { get; set; }
        public object? Ext { get; set; }
        public string? Jar { get; set; }

        public List<string> flags { get; set; }

    }
}
