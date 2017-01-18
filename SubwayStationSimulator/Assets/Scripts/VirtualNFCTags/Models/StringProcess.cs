using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

public class StringProcess{

	static string[] Gostraight = {"Go down","Cross","Continue","Walk past","trail","keeping"};//adjust direction based on the poi
	static string[] GoThrough = {"Go through"};
	static string[] TurnLeft = {"Turn left"};
	static string[] TurnRight = {"Turn right"};
	static string[] deleteDescribe = {"you will","You will","You may","This is"};

	public static string Initial(int diff, string name,string _str){
		_str = ActionConvertor (_str);
		_str = DirectConvertor (diff, name,_str);
		return _str;
	}

	public static string GetInstructionPhase(string _str){
		string[] words = _str.Split('$');
		_str = words[0];
		return _str;
	}

	static string[] WordReplace(string[] s, string[] searchfor, string replacewith){
		for (int j=0; j<s.Length; j++) {
			for (int i=0; i<searchfor.Length; i++) {
				
				s [j] = Regex.Replace (s[j], searchfor [i], replacewith, RegexOptions.IgnoreCase);

			}
		}
		return s;
	}

	static string WordReplace(string s, string[] searchfor, string replacewith){
			for (int i=0; i<searchfor.Length; i++) {
				
				s = Regex.Replace (s, searchfor [i], replacewith, RegexOptions.IgnoreCase);
				
			}
		return s;
	}
	
	static string WordReplace(string s, string searchfor, string replacewith){
		
		s = Regex.Replace (s, searchfor, replacewith, RegexOptions.IgnoreCase);
		
		return s;
	}

	static string Descript(string start, string end, string s){
		string _str;
		List<string> a=new List<string>();
		int first= 0;
		int last = 0;
		while((last < s.Length) && (s.IndexOf (start, last, s.Length - last))>0) {
			first = s.IndexOf (start, last, s.Length - last);
			last = s.IndexOf (end, first + start.Length, s.Length - first - start.Length);
			_str = s.Substring (first +  start.Length, last - first - start.Length);
			a.Add(_str);
			
		}
		foreach (string rs in a) {
			s = WordReplace(s, start+rs+".", "");
		}

		return s;
	}
	
	public static string DeleteDescription(string _str){
		for (int i=0; i<deleteDescribe.Length; i++) {
			_str = Descript(deleteDescribe[i], ".", _str);
		}
		return _str;
	}

	static string Keywords(string start, string end, string s){
		int first= 0;
		int last = 0;
		int checkflag = s.IndexOf ("With ");
		string _str = "";
		first = s.IndexOf (start, last, s.Length - last);

		if (checkflag>0){
			last = s.IndexOf (end, first + start.Length, s.Length - first - start.Length);
			_str = s.Substring (first +  start.Length, last - first - start.Length);
		}
		
		return _str;
	}

	
	static List<string> KeywordsList(string s){
		List<string> a=new List<string>();
		string start = "until ";
		string end = ".";
		string _str;
		int first= 0;
		int last = 0;
		while((last < s.Length) && (s.IndexOf (start, last, s.Length - last))>0) {
			first = s.IndexOf (start, last, s.Length - last);
			last = s.IndexOf (end, first + start.Length, s.Length - first - start.Length);
			_str = s.Substring (first +  start.Length, last - first - start.Length);
			_str = WordReplace(_str,"you reach ","");
			a.Add (_str);
		}
		return a;
	}



	static string ActionConvertor(string _str){
		_str = WordReplace(_str, Gostraight, "4");
		_str = WordReplace(_str, GoThrough, "7");
		_str = WordReplace(_str, TurnLeft, "1");
		_str = WordReplace(_str, TurnRight, "2");
		return _str;
	}

	public static int PlayerDirectionBasedonTags(int tagOpening, string _str){
		string s = StringProcess.Keywords("With the tag to your ",",",_str);
		int face = _str.IndexOf("Face the tag");
		switch (tagOpening){
			case 12: 
				if(s.Equals ("right side")){
					return 3;
				}else if(s.Equals ("left side")){
					return 9;
				}else if(s.Equals("back")){
					return 12;
				}
				if(face >= 0){
					return 6;
				}
			break;
			case 9: 
				if(s.Equals ("right side")){
					return 12;
				}else if(s.Equals ("left side")){
					return 6;
				}else if(s.Equals("back")){
					return 9;
				}
				if(face >= 0){
					return 3;
				}
				break;
			case 6: 
				if(s.Equals ("right side")){
					return 9;
				}else if(s.Equals ("left side")){
					return 3;
				}else if(s.Equals("back")){
					return 6;
				}
				if(face >= 0){
					return 12;
				}
				break;
			case 3: 
				if(s.Equals ("right side")){
					return 6;
				}else if(s.Equals ("left side")){
					return 12;
				}else if(s.Equals("back")){
					return 9;
				}
				if(face >= 0){
					return 3;
				}
				break;
		}
		return 0;
	}

