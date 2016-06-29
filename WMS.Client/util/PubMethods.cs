using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WMS.Client.BasicSvr;
using WMS.Client.SysInfoSvr;

namespace WMS.Client.util
{
    public class PubMethods
    {
        public static void loadMenuWithMenuStrip(MenuStrip pMS, List<SysMenuVO> pLst)
        {
            var topMenus = from m in pLst
                           where m.ParentID == null
                           select m;
            foreach (var item in topMenus)//顶层菜单
            {
                ToolStripMenuItem topMenu = new ToolStripMenuItem();
                topMenu.Name = item.ID.ToString();
                topMenu.Text = item.MenuName;
                if (!string.IsNullOrEmpty(item.Icon))
                {
                    topMenu.Image = Image.FromFile(item.Icon);
                }
                //加载子菜单项
                loadSubMenuInfo(ref topMenu, item.ID, pLst);

                pMS.MdiWindowListItem = topMenu;
                pMS.Items.Add(topMenu);
            }
        }

        private static void loadSubMenuInfo(ref ToolStripMenuItem pTopMenu, int pMenuId, List<SysMenuVO> pLst)
        {
            var items = from m in pLst
                        where m.ParentID == pMenuId
                        select m;
            foreach (var item in items)
            {
                ToolStripMenuItem subMenu = new ToolStripMenuItem();
                subMenu.Name = item.ID.ToString();
                subMenu.Text = item.MenuName;
                if (!string.IsNullOrEmpty(item.Icon))
                {
                    subMenu.Image = Image.FromFile(item.Icon);
                }
                //递归加载菜单项
                loadSubMenuInfo(ref subMenu, item.ID, pLst);

                subMenu.Tag = item.CmdForm;
                if (!subMenu.HasDropDownItems)
                {
                    subMenu.Click += (s1, e1) =>
                    {
                        createFormInstance(item.CmdForm);
                    };
                }
                pTopMenu.DropDownItems.Add(subMenu);
            }
        }

