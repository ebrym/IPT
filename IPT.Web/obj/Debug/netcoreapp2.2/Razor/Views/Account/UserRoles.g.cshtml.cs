#pragma checksum "C:\Users\User\Documents\Visual Studio 2017\Projects\IPT\IPT.Web\Views\Account\UserRoles.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "667d056a6e96b51def904faac1ebc7aa0ebf5a86"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_UserRoles), @"mvc.1.0.view", @"/Views/Account/UserRoles.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Account/UserRoles.cshtml", typeof(AspNetCore.Views_Account_UserRoles))]
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
#line 1 "C:\Users\User\Documents\Visual Studio 2017\Projects\IPT\IPT.Web\Views\_ViewImports.cshtml"
using IPT.Web;

#line default
#line hidden
#line 2 "C:\Users\User\Documents\Visual Studio 2017\Projects\IPT\IPT.Web\Views\_ViewImports.cshtml"
using IPT.Web.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"667d056a6e96b51def904faac1ebc7aa0ebf5a86", @"/Views/Account/UserRoles.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"56ad57364a554300d7c4c24c3a828a2f2e128fa9", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_UserRoles : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<IPT.Web.Models.AccountModel.UserViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Account", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "UserRole", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("Create"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\User\Documents\Visual Studio 2017\Projects\IPT\IPT.Web\Views\Account\UserRoles.cshtml"
  
    ViewBag.Title = "User Roles";

#line default
#line hidden
            BeginContext(105, 88, true);
            WriteLiteral("\r\n\r\n<div class=\"card pd-20 pd-sm-40\">\r\n    <h6 class=\"card-body-title\">User Roles</h6>\r\n");
            EndContext();
            BeginContext(342, 35, true);
            WriteLiteral("\r\n    <div class=\"right\">\r\n        ");
            EndContext();
            BeginContext(377, 104, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "667d056a6e96b51def904faac1ebc7aa0ebf5a865011", async() => {
                BeginContext(464, 13, true);
                WriteLiteral(" Create User ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(481, 497, true);
            WriteLiteral(@"
    </div>
    <div class=""table-wrapper"">
        <table id=""datatable1"" class=""table display responsive nowrap"">
            <thead>
                <tr>
                    <th class=""wd-15p"">First name</th>
                    <th class=""wd-15p"">Last name</th>
                    <th class=""wd-20p"">Department</th>
                    <th class=""wd-15p"">Unit</th>
                    <th class=""wd-10p"">Email</th>
                </tr>
            </thead>
            <tbody>
");
            EndContext();
#line 26 "C:\Users\User\Documents\Visual Studio 2017\Projects\IPT\IPT.Web\Views\Account\UserRoles.cshtml"
                 foreach (var user in Model)
                {

#line default
#line hidden
            BeginContext(1043, 46, true);
            WriteLiteral("                <tr>\r\n                    <td>");
            EndContext();
            BeginContext(1090, 14, false);
#line 29 "C:\Users\User\Documents\Visual Studio 2017\Projects\IPT\IPT.Web\Views\Account\UserRoles.cshtml"
                   Write(user.FirstName);

#line default
#line hidden
            EndContext();
            BeginContext(1104, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(1136, 13, false);
#line 30 "C:\Users\User\Documents\Visual Studio 2017\Projects\IPT\IPT.Web\Views\Account\UserRoles.cshtml"
                   Write(user.LastName);

#line default
#line hidden
            EndContext();
            BeginContext(1149, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(1181, 15, false);
#line 31 "C:\Users\User\Documents\Visual Studio 2017\Projects\IPT\IPT.Web\Views\Account\UserRoles.cshtml"
                   Write(user.Department);

#line default
#line hidden
            EndContext();
            BeginContext(1196, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(1228, 9, false);
#line 32 "C:\Users\User\Documents\Visual Studio 2017\Projects\IPT\IPT.Web\Views\Account\UserRoles.cshtml"
                   Write(user.Unit);

#line default
#line hidden
            EndContext();
            BeginContext(1237, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(1269, 10, false);
#line 33 "C:\Users\User\Documents\Visual Studio 2017\Projects\IPT\IPT.Web\Views\Account\UserRoles.cshtml"
                   Write(user.Email);

#line default
#line hidden
            EndContext();
            BeginContext(1279, 49, true);
            WriteLiteral("</td>\r\n                 \r\n                </tr>\r\n");
            EndContext();
#line 36 "C:\Users\User\Documents\Visual Studio 2017\Projects\IPT\IPT.Web\Views\Account\UserRoles.cshtml"
                }

#line default
#line hidden
            BeginContext(1347, 101, true);
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div><!-- table-wrapper -->\r\n</div><!-- card -->\r\n\r\n\r\n\r\n");
            EndContext();
            BeginContext(1732, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<IPT.Web.Models.AccountModel.UserViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
