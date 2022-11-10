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
		public static string IncomingDir;
		public string ConfigFile="cfg.ini";
		public IniFile cfg;
		public List<CONSTS.Filter> filters;
		public QueueFiles AllFiles;
		private bool UseML = false;
		public MainForm()
		{

			InitializeComponent();
			if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Data"))) 
				Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Data"));
			this.FormClosing+=formClosingMethod;
			dataGridView1.DataBindingComplete += DataGridView1_DataBindingComplete;
			dataGridView1.CellContentClick += cellClick;
			dataGridView1.CellClick+= dataGridView1_CellClick;
			dataGridView1.CellDoubleClick+= dataGridView1_CellDoubleClick;
			comboFilters.SelectedIndexChanged+= comboFilters_SelectedIndexChanged;
			dataGridView1.DataError+= dataGridView1_DataError;
			tbSearch.TextChanged+= tbSearch_TextChanged;
			//folderBrowserDialog1.SelectedPath=@"\\175.16.1.1\e\Принятые Факсы";
			filters=CONSTS.Filters;
            QueueFiles.QueueEvent += QueueFiles_QueueEvent;
		
			#region Read config
			
			cfg=new IniFile(ConfigFile);
			ReadFiltersCfg(cfg);
			ReadSettings(cfg);
			ReadFilesExceptions(cfg);
			tsMenuUseML.Checked = UseML;
			#endregion
			dataGridView1.AllowUserToAddRows=false;
			(dataGridView1.Columns[2] as DataGridViewComboBoxColumn).Items.AddRange(CONSTS.DIRS());
			#region	ComboFilters
			comboFilters.Items.AddRange(namesFilters());
			comboFilters.Items.Add("Все фильтры");
			comboFilters.SelectedItem="Все фильтры";
			
			#endregion
			//if(IncomingDir!=null && Directory.Exists(IncomingDir))
			//	ReadAndDisplayFilesInDGV(IncomingDir, filters,checkBox1.Checked, false);
			
		}

        private void QueueFiles_QueueEvent(string msg)
        {
			CONSTS.InvokeLog(tbLog, msg);
        }

        //ввод строки поиска-фильтрации
        void tbSearch_TextChanged(object sender, EventArgs e)
		{try
			{
			if(String.IsNullOrWhiteSpace(tbSearch.Text))//Show All
			{
				DisplayFilesInDGV(AllFiles.Files);
			}
			
			string SrchString=tbSearch.Text.Trim().ToLower();
			DisplayFilesInDGV(AllFiles.Files.Where(x=>x.FileName.ToLower().Contains(SrchString)));

			}
		
		catch(Exception ex)
		{MessageBox.Show(ex.Message);
		}
		}
		private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{

//			if(dataGridView1.Rows.Count>0)// && dataGridView1.Columns.Contains("DateEvent"))
//			dataGridView1.Sort(dataGridView1.Columns[3],System.ComponentModel.ListSortDirection.Descending);
			HighlightCheckedRows(dataGridView1);
			
		}

		public void HighLightExceptions(UserDataGridView dg)
		{string selectedFile="";
			foreach (DataGridViewRow row in dg.Rows) {
				selectedFile=IncomingDir+ row.Cells[1].Value.ToString();
				if(CONSTS.FilesExceptions.Contains(selectedFile)) 
					row.DefaultCellStyle.BackColor = Color.IndianRed;
			}
		}

		//Select quick filters
		void comboFilters_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (AllFiles == null || AllFiles.Files == null) return;
			string selectedFilterString=comboFilters.SelectedItem.ToString();
			if(selectedFilterString=="Все фильтры")
				DisplayFilesInDGV(AllFiles.Files);
			else
			DisplayFilesInDGV(AllFiles.Files.Where(x => x.DestinationDir.Contains(selectedFilterString)));
			//ReadAndDisplayFilesInDGV(IncomingDir,filters,checkBox1.Checked,false);
			
		}
