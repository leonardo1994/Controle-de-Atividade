using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Mvc.JQuery.DataTables;

[assembly: PreApplicationStartMethod(typeof(WebApplication1.RegisterDataTablesModelBinder), "Start")]

namespace WebApplication1 {
    public static class RegisterDataTablesModelBinder {
        public static void Start() {
            if (!ModelBinders.Binders.ContainsKey(typeof(DataTablesParam)))
                ModelBinders.Binders.Add(typeof(DataTablesParam), new Mvc.JQuery.DataTables.DataTablesModelBinder());
        }
    }
}
