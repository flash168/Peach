

namespace Peach.Model.Models
{
    /// <summary>
    /// drpy home 对象
    /// </summary>
    public class HomeModel
    {
        public List<ClassModel> Class { get; set; }
        public Dictionary<string, List<FilterModel>> filters { get; set; }
    }

    public class ClassModel
    {
        public string? Type_Id { get; set; }
        public string? Type_Name { get; set; }
    }

    public class FilterModel
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public List<ValueModel> Value { get; set; }
    }

    public class ValueModel
    {
        public string n { get; set; }
        public string v { get; set; }
    }

}
