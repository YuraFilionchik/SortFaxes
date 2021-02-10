/*
 * Создано в SharpDevelop.
 * Пользователь: user
 * Дата: 26.05.2020
 * Время: 11:34
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
namespace SortFaxes
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem настройкаToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem фильтрыToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem выбратьИсходнуюПапкуToolStripMenuItem;
		private UserDataGridView dataGridView1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.BindingSource bindingSource1;
		private System.Windows.Forms.DataGridViewCheckBoxColumn copyDataGridViewCheckBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn fileNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn destinationDirDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn dateEventDataGridViewTextBoxColumn;
		private System.Windows.Forms.ToolStripMenuItem повторноПрименитьФильтрToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.настройкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.фильтрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выбратьИсходнуюПапкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.повторноПрименитьФильтрToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridView1 = new SortFaxes.UserDataGridView();
            this.copyDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.fileNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destinationDirDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateEventDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1193, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // настройкаToolStripMenuItem
            // 
            this.настройкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.фильтрыToolStripMenuItem,
            this.выбратьИсходнуюПапкуToolStripMenuItem,
            this.повторноПрименитьФильтрToolStripMenuItem});
            this.настройкаToolStripMenuItem.Name = "настройкаToolStripMenuItem";
            this.настройкаToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.настройкаToolStripMenuItem.Text = "Настройка";
            // 
            // фильтрыToolStripMenuItem
            // 
            this.фильтрыToolStripMenuItem.Name = "фильтрыToolStripMenuItem";
            this.фильтрыToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.фильтрыToolStripMenuItem.Text = "Фильтры...";
            this.фильтрыToolStripMenuItem.Click += new System.EventHandler(this.ФильтрыToolStripMenuItemClick);
            // 
            // выбратьИсходнуюПапкуToolStripMenuItem
            // 
            this.выбратьИсходнуюПапкуToolStripMenuItem.Name = "выбратьИсходнуюПапкуToolStripMenuItem";
            this.выбратьИсходнуюПапкуToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.выбратьИсходнуюПапкуToolStripMenuItem.Text = "Выбрать исходную папку...";
            this.выбратьИсходнуюПапкуToolStripMenuItem.Click += new System.EventHandler(this.ВыбратьИсходнуюПапкуToolStripMenuItemClick);
            // 
            // повторноПрименитьФильтрToolStripMenuItem
            // 
            this.повторноПрименитьФильтрToolStripMenuItem.Name = "повторноПрименитьФильтрToolStripMenuItem";
            this.повторноПрименитьФильтрToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.повторноПрименитьФильтрToolStripMenuItem.Text = "Повторно применить фильтр";
            this.повторноПрименитьФильтрToolStripMenuItem.Click += new System.EventHandler(this.ПовторноПрименитьФильтрToolStripMenuItemClick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(0, 422);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(1193, 31);
            this.button1.TabIndex = 4;
            this.button1.Text = "Переместить выбранные файлы";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.DesktopDirectory;
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.statInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 456);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1193, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(38, 17);
            this.toolStripStatusLabel1.Text = "status";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statInfo
            // 
            this.statInfo.AutoToolTip = true;
            this.statInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statInfo.ForeColor = System.Drawing.Color.MidnightBlue;
            this.statInfo.Name = "statInfo";
            this.statInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statInfo.Size = new System.Drawing.Size(10, 17);
            this.statInfo.Text = ".";
            this.statInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.CheckedCount = 0;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.copyDataGridViewCheckBoxColumn,
            this.fileNameDataGridViewTextBoxColumn,
            this.destinationDirDataGridViewTextBoxColumn,
            this.dateEventDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.bindingSource1;
            this.dataGridView1.Location = new System.Drawing.Point(0, 27);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1193, 423);
            this.dataGridView1.TabIndex = 3;
            // 
            // copyDataGridViewCheckBoxColumn
            // 
            this.copyDataGridViewCheckBoxColumn.DataPropertyName = "Copy";
            this.copyDataGridViewCheckBoxColumn.HeaderText = "Переместить";
            this.copyDataGridViewCheckBoxColumn.Name = "copyDataGridViewCheckBoxColumn";
            // 
            // fileNameDataGridViewTextBoxColumn
            // 
            this.fileNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.fileNameDataGridViewTextBoxColumn.DataPropertyName = "FileName";
            this.fileNameDataGridViewTextBoxColumn.HeaderText = "Имя файла";
            this.fileNameDataGridViewTextBoxColumn.MinimumWidth = 250;
            this.fileNameDataGridViewTextBoxColumn.Name = "fileNameDataGridViewTextBoxColumn";
            this.fileNameDataGridViewTextBoxColumn.Width = 250;
            // 
            // destinationDirDataGridViewTextBoxColumn
            // 
            this.destinationDirDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.destinationDirDataGridViewTextBoxColumn.DataPropertyName = "DestinationDir";
            this.destinationDirDataGridViewTextBoxColumn.HeaderText = "Куда";
            this.destinationDirDataGridViewTextBoxColumn.Name = "destinationDirDataGridViewTextBoxColumn";
            this.destinationDirDataGridViewTextBoxColumn.Width = 56;
            // 
            // dateEventDataGridViewTextBoxColumn
            // 
            this.dateEventDataGridViewTextBoxColumn.DataPropertyName = "DateEvent";
            this.dateEventDataGridViewTextBoxColumn.HeaderText = "Дата события";
            this.dateEventDataGridViewTextBoxColumn.Name = "dateEventDataGridViewTextBoxColumn";
            // 
            // bindingSource1
            // 
            this.bindingSource1.AllowNew = false;
            this.bindingSource1.DataSource = typeof(SortFaxes.QFile);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 478);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.RightToLeftLayout = true;
            this.Text = "SortFaxes";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private System.Windows.Forms.ToolStripStatusLabel statInfo;
    }
}
