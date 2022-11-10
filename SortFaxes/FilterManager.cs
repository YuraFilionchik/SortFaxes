/*
 * Создано в SharpDevelop.
 * Пользователь: user
 * Дата: 28.05.2020
 * Время: 14:50
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SortFaxes
{
	/// <summary>
	/// Description of FilterManager.
	/// </summary>
	public partial class FilterManager : Form
	{
		public static bool UseML;
		public FilterManager(bool useML)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			DisplayFilters();
			lbDirs.SelectedIndexChanged+=  selectDir;
			this.FormClosing+= managerFormClosing;
			textBox2.KeyPress+= textBox2_KeyPress;
			label3.Click+= label3_Click;
			checkBox1.Checked = useML;
			UseML = useML;
		}
		
		void DisplayFilters()
	{
		lbDirs.Items.Clear();
			foreach (CONSTS.Filter filter in CONSTS.Filters) {
				if(lbDirs.Items.Contains(filter.directory)) continue;
				lbDirs.Items.Add(filter.directory);
	}
}

		void label3_Click(object sender, EventArgs e)
		{
			string text="Правило срабатывает, если найдено одно из слов в списке фильтра. " +
				"При добавлениии слов через ; (точку с запятой) правило сработает при наличии всех " +
				"перечисленных слов в указанном порядке";
			MessageBox.Show(text,"О словах-фильтрах", MessageBoxButtons.OK,MessageBoxIcon.Information);
		}
		void textBox2_KeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar==(char)13)
				button2.PerformClick();
		}

		void managerFormClosing(object sender, EventArgs e)
		{
			
		}
		void selectDir(object sender, EventArgs e)
		{
			if(lbDirs.SelectedItems.Count==0)return;
			string selectedDir=lbDirs.SelectedItem.ToString();
			lbWords.Items.Clear();
			CONSTS.Filter selectedFilter=CONSTS.Filters.FirstOrDefault(x=>x.directory==selectedDir);
			if(selectedFilter!=null)
				lbWords.Items.AddRange(selectedFilter.words.ToArray());
			
		}
		//add Directory
		void Button1Click(object sender, EventArgs e)
		{
			if(string.IsNullOrWhiteSpace(textBox1.Text))return;
			if(Directory.Exists(textBox1.Text) && !lbDirs.Items.Contains(textBox1.Text))
			{
				lbDirs.Items.Add(textBox1.Text);
				CONSTS.Filters.Add(new CONSTS.Filter()
				                   {directory=textBox1.Text.Trim(),
				                   	priority=1,
				                   	words=new List<string>()});
				
			}
		}
		
		//add word
		void Button2Click(object sender, EventArgs e)
		{
			string word=textBox2.Text;
			if(string.IsNullOrWhiteSpace(word))return;
			if(lbDirs.SelectedItems.Count==0) {MessageBox.Show("Сначала нужно выбрать папку"); return;}
			string dir=lbDirs.SelectedItem.ToString();
			//if(word.Split(' ').Length>1) {MessageBox.Show("Одно слово без ПРОБЕЛОВ");return;}
			
			if (CONSTS.Filters.Any(x=>x.words.Contains(word))) MessageBox.Show("Такой фильтр уже присутствует в одной из папок");
			else
			{
				lbWords.Items.Add(word);
				int ind=CONSTS.Filters.FindIndex(x=>x.directory==dir);
				if(ind<0) return;
				CONSTS.Filters[ind].words.Add(word);
				//textBox2.Text = "";
			}
		}
		
		void УдалитьToolStripMenuItemClick(object sender, EventArgs e)
		{
	try {
				if(this.ActiveControl.Name=="lbDirs")//delete Directory
				{
					if(lbDirs.SelectedItems.Count!=0)
					CONSTS.RemoveDirFromfilter(lbDirs.SelectedItem.ToString());
					lbDirs.Items.Remove(lbDirs.SelectedItem);
					lbWords.Items.Clear();
						
				}else if(this.ActiveControl.Name=="lbWords")//delete word-filter
				{
					if(lbWords.SelectedItems.Count!=0)
						CONSTS.Filters.Find(x=>x.directory==lbDirs.SelectedItem.ToString()).
							words.Remove(lbWords.SelectedItem.ToString());
					lbWords.Items.Remove(lbWords.SelectedItem);
				}
					
				
	} catch (Exception ex) {
		
				MessageBox.Show(ex.Message);
	}
		}
		void BtBrowseClick(object sender, EventArgs e)
		{
			folderBrowserDialog1.SelectedPath=CONSTS.SelectedPath;
		DialogResult dr=	folderBrowserDialog1.ShowDialog();
		if (dr== DialogResult.OK) textBox1.Text=folderBrowserDialog1.SelectedPath;
		}

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
			EnableControls(!checkBox1.Checked);
			UseML = checkBox1.Checked;
        }

		private void EnableControls(bool state)
        {
			lbWords.Enabled = state;
			button2.Enabled = state;
			textBox2.Enabled = state;
			btRebuild.Visible = !state;
		}

        private void btRebuild_Click(object sender, EventArgs e)
        {
			btRebuild.Enabled = false;
			var oldtext = btRebuild.Text;
			btRebuild.Text = "Идет процесс обучения...";
			var sortedDirs = CONSTS.Filters.ConvertAll(x => x.directory);
			NeuroSorterLibrary.BuilderModel.BuildModel(sortedDirs, MainForm.IncomingDir,NeuroSorterLibrary.BuilderModel.MyTrainerStrategy.OVAAveragedPerceptronTrainer,true);
			btRebuild.Enabled = true;
			btRebuild.Text = oldtext;
		}
    }
}
