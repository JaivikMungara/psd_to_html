#pragma checksum "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "af14dded584ed846c637d093327c72a50c382fb0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_CustomerDashboardPartial), @"mvc.1.0.view", @"/Views/Shared/CustomerDashboardPartial.cshtml")]
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
#line 1 "C:\Users\DELL\source\repos\mainproject\Views\_ViewImports.cshtml"
using mainproject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\DELL\source\repos\mainproject\Views\_ViewImports.cshtml"
using mainproject.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"af14dded584ed846c637d093327c72a50c382fb0", @"/Views/Shared/CustomerDashboardPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"29799c9b9fbc441a0b539a6a005d7f8c05f7119f", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_CustomerDashboardPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<mainproject.ViewModel.CustomerDashboard>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Customer", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "BookService", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/sort.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
  
    String CurrentDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n");
#nullable restore
#line 9 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
  
    int ServiceDashboardCount = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral("<div id=\"dashboard\" class=\"tab-contant\">\r\n    <div class=\"clearfix\">\r\n        <div class=\"d-flex justify-content-between\">\r\n            <p class=\"alignleft \"> Customer Service Request </p>\r\n\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "af14dded584ed846c637d093327c72a50c382fb05038", async() => {
                WriteLiteral(" <button class=\"alignright \">Add new request</button>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n        </div>\r\n        <table id=\"dashbordTable\" class=\"table\">\r\n            <thead class=\"table-light\">\r\n                <tr class=\"text-center\">\r\n                    <th scope=\"col\" style=\"width: 17%\"> Service ID ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "af14dded584ed846c637d093327c72a50c382fb06688", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</th>\r\n                    <th scope=\"col\" style=\"width: 20%\"> Service Date ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "af14dded584ed846c637d093327c72a50c382fb07797", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" </th>\r\n                    <th scope=\"col\" style=\"width: 30%\"> Service Provider ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "af14dded584ed846c637d093327c72a50c382fb08911", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" </th>\r\n                    <th scope=\"col\" style=\"width: 15%\"> Payment ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "af14dded584ed846c637d093327c72a50c382fb010016", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@" </th>
                    <th scope=""col"" style=""width: 9% ;text-align:end;padding-right:0px""> Acti</th>
                    <th scope=""col"" style=""width: 9%  ;text-align:start;padding-left:0px"" "">on</th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 32 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                 if (Model.Count() > 0)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 34 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                     foreach (var item in Model)
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 36 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                         if (item.Status == 1 || item.Status == 2)
                        {
                            ServiceDashboardCount = ServiceDashboardCount + 1;

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr data-value=");
#nullable restore
#line 39 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                      Write(item.ServiceRequestId);

#line default
#line hidden
#nullable disable
            WriteLiteral(" class=\"text-center\">\r\n                                <td data-label=\"Service ID\">\r\n                                    <p>");
#nullable restore
#line 41 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                  Write(item.ServiceRequestId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                </td>\r\n                                <td data-label=\"Service Date\">\r\n                                    <div> <img src=\"~/images/calendar2.png\"/> ");
#nullable restore
#line 44 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                                                         Write(item.Date);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </div>\r\n                                    \r\n                                    <p class=\"service-time\"> ");
#nullable restore
#line 46 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                                        Write(item.StartTime);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 46 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                                                          Write(item.EndTime);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </p>\r\n                                </td>\r\n                                <td data-label=\"Service Provider\" class=\"clearfix\">\r\n");
#nullable restore
#line 49 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                     if (item.ServiceProvider != null)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <div class=\"cap-icon\"> <img");
            BeginWriteAttribute("src", " src=", 2513, "", 2542, 1);
#nullable restore
#line 51 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
WriteAttributeValue("", 2518, item.UserProfilePicture, 2518, 24, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"spavtar\" alt=\"..\"> </div><div style=\"font-weight:400\" class=\"text-start\"> ");
#nullable restore
#line 51 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                                                                                                                                                             Write(item.ServiceProvider);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                                        <div>\r\n");
#nullable restore
#line 53 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                             for (int i = 1; i <= 5; i++)
                                            {
                                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 55 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                                 if (i <= item.AverageRating)
                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                    <i class=\"fa fa-star \" style=\"color:#ECB91C;\"></i>\r\n");
#nullable restore
#line 58 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                                }
                                                else
                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                    <i class=\"fa fa-star \" style=\"color:silver;\"></i>\r\n");
#nullable restore
#line 62 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 62 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                                 
                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <span>");
#nullable restore
#line 64 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                             Write(item.AverageRating);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                        </div>\r\n");
#nullable restore
#line 66 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </td>\r\n                                <td data-label=\"Payment\">\r\n                                    <p class=\"price\">&euro; ");
#nullable restore
#line 69 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                                       Write(item.TotalCost);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </p>\r\n                                </td>\r\n\r\n                                <td data-label=\"Reschedule\"> <button class=\"customerReschedule\" style=\"float:right\" data-bs-toggle=\"modal\" data-bs-target=\"#rescheduleModelServiceRequest\"");
            BeginWriteAttribute("value", " value=", 3985, "", 4014, 1);
#nullable restore
#line 72 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
WriteAttributeValue("", 3992, item.ServiceRequestId, 3992, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Reschedule</button></td>\r\n                                <td data-label=\"Cancel\"><button class=\"customerCancel\" data-bs-toggle=\"modal\" data-bs-target=\"#deleteModelServiceRequest\"");
            BeginWriteAttribute("value", " value=", 4194, "", 4223, 1);
#nullable restore
#line 73 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
WriteAttributeValue("", 4201, item.ServiceRequestId, 4201, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Cancel</button></td>\r\n                            </tr>\r\n");
#nullable restore
#line 75 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 75 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                         
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 76 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                     
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n\r\n        <div class=\"card mobileview clearfix mx-auto\" style=\"width:100%; padding:0% 5% 0% 5%; \">\r\n");
#nullable restore
#line 82 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
             if (Model.Count() > 0)
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 84 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                 foreach (var item in Model)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 86 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                     if (item.Status == 1 || item.Status == 2)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div class=\"card-body\" data-value=");
#nullable restore
#line 88 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                                     Write(item.ServiceRequestId);

#line default
#line hidden
#nullable disable
            WriteLiteral(">\r\n                            <h6 class=\"d-inline pe-2\">Service Id : </h6> ");
#nullable restore
#line 89 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                                                    Write(item.ServiceRequestId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                            <hr />
                            <span>
                                <img src=""~/images/calendar2.png"" alt=""calender""><h6 class=""d-inline pe-2 ""> <span class=""d-none d-sm-block"">Service</span>  Date:</h6><span class=""date mb-0"">");
#nullable restore
#line 92 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                                                                                                                                                                          Write(item.Date);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br>\r\n                                <img src=\"~/images/calendar2.png\" alt=\"calender\"><h6 class=\"pe-2 d-inline  \"><span class=\"d-none d-sm-block\">Service</span> Time:</h6> ");
#nullable restore
#line 93 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                                                                                                                                                  Write(item.StartTime);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 93 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                                                                                                                                                                    Write(item.EndTime);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </span>\r\n                            <hr>\r\n");
#nullable restore
#line 96 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                             if (item.ServiceProvider != null)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <h6 class=\"pe-2 \">Service Provider Name:</h6>\r\n                                <div class=\"cap-icon\">  <img");
            BeginWriteAttribute("src", " src=", 5665, "", 5694, 1);
#nullable restore
#line 99 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
WriteAttributeValue("", 5670, item.UserProfilePicture, 5670, 24, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"..\">   </div>\r\n");
#nullable restore
#line 100 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                           Write(item.ServiceProvider);

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <div>\r\n");
#nullable restore
#line 102 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                     for (int i = 1; i <= 5; i++)
                                    {
                                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 104 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                         if (i <= item.AverageRating)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <i class=\"fa fa-star \" style=\"color:#ECB91C;\"></i>\r\n");
#nullable restore
#line 107 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                        }
                                        else
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <i class=\"fa fa-star \" style=\"color:silver;\"></i>\r\n");
#nullable restore
#line 111 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 111 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                         
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span>");
#nullable restore
#line 113 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                     Write(item.AverageRating);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                </div>\r\n                                <hr />\r\n");
#nullable restore
#line 116 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <p class=\"price\"><h6 class=\"d-inline pe-2\">Price:</h6> &euro; ");
#nullable restore
#line 117 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                                                                     Write(item.TotalCost);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </p>\r\n                            <hr>\r\n                            <div class=\"d-flex justify-content-around\">\r\n                                <button class=\"customerReschedule\"");
            BeginWriteAttribute("value", " value=", 6900, "", 6929, 1);
