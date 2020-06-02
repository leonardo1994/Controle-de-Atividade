using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace WebApplication1.HtmlHelpers
{
    public static class DatePicker
    {
        /// <summary>
        /// Cria um objeto html do tipo data.
        /// Com formatações para Data e Hora.
        /// </summary>
        /// <typeparam name="TModel">Modelo</typeparam>
        /// <typeparam name="TValue">Valor</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">Propriedade do contexto</param>
        /// <param name="additional">Parametros adicionais para adicionar outros atributos ao elemento html como ClassCss, JavaScript, etc.</param>
        /// <returns>Retorna um elemento html do tipo data</returns>
        public static MvcHtmlString DatePickerFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, object additional)
        {
            //var divFormGroup = new TagBuilder("div");
            //divFormGroup.AddCssClass("form-group");

            //var labelControl = htmlHelper.LabelFor(expression, new { @class = "control-label col-md-2" });
            //divFormGroup.InnerHtml = labelControl.ToHtmlString();

            //var divDatePickerFor = new TagBuilder("div");
            //divDatePickerFor.AddCssClass("col-md-10");

            // Cria uma tag <input></input>
            var datePicker = new TagBuilder("input");

            // pego o modelo atual, da propriedade passada na expressão.
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            // Preencho o atributo id do elemento <input id='name' />
            datePicker.MergeAttribute("id", metadata.PropertyName);
            // Preencho o atributo name do elemento <input name='name' />
            datePicker.MergeAttribute("name", metadata.PropertyName);
            // Passo a class css de formatação padrão. <input class="form-control" />
            datePicker.AddCssClass("form-control");

            // Realizo está condição para verificar o tipo de formatação a ser apresentado.
            // Se será do do tipo data normal ou hora.
            if (metadata.DataTypeName == DataType.Date.ToString())
            {
                datePicker.Attributes.Add("value",
                    ((DateTime?)metadata.Model)?.ToString("yyyy-MM-dd") ?? "");
                datePicker.MergeAttribute("type", metadata.DataTypeName.ToLower());
            }
            else if (metadata.DataTypeName == DataType.Time.ToString())
            {
                datePicker.Attributes.Add("value",
                    ((DateTime?)metadata.Model)?.ToString("HH:mm") ?? "");
                datePicker.MergeAttribute("type", metadata.DataTypeName.ToLower());
            }

            var properties = additional.GetType().GetProperties();
            foreach (var item in properties)
            {
                if(item.Name == "class")
                {
                    datePicker.AddCssClass(item.GetValue(additional).ToString());
                }else
                {
                    datePicker.MergeAttribute(item.Name, item.GetValue(additional).ToString());
                }
            }

            //divDatePickerFor.InnerHtml = datePicker.ToString();

            //var validation = htmlHelper.ValidationMessageFor(expression, "", new { @class = "text-danger" });

            //divDatePickerFor.InnerHtml += validation.ToHtmlString();

            //divFormGroup.InnerHtml += divDatePickerFor;

            // Retorno o elemento montado
            return MvcHtmlString.Create(datePicker.ToString());
        }
    }
}
