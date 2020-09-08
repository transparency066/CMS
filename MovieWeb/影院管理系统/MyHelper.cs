using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class MyHelper
    {
        public static MvcHtmlString ActionLinkWithImage(this HtmlHelper html, string imgSrc, string actionName, string controllerName, object routeValue = null)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);//获取或设置有关视图的上下文信息 //获取或设置请求上下文
            //使用指定的请求上下文初始化 System.Web.Mvc.UrlHelper 类的新实例

            string imgUrl = urlHelper.Content(imgSrc);//将虚拟（相对）路径转换为应用程序绝对路径
            TagBuilder imgTagBuilder = new TagBuilder("img");//创建一个img元素
            imgTagBuilder.MergeAttribute("src", imgUrl);//添加新属性
            string img = imgTagBuilder.ToString(TagRenderMode.SelfClosing);//使用指定模式呈现HTML模式的标记(自结束标记)

            string url = urlHelper.Action(actionName, controllerName, routeValue);
            //使用指定的操作名称、控制器名称和路由值生成操作方法的完全限定 URL

            TagBuilder tagBuilder = new TagBuilder("a")
            {
                InnerHtml = img//设置元素内部html值
            };
            tagBuilder.MergeAttribute("href", url);//添加新属性

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }
    }
}