#nullable restore
#line 120 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
WriteAttributeValue("", 6907, item.ServiceRequestId, 6907, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-bs-toggle=\"modal\" data-bs-target=\"#rescheduleModelServiceRequest\">Reschedule</button>\r\n                                <button class=\"customerCancel\"");
            BeginWriteAttribute("value", " value=", 7084, "", 7113, 1);
#nullable restore
#line 121 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
WriteAttributeValue("", 7091, item.ServiceRequestId, 7091, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-bs-toggle=\"modal\" data-bs-target=\"#deleteModelServiceRequest\">Cancel</button>\r\n                            </div>\r\n                        </div>\r\n");
#nullable restore
#line 124 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 124 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                     
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 125 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                 
            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        </div>


    </div>

</div>
<div class=""modal"" tabindex=""-1"" id=""deleteModelServiceRequest"">
    <div class=""modal-dialog modal-dialog-centered"">
        <div class=""modal-content"">
            <input type=""hidden"" id=""CancelRequestId"" name=""CancelRequestId"" />
            <div class=""modal-header"">
                <h5 class=""modal-title"">Cancel Service Request</h5>
                <button type=""button"" class=""btn-close"" data-bs-dismiss=""modal"" aria-label=""Close""></button>
            </div>
            <div class=""modal-body"">
                <label for=""comment"" class=""form-label"">Why you want to cancel the service request?</label>
                <textarea class=""form-control"" id=""cancelReason"" rows=""3""></textarea>
                <div class=""text-center"">
                    <button class=""customerCancel"" type=""button"" data-bs-dismiss=""modal"" class=""mt-2 cancel"" id=""CancelRequestBtn"">Cancel Now</button>
                </div>
            </div>
        </div>
    </div>
