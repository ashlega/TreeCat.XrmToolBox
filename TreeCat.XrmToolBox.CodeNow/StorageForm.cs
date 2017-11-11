using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeCat.XrmToolBox.CodeNow
{
    public partial class StorageForm : Form
    {
        
        private Storage.ICodeNowStorage selectedStorage = null;

        public StorageForm(Storage.ICodeNowStorage storage)
        {
            InitializeComponent();
            SelectedStorage = storage;
        }

        private void StorageForm_Load(object sender, EventArgs e)
        {
            
        }

        
        private void LoadScriptList()
        {
            lbScriptList.Items.Clear();
            foreach(var s in SelectedStorage.GetScriptList())
            {
                lbScriptList.Items.Add(s);
            }
            lbScriptList.Sorted = true;
        }

        public CodeNowScript SelectedScript
        {
            get
            {
                if (lbScriptList.SelectedItem != null) return (CodeNowScript)lbScriptList.SelectedItem;
                else return null;
            }
            
        }

        public Storage.ICodeNowStorage SelectedStorage
        {
            get
            {
                return selectedStorage;
            }
            set
            {
                selectedStorage = value;
                LoadScriptList();
            }
        }

        private void lbScriptList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
