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
		/// {word, dir}
		/// </summary>
		public static Dictionary<string,string> Filter=new Dictionary<string, string>();
		public static void RemoveDirFromfilter(string dir)
		{
			var keys = new List<string>();
			foreach (var word in Filter.Keys)
			{
				if (Filter[word] == dir) keys.Add(word);
			}
			foreach (var word in keys)
			{
				 Filter.Remove(word);
			}
		}

		public static void invokeStatusInfo(StatusStrip stat, string text)
		{
			if (stat.InvokeRequired) stat.Invoke(new Action<string>(s => stat.Items[1].Text = s), text);
			else stat.Items[1].Text = text;
		}
	}
}
