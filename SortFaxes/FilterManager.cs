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

namespace SortFaxes
{
	/// <summary>
	/// Description of FilterManager.
	/// </summary>
	public partial class FilterManager : Form
	{
		public FilterManager()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			lbDirs.Items.Clear();
			foreach (var val in CONSTS.Filter.Values) {
				if(lbDirs.Items.Contains(val)) continue;
				lbDirs.Items.Add(val);
			}
			lbDirs.SelectedIndexChanged+=  selectDir;
			this.FormClosing+= managerFormClosing;
			
		}

		void managerFormClosing(object sender, EventArgs e)
		{
			
		}
		void selectDir(object sender, EventArgs e)
		{
			if(lbDirs.SelectedItems.Count==0)return;
			string selectedDir=lbDirs.SelectedItem.ToString();
			lbWords.Items.Clear();
			foreach (var dic in CONSTS.Filter) {
				if(dic.Value==selectedDir && !lbWords.Items.Contains(dic.Key))
					lbWords.Items.Add(dic.Key);
			}
		}
		//add Directory
		void Button1Click(object sender, EventArgs e)
		{
			if(string.IsNullOrWhiteSpace(textBox1.Text))return;
			if(Directory.Exists(textBox1.Text) && !lbDirs.Items.Contains(textBox1.Text))
			{
				lbDirs.Items.Add(textBox1.Text);
				
				
			}
		}
		
		//add word
		void Button2Click(object sender, EventArgs e)
		{
			string word=textBox2.Text;
			if(string.IsNullOrWhiteSpace(word))return;
			if(lbDirs.SelectedItems.Count==0) {MessageBox.Show("Сначала нужно выбрать папку"); return;}
			string dir=lbDirs.SelectedItem.ToString();
			if(word.Split(' ').Length>1) {MessageBox.Show("Одно слово без ПРОБЕЛОВ");return;}
			
			if (CONSTS.Filter.ContainsKey(word)) MessageBox.Show("Такое слово уже присутствует в одной из папок");
			else
			{
				lbWords.Items.Add(word);
				CONSTS.Filter.Add(word, dir);
				textBox2.Text = "";
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
					CONSTS.Filter.Remove(lbWords.SelectedItem.ToString());
					lbWords.Items.Remove(lbWords.SelectedItem);
				}
					
				
	} catch (Exception ex) {
		
				MessageBox.Show(ex.Message);
	}
		}
	}
}
