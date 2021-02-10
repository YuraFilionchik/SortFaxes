/*
 * Создано в SharpDevelop.
 * Пользователь: user
 * Дата: 26.05.2020
 * Время: 11:58
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace SortFaxes
{
	/// <summary>
	/// Представляет объект со списком файлов для копирования (QFiles)
	/// {Files} - отфильтрованный списов файлов
	/// </summary>
	public class QueueFiles
	{
		public List<QFile> Files;
		public QueueFiles()
		{
			Files=new List<QFile>();
		}
		
		public QueueFiles(IEnumerable<string> filepaths)
		{
			Files=new List<QFile>();
			foreach (string path in filepaths) {
				Files.Add(new QFile(path));
			}
		}
		
		
	}
	/// <summary>
	/// Файл и инфа, извлеченная из его имени
	/// </summary>
	public class QFile
	{
		public bool Copy{get;set;}
		public string FileName{get;set;}
		public string FilePath;
		public string DestinationDir{get;set;}
		public DateTime DateEvent{get;set;}
		
		public QFile()
		{
			FileName="";
			FilePath="";
			DestinationDir="";
			DateEvent=new DateTime(0);
			Copy=false;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="path">Full file path</param>
		/// <param name="filter">{0} - filer word
		/// {1} - target Dir</param>
		public QFile(string path)
		{
			if(!File.Exists(path))
			{
			FileName="";
			FilePath="";
			DestinationDir="";
			DateEvent=new DateTime(0);
			Copy=false;}
			
			FilePath=path;
			FileName=path.Split('\\').Last();
			Copy=false;
			DateEvent=CONSTS.ParseDate(FileName);
			if(DateEvent!=new DateTime(0) && DateEvent < DateTime.Today ) //DateEvent filter
				Copy=true;
			DestinationDir=Filter(FileName);
			if(String.IsNullOrWhiteSpace(DestinationDir)) Copy=false;

		}
		
		
		/// <summary>
		/// Фильтрация имени файла по словам из CONSTS.Filter
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public string Filter(string input)
		{
			//TODO сделать приоритеты фильтров
			foreach (var dic in CONSTS.Filter) 
				if(input.Contains(dic.Key)) return dic.Value;
			return "";
		}
	}
}
