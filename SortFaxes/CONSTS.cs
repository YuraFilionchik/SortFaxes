/*
 * Создано в SharpDevelop.
 * Пользователь: user
 * Дата: 26.05.2020
 * Время: 14:39
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SortFaxes
{
	/// <summary>
	/// Description of CONSTS.
	/// </summary>
	public static class CONSTS
	{
		//данные фильтра одной директории
		public class Filter
		{
			public string directory;
			public int priority;
			public List<string> words;
			
		}
		public static DateTime ParseDate(string str)
		{
			DateTime resDate=new DateTime(0);
			 string[] dateFormats=
			{	@"\d\d[-]\d\d[-]\d\d\d\d",
				@"\d\d[-]\d\d[-]\d\d",
				@"\d\d[.]\d\d[.]\d\d\d\d",
				@"\d\d[.]\d\d[.]\d\d",
				@"\d\d[_]\d\d[_]\d\d\d\d",
				@"\d\d[_]\d\d[_]\d\d"
			};
			foreach (string format in dateFormats) 
			{
				char sep=format[5];
				string[] formats=
				{
				"dd"+sep+"MM"+sep+"yyyy",
				"dd"+sep+"MM"+sep+"yy",
				"d"+sep+"MM"+sep+"yyyy",
				"d"+sep+"MM"+sep+"yy"
				};
				Match  match=Regex.Match(str, format);
				 if(match.Success ) 
				 {
				 	
				 	if(DateTime.TryParseExact(match.Value,formats,null,System.Globalization.DateTimeStyles.None,out resDate ))
				 		return resDate;
				 	
				 }
			}
			return resDate;
			
		}
		/// <summary>
		/// List of Filter{dir,prior,list words}
		/// </summary>
		public static  List<Filter> Filters=new List<Filter>(); 
		
		public static List<string> FilesExceptions=new List<string>();
		public static void RemoveDirFromfilter(string dir)
		{
			var ind=Filters.FindIndex(x=>x.directory==dir);
			if(ind>=0) Filters.Remove(Filters[ind]);
		}
		public static string[] DIRS()
		{
			List<string> dirs=new List<string>();
			foreach (var filter in Filters) {
				dirs.Add(filter.directory);
			}
			dirs.Add("");
			return dirs.ToArray();
		}
		public static string SelectedPath="";
		public static void invokeStatusInfo(StatusStrip stat, string text)
		{
			if (stat.InvokeRequired) stat.Invoke(new Action<string>(s => stat.Items[1].Text = s), text);
			else stat.Items[1].Text = text;
		}

		public static void InvokeLog(TextBox tb, string text)
        {
			if (tb.InvokeRequired) tb.Invoke(new Action<string>(s => tb.Text += s+Environment.NewLine), text);
			else tb.Text+= text+Environment.NewLine;
		}
	}
}
