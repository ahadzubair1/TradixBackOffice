using AspNetCoreHero.Boilerplate.Application.DTOs.Networks;
using AspNetCoreHero.Boilerplate.Application.DTOs.Settings;
using AspNetCoreHero.Boilerplate.Application.Features.Networks.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Account.Controllers
{
    public class NetworkTreeNode
    {
        public string id { get; set; }
        public string name { get; set; }
        public string parent { get; set; }
        public string position { get; set; }

        public string subscriptiontype { get; set; }

        public byte[] profilepicture { get; set; }

        public string country { get; set; } = "";
        public string sponsor { get; set; } = "";
    }


    public class NetworkTreeViewModel
    {
        public List<NetworkTreeNode> Nodes { get; set; }
        public string Html { get; set; }

        public string Farleft { get; set; }
        public string FarRight { get; set; }
    }


    [Area("Account")]
    public class NetworksController : BaseController<NetworksController>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly NetworkTreeSettings _settings;

        public NetworksController(IHttpContextAccessor httpContextAccessor, IOptions<NetworkTreeSettings> settings)
        {
            _httpContextAccessor = httpContextAccessor;
            _settings = settings.Value;
        }

        private string FarLeft(List<NetworkTreeNode> nodes, string parentId, bool isRoot = true, int iteration = 1)
        {
            var parentNode = nodes.Find(node => node.id == parentId);
            if (parentNode != null)
            {
                var children = nodes.Where(node => node.parent == parentId).OrderBy(x => x.position).ToList();
                if (children.Count == 0)
                {
                    return parentNode.id;
                }
                var leftNode = children.FirstOrDefault(node => node.position == "left");
                var rightNode = children.FirstOrDefault(node => node.position == "right");
                if (leftNode != null)
                {
                    // Left node exists, continue searching for the far-left node
                    return FarLeft(nodes, leftNode.id, false, iteration++);
                }
                else if (rightNode != null && iteration > 1)
                {
                    return FarLeft(nodes, rightNode.id, false, iteration++);
                }
            }

            return parentId;

        }

        private string FarRight(List<NetworkTreeNode> nodes, string parentId, bool isRoot = true, int iteration = 1)
        {
            var parentNode = nodes.Find(node => node.id == parentId);
            if (parentNode != null)
            {
                var children = nodes.Where(node => node.parent == parentId).OrderBy(x => x.position).ToList();
                if (children.Count == 0)
                {
                    return parentNode.id;
                }
                var leftNode = children.FirstOrDefault(node => node.position == "left");
                var rightNode = children.FirstOrDefault(node => node.position == "right");
                if (rightNode != null)
                {
                    // Left node exists, continue searching for the far-left node
                    return FarRight(nodes, rightNode.id, false, iteration++);
                }
                else if (leftNode != null && iteration > 1)
                {
                    return FarLeft(nodes, leftNode.id, false, iteration++);
                }
            }

            return parentId;

        }

        private void GenerateTreeView(StringBuilder sb, List<NetworkTreeNode> nodes, string parentId, bool isRoot = true, int depth = 1)
        {



            var parentNode = nodes.Find(node => node.id.ToLower() == parentId.ToLower());

            if (parentNode != null)
            {
                var children = nodes.FindAll(node => node.parent.ToLower() == parentId.ToLower()).OrderBy(x => x.position).ToList();

                if (depth < _settings.Depth)
                {
                    /*if (children.Count == 0)
                    {
                        children.Add(new NetworkTreeNode
                        {
                            position = "left",
                            name = "No User",
                            parent = parentId,
                            id = "",
                            subscriptiontype = "NA",
                            country="",
                            sponsor="",
                           
                           
                        });
                        children.Add(new NetworkTreeNode
                        {
                            position = "right",
                            name = "No User",
                            parent = parentId,
                            id = "",
                            subscriptiontype = "NA",
                            country = "",
                            sponsor = "",

                        });
                    }*/
                }

                //if (!isRoot)
                //{
                if (children.Count == 1)
                {
                    if (children[0].position.Equals("left"))
                    {

                        children.Add(new NetworkTreeNode
                        {

                            position = "right",
                            name = "No User",
                            parent = children[0].parent,
                            id = "",
                            subscriptiontype = "NA",
                            country = "",
                            sponsor = "",




                        });
                    }
                    else
                    {
                        children.Add(children[0]);
                        children[0] = new NetworkTreeNode
                        {
                            position = "left",
                            name = "No User",
                            parent = children[0].parent,
                            id = "",
                            subscriptiontype = "NA",
                            country = "",
                            sponsor = "",

                        };
                    }
                }
                //}


                if (isRoot)
                {


                    // draw the root node
                    //sb.AppendLine($"<li><a href=\"/account/networks?id={parentNode.id}\" class=\"rank-gold\"> <img class=\"user-rank\" src=\"/images/ranks/rank-gold.png\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Gold\"><img class=\"user-avatar\" src=\"/images/teamwork.gif\"> <span class=\"user-name\">{parentNode.name}</span><span>Subscription: {parentNode.subscriptiontype}</span></a>");
                    //sb.AppendLine($"<li><a href=\"/account/networks?id={parentNode.id}\" class=\"rank-gold\"> <img class=\"user-rank\" src=\"/images/ranks/rank-gold.png\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Gold\"><img class=\"user-avatar\" src=\"/images/teamwork.gif\"> <span class=\"user-name\">{parentNode.name}</span><span>Subscription: {parentNode.subscriptiontype}</span></a>");

                    //      sb.AppendLine($"<li><a href=\"/account/networks?id={parentNode.id}\" class=\"{string.Join("", parentNode.subscriptiontype.Split(' ')).ToLower() }\"> <img class=\"user-rank\" src=\"/images/ranks/rank-gold.png\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Gold\">" +
                    //$"<img class=\"user-avatar\" src=\"data:image/*;base64,{Convert.ToBase64String(parentNode.profilepicture ?? new byte[0])}\">" +
                    //$"<span class=\"user-name\">{parentNode.name}</span>" +
                    //$"<span>Subscription: {parentNode.subscriptiontype}</span></a>");

                    sb.AppendLine($"<li><a href=\"/account/networks?id={parentNode.id}\" class=\"{string.Join("", (parentNode.subscriptiontype ?? "NA").Split(' ')).ToLower()}\">" +
              $"<img class=\"user-rank\" src=\"/images/ranks/rank-gold.png\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Gold\">" +
              $"<img class=\"user-avatar\" src=\"data:image/*;base64,{Convert.ToBase64String(parentNode.profilepicture ?? new byte[0])}\">" +
              $"<span class=\"user-name\">{parentNode.name}</span>" +
              $"<span class=\"node-detail\"><label>Sponsor: {parentNode.sponsor}</label><label>Country: {parentNode.country}</label><label>Subscriptions: {parentNode.subscriptiontype ?? "N/A"}</label></span>" +
              $"</a>");



                    // sb.AppendLine("<img id=\"viewableImage\" alt=\"Profile Picture of @Model.Username\" width=\"200\" height=\"200\" class=\"img-circle img-bordered\" style=\"object-fit:cover\" src=\"data:image/*;base64,"+Convert.ToBase64String(parentNode.profilepicture)+"\">\r\n");



                    sb.AppendLine("<ul class=\"child-node\">");
                    foreach (var child in children)
                    {

                        //sb.AppendLine($"<li><a href=\"/account/networks?id={child.id}\" class=\"rank-silver\"><img class=\"user-rank\" src=\"/images/ranks/rank-silver.png\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Silver\"><img class=\"user-avatar\" src=\"/images/teamwork.gif\"><span class=\"user-name\">{child.name}</span><span>Subscription: {child.subscriptiontype}</span></a>");
                        // sb.AppendLine($"<li><a href=\"/account/networks?id={child.id}\" class=\"{string.Join("", child.subscriptiontype.Split(' '))}\"><img class=\"user-rank\" src=\"/images/ranks/rank-silver.png\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Silver\"><img class=\"user-avatar\" src=\"data:image/*;base64,{Convert.ToBase64String(child.profilepicture)}\"><span class=\"user-name\">{child.name}</span><span>Subscription: {child.subscriptiontype}</span></a>");
                        sb.AppendLine($"<li><a href=\"/account/networks?id={child.id}\" class=\"{string.Join("", (child.subscriptiontype ?? "NA").Split(' ')).ToLower()}\"><img class=\"user-rank\" src=\"/images/ranks/rank-silver.png\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Silver\"><img class=\"user-avatar\" src=\"data:image/*;base64,{Convert.ToBase64String(child.profilepicture ?? new byte[0])}\"><span class=\"user-name\">{child.name}</span>" +
                            $"<span class=\"node-detail\"><label>Sponsor: {child.sponsor}</label><label>Country: {child.country}</label><label>Subscriptions: {parentNode.subscriptiontype ?? "N/A"}</label></span>" +
                            $"</a>");



                        if (nodes.FindAll(node => node.parent == child.id).Count > 0)
                        {

                            sb.AppendLine("<ul>");

                            GenerateTreeView(sb, nodes, child.id, false, depth);
                            sb.AppendLine("</ul>");
                        }
                        sb.AppendLine("</li>");
                    }
                    sb.AppendLine("</ul></li>");

                }
                else
                {
                    foreach (var child in children)
                    {
                        //sb.AppendLine($"<li><a href=\"#\">{child.name} ({child.position})</a>");
                        //sb.AppendLine("<ul>");

                        //if (child.id== "")
                        //    sb.AppendLine($"<li><a href=\"/account/networks?id={child.id}\" class=\"rank-silver\"><img class=\"user-rank\" src=\"/images/ranks/rank-silver.png\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Silver\"><img class=\"user-avatar\" src=\"/images/teamwork.gif\"><span class=\"user-name\">{child.name}</span><span>Subscription: Premium</span></a>");
                        //else
                        //{
                        //    sb.AppendLine($"<li><a href=\"/account/networks?id={child.id}\" class=\"rank-silver\"><img class=\"user-rank\" src=\"/images/ranks/rank-silver.png\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Silver\"><img class=\"user-avatar\" src=\"data:image/*;base64,{Convert.ToBase64String(child.profilepicture)}\"><span class=\"user-name\">{child.name}</span><span>Subscription: Premium</span></a>");
                        //}

                        sb.AppendLine($"<li><a href=\"/account/networks?id={child.id}\" class=\"{string.Join("", (child.subscriptiontype ?? string.Empty).Split(' ')).ToLower()}\">" +
              $"<img class=\"user-rank\" src=\"/images/ranks/rank-silver.png\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Silver\">" +
              $"<img class=\"user-avatar\" src={(child.id == "" ? "/images/teamwork.gif" : $"data:image/*;base64,{Convert.ToBase64String(child.profilepicture ?? new byte[0])}")}" +
              $"><span class=\"user-name\">{child.name}</span>" +
              $"<span class=\"node-detail\"><label>Sponsor: {child.sponsor}</label><label>Country: {child.country}</label><label>Subscriptions: {parentNode.subscriptiontype ?? "N/A"}</label></span>" +
              $"</a>");

                        //sb.AppendLine($"<li><a href=\"/account/networks?id={child.id}\" class=\"rank-silver\"><img class=\"user-rank\" src=\"/images/ranks/rank-silver.png\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Silver\"><img class=\"user-avatar\" src=\"data:image/*;base64,{Convert.ToBase64String(child.profilepicture)}\"><span class=\"user-name\">{child.name}</span><span>Subscription: Premium</span></a>");



                        if (nodes.FindAll(node => node.parent == child.id).Count > 0)
                        {
                            sb.AppendLine("<ul>");
                            GenerateTreeView(sb, nodes, child.id, false, depth);
                            sb.AppendLine("</ul>");
                        }
                        sb.AppendLine("</li>");
                    }
                }
            }



        }



        private string GenerateHtml(List<NetworkTreeNode> nodes, string rootId, int depth = 1)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<div class=\"tree\">");
            sb.AppendLine("<ul class=\"parent-node\">");
            depth++;
            GenerateTreeView(sb, nodes, rootId, true, depth);
            sb.AppendLine("</ul>");
            sb.AppendLine("</div>");
            return sb.ToString();
        }
        public IActionResult Index(string id)
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var selectedUserId = string.IsNullOrEmpty(id) ? _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) : id;
            var currentUser_nodes = _mediator.Send(new GetUserDownlineWithDepthViaDapperRequest(Guid.Parse(currentUserId), depth: _settings.Depth)).Result;
            var selectedUser_nodes = _mediator.Send(new GetUserDownlineWithDepthViaDapperRequest(Guid.Parse(selectedUserId), depth: _settings.Depth)).Result;

            NetworkTreeViewModel viewModel = new NetworkTreeViewModel();
            viewModel.Nodes = new List<NetworkTreeNode>();

            foreach (var r in currentUser_nodes)
            {
                viewModel.Nodes.Add(new NetworkTreeNode
                {
                    id = r.UserId.ToString(),
                    name = r.FirstName + ' ' + r.LastName + '(' + r.UserName + ')',
                    parent = (r.ParentUserId.HasValue ? r.ParentUserId.Value.ToString() : ""),
                    position = r.Position.ToString().ToLower(),
                    subscriptiontype = r.SubscriptionType,
                    profilepicture = r.ProfilePicture,
                    country = r.Country,
                    sponsor = r.Sponsor

                });
            }

            string staticHTML = HardCodedTree();


            int depth = 1;
            string html = GenerateHtml(viewModel.Nodes, selectedUserId, depth);

            viewModel.Html = html;

            foreach (var r in currentUser_nodes)
            {
                viewModel.Nodes.Add(new NetworkTreeNode
                {
                    id = r.UserId.ToString(),
                    name = r.FirstName+' '+r.LastName +'('+r.UserName+')',
                    parent = (r.ParentUserId.HasValue ? r.ParentUserId.Value.ToString() : ""),
                    position = r.Position.ToString().ToLower(),
                    subscriptiontype = r.SubscriptionType,
                    country = r.Country,
                    sponsor = r.Sponsor
                });
            }
            viewModel.Farleft = FarLeft(viewModel.Nodes, currentUserId, true, 1);
            viewModel.FarRight = FarRight(viewModel.Nodes, currentUserId, true, 1);


            //viewModel.Nodes.Add(new NetworkTreeNode { id = "1", name = "Parent", parent="0" });
            //viewModel.Nodes.Add(new NetworkTreeNode { id = "2", name = "Child 1", parent = "1" });
            _notify.Information("Hi There!");
            return View(viewModel);
        }

        private static string HardCodedTree()
        {
            return @"<div class=""tree"">
                <ul class=""parent-node"">
                    <li>
                        <a href=""#"" class=""rank-gold"">
                            <img class=""user-rank"" src=""/images/ranks/rank-gold.png"" data-toggle=""tooltip"" data-placement=""top"" title=""Gold"">
                            <img class=""user-avatar"" src=""/images/teamwork.gif"">
                            <span class=""user-name"">John Doe</span>
                            <span>Subscription: Starter</span>
                        </a>
                <ul class=""child-node"">
                    <li>
                    <a href=""#"" class=""rank-silver"">
                            <img class=""user-rank"" src=""/images/ranks/rank-silver.png"" data-toggle=""tooltip"" data-placement=""top"" title=""Silver"">
                            <img class=""user-avatar"" src=""/images/teamwork.gif"">
                            <span class=""user-name"">Arthur Wilson</span>
                            <span>Subscription: Premium</span>
                        </a>
                <ul class=""grandchild-node"">
                    <li>
                        <a href=""#"" class=""rank-elite"">
                            <img class=""user-rank"" src=""/images/ranks/rank-elite.png"" data-toggle=""tooltip"" data-placement=""top"" title=""Elite"">
                            <img class=""user-avatar"" src=""/images/teamwork.gif"">
                            <span class=""user-name"">Jack Seth</span>
                            <span>Subscription: Platinum</span>
                        </a>
                        <ul class=""grandchild-node""></ul>
                    </li>
                    <li>
                        <a href=""#"" class=""rank-star"">
                            <img class=""user-rank"" src=""/images/ranks/rank-star.png"" data-toggle=""tooltip"" data-placement=""top"" title=""Star"">
                            <img class=""user-avatar"" src=""/images/teamwork.gif"">
                            <span class=""user-name"">William Anderson</span>
                            <span>Subscription: Premium</span>
                        </a>
                        <ul class=""grandchild-node""></ul>
                    </li>
                    </ul>
                    </li>
                <li>
                <a href=""#"" class=""rank-gold"">
                            <img class=""user-rank"" src=""/images/ranks/rank-gold.png"" data-toggle=""tooltip"" data-placement=""top"" title=""Gold"">
                            <img class=""user-avatar"" src=""/images/teamwork.gif"">
                            <span class=""user-name"">Oliver Jennet</span>
                            <span>Subscription: Starter</span>
                        </a>
                <ul class=""grandchild-node"">
                <li>
                        <a href=""#"" class=""rank-silver"">
                            <img class=""user-rank"" src=""/images/ranks/rank-silver.png"" data-toggle=""tooltip"" data-placement=""top"" title=""Silver"">
                            <img class=""user-avatar"" src=""/images/teamwork.gif"">
                            <span class=""user-name"">Ellis Peterson</span>
                            <span>Subscription: Platinum</span>
                        </a>
                <ul class=""grandchild-node"">
                </ul>
                </li>
                <li><a href=""#"" class=""rank-elite"">
                            <img class=""user-rank"" src=""/images/ranks/rank-elite.png"" data-toggle=""tooltip"" data-placement=""top"" title=""Elite"">
                            <img class=""user-avatar"" src=""/images/teamwork.gif"">
                            <span class=""user-name"">Herriot John</span>
                            <span>Subscription: Starter</span>
                        </a>
                <ul class=""grandchild-node"">
                </ul>
                </li>
                </ul>
                </li>
                </ul></li>
                </ul>
                </div>";
        }
    }
}