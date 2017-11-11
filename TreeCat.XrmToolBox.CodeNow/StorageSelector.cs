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
    public partial class StorageSelector : Form
    {
        private CodeNowScript selectedScript = null;
        List<Storage.ICodeNowStorage> storageList = null;
        bool saveMode = false;
        public StorageSelector(List<Storage.ICodeNowStorage> storageList, bool saveMode)
        {
            InitializeComponent();
            this.storageList = storageList;
            this.saveMode = saveMode;
            LoadStorageList();
        }

        private void LoadStorageList()
        {
            cbStorage.Items.Clear();
            foreach(var s in storageList)
            {
                if (!saveMode || s.IsSaveSupported)
                {
                    cbStorage.Items.Add(s);
                }
            }
            if(storageList.Count > 0) cbStorage.SelectedIndex = 0;
        }

        public CodeNowScript SelectedScript
        {
            get
            {
                return selectedScript;
            }

        }

        public Storage.ICodeNowStorage SelectedStorage
        {
            get
            {
                if (cbStorage.SelectedItem != null) return (Storage.ICodeNowStorage)cbStorage.SelectedItem;
                else return null;
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //if (SelectedStorage == null) return;
            
        }
    }
}
