#pragma checksum "C:\Users\INSPIRON 5577\Desktop\Git\Transito\trafico\SIGERSIV_web\SIGERSIV_web\Views\Home\Chat.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "262ca96805d14f2acc4829439ebb0c124b2b7d0f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Chat), @"mvc.1.0.view", @"/Views/Home/Chat.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Chat.cshtml", typeof(AspNetCore.Views_Home_Chat))]
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
#line 1 "C:\Users\INSPIRON 5577\Desktop\Git\Transito\trafico\SIGERSIV_web\SIGERSIV_web\Views\_ViewImports.cshtml"
using SIGERSIV_web;

#line default
#line hidden
#line 2 "C:\Users\INSPIRON 5577\Desktop\Git\Transito\trafico\SIGERSIV_web\SIGERSIV_web\Views\_ViewImports.cshtml"
using SIGERSIV_web.Models;

#line default
#line hidden
#line 1 "C:\Users\INSPIRON 5577\Desktop\Git\Transito\trafico\SIGERSIV_web\SIGERSIV_web\Views\Home\Chat.cshtml"
using SIGERSIV.Models;

#line default
#line hidden
#line 2 "C:\Users\INSPIRON 5577\Desktop\Git\Transito\trafico\SIGERSIV_web\SIGERSIV_web\Views\Home\Chat.cshtml"
using System.Net.Sockets;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"262ca96805d14f2acc4829439ebb0c124b2b7d0f", @"/Views/Home/Chat.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"54d9ba2bb2a896ab4d073cd3fb34317b4292e641", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Chat : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "C:\Users\INSPIRON 5577\Desktop\Git\Transito\trafico\SIGERSIV_web\SIGERSIV_web\Views\Home\Chat.cshtml"
  
    ViewData["Title"] = "Chat";
    Layout = null;

#line default
#line hidden
            BeginContext(113, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 8 "C:\Users\INSPIRON 5577\Desktop\Git\Transito\trafico\SIGERSIV_web\SIGERSIV_web\Views\Home\Chat.cshtml"
  
    Personal p = ViewBag.per;
    List<string> mensajes = new List<string>();
    if (ViewBag.mensajes != null)
    {
        mensajes = ViewBag.mensajes;
    }

    TcpClient cliente = ViewBag.clie;
    string nombreUsuario = ViewBag.nombreUsuario;

#line default
#line hidden
            BeginContext(381, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(383, 542, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6b577426ceba4a58add6690aedb9b95a", async() => {
                BeginContext(403, 210, true);
                WriteLiteral("\r\n    <div>\r\n        <label>Escriba su mensaje:</label>\r\n    </div>\r\n    <div class=\"centered\">\r\n        <input type=\"text\" name=\"mensaje\" id=\"mensaje\" />\r\n        <input type=\"hidden\" name=\"socket\" id=\"socket\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 613, "\"", 629, 1);
#line 26 "C:\Users\INSPIRON 5577\Desktop\Git\Transito\trafico\SIGERSIV_web\SIGERSIV_web\Views\Home\Chat.cshtml"
WriteAttributeValue("", 621, cliente, 621, 8, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(630, 73, true);
                WriteLiteral(" />\r\n        <input type=\"hidden\" name=\"nombreUsuario\" id=\"nombreUsuario\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 703, "\"", 725, 1);
#line 27 "C:\Users\INSPIRON 5577\Desktop\Git\Transito\trafico\SIGERSIV_web\SIGERSIV_web\Views\Home\Chat.cshtml"
WriteAttributeValue("", 711, nombreUsuario, 711, 14, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(726, 63, true);
                WriteLiteral(" />\r\n        <input type=\"hidden\" name=\"mensajes\" id=\"mensajes\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 789, "\"", 806, 1);
#line 28 "C:\Users\INSPIRON 5577\Desktop\Git\Transito\trafico\SIGERSIV_web\SIGERSIV_web\Views\Home\Chat.cshtml"
WriteAttributeValue("", 797, mensajes, 797, 9, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(807, 111, true);
                WriteLiteral(" />\r\n        <button class=\"btn btn-primary\" type=\"submit\" onclick=\"/Home/Enviar\">Enviar</button>\r\n    </div>\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(925, 9, true);
            WriteLiteral("\r\n<div>\r\n");
            EndContext();
#line 33 "C:\Users\INSPIRON 5577\Desktop\Git\Transito\trafico\SIGERSIV_web\SIGERSIV_web\Views\Home\Chat.cshtml"
      
        foreach (String men in mensajes)
        {
            if (men != "" || men != null)
            {

#line default
#line hidden
            BeginContext(1053, 23, true);
            WriteLiteral("                <label>");
            EndContext();
            BeginContext(1077, 3, false);
#line 38 "C:\Users\INSPIRON 5577\Desktop\Git\Transito\trafico\SIGERSIV_web\SIGERSIV_web\Views\Home\Chat.cshtml"
                  Write(men);

#line default
#line hidden
            EndContext();
            BeginContext(1080, 43, true);
            WriteLiteral("</label>\r\n                <label></label>\r\n");
            EndContext();
#line 40 "C:\Users\INSPIRON 5577\Desktop\Git\Transito\trafico\SIGERSIV_web\SIGERSIV_web\Views\Home\Chat.cshtml"
            }
        }
    

#line default
#line hidden
            BeginContext(1156, 87, true);
            WriteLiteral("</div>\r\n<script>\r\n    function enviar() {\r\n        location.reload();\r\n    }\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
