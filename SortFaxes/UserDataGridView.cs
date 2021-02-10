using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SortFaxes
{
  public  class UserDataGridView: DataGridView
    {
        private int checkedCount;
        public int CheckedCount { get
            { return GetCheckedCount(); }
            set { checkedCount = value; }
        }
        
        public UserDataGridView()
        {
         //   this.CellContentClick += UserDataGridView_CellClick;        
        }



        public int GetCheckedCount()
        {
            int count = 0;
            foreach (DataGridViewRow row in this.Rows)
            {
                if ((bool)row.Cells[0].EditedFormattedValue) count++;
            }
            return count;
        }


    }
}
