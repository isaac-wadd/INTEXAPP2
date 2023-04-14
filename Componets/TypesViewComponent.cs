using INTEXAPP2.Models;
using Microsoft.AspNetCore.Mvc;

namespace INTEXAPP2.Componets
{
    public class TypesViewComponent : ViewComponent
    {
        private mummiesContext repo { get; set; }

        public TypesViewComponent(mummiesContext temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            var types = repo.Burialmains
            .Select(x => x.Sex)
            .Distinct()
            .OrderBy(x => x);

            return View(types);
        }
    }
}
