using System.ComponentModel;

namespace Demo.Pl.ViweModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [DisplayName("Role")]
        public string Name { get; set; }
    }
}
