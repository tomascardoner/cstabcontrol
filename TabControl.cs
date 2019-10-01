using System.Collections.Generic;

namespace CardonerSistemas
{
    public partial class TabControl : System.Windows.Forms.TabControl
    {
        private List<string> tabPagesOrder;
        private Dictionary<string, System.Windows.Forms.TabPage> tabPagesHidden = new Dictionary<string, System.Windows.Forms.TabPage>();

        public TabControl()
        {
            InitializeComponent();
        }

        public void HideTabPageByName(string tabPageName)
        {
            // The first time the Hide method is called, save the original order of the TabPages
            if (tabPagesOrder == null)
            {
                tabPagesOrder = new List<string>();
                foreach (System.Windows.Forms.TabPage tabPageCurrent in TabPages)
                {
                    tabPagesOrder.Add(tabPageCurrent.Name);
                }
            }

            if (TabPages.ContainsKey(tabPageName))
            {
                System.Windows.Forms.TabPage tabPageToHide;

                // Get the TabPage object
                tabPageToHide = TabPages[tabPageName];

                // Add the TabPage to the internal List
                tabPagesHidden.Add(tabPageName, tabPageToHide);

                // Remove the TabPage from the TabPages collection of the TabControl
                TabPages.Remove(tabPageToHide);
            }
        }

        public void ShowTabPageByName(string tabPageName)
        {
            if (tabPagesHidden.ContainsKey(tabPageName))
            {
                System.Windows.Forms.TabPage tabPageToShow;

                // Get the TabPage object
                tabPageToShow = tabPagesHidden[tabPageName];

                // Add the TabPage to the TabPages collection of the TabControl
                TabPages.Insert(GetTabPageInsertionPoint(tabPageName), tabPageToShow);

                // Remove the TabPage from the internal List
                tabPagesHidden.Remove(tabPageName);
            }
        }

        private int GetTabPageInsertionPoint(string tabPageName)
        {
            int tabPageIndex;
            System.Windows.Forms.TabPage tabPageCurrent;
            int tabNameIndex;
            string tabNameCurrent;

            for (tabPageIndex = 0; tabPageIndex < TabPages.Count; tabPageIndex++)
            {
                tabPageCurrent = TabPages[tabPageIndex];
                for (tabNameIndex = tabPageIndex; tabNameIndex < tabPagesOrder.Count; tabNameIndex++)
                {
                    tabNameCurrent = tabPagesOrder[tabNameIndex];
                    if (tabNameCurrent == tabPageCurrent.Name)
                    {
                        break;
                    }
                    if (tabNameCurrent == tabPageName)
                    {
                        return tabPageIndex;
                    }
                }
            }
            return tabPageIndex;
        }
    }
}
