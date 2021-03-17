﻿/*
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
		
		public QueueFiles(IEnumerable<string> filepaths,  List<CONSTS.Filter> filters, List<string> exceptions)
		{
			Files=new List<QFile>();
			foreach (string path in filepaths) {
				Files.Add(new QFile(path, filters, exceptions));
			}
		}
		
		
	}
	public class QFileComparer : IComparer<QFile>
	{
		#region IComparer implementation
	public int Compare(QFile x, QFile y)
	{
		if(x.DateEvent>y.DateEvent) return -1;
		else if(x.DateEvent<y.DateEvent) return 1;
		else return 0;
	}
	#endregion
	
	}
	/// <summary>
	/// Файл и инфа, извлеченная из его имени
	/// </summary>
	public class QFile:IComparable<QFile>
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

		#region IComparable implementation
		public int CompareTo(QFile f)
		{
			return this.DateEvent.CompareTo(f.DateEvent);
		}
		#endregion		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="path">Full file path</param>
		/// <param name="filter">{0} - filer word
		/// {1} - target Dir</param>
		public QFile(string path,  List<CONSTS.Filter> filters, List<string> exceptions)
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
			DestinationDir=Filter(FileName, filters);
			if(String.IsNullOrWhiteSpace(DestinationDir)) Copy=false;
			if(exceptions.Contains(FilePath))
				Copy=false;

		}
		
		
		/// <summary>
		/// Фильтрация имени файла по словам из CONSTS.Filter
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public string Filter(string input, List<CONSTS.Filter> filters)
		{
			//TODO сделать приоритеты фильтров
			foreach (CONSTS.Filter filter in filters)
			{string[] words;
				foreach (string wordString in filter.words) {
					words=wordString.Split(';');
					
					if(containsAllWord(input.ToLower(),words))
						return filter.directory;	
				}		
				
			}
				
			return "";
		}
		
		//проверяет есть ли в строке последовательность слов words
		private bool containsAllWord(string input, string[] words)
		{int pos=0;
			foreach (string word in words) {
				int find_pos=input.IndexOf(word.ToLower());
				if(find_pos==-1) return false;
				if(find_pos>=pos) pos=find_pos;
					else return false;
					
			}
			return true;
		}
	}
}