</di");
            WriteLiteral("v>\r\n");
            WriteLiteral(@"<button id=""serviceReqdetailsbtn"" data-bs-toggle=""modal"" data-bs-target=""#detailsModelServiceRequest"" class=""d-none"">details</button>
<div class=""modal"" tabindex=""-1"" id=""detailsModelServiceRequest"">
    <div class=""modal-dialog modal-dialog-centered"">
        <div class=""modal-content text-start"">
            <div class=""modal-header"">
                <h5 class=""modal-title"">Service Details</h5>
                <button type=""button"" class=""btn-close"" data-bs-dismiss=""modal"" aria-label=""Close""></button>
            </div>
            <div class=""modal-body float-start"">
                <h4 id=""CDSDDateTime"">05/10/2021 08:00 - 11:30</h4>
                <p>Duration: <span id=""CDSDDuration""></span> </p>
                <hr>
                <p>Service id: <span id=""CDSDId""></span></p>
                <div class=""row""> <p class=""col-2 d-inline"">Extras: </p> <div class=""col-10 d-inline"" id=""CDSDExtra""></div></div>
                <p>Net Amount: <span id=""CDSDAmount"">8750 &euro;</span> </p>
         ");
            WriteLiteral(@"       <p id=""CDSDStatus""></p>
                <hr>
                <p>Service Address:<span id=""CDSDAddress""></span></p>
                <p>Billing Address: <span>same as cleaninng address</span></p>
                <p>phone:<span id=""CDSDPhone""></span></p>
                <p>Email:<span id=""CDSDEmail""> </span></p>
                <hr>
                <p>Comments <span id=""CDSDComment""></span></p>
");
            WriteLiteral(@"                <span id=""customerdashboardbtn"">
                    <hr>
                    <div class=""text-center  d-flex justify-content-around"">
                        <button class=""customerReschedule"" data-bs-toggle=""modal"" data-bs-target=""#rescheduleModelServiceRequest"" id=""' '"">Reschedule</button><button class=""customerCancel"" data-bs-toggle=""modal"" data-bs-target=""#deleteModelServiceRequest"">Cancel</button>
                    </div>
                </span>
");
            WriteLiteral(@"                <span id=""customerServiceHistorybtn"">
                    <hr>
                    <div class=""text-center  d-flex justify-content-around"">
                        <button class=""rate"" data-bs-toggle=""modal"" data-bs-target=""#myRatingModal"">Rate </button>
                    </div>
                </span>
            </div>
        </div>
    </div>
</div>
");
            WriteLiteral(@"<div class=""modal"" tabindex=""-1"" id=""rescheduleModelServiceRequest"">
    <div class=""modal-dialog modal-dialog-centered"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"">Reschedule Service Request</h5>
                <button type=""button"" class=""btn-close"" data-bs-dismiss=""modal"" aria-label=""Close""></button>
            </div>
            <div class=""modal-body"">
                <p>Select New Date & Time</p>
                <div class=""row"">
                    <div class=""col-sm-6 col-12""> <input id=""selected_date"" type=""date"" value=");
#nullable restore
#line 204 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                                                                         Write(CurrentDate);

#line default
#line hidden
#nullable disable
            WriteLiteral(" placeholder=\"canlender\" min=");
#nullable restore
#line 204 "C:\Users\DELL\source\repos\mainproject\Views\Shared\CustomerDashboardPartial.cshtml"
                                                                                                                                  Write(CurrentDate);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"></div>
                    <div class=""col-sm-6 col-12"">
                        <input type=""hidden"" id=""updateRequestId"" name=""updateRequestId"" />
                        <select class=""form-select"" id=""selected_time"">
                            <option selected value=""08:00:00"">8:00 </option>
                            <option value=""08:30:00"">8:30 </option>
                            <option value=""09:00:00"">9:00 </option>
                            <option value=""09:30:00"">9:30 </option>
                            <option value=""10:00:00"">10:00 </option>
                            <option value=""10:30:00"">10:30 </option>
                            <option value=""11:00:00"">11:00 </option>
                            <option value=""11:30:00"">11:30 </option>
                            <option value=""12:00:00""> 12:00  </option>
                            <option value=""12:30:00"">12:30 </option>
                            <option value=""13:00:00""> 13:00  </option>
                   ");
            WriteLiteral(@"         <option value=""13:30:00"">13:30 </option>
                            <option value=""14:00:00""> 14:00  </option>
                            <option value=""14:30:00"">14:30 </option>
                            <option value=""15:00:00""> 15:00  </option>
                            <option value=""15:30:00"">15:30 </option>
                            <option value=""16:00:00""> 16:00  </option>
                            <option value=""16:30:00"">16:30 </option>
                            <option value=""17:00:00""> 17:00  </option>
                            <option value=""17:30:00"">17:30 </option>
                            <option value=""18:00:00""> 18:00  </option>
                        </select>
                    </div>
                </div>
                <div class=""text-center"">
                    <button id=""RescheduleServiceRequest"" type=""submit"" data-bs-dismiss=""modal"" class=""customerReschedule"">Update</button>
                </div>
            </div>
        </div>
   ");
            WriteLiteral(" </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<mainproject.ViewModel.CustomerDashboard>> Html { get; private set; }
    }
}
#pragma warning restore 1591
