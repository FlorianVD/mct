#pragma checksum "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5f6d93e225eebf868d0fec2968105ff6520390ac"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Student_Index), @"mvc.1.0.view", @"/Views/Student/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/home/florian/school/backend/labo04/webapp/Views/_ViewImports.cshtml"
using webapp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/florian/school/backend/labo04/webapp/Views/_ViewImports.cshtml"
using DataAccesPatterns.models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5f6d93e225eebf868d0fec2968105ff6520390ac", @"/Views/Student/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f8ef99a4c2d6e0c5d210b8491e9ca1434a2ed0f9", @"/Views/_ViewImports.cshtml")]
    public class Views_Student_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<DataAccesPatterns.models.Student>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "Form_StudentForName", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h1>Index</h1>\n\n<p>\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5f6d93e225eebf868d0fec2968105ff6520390ac4004", async() => {
                WriteLiteral("Create New");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "5f6d93e225eebf868d0fec2968105ff6520390ac5149", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</partial>\n");
#nullable restore
#line 12 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
     if(ViewBag.search != null) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p>Je zocht op <strong>");
#nullable restore
#line 13 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
                          Write(ViewBag.search);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong></p>\n");
#nullable restore
#line 14 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\n<table class=\"table\">\n    <thead>\n        <tr>\n            <th>\n                ");
#nullable restore
#line 20 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
           Write(Html.DisplayNameFor(model => model.EducationId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 23 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
           Write(Html.DisplayNameFor(model => model.ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 26 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 29 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Gender));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 32 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 35 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
           Write(Html.DisplayNameFor(model => model.DateOfBirth));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 38 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
           Write(Html.DisplayNameFor(model => model.ImageUrl));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th></th>\n        </tr>\n    </thead>\n    <tbody>\n");
#nullable restore
#line 44 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\n            <td>\n                ");
#nullable restore
#line 47 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.EducationId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </td>\n            <td>\n                ");
#nullable restore
#line 50 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </td>\n            <td>\n                ");
#nullable restore
#line 53 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </td>\n            <td>\n                ");
#nullable restore
#line 56 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Gender));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </td>\n            <td>\n                ");
#nullable restore
#line 59 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </td>\n            <td>\n                ");
#nullable restore
#line 62 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.DateOfBirth));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </td>\n            <td>\n                ");
#nullable restore
#line 65 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.ImageUrl));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </td>\n            <td>\n                ");
#nullable restore
#line 68 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
           Write(Html.ActionLink("Edit", "Edit", new { /* ID=item.PrimaryKey */ }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\n                ");
#nullable restore
#line 69 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
           Write(Html.ActionLink("Details", "Details", new { ID=item.ID }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\n                ");
#nullable restore
#line 70 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
           Write(Html.ActionLink("Delete", "Delete", new { /* ID=item.PrimaryKey */ }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </td>\n        </tr>\n");
#nullable restore
#line 73 "/home/florian/school/backend/labo04/webapp/Views/Student/Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\n</table>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<DataAccesPatterns.models.Student>> Html { get; private set; }
    }
}
#pragma warning restore 1591
