using System.Web.Mvc;

namespace HyperGear.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            /*Feedback*/
            context.MapRoute(
               "Feedback",
               "Admin/FeedBack",
               new { Controller = "FeedBack", action = "FeedBack", id = UrlParameter.Optional }
           );
            context.MapRoute(
               "FeedbackRep",
               "Admin/FeedBackRep",
               new { Controller = "FeedBack", action = "Reply", id = UrlParameter.Optional }
           );
            context.MapRoute(
               "FeedbackDet",
               "Admin/FeedBackDet",
               new { Controller = "FeedBack", action = "Details", id = UrlParameter.Optional }
           );
            /*List Order*/
            context.MapRoute(
               "LstOrder",
               "Admin/ListOrder",
               new { Controller = "Order", action = "ListOrder", id = UrlParameter.Optional }
           );

            context.MapRoute(
               "OrderDetails",
               "Admin/OrderDetails",
               new { Controller = "Order", action = "Details", id = UrlParameter.Optional }
           );
            /* List User*/
            context.MapRoute(
                "LstUserAd",
                "Admin/ListUserAd",
                new { Controller = "User", action = "ListUserAdmin", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "ListCus",
                "Admin/Customer",
                new { Controller = "User", action = "ListCustomer", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "UserAdCre",
                "Admin/UserAdCre",
                new { Controller = "User", action = "AdminRegister", id = UrlParameter.Optional }
            );

            /* Liên hệ*/
            context.MapRoute(
                "Contact",
                "Admin/Contact",
                new { Controller = "Contact", action = "Contact", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "ContactCreate",
                "Admin/ContCreate",
                new { Controller = "Contact", action = "Create", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "ContactEdit",
                "Admin/ContEdit",
                new { Controller = "Contact", action = "Edit", id = UrlParameter.Optional }
            );



            /* Menu*/
            context.MapRoute(
                "Menu",
                "Admin/Menu",
                new { Controller = "Menu", action = "MenuList", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "MenuCreate",
                "Admin/MenuCreate",
                new { Controller = "Menu", action = "Create", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "MenuEdit",
                "Admin/MenuEdit",
                new { Controller = "Menu", action = "Edit", id = UrlParameter.Optional }
            );

            /* Danh Mục*/
            context.MapRoute(
                "Category",
                "Admin/Category",
                new { Controller = "Category", action = "CategoryList", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "CateCreate",
                "Admin/CateCreate",
                new { Controller = "Category", action = "Create", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "CateEdit",
                "Admin/CateEdit",
                new { Controller = "Category", action = "Edit", id = UrlParameter.Optional }
            );

            /* Sản Phẩm*/
            context.MapRoute(
                "Product",
                "Admin/Product",
                new { Controller = "Product", action = "List", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "ProdCreate",
                "Admin/ProdCreate",
                new { Controller = "Product", action = "Create", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "ProdEdit",
                "Admin/ProdEdit",
                new { Controller = "Product", action = "Edit", id = UrlParameter.Optional }
            );

            /* Login*/
            context.MapRoute(
                "AuthLogin",
                "Admin/Login",
                new { Controller = "Auth", action = "Login", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { Controller ="Admin", action = "Dashboard", id = UrlParameter.Optional }
            );
        }
    }
}