	public static string DirectConvertor(int diff, string name, string _str){
		string s = StringProcess.Keywords("With "+name+" to your ",",",_str);
		int face = _str.IndexOf("Face "+name);
			switch (diff) {
			case 0: if (s.Equals ("right side")) {
//					Debug.Log ("turn right");
					_str = WordReplace(_str, "to your " + s, "2");
				} else if (s.Equals ("left side")) {
//					Debug.Log ("turn left");
					_str = WordReplace(_str, "to your " +s, "1");
				} else if (s.Equals ("back")) {
					_str = WordReplace(_str, "to your " +s, "0");
				}
				if (face >= 0){
//					Debug.Log ("turn back");
					_str = WordReplace(_str, "Face "+name, "3");
				}
				break;
			case 3: if (s.Equals ("right side")) {
//					Debug.Log ("turn back");
					_str = WordReplace(_str, "to your " +s, "3");
				} else if (s.Equals ("left side")) {
					_str = WordReplace(_str, "to your " +s, "0");
				} else if (s.Equals ("back")) {
//					Debug.Log ("turn right");
					_str = WordReplace(_str, "to your " +s, "2");
				}
				if (face >= 0){
//					Debug.Log ("turn left");
					_str = WordReplace(_str, "Face "+name, "1");
				}
				
				break;
			case 6: if (s.Equals ("right side")) {
//					Debug.Log ("turn left");
					_str = WordReplace(_str,"to your " + s, "1");
				} else if (s.Equals ("left side")) {
//					Debug.Log ("turn right");
					_str = WordReplace(_str, "to your " +s, "2");
				} else if (s.Equals ("back")) {
//					Debug.Log ("turn back");
					_str = WordReplace(_str, "to your " +s, "3");
				}
				if (face >= 0){
				_str = WordReplace(_str, "to your " +s, "0");
				}
				break;
			case 9: if (s.Equals ("right side")) {
					_str = WordReplace(_str, "to your " +s, "0");
				} else if (s.Equals ("left side")) {
//					Debug.Log ("turn back");
					_str = WordReplace(_str, "to your " +s, "3");
				} else if (s.Equals ("back")) {
//					Debug.Log ("turn left");
					_str = WordReplace(_str,"to your " + s, "1");
				}
				if (face >= 0){
//					Debug.Log ("turn right");
					_str = WordReplace(_str, "Face "+name, "2");
				}
				break;
			case -3: if (s.Equals ("right side")) {
					_str = WordReplace(_str, "to your " +s, "0");
				} else if (s.Equals ("left side")) {
//					Debug.Log ("turn back");
					_str = WordReplace(_str, "to your " +s, "3");
				} else if (s.Equals ("back")) {
//					Debug.Log ("turn left");
					_str = WordReplace(_str, "to your " +s, "1");
				}
				if (face >= 0){
//					Debug.Log ("turn right");
					_str = WordReplace(_str, "Face "+name, "2");
				}
				break;
			case -6: if (s.Equals ("right side")) {
//					Debug.Log ("turn left");
					_str = WordReplace(_str,"to your " + s, "1");
				} else if (s.Equals ("left side")) {
//					Debug.Log ("turn right");
					_str = WordReplace(_str, "to your " +s, "2");
				} else if (s.Equals ("back")) {
//					Debug.Log ("turn back");
					_str = WordReplace(_str, "to your " +s, "3");
				}
				if (face >= 0){
					_str = WordReplace(_str, "to your " +s, "0");
				}
				break;
			case -9:
//				Debug.Log ("check:s:"+s);
				if (s.Equals ("right side")) {
//					Debug.Log ("turn back");
					_str = WordReplace(_str,"to your " + s, "3");
				} else if (s.Equals ("left side")) {
					_str = WordReplace(_str, "to your " +s, "0");
				} else if (s.Equals ("back")) {
//					Debug.Log ("turn right");
					_str = WordReplace(_str,"to your " + s, "2");
				}
				if (face >= 0){
//					Debug.Log ("turn left");
					_str = WordReplace(_str, "Face "+name, "1");
				}
				break;
			default: 
				if (s.Equals ("right side")) {
//					Debug.Log ("turn right");
					_str = WordReplace(_str,"to your " + s, "2");
				} else if (s.Equals ("left side")) {
//					Debug.Log ("turn left");
					_str = WordReplace(_str,"to your " + s, "1");
				} else if (s.Equals ("back")) {
//					Debug.Log ("turn back");
					_str = WordReplace(_str,"to your " + s, "3");
				}
				if (face >= 0){
					_str = WordReplace(_str, "to your " +s, "0");
				}
					break;
				}
			
			
		return _str;

	}

}