//open file
		void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if(e.ColumnIndex!=1) return;
			string selectedFile=dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
			System.Diagnostics.Process.Start(IncomingDir+"\\"+ selectedFile);
		}
		void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			
		}
		//Генерация названия для фильтров
		public string[] namesFilters()
		{List<string> names=new List<string>();
			foreach (CONSTS.Filter filter in CONSTS.Filters) {
				string targetDir=filter.directory.TrimEnd('\\').Split('\\').Last();
				if(!names.Contains(targetDir)) names.Add(targetDir);
			}
			return names.ToArray();
		}
		void cellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 0) {

				int total=0;
				if(dataGridView1.Rows.Count!=0) total=dataGridView1.Rows.Count;
				else {SetStatus(0,0); return;}
			//HighlightCheckedRows(dataGridView1);
			SetStatus(dataGridView1.CheckedCount,total);
			//HighlightCheckedRows(dataGridView1);
			}
			
		}

		void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
//			if(e.ColumnIndex==2) //comboBoxCell
//			{
//			List<string> dirs=new List<string>();
//			foreach (CONSTS.Filter filter in CONSTS.Filters) {
//				dirs.Add(filter.directory);
//				
//			}
//			dirs.Add("");
//			(dataGridView1.SelectedCells[2] as DataGridViewComboBoxCell).Items.Clear();
//			(dataGridView1.SelectedCells[2] as DataGridViewComboBoxCell).Items.AddRange(dirs.ToArray());
				//(row[2] as DataGridViewComboBoxCell
			
			//}
		}
		void formClosingMethod(object sender, FormClosingEventArgs e)
		{
			
			SaveFiltersCFG(cfg);
			SaveSettings(cfg);
			SaveFileExceptions(cfg);
		}

		void ВыбратьИсходнуюПапкуToolStripMenuItemClick(object sender, EventArgs e)
		{
			var dr=folderBrowserDialog1.ShowDialog();
			if(dr!=DialogResult.OK) return;
			IncomingDir=folderBrowserDialog1.SelectedPath;
			CONSTS.SelectedPath=IncomingDir;
			this.Text=IncomingDir;
			ReadAndDisplayFilesInDGV(IncomingDir, filters,checkBox1.Checked,false);
		}
		
		// manage filters
		void ФильтрыToolStripMenuItemClick(object sender, EventArgs e)
		{
			FilterManager FM=new FilterManager(UseML);
			FM.ShowDialog();
			UseML = FilterManager.UseML;
			tsMenuUseML.Checked = UseML;
			filters=CONSTS.Filters;
			SaveFiltersCFG(cfg);
		}
		
		/// <summary>
		/// Чтение слов-фильтров из файла конфигурации в CONST.Filter
		/// </summary>
		/// <param name="cfg"></param>
		private void ReadFiltersCfg( IniFile cfg)
		{
			try {
			var keys=cfg.GetAllKeys("filters");
			if(keys.Length!=0 && !(keys.Length==1 && string.IsNullOrWhiteSpace(keys[0])))
			{ 
				foreach (string word in keys) 
				{ //{"word"=dir} Слова фильтра записываются в конф файл в ковычках
					var dir=cfg.ReadINI("filters",word);
					if(String.IsNullOrWhiteSpace(dir)) continue;
					
					
					if(CONSTS.Filters.Count == 0 || CONSTS.Filters.All(x => x.directory != dir))
						CONSTS.Filters.Add(new CONSTS.Filter(){
						                   directory=dir,
						                   priority=0,
						                   words=new List<string>()});
					if (CONSTS.Filters.Count!=0 && CONSTS.Filters.Find(x=>x.directory==dir).words.Contains(word.Trim('"'))) continue;
					CONSTS.Filters.Find(x=>x.directory==dir).words.Add(word.Trim('"'));
				}
			}else
                {
					var dirs = cfg.GetAllKeys("TargetDirs");
                    foreach (var dir in dirs)
                    {
						if (CONSTS.Filters.Count == 0 || CONSTS.Filters.All(x => x.directory != dir))
							CONSTS.Filters.Add(new CONSTS.Filter()
							{
								directory = dir,
								priority = 0,
								words = new List<string>()
							});
					}
                }
			
			} catch (Exception ex) {
				
				MessageBox.Show(ex.Message);
			}
			
			
		}
		
		void ReadFilesExceptions(IniFile cfg)
		{
			try {
			var keys=cfg.GetAllKeys("exceptions");
			if(keys.Length!=0)
			{
				foreach (string filepath in keys) 
				{
					var excFile=cfg.ReadINI("exceptions",filepath);
					if(String.IsNullOrWhiteSpace(excFile)) continue;
					if(!File.Exists(excFile))continue;
					if (!CONSTS.FilesExceptions.Contains(excFile)) 
						CONSTS.FilesExceptions.Add(excFile);
				}
			}
			
			} catch (Exception ex) {
				
				MessageBox.Show(ex.Message, "read files exceptions");
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
					CONSTS.SelectedPath=IncomingDir;
					this.Text=IncomingDir;
				}
				string useml = cfg.ReadINI("SETTINGS", "UseML");
				bool.TryParse(useml, out UseML);
			}
			
		}
		/// <summary>
		/// Сохранение слов-фильтров в файл конфигурации
		/// </summary>
		/// <param name="cfg"></param>
		public static void SaveFiltersCFG(IniFile cfg)
		{
			
			cfg.DeleteSection("filters");
			foreach (CONSTS.Filter filter in CONSTS.Filters)
			{
				cfg.Write("TargetDirs",filter.directory,"");
				foreach(string word in filter.words)
				cfg.Write("filters", word, filter.directory);
			}
		}
		void SaveFileExceptions(IniFile cfg)
		{
			try {cfg.DeleteSection("exceptions");
						foreach (var excfile in CONSTS.FilesExceptions) 
						{
					if(!File.Exists(excfile)) continue;
					
					cfg.Write("exceptions",excfile.Split('\\').Last(),excfile);
						}
			} catch (Exception ex) {
				MessageBox.Show(ex.Message, "Save files exceptions");
			}
		}
		void SaveSettings (IniFile cfg)
		{
			if(IncomingDir!=null && !String.IsNullOrWhiteSpace(IncomingDir))
				cfg.Write("SETTINGS","SelectedDirectory",IncomingDir);
			cfg.Write("SETTINGS", "UseML", UseML.ToString());
		}
		/// <summary>
		/// Read and filtering files from incDir directory
		/// and display it into datagridview1
		/// </summary>
		/// <param name="incDir"></param>
		void ReadAndDisplayFilesInDGV(string incDir, List<CONSTS.Filter> filters, bool HideUncheked, bool rebuildModel)
		{
			if (!Directory.Exists(incDir)) return;
			(dataGridView1.Columns[2] as DataGridViewComboBoxColumn).Items.Clear();
			(dataGridView1.Columns[2] as DataGridViewComboBoxColumn).Items.AddRange(CONSTS.DIRS());
			var filepaths=Directory.GetFiles(incDir);
			if(UseML)
				AllFiles=new QueueFiles(filepaths, filters, CONSTS.FilesExceptions, rebuildModel);//отфильтрованный список файлов
			else
				AllFiles = new QueueFiles(filepaths, filters, CONSTS.FilesExceptions);//отфильтрованный список файлов
			AllFiles.Files.Sort(new QFileComparer());
			if(HideUncheked)bindingSource1.DataSource=AllFiles.Files.Where(x=>x.Copy);
			else bindingSource1.DataSource=AllFiles.Files;
			dataGridView1.DataSource=bindingSource1;
			SetStatus(AllFiles.Files.Count(x=>x.Copy), AllFiles.Files.Count());
		}
		void DisplayFilesInDGV(IEnumerable<QFile> allFiles)
		{
			if(checkBox1.Checked)bindingSource1.DataSource=allFiles.Where(x=>x.Copy);
			else bindingSource1.DataSource=allFiles;
			dataGridView1.DataSource=bindingSource1;
		}
		public void HighlightCheckedRows(UserDataGridView dg)
		{
			if(dg.Rows.Count==0)return;
			foreach (DataGridViewRow row in dg.Rows)
			{
				if(row.Cells[1].Value==null) continue;
				if ((bool)row.Cells[0].EditedFormattedValue)
					row.DefaultCellStyle.BackColor = Color.LightGreen;
				else {
					string filepath=IncomingDir+ row.Cells[1].Value.ToString();
					if(CONSTS.FilesExceptions.Contains(filepath))
					row.DefaultCellStyle.BackColor=Color.IndianRed;
					else
					row.DefaultCellStyle.BackColor = Color.White;
				}
				
				
				
				
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
			var oldtext = button1.Text;
			try {
				button1.Text = "Файлы перемещаются...";
				CONSTS.invokeStatusInfo(statusStrip1, "Перемещение файлов...");
				foreach (DataGridViewRow row in dataGridView1.Rows) {
					if((bool)row.Cells[0].Value) //MOVE
					{
						if (String.IsNullOrWhiteSpace(row.Cells[2].Value.ToString())) continue;
						button1.Enabled = false;
						var SourceFile = IncomingDir+"\\" +row.Cells[1].Value.ToString();
						var TargetFile = row.Cells[2].Value.ToString().TrimEnd('\\')+"\\"+ row.Cells[1].Value.ToString();
						
						Writelog.WriteLog(row.Cells[1].Value.ToString()+" перемещен --> "+row.Cells[2].Value,"history.txt");
						
						if (File.Exists(SourceFile)) 
						{
							if(File.Exists(TargetFile)) //overwrite
								File.Delete(TargetFile);								
							
								File.Move(SourceFile, TargetFile);
						}
						CONSTS.invokeStatusInfo(statusStrip1, "Переносится файл " + row.Cells[1].Value.ToString());
						

					}
					else //SKIP
					{
						//Writelog.WriteLog(row.ToString(),"Uchecked.txt");
					}
					
			}
				ReadAndDisplayFilesInDGV(IncomingDir, filters,checkBox1.Checked,false);
				button1.Enabled = true;
				button1.Text = oldtext;
				CONSTS.invokeStatusInfo(statusStrip1, "Выполнено");
			} catch (Exception ex) {
				button1.Enabled = true;
				button1.Text = oldtext;
				MessageBox.Show(ex.Message);
				CONSTS.invokeStatusInfo(statusStrip1, "Ошибка в процессе перемещения файла");
			}
		}
		void ПовторноПрименитьФильтрToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(IncomingDir!=null && Directory.Exists(IncomingDir))
			ReadAndDisplayFilesInDGV(IncomingDir, filters,checkBox1.Checked,false);
		}
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			if (AllFiles == null || AllFiles.Files == null) return;
			DisplayFilesInDGV(AllFiles.Files);
		
		}
		void ДобавитьФайлВИсключениенеПеремещатьToolStripMenuItemClick(object sender, EventArgs e)
		{
	try {
				string selectedFile="";
				if(dataGridView1.SelectedCells.Count==0)return;
				selectedFile=IncomingDir+ dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
				if(!CONSTS.FilesExceptions.Contains(selectedFile)) 
				{
					CONSTS.FilesExceptions.Add(selectedFile);
					dataGridView1.SelectedRows[0].DefaultCellStyle.BackColor = Color.IndianRed;
				}
				
				
	} catch (Exception ex) {
				MessageBox.Show(ex.Message);
	}
		}
		void УдалитьФайлИзИсключенияToolStripMenuItemClick(object sender, EventArgs e)
		{
			string selectedFile="";
				if(dataGridView1.SelectedCells.Count==0)return;
				selectedFile=IncomingDir+ dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
				if(CONSTS.FilesExceptions.Contains(selectedFile)) 
				{
					CONSTS.FilesExceptions.Remove(selectedFile);
					dataGridView1.SelectedRows[0].DefaultCellStyle.BackColor = Color.White;
				}
		}

        private void tsMenuUseML_Click(object sender, EventArgs e)
        {
			UseML = tsMenuUseML.Checked;
        }

        private void отметитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for(int i=0;i<dataGridView1.Rows.Count;i++)
            {
				dataGridView1.Rows[i].Cells[0].Value = true;
            }
        }

        private void снятьВсеОтметкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
			for (int i = 0; i < dataGridView1.Rows.Count; i++)
			{
				dataGridView1.Rows[i].Cells[0].Value = false;
			}
		}

        private void отметитьВыбранныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
				dataGridView1.Rows[row.Index].Cells[0].Value = !(bool)dataGridView1.Rows[row.Index].Cells[0].Value;
			}
        }

        private void comboFilters_Click(object sender, EventArgs e)
        {

        }
    }
}
