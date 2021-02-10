/*
 * Создано в SharpDevelop.
 * Пользователь: user
 * Дата: 26.05.2020
 * Время: 11:34
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;

namespace SortFaxes
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public string IncomingDir;
		public string ConfigFile="cfg.ini";
		public IniFile cfg;
		
		public MainForm()
		{

			InitializeComponent();
			this.FormClosing+=formClosingMethod;
			dataGridView1.DataBindingComplete += DataGridView1_DataBindingComplete;
			dataGridView1.CellContentClick += cellClick;
			//folderBrowserDialog1.SelectedPath=@"\\175.16.1.1\e\Принятые Факсы";
			#region Read config
			
			cfg=new IniFile(ConfigFile);
			ReadFiltersCfg(cfg);
			ReadSettings(cfg);
			#endregion
			if(IncomingDir!=null && Directory.Exists(IncomingDir))
				DisplayFilesInDGV(IncomingDir);
		}

		private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			HighlightCheckedRows(dataGridView1);
		}

		void cellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex != 0) return;

				int total=0;
				if(dataGridView1.Rows.Count!=0) total=dataGridView1.Rows.Count;
				else {SetStatus(0,0); return;}
			//HighlightCheckedRows(dataGridView1);
			SetStatus(dataGridView1.CheckedCount,total);
			//HighlightCheckedRows(dataGridView1);
		}
		
		void formClosingMethod(object sender, FormClosingEventArgs e)
		{
			
			SaveFiltersCFG(cfg);
			SaveIncominDir(cfg);
		}

		void ВыбратьИсходнуюПапкуToolStripMenuItemClick(object sender, EventArgs e)
		{
			var dr=folderBrowserDialog1.ShowDialog();
			if(dr!=DialogResult.OK) return;
			IncomingDir=folderBrowserDialog1.SelectedPath;
			
			this.Text=IncomingDir;
			DisplayFilesInDGV(IncomingDir);
		}
		
		// manage filters
		void ФильтрыToolStripMenuItemClick(object sender, EventArgs e)
		{
			FilterManager FM=new FilterManager();
			FM.ShowDialog();
			
		}
		
		/// <summary>
		/// Чтение слов-фильтров из файла конфигурации в CONST.Filter
		/// </summary>
		/// <param name="cfg"></param>
		private void ReadFiltersCfg( IniFile cfg)
		{
			try {
			var keys=cfg.GetAllKeys("filters");
			if(keys.Length!=0)
			{
				foreach (string word in keys) 
				{
					var dir=cfg.ReadINI("filters",word);
					if(String.IsNullOrWhiteSpace(dir)) continue;
					if (CONSTS.Filter.ContainsKey(word)) continue;
						CONSTS.Filter.Add(word, dir);
				}
			}
			
			} catch (Exception ex) {
				
				MessageBox.Show(ex.Message);
			}
			
			
		}
		
		void ReadSettings(IniFile cfg)
		{
			if(cfg.KeyExists("SelectedDirectory"))
			{
				string readIncDir=cfg.ReadINI("SETTINGS","SelectedDirectory");
				if(Directory.Exists(readIncDir)) 
				{
					IncomingDir=readIncDir;
					this.Text=IncomingDir;
				}
			}
		}
		/// <summary>
		/// Сохранение слов-фильтров в файл конфигурации
		/// </summary>
		/// <param name="cfg"></param>
		public static void SaveFiltersCFG(IniFile cfg)
		{
			/*var groupsDir=CONSTS.Filter.Values.GroupBy(x=>x);
			foreach (var gr in groupsDir) //группировка по папкам
			{
				string words="";
				foreach (var val in gr) //для каждой папки список слов-фильтров
				{
					string dir=val.ToString();
					var keyVals=CONSTS.Filter.Where(x=>x.Value==val);
					string oldWords=cfg.ReadINI("filters",dir);
					foreach (var key in keyVals.Select(x=>x.Key).Distinct()) {
						if(!oldWords.Contains(key))
						words+=key+";";
					}
					words=words.TrimEnd(';');
					cfg.Write("filters",dir,words);
				}
			}*/

			//new method of saving filter
			//save pair word;dir
			//word - is uniq
			cfg.DeleteSection("filters");
			foreach (var pair in CONSTS.Filter)
			{
				cfg.Write("filters", pair.Key, pair.Value);
			}
		}
		void SaveIncominDir (IniFile cfg)
		{
			if(IncomingDir!=null && !String.IsNullOrWhiteSpace(IncomingDir))
				cfg.Write("SETTINGS","SelectedDirectory",IncomingDir);
		}
		/// <summary>
		/// Read and filtering files from incDir directory
		/// and display it into datagridview1
		/// </summary>
		/// <param name="incDir"></param>
		void DisplayFilesInDGV(string incDir)
		{
			var filepaths=Directory.GetFiles(incDir);
			QueueFiles AllFiles=new QueueFiles(filepaths);//отфильтрованный список файлов
			bindingSource1.DataSource=AllFiles.Files;
			dataGridView1.DataSource=bindingSource1;
			SetStatus(AllFiles.Files.Count(x=>x.Copy), AllFiles.Files.Count());
			
		}
		public void HighlightCheckedRows(UserDataGridView dg)
		{
			foreach (DataGridViewRow row in dg.Rows)
			{
				//var rowstyle = row.DefaultCellStyle;
				//rowstyle.BackColor = Color.LightGreen;
				if ((bool)row.Cells[0].EditedFormattedValue)
					row.DefaultCellStyle.BackColor = Color.LightGreen;
				else row.DefaultCellStyle.BackColor = Color.White;
				
			}
		}
		/// <summary>
		/// set text to stripStatus
		/// </summary>
		/// <param name="checkItems"></param>
		/// <param name="total"></param>
		public void SetStatus(int checkItems,int total)
		{
			// bt.Invoke(new Action<string>(s => bt.Text = (s)), CONSTS.btSyncText1);
			
			toolStripStatusLabel1.Text="Отмечено "+checkItems+" из " + total +" файлов";
			HighlightCheckedRows(dataGridView1);
		}
		//Переместить выбранные файлы
		void Button1Click(object sender, EventArgs e)
		{
	try {
			foreach (DataGridViewRow row in dataGridView1.Rows) {
					if((bool)row.Cells[0].Value) //MOVE
					{
						if (String.IsNullOrWhiteSpace(row.Cells[2].Value.ToString())) continue;
						button1.Enabled = false;
						var SourceFile = IncomingDir+"\\"+ row.Cells[1].Value.ToString();
						var TargetFile = row.Cells[2].Value.ToString()+ row.Cells[1].Value.ToString();
						Writelog.WriteLog(row.Cells[1].Value.ToString()+" перемещен --> "+row.Cells[2].Value,"history.txt");
						if (File.Exists(SourceFile)) File.Move(SourceFile, TargetFile);
						CONSTS.invokeStatusInfo(statusStrip1, "Переностся файл " + row.Cells[1].Value.ToString());
						

					}
					else //SKIP
					{
						//Writelog.WriteLog(row.ToString(),"Uchecked.txt");
					}
					
			}
				DisplayFilesInDGV(IncomingDir);
					button1.Enabled = true;
				CONSTS.invokeStatusInfo(statusStrip1, "Выполнено");
			} catch (Exception ex) {
				button1.Enabled = true;
				MessageBox.Show(ex.Message);
				CONSTS.invokeStatusInfo(statusStrip1, "Ошибка в процессе перемещения файла");
			}
		}
		void ПовторноПрименитьФильтрToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(IncomingDir!=null && Directory.Exists(IncomingDir))
			DisplayFilesInDGV(IncomingDir);
		}
	}
}