        private static void createFormInstance(string pFormName)
        {
            bool flag = false;
            //遍历主窗口上的所有子菜单
            for (int i = 0; i < PubInfoUtil.mainForm.MdiChildren.Length; i++)
            {
                //如果所点击的窗口被打开则重新激活
                if (PubInfoUtil.mainForm.MdiChildren[i].Tag.ToString().ToLower() == pFormName.ToLower())
                {
                    PubInfoUtil.mainForm.MdiChildren[i].Activate();
                    PubInfoUtil.mainForm.MdiChildren[i].Show();
                    PubInfoUtil.mainForm.MdiChildren[i].WindowState = FormWindowState.Normal;
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                try
                {
                    //如果不存在则用反射创建form窗体实例
                    Assembly asm = Assembly.Load("WMS.Client");//程序集名
                    object frmObj = asm.CreateInstance("WMS.Client." + pFormName);//程序集+form的类名
                    Form frms = (Form)frmObj;
                    //tag属性要重新写一次，否则在第二次的时候取不到
                    frms.Tag = pFormName;
                    frms.MdiParent = PubInfoUtil.mainForm;
                    frms.Show();
                    frms.WindowState = FormWindowState.Normal;

                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show(string.Format("不存在对象名：{0}", pFormName));
                }
            }
        }

        public static void loadMenuWithTreeView(TreeView pTV, List<SysMenuVO> pLst)
        {
            pTV.Nodes.Clear();
            var topDept = from m in pLst
                          where m.ParentID == null
                          select m;
            foreach (var item in topDept)//顶层菜单
            {
                TreeNode topTN = new TreeNode();
                topTN.Name = item.ID.ToString();
                topTN.Text = item.MenuName;
                topTN.ImageIndex = item.Status==0 ? 1 : 0;
                topTN.Tag = item;
                //加载子节点
                loadSubNode(ref topTN, item.ID, pLst);

                pTV.Nodes.Add(topTN);
            }
            pTV.ExpandAll();
        }

        private static void loadSubNode(ref TreeNode pTopNode, int pNodeId, List<SysMenuVO> pLst)
        {
            var items = from m in pLst
                        where m.ParentID == pNodeId
                        select m;
            foreach (var item in items)
            {
                TreeNode subNode = new TreeNode();
                subNode.Name = item.ID.ToString();
                subNode.Text = item.MenuName;
                subNode.ImageIndex = item.Status==0 ? 1 : 0;
                subNode.Tag = item;
                //递归加载节点
                loadSubNode(ref subNode, item.ID, pLst);

                pTopNode.Nodes.Add(subNode);
            }
        }

        public static void loadStructureWithTreeView(TreeView pTV, List<SysStructureVO> pLst)
        {
            pTV.Nodes.Clear();
            var topDept = from m in pLst
                          where m.ParentID == null
                          select m;
            foreach (var item in topDept)//顶层组织
            {
                TreeNode topTN = new TreeNode();
                topTN.Name = item.ID.ToString();
                topTN.Text = item.StructureName;
                topTN.ImageIndex = item.Status==0 ? 1 : 0;
                topTN.Tag = item;
                //加载子节点
                loadSubNode(ref topTN, item.ID, pLst);

                pTV.Nodes.Add(topTN);
            }
            pTV.ExpandAll();
        }

        private static void loadSubNode(ref TreeNode pTopNode, int pNodeId, List<SysStructureVO> pLst)
        {
            var items = from m in pLst
                        where m.ParentID == pNodeId
                        select m;
            foreach (var item in items)
            {
                TreeNode subNode = new TreeNode();
                subNode.Name = item.ID.ToString();
                subNode.Text = item.StructureName;
                subNode.ImageIndex = item.Status==0 ? 1 : 0;
                subNode.Tag = item;
                //递归加载节点
                loadSubNode(ref subNode, item.ID, pLst);

                pTopNode.Nodes.Add(subNode);
            }
        }

        public static void loadRoleWithTreeView(TreeView pTV, List<SysRoleVO> pLst)
        {
            pTV.Nodes.Clear();
            var topDept = from m in pLst
                          select m;
            foreach (var item in topDept)//顶层角色
            {
                TreeNode topTN = new TreeNode();
                topTN.Name = item.ID.ToString();
                topTN.Text = item.RoleName;
                topTN.ImageIndex = item.Status==0 ? 1 : 0;
                topTN.Tag = item;

                pTV.Nodes.Add(topTN);
            }
            pTV.ExpandAll();
        }

        public static void bindWithTreeView(TreeView pTV, List<SysRoleMenuVO> pLst)
        {
            foreach (TreeNode tn in pTV.Nodes)
            {
                printRecursive(tn, pLst);
            }
        }

        private static void printRecursive(TreeNode pTn, List<SysRoleMenuVO> pLst)
        {
            var result = from m in pLst
                         where m.MenuID ==Int32.Parse(pTn.Name)
                         select m;
            if (result.Count() > 0)
            {
                pTn.Checked = true;
            }
            else
            {
                pTn.Checked = false;
            }

            foreach (TreeNode tn in pTn.Nodes)
            {
                printRecursive(tn, pLst);
            }
        }

        public static void bindWithTreeView(TreeView pTV,List<SysRoleStructureVO> pLst)
        {
            foreach (TreeNode tn in pTV.Nodes)
            {
                printRecursive(tn, pLst);
            }
        }

        private static void printRecursive(TreeNode pTn, List<SysRoleStructureVO> pLst)
        {
            var result = from m in pLst
                         where m.RoleID == Int32.Parse(pTn.Name)
                         select m;
            if (result.Count() > 0)
            {
                pTn.Checked = true;
            }
            else
            {
                pTn.Checked = false;
            }

            foreach (TreeNode tn in pTn.Nodes)
            {
                printRecursive(tn, pLst);
            }
        }

        public static void bindWithTreeView(TreeView pTV, List<SysRoleAccountVO> pLst)
        {
            foreach (TreeNode tn in pTV.Nodes)
            {
                printRecursive(tn, pLst);
            }
        }

        private static void printRecursive(TreeNode pTn, List<SysRoleAccountVO> pLst)
        {
            var result = from m in pLst
                         where m.RoleID == Int32.Parse(pTn.Name)
                         select m;
            if (result.Count() > 0)
            {
                pTn.Checked = true;
            }
            else
            {
                pTn.Checked = false;
            }

            foreach (TreeNode tn in pTn.Nodes)
            {
                printRecursive(tn, pLst);
            }
        }

        public static void loadWarehouseWithTreeView(TreeView pTV, List<WarehouseVO> pLst)
        {
            pTV.Nodes.Clear();
            var topDept = from m in pLst
                          where m.ParentID == null
                          select m;
            foreach (var item in topDept)//顶层组织
            {
                TreeNode topTN = new TreeNode();
                topTN.Name = item.ID.ToString();
                topTN.Text = item.WareName;
                topTN.ImageIndex = item.Status == 0 ? 1 : 0;
                topTN.Tag = item;
                //加载子节点
                loadSubNode(ref topTN, item.ID, pLst);

                pTV.Nodes.Add(topTN);
            }
            pTV.ExpandAll();
        }

        private static void loadSubNode(ref TreeNode pTopNode, int pNodeId, List<WarehouseVO> pLst)
        {
            var items = from m in pLst
                        where m.ParentID == pNodeId
                        select m;
            foreach (var item in items)
            {
                TreeNode subNode = new TreeNode();
                subNode.Name = item.ID.ToString();
                subNode.Text = item.WareName;
                subNode.ImageIndex = item.Status == 0 ? 1 : 0;
                subNode.Tag = item;
                //递归加载节点
                loadSubNode(ref subNode, item.ID, pLst);

                pTopNode.Nodes.Add(subNode);
            }
        }

        public static void loadItemCategoryWithTreeView(TreeView pTV, List<ItemCategoryVO> pLst)
        {
            pTV.Nodes.Clear();
            var topDept = from m in pLst
                          where m.ParentID == null
                          select m;
            foreach (var item in topDept)//顶层组织
            {
                TreeNode topTN = new TreeNode();
                topTN.Name = item.ID.ToString();
                topTN.Text = item.ItemCategoryName;
                topTN.ImageIndex = item.Status == 0 ? 1 : 0;
                topTN.Tag = item;
                //加载子节点
                loadSubNode(ref topTN, item.ID, pLst);

                pTV.Nodes.Add(topTN);
            }
            pTV.ExpandAll();
        }

        private static void loadSubNode(ref TreeNode pTopNode, int pNodeId, List<ItemCategoryVO> pLst)
        {
            var items = from m in pLst
                        where m.ParentID == pNodeId
                        select m;
            foreach (var item in items)
            {
                TreeNode subNode = new TreeNode();
                subNode.Name = item.ID.ToString();
                subNode.Text = item.ItemCategoryName;
                subNode.ImageIndex = item.Status == 0 ? 1 : 0;
                subNode.Tag = item;
                //递归加载节点
                loadSubNode(ref subNode, item.ID, pLst);

                pTopNode.Nodes.Add(subNode);
            }
        }



    }
}
