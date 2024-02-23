using System.Collections.Generic;
using System;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Account.Models
{
    public class TreeNode
    {
        public string ParentID { get; set; }
        public string name { get; set; }
        public string userId { get; set; }
        public string Left { get;set; }
        public string Right { get; set; }
        public List<TreeNode> Children { get; set; }
        public string subscriptiontype { get; set; }

        public byte[] profilepicture { get; set; }

        public string country { get; set; } = "";
        public string sponsor { get; set; } = "";
    }

    public class BinaryTree
    {
        public static List<TreeNode> FlatToHierarchy(List<TreeNode> list)
        {

            var lookup = new Dictionary<string, TreeNode>();
            var nested = new List<TreeNode>();

            foreach (TreeNode item in list)
            {
                if (lookup.ContainsKey(item.ParentID))
                {
                    var childs = lookup[item.ParentID].Children;
                    // add to the parent's child list 
                    if (childs == null)
                    {
                        lookup[item.ParentID].Children = new List<TreeNode>();
                        lookup[item.ParentID].Children.Add(item);
                    }
                    else
                    {
                        lookup[item.ParentID].Children.Add(item);
                    }
                }
                else
                {
                    // no parent added yet (or this is the first time)
                    nested.Add(item);
                }
                if (!lookup.ContainsKey(item.userId))
                {
                    lookup.Add(item.userId.ToString(), item);
                }
            }

            return nested;
        }
    }
}
