using SSO.Lib.VO.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archer.SSO.helpclass
{
    public class PublicMethods
    {
        public static void loadMenuWithMenuStrip(MenuStrip pMS, List<MenuVO> pLst)
        {
            var topMenus = from m in pLst
                         where m.ParentID == null
                         select m;
            foreach (var item in topMenus)//顶层菜单
            {
                ToolStripMenuItem topMenu = new ToolStripMenuItem();
                topMenu.Name = item.ID.ToString();
                topMenu.Text = item.Name;
                //加载子菜单项
                loadSubMenuInfo(ref topMenu,item.ID,pLst);

                pMS.MdiWindowListItem = topMenu;
                pMS.Items.Add(topMenu);
            }
        }

        private static void loadSubMenuInfo(ref ToolStripMenuItem pTopMenu, Guid pMenuId, List<MenuVO> pLst)
        {
            var items = from m in pLst
                        where m.ParentID == pMenuId
                        select m;
            foreach (var item in items)
            {
                ToolStripMenuItem subMenu = new ToolStripMenuItem();
                subMenu.Name = item.ID.ToString();
                subMenu.Text = item.Name;
                //递归加载菜单项
                loadSubMenuInfo(ref subMenu, item.ID, pLst);

                subMenu.Tag = item.CmdForm;
                subMenu.Click += (s1,e1) => {
                    createFormInstance(item.CmdForm);
                };

                pTopMenu.DropDownItems.Add(subMenu);
            }
        }

        private static void createFormInstance(string pFormName)
        {
            bool flag = false;
            //遍历主窗口上的所有子菜单
            for (int i = 0; i < PublicInfo.mainForm.MdiChildren.Length; i++)
            {
                //如果所点击的窗口被打开则重新激活
                if (PublicInfo.mainForm.MdiChildren[i].Tag.ToString().ToLower() == pFormName.ToLower())
                {
                    PublicInfo.mainForm.MdiChildren[i].Activate();
                    PublicInfo.mainForm.MdiChildren[i].Show();
                    PublicInfo.mainForm.MdiChildren[i].WindowState = FormWindowState.Maximized;
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                try
                {
                    //如果不存在则用反射创建form窗体实例
                    Assembly asm = Assembly.Load("Archer.SSO");//程序集名
                    object frmObj = asm.CreateInstance("Archer.SSO." + pFormName);//程序集+form的类名
                    Form frms = (Form)frmObj;
                    //tag属性要重新写一次，否则在第二次的时候取不到
                    frms.Tag = pFormName;
                    frms.MdiParent = PublicInfo.mainForm;
                    frms.Show();
                    frms.WindowState = FormWindowState.Maximized;

                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show(string.Format("不存在对象名：{0}", pFormName));
                }
            }
        }
   

        public static void loadDepartmentWithTreeView(TreeView pTV,List<DepartmentVO> pLst)
        {
            pTV.Nodes.Clear();
            var topDept = from m in pLst
                          where m.ParentID == null
                          select m;
            foreach (var item in topDept)//顶层部门
            {
                TreeNode topTN = new TreeNode();
                topTN.Name = item.ID.ToString();
                topTN.Text = item.Name;
                topTN.ImageIndex = item.Status.Equals("0") ? 1 : 0;
                topTN.Tag = item;
                //加载子节点
                loadSubNode(ref topTN, item.ID, pLst);

                pTV.Nodes.Add(topTN);
            }
            pTV.ExpandAll();
        }

        private static void loadSubNode(ref TreeNode pTopNode, Guid pNodeId, List<DepartmentVO> pLst)
        {
            var items = from m in pLst
                        where m.ParentID == pNodeId
                        select m;
            foreach (var item in items)
            {
                TreeNode subNode = new TreeNode();
                subNode.Name = item.ID.ToString();
                subNode.Text = item.Name;
                subNode.ImageIndex = item.Status.Equals("0") ? 1 : 0;
                subNode.Tag = item;
                //递归加载节点
                loadSubNode(ref subNode, item.ID, pLst);

                pTopNode.Nodes.Add(subNode);
            }
        }


        public static void loadRoleWithTreeView(TreeView pTV, List<RoleVO> pLst)
        {
            pTV.Nodes.Clear();
            var topDept = from m in pLst
                          where m.ParentID == null
                          select m;
            foreach (var item in topDept)//顶层角色
            {
                TreeNode topTN = new TreeNode();
                topTN.Name = item.ID.ToString();
                topTN.Text =  item.Name ;
                topTN.ImageIndex = item.Status.Equals("0") ? 1 : 0;
                topTN.Tag = item;
                //加载子节点
                loadSubNode(ref topTN, item.ID, pLst);

                pTV.Nodes.Add(topTN);
            }
            pTV.ExpandAll();
        }

        private static void loadSubNode(ref TreeNode pTopNode, Guid pNodeId, List<RoleVO> pLst)
        {
            var items = from m in pLst
                        where m.ParentID == pNodeId
                        select m;
            foreach (var item in items)
            {
                TreeNode subNode = new TreeNode();
                subNode.Name = item.ID.ToString();
                subNode.Text = item.Name;
                subNode.ImageIndex = item.Status.Equals("0") ? 1 : 0;
                subNode.Tag = item;
                //递归加载节点
                loadSubNode(ref subNode, item.ID, pLst);

                pTopNode.Nodes.Add(subNode);
            }
        }


        public static void loadMenuWithTreeView(TreeView pTV, List<MenuVO> pLst)
        {
            pTV.Nodes.Clear();
            var topDept = from m in pLst
                          where m.ParentID == null
                          select m;
            foreach (var item in topDept)//顶层菜单
            {
                TreeNode topTN = new TreeNode();
                topTN.Name = item.ID.ToString();
                topTN.Text = item.Name;
                topTN.ImageIndex = item.Status.Equals("0") ? 1 : 0;
                topTN.Tag = item;
                //加载子节点
                loadSubNode(ref topTN, item.ID, pLst);

                pTV.Nodes.Add(topTN);
            }
            pTV.ExpandAll();
        }

        private static void loadSubNode(ref TreeNode pTopNode, Guid pNodeId, List<MenuVO> pLst)
        {
            var items = from m in pLst
                        where m.ParentID == pNodeId
                        select m;
            foreach (var item in items)
            {
                TreeNode subNode = new TreeNode();
                subNode.Name = item.ID.ToString();
                subNode.Text = item.Name;
                subNode.ImageIndex = item.Status.Equals("0") ? 1 : 0;
                subNode.Tag = item;
                //递归加载节点
                loadSubNode(ref subNode, item.ID, pLst);

                pTopNode.Nodes.Add(subNode);
            }
        }
    

        public static void bindWithTreeView(TreeView pTV,List<RoleRVO> pLst)
        {
            foreach (TreeNode tn in pTV.Nodes)
            {
                printRecursive(tn,pLst);
            }
        }

        private static void printRecursive(TreeNode pTn,List<RoleRVO> pLst)
        {
            var result = from m in pLst
                         //where m.RName.Equals(pTn.Text)
                         where m.RID.Equals(Guid.Parse(pTn.Name))
                         //|| m.RoleID.Equals(Guid.Parse(pTn.Name))
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
                printRecursive(tn,pLst);
            }
        }


        /// <summary>
        /// 向上遍历节点
        /// </summary>
        /// <param name="pTn"></param>
        /// <param name="pFlag"></param>
        public static void tvUpCheck(TreeNode pTn, bool pFlag,Guid pRID)
        {
            if (pTn != null)
            {
                if (pFlag)
                {
                    pTn.Checked = pFlag;
                    tvUpCheck(pTn.Parent, pFlag,pRID);

                    //保存到数据库
                    PublicInfo.basicImpl.saveRID2Role(pRID, Guid.Parse(pTn.Name), pTn.Text);
                }
                else
                {
                    pTn.Checked = pFlag;
                    //从数据表删除
                    PublicInfo.basicImpl.removeRID2Role(pRID, Guid.Parse(pTn.Name));

                    if (pTn.Parent != null)
                    {
                        bool isCheck = false;
                        foreach (TreeNode tn in pTn.Parent.Nodes)
                        {
                            if (tn.Checked)
                            {
                                isCheck = true;
                                break;
                            }
                        }
                        if (!isCheck)
                        {
                            tvUpCheck(pTn.Parent, pFlag,pRID);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 向下遍历节点
        /// </summary>
        /// <param name="pTn"></param>
        /// <param name="pFlag"></param>
        public static void tvDownCheck(TreeNode pTn, bool pFlag,Guid pRID)
        {
            if (pTn.Nodes.Count > 0)
            {
                if (pFlag)
                {
                    //保存到数据表
                    PublicInfo.basicImpl.saveRID2Role(pRID, Guid.Parse(pTn.Name), pTn.Text);
                }
                foreach (TreeNode tn in pTn.Nodes)
                {
                    tn.Checked = pFlag;
                    tvDownCheck(tn, pFlag,pRID);
                    if (!pFlag)
                    {
                        //从数据表删除
                        PublicInfo.basicImpl.removeRID2Role(pRID, Guid.Parse(pTn.Name));
                    }
                }
            }
            else
            {
                if (pFlag)
                {
                    //保存到数据表
                    PublicInfo.basicImpl.saveRID2Role(pRID, Guid.Parse(pTn.Name), pTn.Text);
                }
                else
                {
                    pTn.Checked = pFlag;
                    //从数据表删除
                    PublicInfo.basicImpl.removeRID2Role(pRID, Guid.Parse(pTn.Name));
                }
            }
        }
    
    }
}
